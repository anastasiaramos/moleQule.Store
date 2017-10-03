using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class ProductoProveedorList : ReadOnlyListBaseEx<ProductoProveedorList, ProductoProveedorInfo>
    {
        #region Bussines Methods

        public ProductoProveedorInfo GetItemByAcreedor(long oid, ETipoAcreedor tipo)
        {
            foreach (ProductoProveedorInfo obj in this)
                if ((obj.OidAcreedor == oid) && (obj.ETipoAcreedor == tipo))
                    return obj;
            return null;
        }

        public ProductoProveedorInfo GetItemByProducto(long oid_producto)
        {
            foreach (ProductoProveedorInfo obj in this)
                if (obj.OidProducto == oid_producto)
                    return obj;
            return null;
        }

        #endregion

        #region Factory Methods

        private ProductoProveedorList() { }
        private ProductoProveedorList(IList<ProductoProveedor> lista)
		{
            Fetch(lista);
        }
        private ProductoProveedorList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
        public static ProductoProveedorList GetList(bool childs)
        {
            CriteriaEx criteria = ProductoProveedor.GetCriteria(ProductoProveedor.OpenSession());
            criteria.Childs = childs;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = ProductoProveedorList.SELECT();

            ProductoProveedorList list = DataPortal.Fetch<ProductoProveedorList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static ProductoProveedorList GetList()
		{ 
			return ProductoProveedorList.GetList(true); 
		}
        public static ProductoProveedorList GetList(CriteriaEx criteria)
        {
            return ProductoProveedorList.RetrieveList(typeof(ProductoProveedor), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Builds a ProductoProveedorList from a IList<!--<ProductoProveedorInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ProductoProveedorList</returns>
        public static ProductoProveedorList GetChildList(IList<ProductoProveedorInfo> list)
        {
            ProductoProveedorList flist = new ProductoProveedorList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ProductoProveedorInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static ProductoProveedorList GetChildList(IList<ProductoProveedor> list) { return new ProductoProveedorList(list); }
		public static ProductoProveedorList GetChildList(int sessionCode, IDataReader reader) { return new ProductoProveedorList(sessionCode, reader); }
		public static ProductoProveedorList GetChildList(IAcreedorInfo parent, bool childs)
		{
			CriteriaEx criteria = ProductoProveedor.GetCriteria(ProductoProveedor.OpenSession());

			criteria.Query = ProductoProveedorList.SELECT(parent);
			criteria.Childs = childs;

			ProductoProveedorList list = DataPortal.Fetch<ProductoProveedorList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

		#endregion

		#region Data Access

        // called to copy objects data from list
        private void Fetch(IList<ProductoProveedor> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (ProductoProveedor item in lista)
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
				this.AddItem(ProductoProveedorInfo.GetChild(SessionCode, reader, false));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

		// called to retrieve data from db
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
						this.AddItem(ProductoProveedorInfo.GetChild(SessionCode, reader, Childs));

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
        public static string SELECT(QueryConditions conditions) { return ProductoProveedor.SELECT(conditions, false); }
        public static string SELECT(ProductInfo item) { return SELECT(new QueryConditions { Producto = item }); }
        public static string SELECT(IAcreedorInfo item) 
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoAcreedor = new ETipoAcreedor[1] { item.ETipoAcreedor },
				Acreedor = item
			};

			return SELECT(conditions); 
		}

        #endregion
	
	}
}