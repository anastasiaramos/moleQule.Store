using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PaymentSummary : ReadOnlyBaseEx<PaymentSummary>
    {
        #region Business Methods

        private long _oid_agente;
        private long _tipo_agente;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _observaciones = string.Empty;
        private decimal _total_facturado;
        private decimal _total_estimado;
        private decimal _pagado;
        private decimal _efectos_negociados;
        private decimal _efectos_devueltos;
        private decimal _efectos_pendientes_vto;

        public virtual long OidAgente { get { return _oid_agente; } set { _oid_agente = value; } }
        public virtual long TipoAcreedor { get { return _tipo_agente; } set { _tipo_agente = value; } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_tipo_agente; } }
        public virtual string NombreTipoAcreedor { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual decimal TotalFacturado { get { return _total_facturado; } set { _total_facturado = value; } }
        public virtual decimal TotalEstimado { get { return _total_estimado; } set { _total_estimado = value; } }
        public virtual decimal Pagado { get { return _pagado; } set { _pagado = value; } }
        public virtual decimal Pendiente { get { return _total_facturado - _pagado - _efectos_pendientes_vto - _efectos_negociados - _efectos_devueltos; } }
        public virtual decimal PendienteEstimado { get { return _total_facturado + _total_estimado - _pagado; } }
        public virtual decimal EfectosNegociados { get { return _efectos_negociados; } set { _efectos_negociados = value; } }
        public virtual decimal EfectosDevueltos { get { return _efectos_devueltos; } set { _efectos_devueltos = value; } }
        public virtual decimal EfectosPendientesVto { get { return _efectos_pendientes_vto; } set { _efectos_pendientes_vto = value; } }

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(IDataReader source)
        {
            _tipo_agente = Format.DataReader.GetInt64(source, "TIPO_AGENTE");
            string oid = ((long)(_tipo_agente + 1)).ToString("00") + "00000" + Format.DataReader.GetInt64(source, "OID_AGENTE").ToString();
            Oid = Convert.ToInt64(oid);
            _oid_agente = Format.DataReader.GetInt64(source, "OID_AGENTE");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES_ACREEDOR");
            _total_facturado = Format.DataReader.GetDecimal(source, "TOTAL_FACTURADO");
            _total_estimado = Format.DataReader.GetDecimal(source, "TOTAL_ESTIMADO");
            _pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");
			_efectos_negociados = Format.DataReader.GetDecimal(source, "EFECTOS_NEGOCIADOS");
			_efectos_devueltos = Format.DataReader.GetDecimal(source, "EFECTOS_DEVUELTOS");
			_efectos_pendientes_vto = Format.DataReader.GetDecimal(source, "EFECTOS_PTES_VTO");
        }

        #endregion

        #region Factory Methods

        private PaymentSummary() {}

        private PaymentSummary(IDataReader source)
        {
            CopyValues(source);
        }

        public void Refresh(IAcreedor acreedor)
        {
            _oid_agente = acreedor.Oid;
            Oid = acreedor.Oid;
            _codigo = acreedor.Codigo;
            _nombre = acreedor.Nombre;
            _observaciones = acreedor.Observaciones;

            _pagado = 0;
            _efectos_devueltos = 0;
            _efectos_negociados = 0;
            _efectos_pendientes_vto = 0;

            PayrollList _nominas = null;

            foreach (Payment item in acreedor.Pagos)
            {
				if (item.EEstado == EEstado.Anulado) continue;

                if (item.OidAgente == 0
                    && item.ETipoPago == ETipoPago.Nomina)
                {
                    if (_nominas == null)
                        _nominas = PayrollList.GetListByEmpleado(acreedor.Oid, false);

                    foreach (TransactionPayment pf in item.Operations)
                    {
                        if (_nominas.Contains(pf.OidOperation))
                        {
                            if (item.EEstadoPago == EEstado.Pagado && item.Vencimiento <= DateTime.Now)
                                _pagado += pf.Cantidad;
                            else if (item.EEstadoPago == EEstado.Pagado && item.Vencimiento > DateTime.Now)
                                _efectos_negociados += pf.Cantidad;
                            else if (item.EEstadoPago != EEstado.Pagado && item.Vencimiento <= DateTime.Now)
                                _efectos_devueltos += pf.Cantidad;
                            else if (item.EEstadoPago != EEstado.Pagado && item.Vencimiento > DateTime.Now)
                                _efectos_pendientes_vto += pf.Cantidad;
                        }
                    }
                }
                else
                {
                    if (item.EEstadoPago == EEstado.Pagado && item.Vencimiento <= DateTime.Now)
                        _pagado += item.Importe;
                    else if (item.EEstadoPago == EEstado.Pagado && item.Vencimiento > DateTime.Now)
                        _efectos_negociados += item.Importe;
                    else if (item.EEstadoPago != EEstado.Pagado && item.Vencimiento <= DateTime.Now)
                        _efectos_devueltos += item.Importe;
                    else if (item.EEstadoPago != EEstado.Pagado && item.Vencimiento > DateTime.Now)
                        _efectos_pendientes_vto += item.Importe;
                }
            }
        }

        public static PaymentSummary Get(IDataReader source)
        {
            if (source == null) return null;
            return new PaymentSummary(source);
        }
        public static PaymentSummary Get(ETipoAcreedor tipo, long oid)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(ProviderBaseInfo.New(oid, tipo));

            Expedient.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<PaymentSummary>(criteria);
        }
        public static PaymentSummary Get(IAcreedor acreedor)
        {
            CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = PaymentSummary.SELECT(acreedor);

            Expedient.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<PaymentSummary>(criteria);
        }

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                SessionCode = criteria.SessionCode;

                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
                }
            }
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}
        }

        #endregion

		#region Autorization Rules

		public static bool CanAddObject()
		{
			return Payment.CanAddObject();
		}

		public static bool CanGetObject()
		{
			return Payment.CanGetObject();
		}

		public static bool CanDeleteObject()
		{
			return Payment.CanDeleteObject();
		}

		public static bool CanEditObject()
		{
			return Payment.CanEditObject();
		}

		#endregion

		#region SQL

		public static ProviderBaseInfo.SelectCaller local_caller = new ProviderBaseInfo.SelectCaller(SELECT_BASE);
		public static ProviderBaseInfo.SelectCaller local_caller_PENDIENTES = new ProviderBaseInfo.SelectCaller(SELECT_BASE_PENDIENTES);
		public static ProviderBaseInfo.SelectCaller local_caller_PENDIENTES_VTO = new ProviderBaseInfo.SelectCaller(SELECT_BASE_PENDIENTES_VTO);
		public static ProviderBaseInfo.SelectCaller local_caller_NEGOCIADO = new ProviderBaseInfo.SelectCaller(SELECT_BASE_NEGOCIADO);
		public static ProviderBaseInfo.SelectCaller local_caller_ESTIMADO = new ProviderBaseInfo.SelectCaller(SELECT_BASE_ESTIMADO);

		public static string SELECT(IAcreedor acreedor)
		{
			QueryConditions conditions = new QueryConditions { Acreedor = acreedor.IGetInfo() };
			conditions.TipoAcreedor[0] = acreedor.ETipoAcreedor;
            if (acreedor.ETipoAcreedor == ETipoAcreedor.Empleado)
                return Employee.SELECT_PAGOS_NOMINAS();
            else
                return SELECT_BASE(conditions);
		}
		public static string SELECT(IAcreedorInfo acreedor)
		{
			QueryConditions conditions = new QueryConditions { Acreedor = acreedor };
			conditions.TipoAcreedor[0] = acreedor.ETipoAcreedor;
            if (acreedor.ETipoAcreedor == ETipoAcreedor.Empleado)
                return Employee.SELECT_PAGOS_NOMINAS(conditions);
            else
			    return SELECT_BASE(conditions);
		}

        internal static string FIELDS(ETipoAcreedor tipo)
        {
            string query = @"
            SELECT A.""OID"" AS ""OID_AGENTE""
                ," + (long)tipo + @" AS ""TIPO_AGENTE""
                ,A.""CODIGO""";

            switch (tipo)
            {
                case ETipoAcreedor.Empleado:
                    
                    query += @" 
                    ,A.""APELLIDOS"" || ', ' || A.""NOMBRE"" AS ""NOMBRE""";

                    break;

                default:
                    query += @" 
                    ,A.""NOMBRE""";

                    break;
            }

            query += @"
                ,A.""OBSERVACIONES"" AS ""OBSERVACIONES_ACREEDOR""
                ,COALESCE(""TOTAL_FACTURADO"",0) AS ""TOTAL_FACTURADO""
                ,COALESCE(""TOTAL_ESTIMADO"", 0) AS ""TOTAL_ESTIMADO""
                ,COALESCE(""TOTAL_PAGADO"",0) AS ""TOTAL_PAGADO""
                ,COALESCE(""EFECTOS_NEGOCIADOS"",0) AS ""EFECTOS_NEGOCIADOS""
                ,COALESCE(""EFECTOS_DEVUELTOS"",0) AS ""EFECTOS_DEVUELTOS""
                ,COALESCE(""EFECTOS_PTES_VTO"",0) AS ""EFECTOS_PTES_VTO""";

            return query;
        }

        internal static string JOIN_PAGOS_BASE(ETipoAcreedor tipo, QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string py = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollRecord));

            if (conditions.FechaAuxFin == DateTime.MinValue)
                conditions.FechaAuxFin = DateTime.Today;

            if (conditions.FechaAuxFin != DateTime.MinValue)
                conditions.FechaAuxIni = conditions.FechaAuxFin;
            else
                conditions.FechaAuxIni = conditions.FechaIni;

            string query = string.Empty;
            
            switch (tipo)
            {
                case ETipoAcreedor.Empleado:
                query += @"
                LEFT JOIN (SELECT PY.""OID_EMPLEADO""
                                    ,SUM(PY.""NETO"") AS ""TOTAL_FACTURADO""
                            FROM " + py + @" AS PY
                            WHERE PY.""FECHA"" <= '" + conditions.FechaAuxIniLabel + @"'
                                AND PY.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            GROUP BY PY.""OID_EMPLEADO"")
                    AS FP ON FP.""OID_EMPLEADO"" = A.""OID""";
                break;

                default:
                query += @"
                LEFT JOIN (SELECT II.""OID_ACREEDOR""
                                    ,SUM(II.""TOTAL"") AS ""TOTAL_FACTURADO""
                            FROM " + ii + @" AS II
                            WHERE II.""TIPO_ACREEDOR"" = " + (long)tipo + @"
                                AND II.""FECHA"" <= '" + conditions.FechaAuxIniLabel + @"'
                                AND II.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            GROUP BY ""OID_ACREEDOR"")
                    AS FP ON FP.""OID_ACREEDOR"" = A.""OID""";
                break;
            }

            query += @"
            LEFT JOIN (SELECT ""OID_AGENTE""
                                ,""TIPO_AGENTE""
                                ,SUM(""IMPORTE"") AS ""TOTAL_PAGADO""
                        FROM " + pg + @"
                        WHERE ""TIPO_AGENTE"" = " + (long)tipo + @"
                            AND ""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @" 
                            AND ""VENCIMIENTO"" <= '" + conditions.FechaAuxFinLabel + @"'
                            AND ""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_AGENTE"", ""TIPO_AGENTE"")
                AS P1 ON P1.""OID_AGENTE"" = A.""OID""
            LEFT JOIN (SELECT ""OID_AGENTE""
                                ,""TIPO_AGENTE""
                                ,SUM(""IMPORTE"") AS ""EFECTOS_NEGOCIADOS""
                        FROM " + pg + @"
                        WHERE ""TIPO_AGENTE"" = " + (long)tipo + @"
                            AND ""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
                            AND ""VENCIMIENTO"" > '" + conditions.FechaAuxFinLabel + @"'
                            AND ""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_AGENTE"", ""TIPO_AGENTE"")
                AS P2 ON P2.""OID_AGENTE"" = A.""OID""
            LEFT JOIN (SELECT ""OID_AGENTE""
                                ,""TIPO_AGENTE""
                                ,SUM(""IMPORTE"") AS ""EFECTOS_DEVUELTOS""
                        FROM " + pg + @"
                        WHERE ""TIPO_AGENTE"" = " + (long)tipo + @"
                            AND ""ESTADO_PAGO"" != " + (long)EEstado.Pagado + @"
                            AND ""VENCIMIENTO"" <= '" + conditions.FechaAuxFinLabel + @"'
                            AND ""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_AGENTE"", ""TIPO_AGENTE"")
                AS P3 ON P3.""OID_AGENTE"" = A.""OID""";

            return query;
        }

        internal static string JOIN_PAGOS(ETipoAcreedor tipo, QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query = 
            JOIN_PAGOS_BASE(tipo, conditions) + @"
            LEFT JOIN (SELECT ""OID_AGENTE""
                                ,""TIPO_AGENTE""
                                ,SUM(""IMPORTE"") AS ""EFECTOS_PTES_VTO""
                        FROM " + pg + @"
                        WHERE ""TIPO_AGENTE"" = " + (long)tipo + @"
                            AND ""ESTADO_PAGO"" != " + (long)EEstado.Pagado + @"
                            AND ""VENCIMIENTO"" > '" + conditions.FechaAuxFinLabel + @"'
                            AND ""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_AGENTE"", ""TIPO_AGENTE"")
                AS P4 ON P4.""OID_AGENTE"" = A.""OID""";

            return query;
        }
        
        internal static string JOIN_PAGOS_PENDIENTE_VTO(ETipoAcreedor tipo, QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query =
            JOIN_PAGOS_BASE(tipo, conditions) + @"
            LEFT JOIN (SELECT ""OID_AGENTE""
                                ,""TIPO_AGENTE""
                                ,SUM(""IMPORTE"") AS ""EFECTOS_PTES_VTO""
                        FROM " + pg + @"
                        WHERE ""TIPO_AGENTE"" = " + (long)tipo + @"
                            AND ""ESTADO_PAGO"" != " + (long)EEstado.Pagado + @"
                            AND ""VENCIMIENTO"" <= '" + conditions.FechaAuxFinLabel + @"'
                            AND ""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_AGENTE"", ""TIPO_AGENTE"")
                AS P4 ON P4.""OID_AGENTE"" = A.""OID""";

            return query;
        }

		public static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query = @"
            WHERE TRUE";

			if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos) 
                query += @"
                AND A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];

			if (conditions.Acreedor != null) 
                query += @" 
                AND A.""OID"" = " + conditions.Acreedor.Oid;

			/*if (AppContext.User.IsPartner)
			{
				query += Common.EntityBase.GET_IN_LIST_CONDITION(AppContext.Principal.Partners, "A");
				query += " AND A.\"TIPO\" = " + (long)ETipoAcreedor.Partner;
			}*/

			return query;
		}

		public static string SELECT_BASE(QueryConditions conditions)
		{
			string e = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
			string nv = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ShippingCompanyRecord));
			string tr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.TransporterRecord));
			string dp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.CustomAgentRecord));

			string query =  
			FIELDS(conditions.TipoAcreedor[0]) + @"
		    FROM " + ProviderBaseInfo.TABLE(conditions.TipoAcreedor[0]) + @" AS A";

            query += (conditions.ExtraJoin != string.Empty) 
                        ? conditions.ExtraJoin 
                        : JOIN_PAGOS(conditions.TipoAcreedor[0], conditions);

			switch (conditions.TipoAcreedor[0])
			{
				case ETipoAcreedor.Proveedor:

					query += @"
                    LEFT JOIN (SELECT SUM(""G_PROV_TOTAL"") AS ""TOTAL_ESTIMADO""
                                        ,""OID_PROVEEDOR"" 
                                FROM " + e + @" 
                                WHERE ""OID_FACTURA_PRO"" = 0 GROUP BY ""OID_PROVEEDOR"") 
                        AS E ON E.""OID_PROVEEDOR"" = A.""OID""";
					break;

				case ETipoAcreedor.Naviera:

					query += @"
                    LEFT JOIN (SELECT SUM(""G_NAV_TOTAL"") AS ""TOTAL_ESTIMADO""
                                        ,""OID_NAVIERA"" 
                                FROM " + e + @" 
                                WHERE ""OID_FACTURA_NAV"" = 0 
                                GROUP BY ""OID_NAVIERA"") 
                        AS E ON E.""OID_NAVIERA"" = A.""OID""";
					break;

				case ETipoAcreedor.TransportistaDestino:

					query += @"
                    LEFT JOIN (SELECT SUM(""G_TRANS_DEST_TOTAL"") AS ""TOTAL_ESTIMADO""
                                        ,""OID_TRANS_DESTINO"" 
                                FROM " + e + @" 
                                WHERE ""OID_FACTURA_TDE"" = 0 
                                GROUP BY ""OID_TRANS_DESTINO"") 
                        AS E ON E.""OID_TRANS_DESTINO"" = A.""OID""";
					break;

				case ETipoAcreedor.TransportistaOrigen:

					query += @"
                    LEFT JOIN (SELECT SUM(""G_TRANS_TOTAL"") AS ""TOTAL_ESTIMADO""
                                        ,""OID_TRANS_ORIGEN"" 
                                FROM " + e + @" 
                                WHERE ""OID_FACTURA_TOR"" = 0 
                                GROUP BY ""OID_TRANS_ORIGEN"") 
                        AS E ON E.""OID_TRANS_ORIGEN"" = A.""OID""";
					break;

				case ETipoAcreedor.Despachante:

					query += @"
                    LEFT JOIN (SELECT SUM(""G_DESP_TOTAL"") AS ""TOTAL_ESTIMADO""
                                        ,""OID_DESPACHANTE"" 
                                FROM " + e + @" 
                                WHERE ""OID_FACTURA_DES"" = 0 
                                GROUP BY ""OID_DESPACHANTE"") 
                        AS E ON E.""OID_DESPACHANTE"" = A.""OID""";
					break;

				default:
					query += @"
                    LEFT JOIN (SELECT 0 AS ""TOTAL_ESTIMADO"" 
                                FROM " + e + @" 
                                WHERE FALSE) 
                        AS E ON E.""TOTAL_ESTIMADO"" != 0";
					break;
			}

			query += WHERE(conditions);

			return query;
		}

		public static string SELECT_BASE_PENDIENTES(QueryConditions conditions)
		{
            conditions.ExtraJoin =
            JOIN_PAGOS(conditions.TipoAcreedor[0], conditions);

			string query = 
            SELECT_BASE(conditions) + @"
            AND (COALESCE(""TOTAL_FACTURADO"", 0) > COALESCE(""TOTAL_PAGADO"", 0))";

            conditions.ExtraJoin = string.Empty;

			return query;
		}

		public static string SELECT_BASE_PENDIENTES_VTO(QueryConditions conditions)
		{
            conditions.ExtraJoin =
            JOIN_PAGOS_PENDIENTE_VTO(conditions.TipoAcreedor[0], conditions);

			string query = 
            SELECT_BASE(conditions) + @"
            AND (NOT ""EFECTOS_PTES_VTO"" ISNULL AND""EFECTOS_PTES_VTO"" > 0)";

            conditions.ExtraJoin = string.Empty;

			return query;
		}

		public static string SELECT_BASE_NEGOCIADO(QueryConditions conditions)
		{
            conditions.ExtraJoin =
            JOIN_PAGOS(conditions.TipoAcreedor[0], conditions);

			string query = 
            SELECT_BASE(conditions) + @"
            AND (NOT ""EFECTOS_NEGOCIADOS"" ISNULL AND""EFECTOS_NEGOCIADOS"" > 0)";

            conditions.ExtraJoin = string.Empty;

			return query;
		}

		public static string SELECT_BASE_ESTIMADO(QueryConditions conditions)
		{
            conditions.ExtraJoin =
            JOIN_PAGOS(conditions.TipoAcreedor[0], conditions);

			string query = 
            SELECT_BASE(conditions) + @"
			AND (COALESCE(""TOTAL_ESTIMADO"", 0) > 0)";

            conditions.ExtraJoin = string.Empty;

			return query;
		}

		public static string SELECT(QueryConditions conditions)
		{
			string query = string.Empty;

            if ((conditions.TipoAcreedor[0] == ETipoAcreedor.Todos) ||
                (conditions.TipoAcreedor.Length > 1))
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller, conditions);
            else 
                query = SELECT_BASE(conditions);


			return query;
		}

		public static string SELECT_PENDIENTES(QueryConditions conditions)
		{
			string query;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_PENDIENTES, conditions);
			else
				query = SELECT_BASE_PENDIENTES(conditions);

			return query;
		}

		public static string SELECT_PENDIENTES_VTO(QueryConditions conditions)
		{
			string query;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_PENDIENTES_VTO, conditions);
			else
				query = SELECT_BASE_PENDIENTES_VTO(conditions);

			return query;
		}

		public static string SELECT_NEGOCIADO(QueryConditions conditions)
		{
			string query;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_NEGOCIADO, conditions);
			else
				query = SELECT_BASE_NEGOCIADO(conditions);

			return query;
		}

		public static string SELECT_ESTIMADO(QueryConditions conditions)
		{
			string query;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_ESTIMADO, conditions);
			else
				query = SELECT_BASE_ESTIMADO(conditions);

			return query;
		}

		internal static string SELECT_EXPEDIENTES_PENDIENTES(QueryConditions conditions)
		{
            conditions.ExtraWhere = @"
                AND ""TOTAL_PAGADO"" - ""EFECTOS_PTES_VTO"" != 0";

			string query = string.Empty;

			conditions.TipoAcreedor[0] = ETipoAcreedor.Proveedor;
			query = SELECT_BASE_PENDIENTES(conditions);

			conditions.TipoAcreedor[0] = ETipoAcreedor.Naviera;
			query += " UNION " + SELECT_BASE_PENDIENTES(conditions);

			conditions.TipoAcreedor[0] = ETipoAcreedor.Despachante;
			query += " UNION " + SELECT_BASE_PENDIENTES(conditions);

			conditions.TipoAcreedor[0] = ETipoAcreedor.TransportistaOrigen;
			query += " UNION " + SELECT_BASE_PENDIENTES(conditions);

			conditions.TipoAcreedor[0] = ETipoAcreedor.TransportistaDestino;
			query += " UNION " + SELECT_BASE_PENDIENTES(conditions);

            conditions.ExtraWhere = string.Empty;

			return query;
		}

		internal static string SELECT_EXPLOTACION_PENDIENTES(QueryConditions conditions)
		{
            conditions.ExtraWhere = @"
                AND ""TOTAL_PAGADO"" - ""EFECTOS_PTES_VTO"" != 0";

			conditions.TipoAcreedor[0] = ETipoAcreedor.Acreedor;
			string query = SELECT_BASE_PENDIENTES(conditions);

            conditions.ExtraWhere = string.Empty;

			return query;
		}

        internal static string SELECT_UNPAID_PAYROLLS(QueryConditions conditions)
        {
            conditions.ExtraWhere = @"
                AND ""TOTAL_PAGADO"" - ""EFECTOS_PTES_VTO"" != 0";

            conditions.TipoAcreedor[0] = ETipoAcreedor.Empleado;
            string query = SELECT_BASE_PENDIENTES(conditions);

            conditions.ExtraWhere = string.Empty;

            return query;
        }

		#endregion
	}
}