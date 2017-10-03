using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule;
using moleQule.Common.Structs;
using moleQule.Common; 
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class PaymentSummaryList : ReadOnlyListBaseEx<PaymentSummaryList, PaymentSummary>
	{
		#region Business Methods

		public decimal TotalPendiente()
		{
			decimal total = 0;

			foreach (PaymentSummary item in this)
				total += item.Pendiente;

			return total;
		}

		public decimal TotalPendienteVto()
		{
			decimal total = 0;

			foreach (PaymentSummary item in this)
				total += item.EfectosPendientesVto;

			return total;
		}

		public decimal TotalNegociado()
		{
			decimal total = 0;

			foreach (PaymentSummary item in this)
				total += item.EfectosNegociados;

			return total;
		}

		public decimal TotalEstimado()
		{
			decimal total = 0;

			foreach (PaymentSummary item in this)
				total += item.TotalEstimado;

			return total;
		}

		#endregion

		#region Common Factory Methods

		private PaymentSummaryList() {}
		private PaymentSummaryList(IDataReader reader)
		{
			Fetch(reader);
		}

		public static PaymentSummaryList GetList(IList<PaymentSummary> list)
		{
			PaymentSummaryList flist = new PaymentSummaryList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (PaymentSummary item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

		#endregion

		#region Root Factory Methods

		public static PaymentSummaryList NewList() { return new PaymentSummaryList(); }

        public static PaymentSummaryList GetList(bool childs) { return GetList(SELECT()); }
		public static PaymentSummaryList GetList(ETipoAcreedor providerType, bool childs) 
        {
			QueryConditions conditions = new QueryConditions { TipoAcreedor = new ETipoAcreedor[1] { providerType } };
            return GetList(SELECT(conditions)); 
        }
        public static PaymentSummaryList GetProvidersList(bool childs)
        {
            QueryConditions conditions = new QueryConditions 
            { 
                TipoAcreedor = new ETipoAcreedor[] 
                { 
                    ETipoAcreedor.Acreedor,
                    ETipoAcreedor.Despachante,
                    ETipoAcreedor.Naviera,
                    ETipoAcreedor.Proveedor,
                    ETipoAcreedor.TransportistaDestino,
                    ETipoAcreedor.TransportistaOrigen,                    
                } 
            };

            return GetList(SELECT(conditions));
        }

        public static PaymentSummaryList GetPendientesList() { return GetList(SELECT_PENDIENTES()); }

        public static PaymentSummaryList GetPendientesVtoList(DateTime dueDateFrom, DateTime dueDateTill) 
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaAuxIni = dueDateFrom,
                FechaAuxFin = dueDateTill
            };

            return GetList(PaymentSummary.SELECT_PENDIENTES_VTO(conditions)); 
        }

		public static PaymentSummaryList GetNegociadoList() {	return GetList(SELECT_NEGOCIADO()); }

		public static PaymentSummaryList GetEstimadoList() {	return GetEstimadoList(ETipoAcreedor.Todos); }
		public static PaymentSummaryList GetEstimadoList(ETipoAcreedor tipo) { return GetList(SELECT_ESTIMADO(tipo)); }

		public static PaymentSummaryList GetExplotacionPendientesList(DateTime dueDateFrom, DateTime dueDateTill) 
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaAuxIni = dueDateFrom,
                FechaAuxFin = dueDateTill
            };

            return GetList(PaymentSummary.SELECT_EXPLOTACION_PENDIENTES(conditions)); 
        }
        public static PaymentSummaryList GetExpedientesPendientesList(DateTime dueDateFrom, DateTime dueDateTill) 
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaAuxIni = dueDateFrom,
                FechaAuxFin = dueDateTill
            };

            return GetList(PaymentSummary.SELECT_EXPEDIENTES_PENDIENTES(conditions)); 
        }
        public static PaymentSummaryList GetUnpaidPayrollList(DateTime dueDateFrom, DateTime dueDateTill)
        {
            QueryConditions conditions = new QueryConditions
            {
                TipoAcreedor = new ETipoAcreedor[] { ETipoAcreedor.Empleado },
                FechaAuxIni = dueDateFrom,
                FechaAuxFin = dueDateTill
            };

            return GetList(PaymentSummary.SELECT_UNPAID_PAYROLLS(conditions));
        }

		public static PaymentSummaryList GetList(string query)
		{
			CriteriaEx criteria = Payment.GetCriteria(Payment.OpenSession());
			criteria.Childs = false;

			criteria.Query = query;

			PaymentSummaryList list = DataPortal.Fetch<PaymentSummaryList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Builds a PaymentSummaryList from a IList<!--<PaymentSummary>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PResumenList</returns>
        public static PaymentSummaryList GetChildList(IList<PaymentSummary> list)
        {
            PaymentSummaryList flist = new PaymentSummaryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (PaymentSummary item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static PaymentSummaryList GetChildList(IDataReader reader) { return new PaymentSummaryList(reader); }

        #endregion

        #region Root Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                IsReadOnly = false;

                while (reader.Read())
                    this.AddItem(PaymentSummary.Get(reader));

                IsReadOnly = true;
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                {
                    this.AddItem(PaymentSummary.Get(reader));
                }

                IsReadOnly = true;

            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return PaymentSummary.SELECT(conditions); }
		public static string SELECT_PENDIENTES() { return PaymentSummary.SELECT_PENDIENTES(new QueryConditions()); }
		public static string SELECT_NEGOCIADO() { return PaymentSummary.SELECT_NEGOCIADO(new QueryConditions()); }
		public static string SELECT_ESTIMADO(ETipoAcreedor providerType) { return PaymentSummary.SELECT_ESTIMADO(new QueryConditions { TipoAcreedor = new ETipoAcreedor[1] { providerType } }); }

        #endregion
    }
}