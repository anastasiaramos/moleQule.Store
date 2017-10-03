using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using moleQule.Common.Structs;
using moleQule;
using moleQule.CslaEx; 
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class InputInvoiceList : ReadOnlyListBaseEx<InputInvoiceList, InputInvoiceInfo, InputInvoice>
	{
		#region Business Methods

		public decimal Total()
		{
			decimal total = 0;

			foreach (InputInvoiceInfo item in this)
				total += item.Total;

			return total;
		}

		public decimal TotalImpuestos()
		{
			decimal total = 0;

			foreach (InputInvoiceInfo item in this)
				total += item.Impuestos;

			return total;
		}

		public decimal TotalExpediente()
		{
			decimal total = 0;

			foreach (InputInvoiceInfo item in this)
				total += item.TotalExpediente;

			return total;
		}

		public decimal TotalPendiente()
        {
			decimal total = 0;

            foreach (InputInvoiceInfo item in this)
				total += item.Pendiente;

			return total;
        }

        public InputInvoiceInfo GetItemByAcreedor(long oid_acreedor, ETipoAcreedor tipo)
        {
            foreach (InputInvoiceInfo item in this)
                if ((item.OidAcreedor == oid_acreedor) && (item.ETipoAcreedor == tipo))
                    return item;

            return null;
        }

        public InputInvoiceInfo GetItemByTipoAcreedor(ETipoAcreedor tipo)
        {
            foreach (InputInvoiceInfo item in this)
                if (item.ETipoAcreedor == tipo)
                    return item;

            return null;
        }

		public InputInvoiceInfo GetItemByNFactura(string n_factura, int year, long oid_acreedor, ETipoAcreedor tipo)
		{
			foreach (InputInvoiceInfo item in this)
				if ((item.NFactura == n_factura) && 
					(item.Fecha.Year == year) &&
					(item.OidAcreedor == oid_acreedor) &&
					(item.ETipoAcreedor == tipo))
					return item;

			return null;
		}

		public void UpdatePagoValues(Payment pago)
		{
			InputInvoiceInfo item;
			decimal acumulado;
			TransactionPayment pagoFactura;

			for (int i = 0; i < Items.Count; i++)
			{
				item = Items[i];

				pagoFactura = pago.Operations.GetItemByFactura(item.Oid);

				if (pagoFactura != null)
					item.Asignado = pagoFactura.Cantidad;

				if (i == 0) acumulado = 0;
				else acumulado = Items[i - 1].Acumulado;

				item.Acumulado = acumulado + item.Pendiente;
				item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
			}
		}

        #endregion

		#region Factory Methods

		private InputInvoiceList() { }

        private InputInvoiceList(IDataReader reader)
        {
            Fetch(reader);
        }

        private InputInvoiceList(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>FacturaRecibidaList</returns>
        public static InputInvoiceList GetChildList(bool childs)
        {
            CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = InputInvoiceSQL.SELECT();

            InputInvoiceList list = DataPortal.Fetch<InputInvoiceList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static InputInvoiceList NewList() { return new InputInvoiceList(); }

		public static InputInvoiceList GetList()  { return GetList(true); }
		public static InputInvoiceList GetList(bool childs) { return GetList(ETipoAcreedor.Todos, childs); }
		public static InputInvoiceList GetList(ETipoAcreedor tipo, bool childs)
		{
			return GetList(tipo, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor tipo, int year, bool childs)
		{
			return GetList(tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor tipo, DateTime from, DateTime till, bool childs)
		{
			return GetList(tipo, 0, 0, ETipoFacturas.Todas, ETipoFactura.Todas, from, till, childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor tipo, long oidAcreedor, DateTime from, DateTime till, bool childs)
		{
			return GetList(tipo, oidAcreedor, 0, ETipoFacturas.Todas, ETipoFactura.Todas, from, till, childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor providerType, CriteriaEx criteria, bool childs)
		{
			return GetList(providerType, 0, criteria, childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor providerType, long oidProvider, CriteriaEx criteria, bool childs)
		{
			return GetList(providerType, oidProvider, DateTime.MinValue, DateTime.MaxValue, EStepGraph.None, criteria, childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor tipo, long oidAcreedor, bool childs)
		{
			return GetList(tipo, oidAcreedor, 0, ETipoFacturas.Todas, ETipoFactura.Todas, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static InputInvoiceList GetList(ETipoAcreedor providerType,
											long oidProvider,
											long oidSerie,
											ETipoFacturas invoicesType,
											ETipoFactura invoiceType,
											DateTime from,
											DateTime till,
											bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
				Serie = (oidSerie != 0) ? Serie.Serie.New().GetInfo() : null,
				TipoFacturas = invoicesType,
				TipoFactura = invoiceType,
				FechaIni = from,
				FechaFin = till,
			};
			if (oidSerie != 0) conditions.Serie.Oid = oidSerie;

            return GetList(InputInvoiceSQL.SELECT(conditions), childs);
		}

		public static InputInvoiceList GetList(ETipoAcreedor providerType,
													long oidProvider,
													DateTime from, 
													DateTime till, 
													EStepGraph step, 
													CriteriaEx criteria, 
													bool childs)
		{
			QueryConditions conditions = new QueryConditions()
			{
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null,
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
				Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
				FechaIni = from,
				FechaFin = till,
				Step = step,
			};

			string query = InputInvoiceSQL.SELECT(conditions);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}

		public static InputInvoiceList GetPendientesList(bool childs)
		{
			return GetPendientesList(ETipoAcreedor.Todos, childs);
		}
		public static InputInvoiceList GetPendientesList(ETipoAcreedor providerType, bool childs)
		{
			return GetPendientesList(providerType, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static InputInvoiceList GetPendientesList(ETipoAcreedor providerType, int year, bool childs)
		{
			return GetPendientesList(providerType, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static InputInvoiceList GetPendientesList(ETipoAcreedor providerType, DateTime from, DateTime till, bool childs)
		{
			return GetList(providerType, 0, 0, ETipoFacturas.Pendientes, ETipoFactura.Todas, from, till, childs);
		}
		public static InputInvoiceList GetPendientesList(long oidProvider, ETipoAcreedor providerType, bool childs)
		{
			QueryConditions conditions = new QueryConditions 
			{
				TipoFacturas = ETipoFacturas.Pendientes,
				Acreedor = ProviderBaseInfo.New(oidProvider, providerType),
				TipoAcreedor = new ETipoAcreedor[1] { providerType }
			};

			return GetList(InputInvoiceSQL.SELECT(conditions), childs);
		}

		public static InputInvoiceList GetPendientesList(DateTime expirationFrom,
															DateTime expirationTill,
															EMedioPago paymentMethod,
															bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoFacturas = ETipoFacturas.Pendientes,
				FechaAuxIni = expirationFrom,
				FechaAuxFin = expirationTill,
				MedioPago = paymentMethod
			};

			return GetList(InputInvoiceSQL.SELECT(conditions), childs);
		}

		public static InputInvoiceList GetPagadasList(bool childs)
		{
			return GetPagadasList(ETipoAcreedor.Todos, childs);
		}
		public static InputInvoiceList GetPagadasList(ETipoAcreedor tipo, bool childs)
		{
			return GetPagadasList(tipo, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static InputInvoiceList GetPagadasList(ETipoAcreedor tipo, int year, bool childs)
		{
			return GetPagadasList(tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static InputInvoiceList GetPagadasList(ETipoAcreedor tipo, DateTime from, DateTime till, bool childs)
		{
			return GetList(tipo, 0, 0, ETipoFacturas.Pagadas, ETipoFactura.Todas, from, till, childs);
		}

        public static InputInvoiceList GetListByDate(DateTime from, DateTime till, bool childs)
        {
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till
			};

			return GetList(InputInvoiceSQL.SELECT_CONTROL_PAGOS(conditions), childs);
        }

		public static InputInvoiceList GetListByAcreedor(long oid, ETipoAcreedor providerType, bool childs)
        {
            QueryConditions conditions = new QueryConditions 
			{
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
				Acreedor = ProviderBaseInfo.New(oid, providerType) 
			};

            return GetList(InputInvoiceSQL.SELECT(conditions), childs);
        }

		public static InputInvoiceList GetListByModelo(EModelo model, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				EModelo = model,
				FechaIni = from,
				FechaFin = till
			};

			return GetList(InputInvoiceSQL.SELECT_BY_MODELO(conditions), childs);
		}

		public static InputInvoiceList GetListByPagoAndPendientesByAcreedor(long oid_pago, long oid_acreedor, ETipoAcreedor tipo, bool childs)
		{
			InputInvoiceList byPago = GetListByPago(oid_pago, childs);
			InputInvoiceList pendientes = GetPendientesList(oid_acreedor, tipo, childs);

			InputInvoiceList list = new InputInvoiceList();
			list.IsReadOnly = false;

			foreach (InputInvoiceInfo item in byPago)
				list.AddItem(item);

			foreach (InputInvoiceInfo item in pendientes)
				if (list.GetItem(item.Oid) == null) list.AddItem(item);

			list.IsReadOnly = true;

			return list;
		}

        public static InputInvoiceList GetListByPago(long oid, bool childs)
        {
			QueryConditions conditions = new QueryConditions { Payment = PaymentInfo.New(oid, ETipoPago.Todos) };

            return GetList(InputInvoiceSQL.SELECT_BY_PAGO(conditions), childs);
        }

        public static InputInvoiceList GetListByExpediente(long oid, bool childs)
        {
            CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
            criteria.Childs = childs;
			
			QueryConditions conditions = new QueryConditions
			{
				Expedient = ExpedientInfo.New(oid)
			};

			criteria.Query = InputInvoiceSQL.SELECT_EXPEDIENTES(conditions);
            InputInvoiceList list = DataPortal.Fetch<InputInvoiceList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
        }
		public static InputInvoiceList GetCostesByExpedienteList(long oid, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Expedient = ExpedientInfo.New(oid),
				TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Proveedor },
			};

			return GetList(InputInvoiceSQL.SELECT_COSTES_EXPEDIENTE(conditions), childs);
		}

		public static InputInvoiceList GetListNoAsignadasByAcreedor(long oid, ETipoAcreedor providerType, bool childs)
		{
			QueryConditions conditions = new QueryConditions 
			{ 
				Acreedor = ProviderBaseInfo.New(oid, providerType),
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
			};

			return GetList(InputInvoiceSQL.SELECT_SIN_EXPEDIENTE(conditions), childs);
		}

		public static InputInvoiceList GetListNoAsignadas(bool childs) { return GetListNoAsignadas(ETipoAcreedor.Todos, childs); }

		public static InputInvoiceList GetListNoAsignadas(ETipoAcreedor providerType, bool childs)
        {
			QueryConditions conditions = new QueryConditions
			{
				TipoAcreedor = new ETipoAcreedor[1] { providerType }
			};
			return GetList(InputInvoiceSQL.SELECT_SIN_EXPEDIENTE(conditions), childs);
        }

		public static InputInvoiceList GetExplotacionList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(InputInvoiceSQL.SELECT_EXPLOTACION(conditions), childs);
		}

		public static InputInvoiceList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(InputInvoiceSQL.SELECT(conditions), childs);
		}
		private static InputInvoiceList GetList(string query, bool childs)
		{
			CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			InputInvoiceList list = DataPortal.Fetch<InputInvoiceList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static InputInvoiceList GetList(IList<InputInvoiceInfo> list)
        {
            InputInvoiceList flist = new InputInvoiceList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputInvoiceInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static InputInvoiceList GetList(IList<InputInvoice> list)
		{
			InputInvoiceList flist = new InputInvoiceList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (InputInvoice item in list)
					flist.AddItem(item.GetInfo());

				flist.IsReadOnly = true;
			}

			return flist;
		}

        public static SortedBindingList<InputInvoiceInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<InputInvoiceInfo> sortedList = new SortedBindingList<InputInvoiceInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<InputInvoiceInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<InputInvoiceInfo> sortedList = new SortedBindingList<InputInvoiceInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
		
        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static InputInvoiceList GetChildList()
        {
            return InputInvoiceList.GetChildList(true);
        }

        /// <summary>
        /// Builds a FacturaRecibidaList from a IList<!--<FacturaRecibidaInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>FacturaRecibidaList</returns>
        public static InputInvoiceList GetChildList(IList<InputInvoiceInfo> list)
        {
            InputInvoiceList flist = new InputInvoiceList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputInvoiceInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a FacturaRecibidaList from IList<!--<FacturaRecibida>--> and retrieve FacturaRecibidaInfo Childs from DB
        /// </summary>
        /// <param name="list"></param>
        /// <returns>FacturaRecibidaList</returns>
        public static InputInvoiceList GetChildList(IList<InputInvoice> list)
        {
            InputInvoiceList flist = new InputInvoiceList();

            if (list != null)
            {
                int sessionCode = InputInvoice.OpenSession();
                CriteriaEx criteria = null;

                flist.IsReadOnly = false;

                foreach (InputInvoice item in list)
                {
                    criteria = InputInvoiceLine.GetCriteria(sessionCode);
                    criteria.AddEq("OidFactura", item.Oid);
                    item.Conceptos = InputInvoiceLines.GetChildList(criteria.List<InputInvoiceLine>());

                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                InputInvoice.CloseSession(sessionCode);
            }

            return flist;
        }

        public static InputInvoiceList GetChildList(IDataReader reader) { return new InputInvoiceList(reader); }
        public static InputInvoiceList GetChildList(IDataReader reader, bool childs) { return new InputInvoiceList(reader, childs); }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

					while (reader.Read())
					{
						InputInvoiceInfo item = InputInvoiceInfo.GetChild(SessionCode, reader, Childs);
						if (this.GetItem(item.Oid) == null) this.AddItem(item);
					}

                    IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(InputInvoiceSQL.SELECT_COUNT(criteria), criteria.Session);
						if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
					}
                }
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (InputInvoice item in list)
                        this.AddItem(item.GetInfo(false));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
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
                    this.AddItem(InputInvoiceInfo.GetChild(SessionCode, reader, Childs));

                IsReadOnly = true;

            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion
    }
}