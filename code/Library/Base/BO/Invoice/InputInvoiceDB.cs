using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.CslaEx; 
using moleQule;
using moleQule.Common;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class InputInvoiceBase
	{
		#region Attributes

        private InputInvoiceRecord _record = new InputInvoiceRecord();

        /// NO ENLAZADOS
		internal DateTime _step_date;
        internal string _expediente = string.Empty;
		private decimal _pagado = 0;
        private decimal _pendiente = 0;
		internal string _n_serie = string.Empty;
		internal string _serie = string.Empty;
		internal string _usuario = string.Empty;
		internal string _n_acreedor = string.Empty;
		internal decimal _efectos_negociados;
		internal decimal _efectos_devueltos;
		internal decimal _efectos_pendientes_vto;
		private decimal _pendiente_asignar = 0;
		private decimal _allocated;
		private DateTime _allocation_date;
		internal DateTime _fecha_pago;
		internal string _id_pago = string.Empty;
		private string _linked = Resources.Labels.SET_PAGO;
        private decimal _aggregate;
		internal string _id_mov_contable = string.Empty;
		internal decimal _total_expediente;

		internal ImpuestoResumenList _impuestos_list = new ImpuestoResumenList();
        internal ImpuestoResumenList _irpf_list = new ImpuestoResumenList();

		#endregion

		#region Properties

        public InputInvoiceRecord Record { get { return _record; } set { _record = value; } }

		//NO ENLAZADOS	
		public DateTime StepDate { get { return _step_date; } set { _step_date = value; } }
		public virtual EEstado EEstado { get { return (EEstado)_record.Estado; } }
		public virtual string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAcreedor; } set { _record.TipoAcreedor = (long)value; } }
		public virtual string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		public virtual EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } }
		public virtual string FormaPagoLabel { get { return moleQule.Common.Structs.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
		public virtual EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } }
		public virtual string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }

        //Si el tipo de acreedor de la factura 
		public virtual bool NecesitaExpediente { get { return ETipoAcreedor != ETipoAcreedor.Acreedor && _expediente == string.Empty; } }

		public string NSerieSerie { get { return _n_serie + " - " + _serie; } }
		public Decimal Subtotal { get { return Decimal.Round(_record.BaseImponible + _record.Descuento, 2); } }

        public decimal Allocated { get { return _allocated; } set { _allocated = value; } }
        public decimal Pagado { get { return _pagado; } set { _pagado = value; } }
		public decimal Pendiente { get { return _pendiente; } set { _pendiente = value; } }
        public decimal PendienteAsignar { get { return Math.Min(_pendiente, _pendiente_asignar); } set { _pendiente_asignar = value; } }
        public string AllocationDate { get { return (_allocation_date != DateTime.MinValue) ? _allocation_date.ToShortDateString() : "---"; } set { _allocation_date = DateTime.Parse(value); } }
        public string Linked { get { return _linked; } set { _linked = value; } }
        public decimal Aggregate { get { return _aggregate; } set { _aggregate = value; } }

        public string FechaRegistroLb { get { return (_record.FechaRegistro != DateTime.MinValue) ? _record.FechaRegistro.ToShortDateString() : "---"; } }
		public bool Pagada { get { return (_pagado >= _record.Total); } }
		public long DiasTranscurridos
		{
			get
			{
				if (Pagada)
					return (_fecha_pago != DateTime.MinValue) ? _fecha_pago.Subtract(_record.Fecha).Days : 0;
				else
					return DateTime.Today.Subtract(_record.Fecha).Days;
			}
		}
		public string FileName 
        { 
            get
            {
                string[] file_name = {"FAC",
                                        AppContext.ActiveSchema.Name.Replace(".", ""),
                                        _record.Acreedor.Replace(".", "").PadLeft(15).Substring(0, 15),
                                        _record.Fecha.ToString("dd-MM-yyyy"),
                                        _n_serie,
                                        _record.Codigo};


                return String.Join<string>("_", file_name) + ".pdf";
            } 
        }
		public string FechaPago { get { return (_fecha_pago != DateTime.MinValue) ? _fecha_pago.ToShortDateString() : "---"; } }
		public decimal Acumulado { get { return _aggregate; } set { _aggregate = value; } }
		public string IDMovimientoContable { get { return _id_mov_contable; } }
		public virtual string Expediente { get { return _expediente; } set { _expediente = value; } }

		#endregion

		#region Business Methods

		public void CopyValues(IDataReader source)
		{
			if (source == null) return;

			InputInvoiceSQL.ETipoQuery tipo = (InputInvoiceSQL.ETipoQuery)Format.DataReader.GetInt64(source, "TIPO_QUERY");

			switch (tipo)
			{
                case InputInvoiceSQL.ETipoQuery.MODELO:
					{
                        //Oid = Format.DataReader.GetInt64(source, "OID");
						_record.OidAcreedor = Format.DataReader.GetInt64(source, "OID");
						_record.TipoAcreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
						_n_acreedor = Format.DataReader.GetString(source, "N_ACREEDOR");
						_record.VatNumber= Format.DataReader.GetString(source, "VAT_NUMBER");
						_record.Acreedor = Format.DataReader.GetString(source, "ACREEDOR");
						_record.Total = Format.DataReader.GetDecimal(source, "TOTAL");
						_record.BaseImponible = Format.DataReader.GetDecimal(source, "TOTAL_EFECTIVO");

						_record.TipoAcreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
						string oid = ((long)(_record.TipoAcreedor + 1)).ToString("00") + "00000" + _record.Oid.ToString();
						_record.Oid = Convert.ToInt64(oid);
					}
					break;

                case InputInvoiceSQL.ETipoQuery.AGRUPADO:

					_record.Oid = Format.DataReader.GetDateTime(source, "STEP").ToBinary();
					_record.Total = Format.DataReader.GetDecimal(source, "TOTAL");
					_step_date = Format.DataReader.GetDateTime(source, "STEP");

					break;

				default:
					{
                        _record.CopyValues(source);

                        if (ETipoAcreedor == ETipoAcreedor.Acreedor)
                            _expediente = "ACREEDOR";
                        else
                            _expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");

						_usuario = Format.DataReader.GetString(source, "USUARIO");
						_n_serie = Format.DataReader.GetString(source, "N_SERIE");
						_serie = Format.DataReader.GetString(source, "SERIE");
						_id_mov_contable = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");
						_n_acreedor = Format.DataReader.GetString(source, "N_EMISOR");
						_pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
						_pendiente_asignar = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");
						_pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");
						_allocated = Format.DataReader.GetDecimal(source, "ASIGNADO");
						_efectos_negociados = Format.DataReader.GetDecimal(source, "NEGOCIADO");
						_efectos_devueltos = Format.DataReader.GetDecimal(source, "DEVUELTO");
						_efectos_pendientes_vto = Format.DataReader.GetDecimal(source, "PENDIENTE_VTO");
						_fecha_pago = Format.DataReader.GetDateTime(source, "FECHA_PAGO");
						_id_pago = Format.DataReader.GetString(source, "ID_PAGO");
                        _total_expediente = Format.DataReader.GetDecimal(source, "TOTAL_EXPEDIENTE");
                        //_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");

						_id_mov_contable = (_id_mov_contable == "/") ? string.Empty : _id_mov_contable;

						//Si no tiene expediente comprobamos si tiene el del gasto asociado
                        if (_expediente == string.Empty)
                        {
                            _record.OidExpediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE_GASTO");
                            _expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE_GASTO");
                        }
					}
					break;
			}
		}
		public void CopyValues(InputInvoice source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_usuario = source.Usuario;
			_serie = source.Serie;
			_n_serie = source.NSerie;
			_pagado = source.Pagado;
			_pendiente = source.Pendiente;
			_pendiente_asignar = source.PendienteAsignar;
			_id_mov_contable = source.IDMovimientoContable;
			_total_expediente = source.TotalExpediente;
		}
		public void CopyValues(InputInvoiceInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

            _expediente = source.Expediente == string.Empty && source.ETipoAcreedor == ETipoAcreedor.Acreedor ? "ACREEDOR" : source.Expediente;

			_usuario = source.Usuario;
			_serie = source.Serie;
			_n_serie = source.NSerie;
			_pagado = source.Pagado;
			_pendiente = source.Pendiente;
			_pendiente_asignar = source.PendienteAsignar;
			_id_mov_contable = source.IDMovimientoContable;
			_total_expediente = source.TotalExpediente;

			_step_date = source.StepDate;
		}

		private void InsertImpuesto(InputInvoiceLineInfo item)
		{
			if (item.OidImpuesto == 0) return;
            if (item.Impuestos == 0) return; 
            
            ImpuestoInfo impuesto = ImpuestoInfo.Get(item.OidImpuesto, false);

            if (impuesto != null)
            {
                ImpuestoResumen iresumen = new ImpuestoResumen
                {
                    OidImpuesto = item.OidImpuesto,
                    BaseImponible = item.BaseImponible,
                    Importe = item.BaseImponible * impuesto.Porcentaje / 100
                };

                _impuestos_list.Insert(iresumen);
            }
		}
        private void InsertIRPF(InputInvoiceLineInfo item)
        {
            if (item.PIRPF == 0) return;
            
            if (_irpf_list.Contains(item.PIRPF))
            {
                ((ImpuestoResumen)_irpf_list[item.PIRPF]).Importe += item.IRPF;
            }
            else
            {
                ImpuestoResumen irpf = new ImpuestoResumen 
                {
                    Nombre = String.Format("IRPF AL {0} %", item.PIRPF),
                    Importe = item.IRPF,
                    Porcentaje = item.PIRPF
                };

                _irpf_list.Add(item.PIRPF, irpf);
            }
        }
		public Hashtable GetImpuestos(InputInvoiceLines conceptos)
		{
			try
			{
				_impuestos_list.Clear();
                _record.Impuestos = 0;

                foreach (InputInvoiceLine item in conceptos)
                {
                    InsertImpuesto(item.GetInfo(false));
                    _record.Impuestos += item.Impuestos;
                }

				/*_impuestos = */_impuestos_list.TotalizeImpuestos();

				return _impuestos_list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, _record.NFactura, _record.Acreedor));
			}
		}
		public Hashtable GetImpuestos(InputInvoiceLineList conceptos)
		{
			try
			{
				_impuestos_list.Clear();
                _record.Impuestos = 0;

                foreach (InputInvoiceLineInfo item in conceptos)
                {
                    InsertImpuesto(item);
                    _record.Impuestos += item.Impuestos;
                }

				/*_impuestos = */_impuestos_list.TotalizeImpuestos();

				return _impuestos_list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, _record.NFactura, _record.Acreedor));
			}
        }
        public Hashtable GetIRPF(InputInvoiceLines conceptos)
        {
            try
            {
                _irpf_list.Clear();
                _record.Irpf = 0;

                foreach (InputInvoiceLine item in conceptos)
                {
                    InsertIRPF(item.GetInfo(false));
                    _record.Irpf += item.IRPF;
                }

                return _irpf_list;
            }
            catch
            {
                throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, _record.NFactura, _record.Acreedor));
            }
        }
        public Hashtable GetIRPF(InputInvoiceLineList conceptos)
        {
            try
            {
                _irpf_list.Clear();
                _record.Irpf = 0;

                foreach (InputInvoiceLineInfo item in conceptos)
                {
                    InsertIRPF(item);

                    _record.Irpf += item.IRPF;
                }

                return _irpf_list;
            }
            catch
            {
                throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, _record.NFactura, _record.Acreedor));
            }
        }

		#endregion
	}

    [Serializable()]
    public class InputInvoiceSQL : SQLBuilder, ISQLBuilder
    {
        #region Common

		internal enum ETipoQuery { GENERAL = 0, SIN_EXPEDIENTE = 1, BY_PAGO = 2, CONTROL_PAGOS = 3, PENDIENTES = 4, MODELO = 5, PAGADAS = 6, COSTES = 7, AGRUPADO = 8 }

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
            {
                { 
                    "Gross", 
                    new ForeignField() {                        
						Property = "GROSS", 
                        TableAlias = String.Empty, 
                        Column = null
                    }
                }
                ,
                { 
                    "Taxes", 
                    new ForeignField() {                        
						Property = "TAXES", 
                        TableAlias = String.Empty, 
                        Column = null
                    }
                },
                { 
                    "Total", 
                    new ForeignField() {                        
						Property = "TOTAL", 
                        TableAlias = String.Empty, 
                        Column = null
                    }
                }
            };
		}

		public static ProviderBaseInfo.SelectLocalCaller local_caller_BASE = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_CONTROL_PAGOS = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_CONTROL_PAGOS);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_PAGADAS = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_PAGADAS);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_PENDIENTES = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_PENDIENTES);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_EXPEDIENTES = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_EXPEDIENTES);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_SIN_EXPEDIENTE = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_SIN_EXPEDIENTE);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_BY_PAGO = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_BY_PAGO);
		public static ProviderBaseInfo.SelectLocalCaller local_caller_BY_MODELO = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_BY_MODELO);
        public static ProviderBaseInfo.SelectLocalCaller local_caller_COSTES_EXPEDIENTE = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_COSTES_EXPEDIENTE);
        public static ProviderBaseInfo.SelectLocalCaller local_caller_EXISTS = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_EXISTS);

        #endregion

        #region FIELDS

        internal static string SELECT_FIELDS(ETipoQuery tipo, QueryConditions conditions = null)
		{
			string query = string.Empty;

			query = "SELECT " + (long)tipo + " AS \"TIPO_QUERY\"" +
					"		,F.*" +
					"       ,COALESCE(A.\"CODIGO\", '') AS \"N_EMISOR\"" +
							//Aquellas facturas con el campo COBRADO (del agregado SUM) que sean NULL (es decir, no
							//tienen ningun cobro asociado), el PENDIENTE sera igual al TOTAL de la factura, 
							//gracias al comando COALESCE
					"       ,COALESCE(PR.\"TOTAL_PAGADO\", 0) AS \"TOTAL_PAGADO\"" +
					"       ,PR.\"FECHA_PAGO\"" +
					"       ,PR.\"ID_PAGO\"" +
					"       ,COALESCE(G.\"OID_EXPEDIENTE\", 0) AS \"OID_EXPEDIENTE_GASTO\"" +
					"       ,COALESCE(G.\"CODIGO_EXPEDIENTE\", '') AS \"CODIGO_EXPEDIENTE_GASTO\"" +
					"		,COALESCE(US.\"NAME\", '') AS \"USUARIO\"" +
					"       ,S.\"IDENTIFICADOR\" AS \"N_SERIE\"" +
					"       ,S.\"NOMBRE\" AS \"SERIE\"" +
					"		,COALESCE(RG.\"CODIGO\", '') || '/' || COALESCE(LR.\"ID_EXPORTACION\", '') AS \"ID_MOVIMIENTO_CONTABLE\"";

			switch (tipo)
			{
				case ETipoQuery.GENERAL:
				case ETipoQuery.SIN_EXPEDIENTE:
					{
                        query +=
                        "       ,COALESCE(F.\"TOTAL\" - PR.\"TOTAL_PAGADO\", F.\"TOTAL\") AS \"PENDIENTE\"" +
						"       ,COALESCE(E.\"CODIGO\", COALESCE(CF2.\"CODIGO_EXPEDIENTE\", '')) AS \"CODIGO_EXPEDIENTE\"" + 
                        "       ,0 AS \"ASIGNADO\"" +
						"       ,0 AS \"PENDIENTE_ASIGNAR\"" +
						"       ,0 AS \"NEGOCIADO\"" +
						"       ,0 AS \"DEVUELTO\"" +
						"       ,0 AS \"PENDIENTE_VTO\"" +
						"       ,0 AS \"TOTAL_EXPEDIENTE\"";
					}
					break;

				case ETipoQuery.BY_PAGO:
                case ETipoQuery.PENDIENTES:
					{
                        query +=
                        "       ,COALESCE(F.\"TOTAL\" - (PR.\"TOTAL_PAGADO\" - COALESCE(PF.\"ASIGNADO\", 0)), F.\"TOTAL\") AS \"PENDIENTE\"" +
                        "       ,COALESCE(E.\"CODIGO\", COALESCE(CF2.\"CODIGO_EXPEDIENTE\", '')) AS \"CODIGO_EXPEDIENTE\"" + 
                        "       ,COALESCE(PF.\"ASIGNADO\", 0) AS \"ASIGNADO\"" +
						"		,COALESCE(F.\"TOTAL\", 0) - COALESCE(PF2.\"TOTAL_ASIGNADO\", 0) AS \"PENDIENTE_ASIGNAR\"" +
						"       ,0 AS \"NEGOCIADO\"" +
						"       ,0 AS \"DEVUELTO\"" +
						"       ,0 AS \"PENDIENTE_VTO\"" +
						"       ,0 AS \"TOTAL_EXPEDIENTE\"";
					}
					break;

				case ETipoQuery.PAGADAS:
					{
                        query +=
                        "       ,COALESCE(F.\"TOTAL\" - (PR.\"TOTAL_PAGADO\" - COALESCE(F.\"TOTAL\", 0)), F.\"TOTAL\") AS \"PENDIENTE\"" +
                        "       ,COALESCE(E.\"CODIGO\", COALESCE(CF2.\"CODIGO_EXPEDIENTE\", '')) AS \"CODIGO_EXPEDIENTE\"" + 
                        "       ,COALESCE(F.\"TOTAL\", 0) AS \"ASIGNADO\"" +
						"		,0 AS \"PENDIENTE_ASIGNAR\"" +
						"       ,0 AS \"NEGOCIADO\"" +
						"       ,0 AS \"DEVUELTO\"" +
						"       ,0 AS \"PENDIENTE_VTO\"" +
						"       ,0 AS \"TOTAL_EXPEDIENTE\"";
					}
					break;

				case ETipoQuery.CONTROL_PAGOS:
					{
                        query +=
                        "       ,COALESCE(F.\"TOTAL\" - (PR.\"TOTAL_PAGADO\" - COALESCE(PR.\"TOTAL_PAGADO\", 0)), F.\"TOTAL\") AS \"PENDIENTE\"" +
						"       ,COALESCE(E.\"CODIGO\", '') AS \"CODIGO_EXPEDIENTE\"" +
						"       ,COALESCE(PR.\"TOTAL_PAGADO\", 0) AS \"ASIGNADO\"" +
                        "       ,0 AS \"PENDIENTE_ASIGNAR\"" +
                        "       ,0 AS \"NEGOCIADO\"" +
                        "       ,0 AS \"DEVUELTO\"" +
                        "       ,0 AS \"PENDIENTE_VTO\"" +
                        "       ,0 AS \"TOTAL_EXPEDIENTE\"";
					}
					break;

				case ETipoQuery.COSTES:
					{
                        query +=
                        "       ,COALESCE(F.\"TOTAL\" - PR.\"TOTAL_PAGADO\", F.\"TOTAL\") AS \"PENDIENTE\"" +
						"       ,COALESCE(E.\"CODIGO\", '') AS \"CODIGO_EXPEDIENTE\"" +
                        "       ,0 AS \"ASIGNADO\"" +
						"       ,0 AS \"PENDIENTE_ASIGNAR\"" +
						"       ,0 AS \"NEGOCIADO\"" +
						"       ,0 AS \"DEVUELTO\"" +
						"       ,0 AS \"PENDIENTE_VTO\"" +
						"		,COALESCE(CF.\"TOTAL_EXPEDIENTE\", 0) AS \"TOTAL_EXPEDIENTE\"";
					}
					break;

                case ETipoQuery.AGRUPADO:
                    query = @"
                        SELECT " + (long)tipo + @" AS ""TIPO_QUERY"" 
                            ,DATE_TRUNC('" + conditions.Step.ToString() + @"', F.""FECHA"") AS ""STEP""
							,SUM(F.""TOTAL"") AS ""TOTAL""";

					break;
			}

			return query;
		}

        #endregion

        #region SELECT

        public static string SELECT() { return SELECT(new QueryConditions(), false); }
        public static string SELECT(long oid) { return SELECT(oid, ETipoAcreedor.Todos); }
        public static string SELECT(long oid, ETipoAcreedor tipo) { return SELECT(oid, tipo, true); }
        internal static string SELECT(long oid, ETipoAcreedor providerType, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                TipoAcreedor = new ETipoAcreedor[1] { providerType },
                FacturaRecibida = InputInvoiceInfo.New(oid)
            };

            query = SELECT(conditions);

            if (providerType != ETipoAcreedor.Todos)
                query += LOCK("F", lockTable);

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable = false)
        {
			string query = string.Empty;

			switch (conditions.TipoFacturas)
			{
				case ETipoFacturas.Todas:
					{
						if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
							query = ProviderBaseInfo.SELECT_BUILDER(local_caller_BASE, conditions);
						else
							query = SELECT_BASE(conditions, conditions.TipoAcreedor[0]);
					}
					break;

				case ETipoFacturas.Pendientes:
					{
						query = SELECT_PENDIENTES(conditions);
					}
					break;

				case ETipoFacturas.Pagadas:
					{
						query = SELECT_PAGADAS(conditions);
					}
					break;
			}

			if (conditions != null)
			{
				if (conditions.Step != EStepGraph.None)
				{
					query += @"
					GROUP BY ""STEP""
					ORDER BY ""STEP""";
				}
				else
				{
					if (conditions.Orders == null)
					{
						conditions.Orders = new OrderList();
						conditions.Orders.Add(FilterMng.BuildOrderItem("Fecha", ListSortDirection.Descending, typeof(InputInvoice)));
						conditions.Orders.Add(FilterMng.BuildOrderItem("Codigo", ListSortDirection.Descending, typeof(InputInvoice)));
					}

					query += ORDER(conditions.Orders, string.Empty, ForeignFields());
					query += LIMIT(conditions.PagingInfo);
				}
			}

            return query;
        }

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}

        public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
        public static string SELECT_COUNT(QueryConditions conditions)
        {
            string query;

            query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
                JOIN_BASE(conditions) +
                WHERE(conditions);

            return query;
        }

       	internal static string SELECT_BASE(ETipoQuery tipo, QueryConditions conditions)
		{
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

			string tipoFactura = ((long)ETipoPago.Factura).ToString();

			/// OJO!! PUEDE FALLAR SI HAY UNA FACTURA ASOCIADA A VARIOS EXPEDIENTES
            string query =
			SELECT_FIELDS(tipo) +
			JOIN_BASE(conditions) + @"
			LEFT JOIN (SELECT PF.""OID_OPERACION""
							,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
							,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
							,MAX(PG.""FECHA"") AS ""FECHA_PAGO""
							,MAX(PG.""CODIGO"") AS ""ID_PAGO""
						FROM " + pf + @" AS PF
						INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" = " + tipoFactura + @"
						WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY PF.""OID_OPERACION"")
				AS PR ON PR.""OID_OPERACION"" = F.""OID""";

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions, ETipoAcreedor tipo)
		{
			string query;

			if (conditions.Step != EStepGraph.None)
			{
				query =
					SELECT_FIELDS(ETipoQuery.AGRUPADO, conditions) +
					JOIN_BASE(conditions) +
					JOIN_ACREEDOR(tipo) +
					WHERE(conditions);
			}
			else
			{
				/// OJO!! PUEDE FALLAR SI HAY UNA FACTURA ASOCIADA A VARIOS EXPEDIENTES
				query = SELECT_BASE(ETipoQuery.GENERAL, conditions) +
						JOIN_ACREEDOR(tipo) +
						WHERE(conditions);
			}

			return query;
		}

		internal static string SELECT_BASE_CONTROL_PAGOS(QueryConditions conditions, ETipoAcreedor tipo)
        {
            string afp = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string p = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            string query;
			string tipoFactura = ((long)ETipoPago.Factura).ToString();

            /// OJO!! PUEDE FALLAR SI HAY UNA FACTURA ASOCIADA A VARIOS EXPEDIENTES
			query = SELECT_FIELDS(ETipoQuery.CONTROL_PAGOS) + "," +
					"       COALESCE(PF2.\"NEGOCIADO\", 0) AS \"NEGOCIADO\"," +
					"       COALESCE(PF3.\"DEVUELTO\", 0) AS \"DEVUELTO\"," +
					"       COALESCE(PF4.\"PENDIENTE_VTO\", 0) AS \"PENDIENTE_VTO\"" +
					JOIN_BASE(conditions) +
					JOIN_ACREEDOR(tipo) +
					" LEFT JOIN " + afp + " AS AFP ON AFP.\"OID_FACTURA\" = F.\"OID\"" +
					" LEFT JOIN (SELECT SUM(PB1.\"CANTIDAD\") AS \"TOTAL_PAGADO\"" +
					"					,PB1.\"OID_OPERACION\"" +
					"					,MAX(PB1.\"FECHA\") AS \"FECHA_PAGO\"" +
					"					,MAX(PB1.\"OID_PAGO\") AS \"OID_PAGO\"" +
					"					,MAX(PB1.\"CODIGO\") AS \"ID_PAGO\"" +
					"            FROM (SELECT PFb1.\"CANTIDAD\"" +
					"						,PFb1.\"OID_OPERACION\"" +
					"						,PFb1.\"OID_PAGO\", Pb1.\"FECHA\", Pb1.\"CODIGO\"" +
					"                  FROM " + pf + " AS PFb1" +
					"                  INNER JOIN " + p + " AS Pb1 ON Pb1.\"OID\" = PFb1.\"OID_PAGO\" AND Pb1.\"ESTADO\" != " + (long)EEstado.Anulado + " AND PFb1.\"TIPO_PAGO\" = " + tipoFactura +
                    "                  WHERE \"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "') AS PB1" +
					"            GROUP BY PB1.\"OID_OPERACION\")" +
					"   AS PR ON PR.\"OID_OPERACION\" = F.\"OID\"" +
					" LEFT JOIN (SELECT SUM(\"CANTIDAD\") AS \"NEGOCIADO\"" +
					"					,PB2.\"OID_OPERACION\" " +
					"            FROM (SELECT PFb2.\"CANTIDAD\"" +
					"						,PFb2.\"OID_OPERACION\"" +
					"                  FROM " + pf + " AS PFb2" +
					"                  INNER JOIN " + p + " AS Pb2 ON Pb2.\"OID\" = PFb2.\"OID_PAGO\" AND Pb2.\"ESTADO\" != " + (long)EEstado.Anulado + " AND PFb2.\"TIPO_PAGO\" = " + tipoFactura +
                    "                  WHERE \"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "') AS PB2" +
					"            GROUP BY PB2.\"OID_OPERACION\")" +
					"   AS PF2 ON PF2.\"OID_OPERACION\" = F.\"OID\"" +
					" LEFT JOIN (SELECT SUM(\"CANTIDAD\") AS \"DEVUELTO\"" +
					"					,PB3.\"OID_OPERACION\" " +
					"            FROM (SELECT PFb3.\"CANTIDAD\",  PFb3.\"OID_OPERACION\"" +
					"                  FROM " + pf + " AS PFb3" +
					"                  INNER JOIN " + p + " AS Pb3 ON Pb3.\"OID\" = PFb3.\"OID_PAGO\" AND Pb3.\"ESTADO\" != " + (long)EEstado.Anulado + " AND PFb3.\"TIPO_PAGO\" = " + tipoFactura +
                    "                  WHERE \"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "') AS PB3" +
					"            GROUP BY PB3.\"OID_OPERACION\")" +
					"   AS PF3 ON PF3.\"OID_OPERACION\" = F.\"OID\"" +
					" LEFT JOIN (SELECT SUM(\"CANTIDAD\") AS \"PENDIENTE_VTO\"" +
					"					,PB4.\"OID_OPERACION\" " +
					"			 FROM (SELECT PFb4.\"CANTIDAD\",  PFb4.\"OID_OPERACION\"" +
					"                  FROM " + pf + " AS PFb4" +
					"                  INNER JOIN " + p + " AS Pb4 ON Pb4.\"OID\" = PFb4.\"OID_PAGO\" AND Pb4.\"ESTADO\" != " + (long)EEstado.Anulado + " AND PFb4.\"TIPO_PAGO\" = " + tipoFactura +
                    "                  WHERE \"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "') AS PB4" +
					"            GROUP BY PB4.\"OID_OPERACION\")" +
					"   AS PF4 ON PF4.\"OID_OPERACION\" = F.\"OID\"" +
					" LEFT JOIN " + p + " AS P ON P.\"OID\" = PR.\"OID_PAGO\"" +
					WHERE(conditions);

            return query;
        }

		internal static string SELECT_BASE_PAGADAS(QueryConditions conditions, ETipoAcreedor tipo)
		{
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            conditions.ExtraWhere = @"
                AND (PR.""TOTAL_PAGADO"" = F.""TOTAL"")";

			string query = 
            SELECT_BASE(ETipoQuery.PAGADAS, conditions) + @"
			LEFT JOIN " + pg + @" AS P ON P.""OID"" = PR.""OID_PAGO""" +
			JOIN_ACREEDOR(tipo) +
			WHERE(conditions);

			return query;
		}

		internal static string SELECT_BASE_PENDIENTES(QueryConditions conditions, ETipoAcreedor tipo)
		{
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

            conditions.ExtraWhere += @"
                AND (PR.""TOTAL_PAGADO"" != F.""TOTAL"" OR PR.""TOTAL_PAGADO"" IS NULL)
                AND (F.""PREVISION_PAGO""" + BETWEEN(conditions.FechaAuxIniLabel, conditions.FechaAuxFinLabel) + ")";

            if (conditions.MedioPago != EMedioPago.Todos)
                conditions.ExtraWhere += @"
                AND F.""MEDIO_PAGO"" = " + (long)conditions.MedioPago;

            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                conditions.ExtraWhere += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "F", "MEDIO_PAGO");

			string query;
			string tipoFactura = "(" + (long)ETipoPago.Factura + ")";
			long oid_pago = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

			query = 
			SELECT_BASE(ETipoQuery.PENDIENTES, conditions) +
			JOIN_ACREEDOR(tipo);

            // IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTA FACTURA
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""ASIGNADO""
                            ,PG.""FECHA"" AS ""FECHA_PAGO""
                            ,PG.""OID"" AS ""OID_PAGO""
                            ,PG.""CODIGO"" AS ""ID_PAGO""
                        FROM " + pf + @" AS PF
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipoFactura + @"
                        WHERE PG.""OID"" = " + oid_pago + @"
                            AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PG.""OID"", PG.""FECHA"", PG.""OID"", PG.""CODIGO"")
                AS PF ON PF.""OID_OPERACION"" = F.""OID""
            LEFT JOIN " + pg + @" AS P ON P.""OID"" = PF.""OID_PAGO""";

            // IMPORTE TOTAL ASIGNADO A ESTA FACTURA POR TODOS LOS PAGOS
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                            ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + pf + @" AS PF
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipoFactura + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PF2 ON PF2.""OID_OPERACION"" = F.""OID""";
							
			query += 
            WHERE(conditions);

			return query;
		}

		internal static string SELECT_BASE_BY_MODELO(QueryConditions conditions, ETipoAcreedor tipo)
		{
			string query = string.Empty;

			switch (conditions.EModelo)
			{
				case EModelo.Modelo111:
					{
                        conditions.ExtraWhere += @"
						AND F.""P_IRPF"" != 0";
						
                        query =
						SELECT_BASE(ETipoQuery.GENERAL, conditions) +
						JOIN_ACREEDOR(tipo) +
						WHERE(conditions);
					}
					break;
			}

			return query;
		}

		internal static string SELECT_BASE_BY_PAGO(QueryConditions conditions, ETipoAcreedor tipo)
		{
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));

			string tipoFactura = "(" + ((long)ETipoPago.Factura).ToString() + ")";
			long oid_pago = (conditions.Payment != null) ? conditions.Payment.Oid : 0;

			/// OJO!! PUEDE FALLAR SI HAY UNA FACTURA ASOCIADA A VARIOS EXPEDIENTES
            string query =
            SELECT_BASE(ETipoQuery.BY_PAGO, conditions);

            // IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTA FACTURA
			query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""ASIGNADO""       
                            ,PG.""FECHA"" AS ""FECHA_PAGO""
                            ,PG.""OID"" AS ""OID_PAGO""
                            ,PG.""CODIGO"" AS ""ID_PAGO""
                        FROM " + pf + @" AS PF
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipoFactura + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            AND PG.""OID"" = " + oid_pago + @"
                        GROUP BY PF.""OID_OPERACION"", PG.""OID"", PG.""FECHA"", PG.""OID"", PG.""CODIGO"")
                AS PF ON PF.""OID_OPERACION"" = F.""OID""
            LEFT JOIN " + pg + @" AS P ON P.""OID"" = PF.""OID_PAGO""";

			// IMPORTE TOTAL ASIGNADO A ESTA FACTURA POR TODOS LOS PAGOS
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                            ,MAX(PF.""OID_PAGO"") AS ""OID_PAGO""
                        FROM " + pf + @" AS PF
                        INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipoFactura + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PF2 ON PF2.""OID_OPERACION"" = F.""OID""";

            query += 
			JOIN_ACREEDOR(tipo) +
		    WHERE(conditions);

			return query;
		}

		internal static string SELECT_BASE_COSTES_EXPEDIENTE(QueryConditions conditions, ETipoAcreedor tipo)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceLineRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

			string query = 
            SELECT_BASE(ETipoQuery.COSTES, conditions);

			query += @"
            INNER JOIN (SELECT CF.""OID_FACTURA""
                            ,CF.""OID_EXPEDIENTE""
                            ,SUM(""TOTAL"") AS ""TOTAL_EXPEDIENTE""
                        FROM " + cf + @" AS CF
                        INNER JOIN " + ba + @" AS PA ON PA.""OID"" = CF.""OID_BATCH""
                        WHERE CF.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid + @"
                        GROUP BY CF.""OID_FACTURA"", CF.""OID_EXPEDIENTE"")
                AS CF ON CF.""OID_FACTURA"" = F.""OID""";

			query += 
            JOIN_ACREEDOR(tipo) +
            WHERE(conditions);

			return query;
		}

		internal static string SELECT_BASE_EXPEDIENTES(QueryConditions conditions, ETipoAcreedor tipo)
		{
			string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string p = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string ga = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
			string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string cf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceLineRecord));
            string ca = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));			

			string tipoFactura = ((long)ETipoPago.Factura).ToString();

			/// OJO!! PUEDE FALLAR SI HAY UNA FACTURA ASOCIADA A VARIOS EXPEDIENTES
            string query = 
            SELECT_FIELDS(ETipoQuery.GENERAL) + @"
            FROM " + fp + @" AS F
            LEFT JOIN " + us + @" AS US ON US.""OID"" = F.""OID_USUARIO""
            LEFT JOIN " + s + @" AS S ON S.""OID"" = F.""OID_SERIE""
            LEFT JOIN " + ex + @" AS E ON E.""OID"" = F.""OID_EXPEDIENTE""
            LEFT JOIN (SELECT SUM(""CANTIDAD"") AS ""TOTAL_PAGADO""
                            ,PF1.""OID_OPERACION""
                            ,MAX(P.""OID"") AS ""OID_PAGO""
                            ,MAX(P.""FECHA"") AS ""FECHA_PAGO""
                            ,MAX(P.""CODIGO"") AS ""ID_PAGO""
                        FROM " + pf + @" AS PF1
                        INNER JOIN " + p + @" AS P ON P.""OID"" = PF1.""OID_PAGO"" AND PF1.""TIPO_PAGO"" = " + tipoFactura + @"
                        WHERE P.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_OPERACION"")
                AS PR ON F.""OID"" = PR.""OID_OPERACION""" +
            JOIN_ACREEDOR(tipo) + @"
            INNER JOIN (SELECT CF.""OID_FACTURA""
                            ,GT.""OID_EXPEDIENTE""
                            ,EX.""CODIGO"" AS ""CODIGO_EXPEDIENTE""
                        FROM " + ga + @" AS GT
                        INNER JOIN " + ex + @" AS EX ON EX.""OID"" = GT.""OID_EXPEDIENTE""
                        INNER JOIN " + cf + @" AS CF ON GT.""OID_CONCEPTO_FACTURA"" = CF.""OID""
                        GROUP BY CF.""OID_FACTURA"", GT.""OID_EXPEDIENTE"", EX.""CODIGO"")
                AS G ON G.""OID_FACTURA"" = F.""OID"" AND G.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid + @"
            LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION"", ""OID_ENTIDAD"", MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
                        FROM " + lr + @" AS LR
                        WHERE LR.""TIPO_ENTIDAD"" = " + (long)moleQule.Common.Structs.ETipoEntidad.FacturaRecibida + @"
                            AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
                        GROUP BY ""OID_ENTIDAD"")
                AS LR ON F.""OID"" = LR.""OID_ENTIDAD""
            LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""
            LEFT JOIN " + pg + @" AS P ON P.""OID"" = PR.""OID_PAGO""
            INNER JOIN (SELECT E.""CODIGO"" AS ""CODIGO_EXPEDIENTE""
                                ,E.""OID"" AS ""OID_EXPEDIENTE""
                                ,CF.""OID_FACTURA""
                        FROM " + cf + @" AS CF
                        LEFT JOIN (SELECT GT.""OID_CONCEPTO_FACTURA""
                                        ,GT.""OID_EXPEDIENTE""
                                        ,GT.""CODIGO"" AS ""CODIGO_EXPEDIENTE""
                                    FROM " + ga + @" AS GT			
                                    INNER JOIN " + ex + @" AS EX ON EX.""OID"" = GT.""OID_EXPEDIENTE""			
                                    GROUP BY ""OID_CONCEPTO_FACTURA"", ""OID_EXPEDIENTE"", ""CODIGO_EXPEDIENTE"")
                            AS GT ON GT.""OID_CONCEPTO_FACTURA"" = CF.""OID""			
                        INNER JOIN " + ex + @" AS E ON E.""OID"" = CASE WHEN CF.""OID_EXPEDIENTE"" != 0 THEN CF.""OID_EXPEDIENTE"" ELSE GT.""OID_EXPEDIENTE"" END 
                        INNER JOIN " + ca + @" AS CA ON CA.""OID"" = CF.""OID_CONCEPTO_ALBARAN""
                        WHERE CA.""OID_ALMACEN"" = 0
                        GROUP BY CF.""OID_FACTURA"", E.""OID"", E.""CODIGO"")
                AS CF2 ON CF2.""OID_FACTURA"" = F.""OID"" AND CF2.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid +
			WHERE(conditions);

			return query;
		}

		internal static string SELECT_BASE_SIN_EXPEDIENTE(QueryConditions conditions, ETipoAcreedor tipo)
		{
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string ep = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
            string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string iil = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceLineRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            
            // NO ESTA TOTALMENTE ASIGNADA Y NO ES UNA FACTURA DE COSTE DE MERCANCIA
            conditions.ExtraWhere += @"			
                AND F.""OID"" NOT IN (  SELECT ""OID_FACTURA""
                                        FROM " + ii + @" AS FP
                                        INNER JOIN (SELECT ""OID_FACTURA""
                                                            ,SUM(""TOTAL"") AS ""TOTAL""
                                                    FROM " + ep + @"
                                                    GROUP BY ""OID_FACTURA"") 
                                            AS GT ON FP.""OID"" = GT.""OID_FACTURA""
                                        WHERE ABS(FP.""BASE_IMPONIBLE"") <= ABS(GT.""TOTAL"")
                                        UNION
                                        SELECT ""OID_FACTURA""
                                        FROM " + ii + @" AS FP
                                        INNER JOIN (SELECT CF.""OID_FACTURA""
                                        FROM " + iil + @" AS CF
                                        INNER JOIN " + ba + @" AS PA ON PA.""OID"" = CF.""OID_BATCH""
                                        GROUP BY CF.""OID_FACTURA"") AS CF ON CF.""OID_FACTURA"" = FP.""OID"")";	
            
            string query = 
            SELECT_BASE(ETipoQuery.SIN_EXPEDIENTE, conditions) + @"
            LEFT JOIN " + pa + @" AS P ON P.""OID"" = PR.""OID_PAGO""" +
            JOIN_ACREEDOR(tipo) +
            WHERE(conditions);

			return query;
		}
		
		internal static string SELECT_PAGADAS(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_PAGADAS, conditions);
			else
				query = SELECT_BASE_PAGADAS(conditions, conditions.TipoAcreedor[0]);

			return query;
		}

		internal static string SELECT_PENDIENTES(QueryConditions conditions)
		{
			string query = string.Empty;

            conditions.Estado = EEstado.NoAnulado;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_PENDIENTES, conditions);
			else
				query = SELECT_BASE_PENDIENTES(conditions, conditions.TipoAcreedor[0]);

			return query;
		}

		internal static string SELECT_CONTROL_PAGOS(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_CONTROL_PAGOS, conditions);
			else
				query = SELECT_BASE_CONTROL_PAGOS(conditions, conditions.TipoAcreedor[0]);

            query += @"
            ORDER BY ""FECHA"", ""CODIGO""";

			return query;
		}

		internal static string SELECT_EXPEDIENTES(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_EXPEDIENTES, conditions);
			else
				query = SELECT_BASE_EXPEDIENTES(conditions, conditions.TipoAcreedor[0]);

			query += @"
            ORDER BY ""TIPO_ACREEDOR"", ""CODIGO""";

			return query;
		}

		internal static string SELECT_SIN_EXPEDIENTE(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_SIN_EXPEDIENTE, conditions);
			else
				query = SELECT_BASE_SIN_EXPEDIENTE(conditions, conditions.TipoAcreedor[0]);

			query += @"
            ORDER BY ""TIPO_ACREEDOR"", ""CODIGO""";

			return query;
		}

		internal static string SELECT_COSTES_EXPEDIENTE(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_COSTES_EXPEDIENTE, conditions);
			else
				query = SELECT_BASE_COSTES_EXPEDIENTE(conditions, conditions.TipoAcreedor[0]);

			query += @"
            ORDER BY ""TIPO_ACREEDOR"", ""CODIGO""";

			return query;
		}

		internal static string SELECT_BY_MODELO(QueryConditions conditions)
		{
			string query = string.Empty;

			query = ProviderBaseInfo.SELECT_BUILDER(local_caller_BY_MODELO, conditions);

            query += @"
            ORDER BY ""FECHA"", ""CODIGO""";

			return query;
		}

		internal static string SELECT_BY_PAGO(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
				query = ProviderBaseInfo.SELECT_BUILDER(local_caller_BY_PAGO, conditions);
			else
				query = SELECT_BASE_BY_PAGO(conditions, conditions.TipoAcreedor[0]);

			query += @"
            ORDER BY ""FECHA"", ""CODIGO""";

			return query;
		}

		internal static string SELECT_EXPLOTACION(QueryConditions conditions)
		{
			string query = string.Empty;

			query = 
            SELECT_BASE_PENDIENTES(conditions, ETipoAcreedor.Proveedor) + @"
            UNION " + 
            SELECT_BASE_PENDIENTES(conditions, ETipoAcreedor.Naviera) + @"
			UNION " + 
            SELECT_BASE_PENDIENTES(conditions, ETipoAcreedor.Despachante) + @"
			UNION " + 
            SELECT_BASE_PENDIENTES(conditions, ETipoAcreedor.TransportistaOrigen) + @"
			UNION " + 
            SELECT_BASE_PENDIENTES(conditions, ETipoAcreedor.TransportistaDestino);

			query += @"
            ORDER BY ""TIPO_ACREEDOR"", ""CODIGO""";

			return query;
		}

        internal static string SELECT_BASE_EXISTS(Library.Store.QueryConditions conditions, ETipoAcreedor tipo)
        {
            string query = string.Empty;

            query = SELECT_BASE(conditions, tipo);

            if (conditions.FacturaRecibida != null)
            {
                if (conditions.FacturaRecibida.Fecha != DateTime.MinValue)
                    query += @"
                    AND F.""FECHA""" + BETWEEN(QueryConditions.GetFechaMinLabel(conditions.FacturaRecibida.Fecha), 
                                                QueryConditions.GetFechaMaxLabel(conditions.FacturaRecibida.Fecha));

                if (!string.IsNullOrEmpty(conditions.FacturaRecibida.NFactura))
                    query += @"
                    AND F.""N_FACTURA"" = '" + conditions.FacturaRecibida.NFactura + "'";

                if (conditions.FacturaRecibida.Total != 0)
                    query += @"
                    AND F.""TOTAL"" = " + conditions.FacturaRecibida.Total;
            }

            return query;
        }

        internal static string SELECT_EXISTS(Library.Store.QueryConditions conditions)
        {
            string query = string.Empty;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_EXISTS, conditions);
            else
                query = SELECT_BASE_EXISTS(conditions, conditions.TipoAcreedor[0]);

            query += @"
            ORDER BY ""FECHA"", ""SERIE"", ""CODIGO""";

            return query;
        }

        #endregion
                
        #region JOIN

        public static string JOIN_ACREEDOR(ETipoAcreedor tipo)
		{
			string query;

			if (tipo != ETipoAcreedor.Todos)
				query = @"
				INNER JOIN " + ProviderBaseInfo.TABLE(tipo) + @" AS A ON A.""OID"" = F.""OID_ACREEDOR"" AND F.""TIPO_ACREEDOR"" = " + (long)tipo;
			else
				query = @"
				LEFT JOIN " + ProviderBaseInfo.TABLE(tipo) + @" AS A ON A.""OID"" = F.""OID_ACREEDOR"" AND F.""TIPO_ACREEDOR"" = " + (long)tipo;

			return query;
		}
        public static string JOIN_ACREEDOR(ETipoEntidad tipo)
		{
			return JOIN_ACREEDOR(moleQule.Store.Structs.EnumConvert.ToETipoAcreedor(tipo));
		}

		internal static string JOIN_BASE(QueryConditions conditions)
		{
			string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
            string ga = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string cf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceLineRecord));

			string query;

            query = @"
			FROM " + fp + @" AS F
            LEFT JOIN " + us + @" AS US ON US.""OID"" = F.""OID_USUARIO""
            LEFT JOIN " + se + @" AS S ON S.""OID"" = F.""OID_SERIE""
            LEFT JOIN " + ex + @" AS E ON E.""OID"" = F.""OID_EXPEDIENTE""
            LEFT JOIN (SELECT GT.""OID_FACTURA""
                            ,GT.""OID_EXPEDIENTE""
                            ,EX.""CODIGO"" AS ""CODIGO_EXPEDIENTE""
						FROM " + ga + @" AS GT 
						INNER JOIN " + ex + @" AS EX ON EX.""OID"" = GT.""OID_EXPEDIENTE""
						GROUP BY ""OID_FACTURA"", ""OID_EXPEDIENTE"", EX.""CODIGO"")
					AS G ON G.""OID_FACTURA"" = F.""OID""
            LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION""
                            ,""OID_ENTIDAD""
                            ,MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
						FROM " + lr + @" AS LR
						WHERE LR.""TIPO_ENTIDAD"" = " + (long)moleQule.Common.Structs.ETipoEntidad.FacturaRecibida + @"
						AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
						GROUP BY ""OID_ENTIDAD"")
					AS LR ON F.""OID"" = LR.""OID_ENTIDAD""
            LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""
            LEFT JOIN (	SELECT MAX(E.""CODIGO"") AS ""CODIGO_EXPEDIENTE""
								,CF.""OID_FACTURA""
						FROM " + cf + @" AS CF
						INNER JOIN " + ex + @" AS E ON E.""OID"" = CF.""OID_EXPEDIENTE""
						GROUP BY CF.""OID_FACTURA"")
					AS CF2 ON CF2.""OID_FACTURA"" = F.""OID""";

			return query + " " + conditions.ExtraJoin;
		}

        #endregion

        #region WHERE

        internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "F", ForeignFields());

			query += @"
				AND (F.""FECHA""" + BETWEEN(conditions.FechaIniLabel, conditions.FechaFinLabel) + ")";

            query += Common.EntityBase.NO_NULL_RECORDS_CONDITION("F");
			query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "F");

			if (conditions.FacturaRecibida != null && conditions.FacturaRecibida.Oid != 0)
				query += @"
					AND F.""OID"" = " + conditions.FacturaRecibida.Oid;

			if (conditions.OidList != null)
				query += @"
					AND F.""OID""" + IN(conditions.OidList);

			if ((conditions.Acreedor != null) && (conditions.Acreedor.OidAcreedor != 0))
				query += @"
					AND F.""OID_ACREEDOR"" = " + conditions.Acreedor.OidAcreedor;

			if (conditions.TipoAcreedor[0] != ETipoAcreedor.Todos)
				query += @"
					AND F.""TIPO_ACREEDOR"" = " + Convert.ToInt32(conditions.TipoAcreedor[0]);

			if (conditions.Serie != null)
				query += @"
					AND F.""OID_SERIE"" = " + conditions.Serie.Oid;

			if (conditions.Payment != null)
				query += @"
					AND P.""OID"" = " + conditions.Payment;

			return query + " " + conditions.ExtraWhere;
		}

        #endregion

        #region UPDATE
        
        public static string UPDATE_TIPO(QueryConditions conditions)
		{
            string ii = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
			
            string query = @"
            UPDATE " + ii + @" AS F SET ""TIPO_ACREEDOR"" = " + conditions.Acreedor.TipoAcreedor +
            WHERE(conditions);

			return query;
		}

		#endregion
	}
}

