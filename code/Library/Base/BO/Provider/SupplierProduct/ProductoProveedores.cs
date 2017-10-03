using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class ProductoProveedores : BusinessListBaseEx<ProductoProveedores, ProductoProveedor>
    {
        #region Business Methods
	
		public ProductoProveedor NewItem(IAcreedor proveedor, ProductInfo producto)
		{
			this.NewItem(ProductoProveedor.NewChild(proveedor, producto));
			return this[Count - 1];
		}

        public ProductoProveedor GetItemByProveedor(long oid_proveedor)
        {
            foreach (ProductoProveedor obj in this)
                if (obj.OidAcreedor == oid_proveedor)
                    return obj;
            return null;
        }

        public ProductoProveedor GetItemByProducto(long oid_producto)
        {
            foreach (ProductoProveedor obj in this)
                if (obj.OidProducto == oid_producto)
                    return obj;
            return null;
        }

        #endregion

        #region Common Factory Methods

        private ProductoProveedores()
        {
            MarkAsChild();
        }
        private ProductoProveedores(IList<ProductoProveedor> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private ProductoProveedores(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static ProductoProveedores NewChildList() { return new ProductoProveedores(); }

        public static ProductoProveedores GetChildList(IList<ProductoProveedor> lista) { return new ProductoProveedores(lista); }

		public static ProductoProveedores GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static ProductoProveedores GetChildList(int sessionCode, IDataReader reader, bool childs) { return new ProductoProveedores(sessionCode, reader, childs); }

        public static ProductoProveedores GetChildList(IAcreedor parent, bool childs)
        {
            CriteriaEx criteria = ProductoProveedor.GetCriteria(parent.SessionCode);
            criteria.Query = ProductoProveedores.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<ProductoProveedores>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    ProductoProveedor.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        ProductoProveedor obj = ProductoProveedor.GetChild(SessionCode, reader);
                        this.AddItem(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<ProductoProveedor> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (ProductoProveedor item in lista)
                this.AddItem(ProductoProveedor.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(ProductoProveedor.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Product parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (ProductoProveedor obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (ProductoProveedor obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }
		
        public void Update(IAcreedor parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (ProductoProveedor obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (ProductoProveedor obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }		

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return ProductoProveedor.SELECT(conditions, true); }
        public static string SELECT(Product item) { return SELECT(new QueryConditions { Producto = item.GetInfo() }); }
        public static string SELECT(IAcreedor item) 
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoAcreedor = new ETipoAcreedor[1] { item.ETipoAcreedor },
				Acreedor = item.IGetInfo()
			};
            
			return SELECT(conditions); 
		}

        #endregion
    }
}
