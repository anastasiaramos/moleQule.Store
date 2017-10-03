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

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class NominaInfo : ReadOnlyBaseEx<NominaInfo>, ITransactionPayment
	{
		#region ITransactionPayment

		public decimal Total { get { return Neto; } set { _base.Record.Neto = value; } }
		public decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }
		public decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
		public decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public decimal PendienteAsignar { get { return Math.Min(_base._pendiente_asignar, _base._pendiente); } set { _base._pendiente_asignar = value; } }
		public decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
		public string FechaAsignacion { get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; } set { _base._fecha_asignacion = DateTime.Parse(value); } }
		public string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }
		public string NFactura { get { return _base._id_remesa; } set { _base._id_remesa = value; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return ETipoAcreedor.Empleado; } set {} }
        public virtual string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
        public long DiasTranscurridos { get { return _base.DiasTranscurridos; } }
		
		#endregion

		#region Attributes

		public PayrollBase _base = new PayrollBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidRemesa { get { return _base.Record.OidRemesa; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long OidTipo { get { return _base.Record.OidTipo; } }
		public long OidEmpleado { get { return _base.Record.OidEmpleado; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal Bruto { get { return _base.Record.Bruto; } }
		public Decimal BaseIRPF { get { return _base.Record.BaseIrpf; } }
		public Decimal Neto { get { return _base.Record.Neto; } }
		public Decimal PIRPF { get { return _base.Record.PIrpf; } }
		public Decimal Seguro { get { return _base.Record.Seguro; } }
		public Decimal Descuentos { get { return _base.Record.Descuentos; } }
		public DateTime PrevisionPago { get { return _base.Record.PrevisionPago; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long Estado { get { return _base.Record.Estado; } }

        //NO ENLAZADAS
		public virtual string IDRemesa { get { return _base._id_remesa; } }
		public virtual string Empleado { get { return _base._empleado; } set { _base._empleado = value; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string IDExpediente { get { return _base._id_expediente; } set { _base._id_expediente = value; } }
		public virtual DateTime FechaPago { get { return _base._fecha_pago; } }
		public virtual long OidPago { get { return _base._oid_pago; } set { _base._oid_pago = value; } }
		public virtual string IDPago { get { return _base._id_pago; } set { _base._id_pago = value; } }
		public virtual long MedioPago { get { return _base._medio_pago; } }
		public virtual EEstado EEstado { get { return _base.EEstado; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual bool Pagado { get { return _base._pagado; } }
		public virtual Decimal IRPF { get { return _base.IRPF; } }

        #endregion

        #region Business Methods
        
        public void CopyFrom(Payroll source) { _base.CopyValues(source); }
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
			Pendiente = 0;
		}

        #endregion

        #region Common Factory Methods

        protected NominaInfo() { /* require use of factory methods */ }

        private NominaInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
        internal NominaInfo(Payroll source, bool childs)
        {
            _base.CopyValues(source);
        }

		public static NominaInfo New(long oid = 0) { return new NominaInfo() { Oid = oid }; }

        #endregion

        #region Root Factory Methods

		public static NominaInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = Payroll.GetCriteria(Payroll.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = NominaInfo.SELECT(oid);

			NominaInfo obj = DataPortal.Fetch<NominaInfo>(criteria);
			Payroll.CloseSession(criteria.SessionCode);

			return obj;
		}
		public static NominaInfo Get(long oid, bool childs, bool cache)
		{
			NominaInfo item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(PayrollList)))
			{
				//no está en la cache de objetos
				if (!Cache.Instance.Contains(typeof(NominaInfo)))
				{
					item = NominaInfo.Get(oid, childs);
					Cache.Instance.Save(typeof(NominaInfo), item);
				}
				else
					item = Cache.Instance.Get(typeof(NominaInfo)) as NominaInfo;
			}
			else
			{
				PayrollList items = Cache.Instance.Get(typeof(PayrollList)) as PayrollList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = NominaInfo.Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(PayrollList), items);
				}
			}

			return item;
		}

        public static NominaInfo GetByFactura(long oid_factura, bool childs)
        {
            CriteriaEx criteria = Payroll.GetCriteria(Expedient.OpenSession());

			QueryConditions conditions = new QueryConditions { FacturaRecibida = InputInvoice.New().GetInfo(false) };
			conditions.FacturaRecibida.Oid = oid_factura;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = NominaInfo.SELECT(conditions);

            criteria.Childs = childs;
            NominaInfo obj = DataPortal.Fetch<NominaInfo>(criteria);
            Payroll.CloseSession(criteria.SessionCode);
            return obj;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static NominaInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
            return new NominaInfo(sessionCode, reader, childs);
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
                        _base.CopyValues(reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
        }

        #endregion

        #region Child Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

		public static string SELECT(long oid) { return Payroll.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Payroll.SELECT(conditions, false); }

        #endregion
    }
}