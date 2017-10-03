using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class PayrollList : ReadOnlyListBaseEx<PayrollList, NominaInfo>
	{
		#region Business Methods

		public decimal TotalPendiente()
		{
			decimal total = 0;
			foreach (NominaInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.Pendiente;
			}

			return total;
		}

		public bool ExistsExpediente(long oid)
		{
			foreach (NominaInfo item in this)
				if (item.OidExpediente == oid)
					return true;

			return false;
		}

        public void UpdatePagoValues(Payment pago)
        {
            NominaInfo item;
            decimal acumulado;

            for (int i = 0; i < Items.Count; i++)
            {
                item = Items[i];

                if (i == 0) acumulado = 0;
                else acumulado = Items[i - 1].Acumulado;

                item.Acumulado = acumulado + item.Pendiente;
                item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
            }
        }

		#endregion

		#region Common Factory Methods

		/// <summary>
        /// Constructores
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PayrollList() {}
		private PayrollList(IList<Payroll> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private PayrollList(IDataReader reader, bool childs)
        {
			Childs = childs;
            Fetch(reader);
        }
		private PayrollList(IList<NominaInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }

		#endregion

		#region Root Factory Methods

		public static PayrollList NewList() { return new PayrollList(); }

		public static PayrollList GetList() {  return PayrollList.GetList(true); }
		public static PayrollList GetList(bool childs) { return PayrollList.GetList(DateTime.MinValue, DateTime.MaxValue, childs); }
		public static PayrollList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static PayrollList GetList(DateTime f_ini, DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = f_ini,
				FechaFin = f_fin,
			};

			return GetList(conditions, childs);
		}
		public static PayrollList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(PayrollList.SELECT(conditions), childs);
		}
		public static PayrollList GetList(QueryConditions conditions, ExpedientInfo ini, ExpedientInfo fin)
		{
			CriteriaEx criteria = Stock.GetCriteria(Payroll.OpenSession());
			criteria.Childs = false;

			criteria.Query = PayrollList.SELECT(conditions, ini, fin);

			PayrollList list = DataPortal.Fetch<PayrollList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
		}

        public static PayrollList GetListByEmpleado(long oid_empleado, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Acreedor = ProviderBaseInfo.New(oid_empleado, ETipoAcreedor.Empleado)
            };

            return GetList(conditions, childs);
        }

		public static PayrollList GetPendientesList(bool childs)
		{
			return GetPendientesList(null, DateTime.MinValue, DateTime.MaxValue, childs);
		}
        public static PayrollList GetPendientesList(long oid_empleado, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Acreedor = ProviderBaseInfo.New(oid_empleado, ETipoAcreedor.Empleado)
            };

            return GetPendientesList(conditions, childs);
        }
		public static PayrollList GetPendientesList(PaymentInfo pago, bool childs)
		{
			return GetPendientesList(pago, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static PayrollList GetPendientesList(DateTime from, DateTime till, bool childs)
		{
			return GetPendientesList(null, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static PayrollList GetPendientesList(PaymentInfo pago, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Payment = pago,
				FechaAuxIni = from,
				FechaAuxFin = till,
			};

			return GetPendientesList(conditions, childs);
		}
		public static PayrollList GetPendientesList(QueryConditions conditions, bool childs)
		{
			return GetList(Payroll.SELECT_PENDIENTES(conditions, false), childs);
		}

        public static PayrollList GetByPagoAndPendientesList(PaymentInfo pago, long oid_empleado, bool childs)
        {
            PayrollList byPago = GetByPagoList(pago, childs);
            PayrollList pendientes = GetPendientesList(oid_empleado, childs);

            PayrollList list = new PayrollList();
            list.IsReadOnly = false;

            foreach (NominaInfo item in byPago)
                list.AddItem(item);

            foreach (NominaInfo item in pendientes)
                if (list.GetItem(item.Oid) == null) list.AddItem(item);

            list.IsReadOnly = true;

            return list;
        }

		public static PayrollList GetByPagoAndPendientesList(PaymentInfo pago, bool childs)
		{
			PayrollList byPago = GetByPagoList(pago, childs);
			PayrollList pendientes = GetPendientesList(childs);

			PayrollList list = new PayrollList();
			list.IsReadOnly = false;

			foreach (NominaInfo item in byPago)
				list.AddItem(item);

			foreach (NominaInfo item in pendientes)
				if (list.GetItem(item.Oid) == null) list.AddItem(item);

			list.IsReadOnly = true;

			return list;
		}
		public static PayrollList GetByPagoList(PaymentInfo pago, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Payment = pago };

			return GetList(SELECT_BY_PAGO(conditions), childs);
		}

		public static PayrollList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Payroll.GetCriteria(Payroll.OpenSession());
			criteria.Childs = childs;

			//No criteria. Retrieve all de List

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			PayrollList list = DataPortal.Fetch<PayrollList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static PayrollList GetList(IList<Payroll> list) { return new PayrollList(list, false); }
		public static PayrollList GetList(IList<NominaInfo> list) { return new PayrollList(list, false); }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<NominaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<NominaInfo> sortedList = new SortedBindingList<NominaInfo>(GetList());

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		public static SortedBindingList<NominaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
		{
			SortedBindingList<NominaInfo> sortedList = new SortedBindingList<NominaInfo>(GetList(childs));

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}

		#endregion

		#region Child Factory Methods
			
        public static PayrollList GetChildList(IList<NominaInfo> list)
        {
            PayrollList flist = new PayrollList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (NominaInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static PayrollList GetChildList(IList<Payroll> list) { return new PayrollList(list, false); }
        public static PayrollList GetChildList(IDataReader reader) { return new PayrollList(reader, false); }
		public static PayrollList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = Payroll.GetCriteria(Payroll.OpenSession());

			criteria.Query = PayrollList.SELECT(parent);
			criteria.Childs = childs;

			PayrollList list = DataPortal.Fetch<PayrollList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}
		
		#endregion

		#region Common Data Access

		// called to copy objects data from list
		private void Fetch(IList<Payroll> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;

			foreach (Payroll item in lista)
				this.AddItem(item.GetInfo());

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

		// called to copy objects data from list
		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;

			while (reader.Read())
				this.AddItem(NominaInfo.GetChild(SessionCode, reader, Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region Root Data Access

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="criteria">Criterios de la consulta</param>
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(NominaInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return Payroll.SELECT(conditions, false); }
        public static string SELECT_ASOCIADO(QueryConditions conditions) { return Payroll.SELECT_ASOCIADO(conditions, false); }
		public static string SELECT(ExpedientInfo source) { return Payroll.SELECT(new QueryConditions { Expedient = source }, false); }
		public static string SELECT(QueryConditions conditions, ExpedientInfo ini, ExpedientInfo fin)
		{
			string query = string.Empty;

			query = SELECT(conditions);

			if (ini != null)
				query += " AND EX.\"CODIGO\" >='" + ini.Codigo + "'";

			if (fin != null)
				query += " AND EX.\"CODIGO\" <='" + fin.Codigo + "'";

			query += " ORDER BY EX.\"CODIGO\", G.\"CODIGO\"";

			return query;
		}

		public static string SELECT(PayrollBatchInfo source) { return Payroll.SELECT(new QueryConditions { RemesaNomina = source }, false); }
		public static string SELECT(PaymentInfo source) { return Payroll.SELECT(new QueryConditions { Payment = source }, false); }
		public static string SELECT_BY_PAGO(QueryConditions conditions) { return Payroll.SELECT_BY_PAGO(conditions, false); }
		
        #endregion
	}
}

