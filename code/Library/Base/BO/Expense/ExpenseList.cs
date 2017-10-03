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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class ExpenseList : ReadOnlyListBaseEx<ExpenseList, ExpenseInfo>
	{
		#region Business Methods

		public decimal TotalPendiente()
		{
			decimal total = 0;
			foreach (ExpenseInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.Pendiente;
			}

			return total;
		}

		public decimal TotalPendienteLiquidacion()
		{
			decimal total = 0;
			foreach (ExpenseInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.TotalPteLiquidacion;
			}

			return total;
		}

		public ExpenseInfo GetItemByTipoAcreedor(ETipoAcreedor tipo)
		{
			foreach (ExpenseInfo item in this)
				if (item.ETipoAcreedor == tipo)
					return item;

			return null;
		}

		public ExpenseInfo GetItemByExpediente(long oid, ETipoAcreedor tipo, ECategoriaGasto categoria)
		{
			foreach (ExpenseInfo item in this)
				if ((item.OidExpediente == oid) && (item.ETipoAcreedor == tipo) && (item.ECategoriaGasto == categoria))
					return item;

			return null;
		}

		public bool ExistsExpediente(long oid)
		{
			foreach (ExpenseInfo item in this)
				if (item.OidExpediente == oid)
					return true;

			return false;
		}

		public bool ExpedienteIsComplete(long oid)
		{
			bool[] gastos = new bool[5] { false, false, false, false, false};

			foreach (ExpenseInfo item in this)
			{
				if (item.OidExpediente == oid)
				{
					if (item.ECategoriaGasto == ECategoriaGasto.Stock) gastos[0] = true;
					if ((item.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente) && (item.ETipoAcreedor == ETipoAcreedor.Naviera)) gastos[1] = true;
					if ((item.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente) && (item.ETipoAcreedor == ETipoAcreedor.Despachante)) gastos[2] = true;
					if ((item.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente) && (item.ETipoAcreedor == ETipoAcreedor.TransportistaOrigen)) gastos[3] = true;
					if ((item.ECategoriaGasto == ECategoriaGasto.GeneralesExpediente) && (item.ETipoAcreedor == ETipoAcreedor.TransportistaDestino)) gastos[4] = true;
				}
			}

			foreach (bool item in gastos) 
				if (item == false) return false;

			return true;
		}

        public void UpdatePagoValues(Payment pago)
        {
            ExpenseInfo item;
            decimal acumulado;

            for (int i = 0; i < Items.Count; i++)
            {
                item = Items[i];

                /*if (item.OidPago != pago.Oid)
                    item.Asignado = 0;*/

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
		private ExpenseList() {}
		private ExpenseList(IList<Expense> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private ExpenseList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }		
		private ExpenseList(IList<ExpenseInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }

		public List<ExpenseInfo> GetSubListByTipo(ECategoriaGasto tipo)
		{
			List<ExpenseInfo> list = new List<ExpenseInfo>();

			foreach (ExpenseInfo item in this)
			{
				if (item.ECategoriaGasto == tipo)
					list.Add(item);
			}

			return list;
		}

		#endregion

		#region Root Factory Methods

		public static ExpenseList NewList() { return new ExpenseList(); }

		public static ExpenseList GetList(bool childs = true) 
		{ 
			return ExpenseList.GetList(ECategoriaGasto.Todos, childs); 
		}
		public static ExpenseList GetList(ECategoriaGasto categoria, bool childs)
		{
			return GetList(categoria, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static ExpenseList GetList(ECategoriaGasto categoria, int year, bool childs)
		{
			return GetList(categoria, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static ExpenseList GetList(ECategoriaGasto categoria, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				CategoriaGasto = categoria,
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(conditions, childs);
		}
		public static ExpenseList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(ExpenseList.SELECT(conditions), childs);
		}
		public static ExpenseList GetList(QueryConditions conditions, ExpedientInfo fromExp, ExpedientInfo tillExp)
		{
			CriteriaEx criteria = Stock.GetCriteria(Expense.OpenSession());
			criteria.Childs = false;

            criteria.Query = ExpenseList.SELECT(conditions, fromExp, tillExp);

			ExpenseList list = DataPortal.Fetch<ExpenseList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
		}
        public static ExpenseList GetByConceptoAlbaranList(long oidLine, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                ConceptoAlbaranProveedor = InputDeliveryLine.NewChild().GetInfo(),
                CategoriaGasto = ECategoriaGasto.Expediente
            };
            conditions.ConceptoAlbaranProveedor.Oid = oidLine;

            return GetList(conditions, childs);
        }
        public static ExpenseList GetAsociadoList(QueryConditions conditions, bool childs)
        {
            return GetList(ExpenseList.SELECT_ASOCIADO(conditions), childs);
        }
        public static ExpenseList GetByFacturaExpedienteList(QueryConditions conditions, ExpedientInfo fromExp, ExpedientInfo tillExp)
        {
            CriteriaEx criteria = Stock.GetCriteria(Expense.OpenSession());
            criteria.Childs = false;

            criteria.Query = ExpenseList.SELECT_BY_FACTURA_EXPEDIENTE(conditions, fromExp, tillExp);

            ExpenseList list = DataPortal.Fetch<ExpenseList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static ExpenseList GetPendientesList(ECategoriaGasto categoria, bool childs)
		{
			return GetPendientesList(categoria, null, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static ExpenseList GetPendientesList(ECategoriaGasto categoria, PaymentInfo payment, bool childs)
		{
			return GetPendientesList(categoria, payment, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static ExpenseList GetPendientesList(ECategoriaGasto categoria, DateTime from, DateTime till, bool childs)
		{
			return GetPendientesList(categoria, null, from, till, childs);
		}
		public static ExpenseList GetPendientesList(ECategoriaGasto categoria, PaymentInfo payment, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				CategoriaGasto = categoria,
				Payment = payment,
				FechaAuxIni = from,
				FechaAuxFin = till,
			};

			return GetPendientesList(conditions, childs);
		}
		public static ExpenseList GetPendientesList(QueryConditions conditions, bool childs)
		{
			return GetList(SELECT_PENDIENTES(conditions), childs);
		}

		public static ExpenseList GetPendientesLiquidacionList(ECategoriaGasto categoria, DateTime dueDateFrom, DateTime dueDateTill, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				CategoriaGasto = categoria,
                FechaAuxIni = dueDateFrom,
                FechaAuxFin = dueDateTill,
			};

			return GetList(Expense.SELECT_PENDIENTES_LIQUIDACION(conditions, false), childs);
		}

		public static ExpenseList GetPendientesNominaList(ECategoriaGasto categoria, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				CategoriaGasto = categoria,
				FechaAuxIni = from,
				FechaAuxFin = till,
			};

			return GetList(SELECT_PENDIENTES_NOMINAS(conditions), childs);
		}

		public static ExpenseList GetByPagoAndPendientesList(ECategoriaGasto categoria, PaymentInfo pago, bool childs)
		{
			ExpenseList byPago = GetByPagoList(pago, childs);
			ExpenseList pendientes = GetPendientesList(categoria, childs);

			ExpenseList list = new ExpenseList();
			list.IsReadOnly = false;

			foreach (ExpenseInfo item in byPago)
				list.AddItem(item);

			foreach (ExpenseInfo item in pendientes)
				if (list.GetItem(item.Oid) == null) list.AddItem(item);
            
			list.IsReadOnly = true;

			return list;
		}
		public static ExpenseList GetByPagoList(PaymentInfo pago, bool childs)
		{
            if (pago.Oid != -1)
            {
                QueryConditions conditions = new QueryConditions
                    {
                        Payment = pago,
                        PaymentType = pago.ETipoPago == ETipoPago.Fraccionado ? ETipoPago.Fraccionado : ETipoPago.Todos
                    };

                return GetList(SELECT_BY_PAGO(conditions), childs);
            }
            else
            {
                List<long> oid_list = new List<long>();

                foreach (TransactionPaymentInfo item in pago.Operations)
                    oid_list.Add(item.OidOperation);

                QueryConditions conditions = new QueryConditions
                {
                    OidList = oid_list,
                    Payment = pago,
                    PaymentType = pago.ETipoPago == ETipoPago.Fraccionado ? ETipoPago.Fraccionado : ETipoPago.Todos
                };

                conditions.Payment.Oid = 0;

                ExpenseList list = GetList(SELECT_BY_PAGO(conditions), childs);

                foreach (ExpenseInfo gasto in list)
                    gasto.Asignado = 0;

                foreach (TransactionPaymentInfo item in pago.Operations)
                    list.GetItem(item.OidOperation).Asignado += item.Cantidad;

                return list;
            }
		}

		public static ExpenseList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Expense.GetCriteria(Expense.OpenSession());
			criteria.Childs = childs;

			//No criteria. Retrieve all de List

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			ExpenseList list = DataPortal.Fetch<ExpenseList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static ExpenseList GetList(IList<Expense> list) { return new ExpenseList(list, false); }
		public static ExpenseList GetList(IList<ExpenseInfo> list) { return new ExpenseList(list, false); }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ExpenseInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<ExpenseInfo> sortedList = new SortedBindingList<ExpenseInfo>(GetList());

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		public static SortedBindingList<ExpenseInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
		{
			SortedBindingList<ExpenseInfo> sortedList = new SortedBindingList<ExpenseInfo>(GetList(childs));

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}

		#endregion

		#region Child Factory Methods
			
        public static ExpenseList GetChildList(IList<ExpenseInfo> list)
        {
            ExpenseList flist = new ExpenseList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ExpenseInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static ExpenseList GetChildList(IList<Expense> list) { return new ExpenseList(list, false); }
        public static ExpenseList GetChildList(IDataReader reader) { return new ExpenseList(reader, false); }
		public static ExpenseList GetChildList(ExpedientInfo parent, bool childs)
		{
			CriteriaEx criteria = Expense.GetCriteria(Expense.OpenSession());

			criteria.Query = ExpenseList.SELECT(parent);
			criteria.Childs = childs;

			ExpenseList list = DataPortal.Fetch<ExpenseList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}
		
		#endregion

		#region Common Data Access

		// called to copy objects data from list
		private void Fetch(IList<Expense> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;

			foreach (Expense item in lista)
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
				this.AddItem(ExpenseInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(ExpenseInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return Expense.SELECT(conditions, false); }
        public static string SELECT_ASOCIADO(QueryConditions conditions) { return Expense.SELECT_ASOCIADO(conditions, false); }
        public static string SELECT_BY_FACTURA_EXPEDIENTE(QueryConditions conditions) { return Expense.SELECT_BY_FACTURA_EXPEDIENTE(conditions, false); }
        public static string SELECT(ExpedientInfo source) { return Expense.SELECT(new QueryConditions { Expedient = source, CategoriaGasto = ECategoriaGasto.Expediente }, false); }
		public static string SELECT(QueryConditions conditions, ExpedientInfo ini, ExpedientInfo fin)
		{
			string query = string.Empty;

			query = SELECT(conditions);

			if (ini != null)
				query += " AND EX.\"CODIGO\" >='" + ini.Codigo + "'";

			if (fin != null)
				query += " AND EX.\"CODIGO\" <='" + fin.Codigo + "'";

			query += " ORDER BY EX.\"CODIGO\", GT.\"CODIGO\"";

			return query;
		}
        public static string SELECT_BY_FACTURA_EXPEDIENTE(QueryConditions conditions, ExpedientInfo ini, ExpedientInfo fin)
        {
            string query = string.Empty;

            query = SELECT_BY_FACTURA_EXPEDIENTE(conditions);

            if (ini != null)
                query += " AND EX.\"CODIGO\" >='" + ini.Codigo + "'";

            if (fin != null)
                query += " AND EX.\"CODIGO\" <='" + fin.Codigo + "'";

            query += " GROUP BY GT.\"OID_EXPEDIENTE\", GT.\"OID_FACTURA\", GT.\"TIPO\", GT.\"OID_USUARIO\", US.\"NAME\", TG.\"NOMBRE\", FP.\"OID_ACREEDOR\", FP.\"TIPO_ACREEDOR\", FP.\"EMISOR\", FP.\"FECHA\", FP.\"N_FACTURA\", FP.\"BASE_IMPONIBLE\", EX.\"CODIGO\", LC.\"CODIGO\", MV.\"CODIGO\", MV2.\"CODIGO\"";
            
            query += " ORDER BY EX.\"CODIGO\", \"OID\"";

            return query;
        }

		public static string SELECT(PayrollBatchInfo source) { return Expense.SELECT(new QueryConditions { RemesaNomina = source }, false); }
        public static string SELECT(PaymentInfo source)
        {
            return Expense.SELECT_BY_PAGO(new QueryConditions
            {
                Payment = source,
                PaymentType = source.ETipoPago == ETipoPago.Fraccionado ? ETipoPago.Fraccionado : ETipoPago.Todos
            }, false);
        }
		public static string SELECT_BY_PAGO(QueryConditions conditions) { return Expense.SELECT_BY_PAGO(conditions, false); }
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return Expense.SELECT_PENDIENTES(conditions, false); }
		public static string SELECT_PENDIENTES_NOMINAS(QueryConditions conditions) { return Expense.SELECT_PENDIENTES_NOMINAS(conditions, false); }
		
        #endregion
	}
}