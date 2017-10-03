using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class InputInvoiceLineList : ReadOnlyListBaseEx<InputInvoiceLineList, InputInvoiceLineInfo>
    {
        #region Business Methods

		public InputInvoiceLineInfo GetItemByProducto(long oid)
		{
			foreach (InputInvoiceLineInfo item in Items)
				if (item.OidProducto == oid)
					return item;

			return null;
		}

		public decimal GetSubTotalByExpediente(long oid)
		{ 
			decimal total = 0;

			foreach (InputInvoiceLineInfo item in this)
				if (item.OidExpediente == oid) total += item.Subtotal;

			return total;
		}

		public decimal Total()
		{
			decimal total = 0;

			foreach (InputInvoiceLineInfo item in this)
				total += item.Total;

			return total;
		}

		public decimal TotalImpuestos()
		{
			decimal total = 0;

			foreach (InputInvoiceLineInfo item in this)
				total += item.Impuestos;

			return total;
		}

		#endregion

        #region Factory Methods

        private InputInvoiceLineList() { }
        private InputInvoiceLineList(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>ConceptoFacturaRecibidaList</returns>
        public static InputInvoiceLineList GetChildList(bool childs)
        {
            CriteriaEx criteria = InputInvoiceLine.GetCriteria(InputInvoiceLine.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            InputInvoiceLineList list = DataPortal.Fetch<InputInvoiceLineList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static InputInvoiceLineList GetList() { return InputInvoiceLineList.GetList(true);	}
        public static InputInvoiceLineList GetList(bool childs)
        {
			return GetList(InputInvoiceLineList.SELECT(), childs);
        }

        public static InputInvoiceLineList GetCostesByExpedienteList(long oidExpediente, bool childs, bool cache)
		{
			InputInvoiceLineList list;

			if (!Cache.Instance.Contains(typeof(InputInvoiceLineList)))
			{
				QueryConditions conditions = new QueryConditions
				{
                    Expedient = ExpedientInfo.New(oidExpediente),
					TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Proveedor }
				};

				list = GetList(InputInvoiceLine.SELECT_COSTES(conditions, false), childs);
				Cache.Instance.Save(typeof(InputInvoiceLineList), list);
			}
			else
				list = Cache.Instance.Get(typeof(InputInvoiceLineList)) as InputInvoiceLineList;

			return list;			
		}

        public static InputInvoiceLineList GetByFacturaList(long oid_factura, bool childs)
        {
            InputInvoiceLineList list;

            QueryConditions conditions = new QueryConditions
            {
                FacturaRecibida = InputInvoice.New().GetInfo(false)
            };
            conditions.FacturaRecibida.Oid = oid_factura;

            list = GetList(InputInvoiceLine.SELECT(conditions, false), childs);

            return list;
        }

		private static InputInvoiceLineList GetList(string query, bool childs)
		{
			CriteriaEx criteria = InputInvoiceLine.GetCriteria(InputInvoiceLine.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			InputInvoiceLineList list = DataPortal.Fetch<InputInvoiceLineList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static InputInvoiceLineList GetList(IList<InputInvoiceLineInfo> list)
        {
            InputInvoiceLineList flist = new InputInvoiceLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputInvoiceLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<InputInvoiceLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<InputInvoiceLineInfo> sortedList = new SortedBindingList<InputInvoiceLineInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<InputInvoiceLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<InputInvoiceLineInfo> sortedList = new SortedBindingList<InputInvoiceLineInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Builds a ConceptoFacturaRecibidaList from a IList<!--<ConceptoFacturaRecibida>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ConceptoFacturaRecibidaList</returns>
        public static InputInvoiceLineList GetList(IList<InputInvoiceLine> list)
        {
            InputInvoiceLineList flist = new InputInvoiceLineList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (InputInvoiceLine item in list)
                    flist.AddItem(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static InputInvoiceLineList GetChildList()
        {
            return InputInvoiceLineList.GetChildList(true);
        }
        public static InputInvoiceLineList GetChildList(IList<InputInvoiceLineInfo> list)
        {
            InputInvoiceLineList flist = new InputInvoiceLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (InputInvoiceLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static InputInvoiceLineList GetChildList(IList<InputInvoiceLine> list)
        {
            InputInvoiceLineList flist = new InputInvoiceLineList();

            if (list != null)
            {
                int sessionCode = InputInvoiceLine.OpenSession();

                flist.IsReadOnly = false;

                foreach (InputInvoiceLine item in list)
                {
                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                InputInvoiceLine.CloseSession(sessionCode);
            }

            return flist;
        }
        public static InputInvoiceLineList GetChildList(int sessionCode, IDataReader reader) { return new InputInvoiceLineList(sessionCode, reader); }
		public static InputInvoiceLineList GetChildList(InputInvoiceInfo parent, bool childs)
		{
			CriteriaEx criteria = InputInvoiceLine.GetCriteria(InputInvoiceLine.OpenSession());

			criteria.Query = InputInvoiceLineList.SELECT(parent);
			criteria.Childs = childs;

			InputInvoiceLineList list = DataPortal.Fetch<InputInvoiceLineList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

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
                        this.AddItem(InputInvoiceLineInfo.Get(reader, Childs));

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
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (InputInvoiceLine item in list)
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
                {
                    this.AddItem(InputInvoiceLineInfo.Get(reader, Childs));
                }

                IsReadOnly = true;

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
		public static string SELECT(Library.Store.QueryConditions conditions) { return InputInvoiceLine.SELECT(conditions, false); }
		public static string SELECT(InputInvoiceInfo factura)
		{
			string query;

			QueryConditions conditions = new QueryConditions { FacturaRecibida = factura };
			query = InputInvoiceLine.SELECT(conditions, false);

			return query;
		}

        #endregion
    }
}



