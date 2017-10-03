using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.Base;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class ExpenseInfo : ReadOnlyBaseEx<ExpenseInfo>, ITransactionPayment
	{
		#region ITransactionPayment

		public decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
		public decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
		public string FechaAsignacion { get { return _base.FechaAsignacion; } set { _base.FechaAsignacion = value; } }
		public string NFactura { get { return _base._numero_factura; } set { _base._numero_factura = value; } }
		public decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public decimal PendienteAsignar { get { return _base.PendienteAsignar; } set { _base.PendienteAsignar = value; } }
		public decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }
		public string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { _base._tipo_acreedor = (long)value; } }

		#endregion

		#region Attributes

		public ExpenseBase _base = new ExpenseBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long OidTipo { get { return _base.Record.OidTipo; } }
		public long OidEmpleado { get { return _base.Record.OidEmpleado; } }
		public long OidRemesaNomina { get { return _base.Record.OidNomina; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidFactura { get { return _base.Record.OidFactura; } }
		public long OidAlbaran { get { return _base.Record.OidAlbaran; } }
		public long OidConceptoFactura { get { return _base.Record.OidConceptoFactura; } }
		public long OidConceptoAlbaran { get { return _base.Record.OidConceptoAlbaran; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public DateTime PrevisionPago { get { return _base.Record.PrevisionPago; } }
		public long Categoria { get { return _base.Record.CategoriaGasto; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long Estado { get { return _base.Record.Estado; } }

        //NO ENLAZADAS
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string Tipo { get { return _base._tipo; } set { _base._tipo = value; } }
		public virtual string CodigoExpediente { get { return _base._id_expediente; } set { _base._id_expediente = value; } }
		public virtual DateTime FechaPago { get { return _base._fecha_pago; } }
		public virtual long OidPago { get { return _base._oid_pago; } set { _base._oid_pago = value; } }
		public virtual string IDPago { get { return _base._id_pago; } set { _base._id_pago = value; } }
		public virtual string IDRemesaNomina { get { return _base._id_remesa_nomina; } set { _base._id_remesa_nomina = value; } }
		public virtual string Empleado { get { return _base._empleado; } set { _base._empleado = value; } }
		public virtual long MedioPago { get { return _base._medio_pago; } }
		public virtual long OidAcreedor { get { return _base._oid_acreedor; } set { _base._oid_acreedor = value; } }
		public virtual string Acreedor { get { return _base._nombre_acreedor; } set { _base._nombre_acreedor = value; } }
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public virtual DateTime FechaFactura { get { return _base._fecha_factura; } set { _base._fecha_factura = value; } }
		public virtual Decimal BaseFactura { get { return _base._base_factura; } set { _base._base_factura = value; } }
		public virtual Decimal ImpuestosFactura { get { return _base._impuestos_factura; } set { _base._impuestos_factura = value; } }
		public virtual DateTime PrevisionFactura { get { return _base._fecha_factura; } set { _base._prevision_factura = value; } }
		public virtual ECategoriaGasto ECategoriaGasto { get { return _base.ECategoriaGasto; } set { _base.Record.CategoriaGasto = (long)value; } }
		public virtual string CategoriaGastoLabel { get { return _base.CategoriaGastoLabel; } }
		public virtual EEstado EEstado { get { return _base.EEstado; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual EEstado EEstadoPago { get { return _base.EEstadoPago; } }
		public virtual string EstadoPagoLabel { get { return _base.EstadoPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
        public virtual bool Pagado { get { return _base._pagado; } }
        public virtual string IDMovimientoBanco { get { return _base._id_mov_banco; } }
        public virtual string IDLineaCaja { get { return _base._id_linea_caja; } }
		public virtual Decimal TotalLiquidado { get { return _base.TotalLiquidado; } set { _base.TotalLiquidado = value; } }
		public virtual Decimal TotalPteLiquidacion { get { return _base.TotalPteLiquidacion; } }
		public virtual EEstado EEstado2 
		{ 
			get 
			{
				return !(new List<EEstado>() { EEstado.Exportado, EEstado.Contabilizado, EEstado.Anulado }).Contains(EEstado)
							? EEstado.Abierto
							: EEstado;
			} 
		}
		public virtual string EstadoLabel2 { get { return Base.EnumText<EEstado>.GetLabel(EEstado2); ; } }

        #endregion

        #region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_base.CopyValues(source);
			Oid = Format.DataReader.GetInt64(source, "OID");

		}
		protected void CopyValues(Expense source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        public void CopyFrom(Expense source) { CopyValues(source); }
		public void CopyFrom(Payment source)
		{
			_base._oid_pago = source.Oid;
			_base._id_pago = source.Codigo;
			_base._fecha_pago = source.Vencimiento;
			_base._medio_pago = source.MedioPago;
		}

		public void Vincula() 
		{ 
			Vinculado = Library.Store.Resources.Labels.RESET_PAGO;
			Asignado = Total;
            TotalPagado = Total;
            PendienteAsignar = 0;
		}

        #endregion

        #region Factory Methods

        protected ExpenseInfo() { /* require use of factory methods */ }

        private ExpenseInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
        internal ExpenseInfo(Expense source, bool childs)
        {
            CopyValues(source);
        }

		public static ExpenseInfo New(long oid = 0) { return new ExpenseInfo() { Oid = oid }; }

        #endregion

        #region Root Factory Methods

		public static ExpenseInfo Get(long oid) { return Get(oid, false); }
		public static ExpenseInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = Expense.GetCriteria(Expense.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ExpenseInfo.SELECT(oid);

			ExpenseInfo obj = DataPortal.Fetch<ExpenseInfo>(criteria);
			Expense.CloseSession(criteria.SessionCode);

			return obj;
		}
		public static ExpenseInfo Get(long oid, bool childs, bool cache)
		{
			ExpenseInfo item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(ExpenseList)))
			{
				//no está en la cache de objetos
				if (!Cache.Instance.Contains(typeof(ExpenseInfo)))
				{
					item = ExpenseInfo.Get(oid, childs);
					Cache.Instance.Save(typeof(ExpenseInfo), item);
				}
				else
					item = Cache.Instance.Get(typeof(ExpenseInfo)) as ExpenseInfo;
			}
			else
			{
				ExpenseList items = Cache.Instance.Get(typeof(ExpenseList)) as ExpenseList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = ExpenseInfo.Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(ExpenseList), items);
				}
			}

			return item;
		}

        public static ExpenseInfo GetByFactura(long oid_factura, bool childs)
        {
            CriteriaEx criteria = Expense.GetCriteria(Expedient.OpenSession());

			QueryConditions conditions = new QueryConditions { FacturaRecibida = InputInvoice.New().GetInfo(false) };
			conditions.FacturaRecibida.Oid = oid_factura;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = ExpenseInfo.SELECT(conditions);

            criteria.Childs = childs;
            ExpenseInfo obj = DataPortal.Fetch<ExpenseInfo>(criteria);
            Expense.CloseSession(criteria.SessionCode);
            return obj;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ExpenseInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
            return new ExpenseInfo(sessionCode, reader, childs);
        }

        #endregion

        #region Root Data Access

        // called to retrieve data from db
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;
            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
                    if (reader.Read())
                        CopyValues(reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region Child Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

		public static string SELECT(long oid) { return Expense.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Expense.SELECT(conditions, false); }

        #endregion
    }

	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
	public class SerialGastoInfo : SerialInfo
	{
		#region Common Factory Methods

		protected SerialGastoInfo() { /* require use of factory methods */ }

		#endregion

		#region Root Factory Methods

		public static SerialGastoInfo Get(ECategoriaGasto categoria, Expedient expediente, int year)
		{
			CriteriaEx criteria = Expense.GetCriteria(Expense.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(categoria, expediente, year);

			SerialGastoInfo obj = DataPortal.Fetch<SerialGastoInfo>(criteria);
			Expense.CloseSession(criteria.SessionCode);
			return obj;
		}

		/// <summary>
		/// Obtiene el siguiente serial para una entidad desde la base de datos
		/// </summary>
		/// <param name="entity">Tipo de Entidad</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static long GetNext(ECategoriaGasto categoria, Expedient expediente, int year)
		{
			return Get(categoria, expediente, year).Value + 1;
		}

		#endregion

		#region Root Data Access

		#endregion

		#region SQL

		public static string SELECT(ECategoriaGasto categoria, Expedient expediente, int year)
		{
            string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
			string query = string.Empty;

			query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"";

			query += " FROM " + gt + " AS GT";
			query += " WHERE \"TIPO\" = " + (long)categoria + " AND EXTRACT(year FROM \"FECHA\") = " + year.ToString();

			if (expediente != null)
				query += " AND \"OID_EXPEDIENTE\" = " + expediente.Oid;

			return query;
		}

		#endregion
	}
}

