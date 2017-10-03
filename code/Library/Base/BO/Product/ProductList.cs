using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 
using moleQule.CslaEx; 
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class ProductList : ReadOnlyListBaseEx<ProductList, ProductInfo, Product>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ProductList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ProductList(IList<Product> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ProductList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ProductList(IList<ProductInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static ProductList NewList() { return new ProductList(); }

		public static ProductList GetList(bool childs = true)
		{
			CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;
			
	        if (nHManager.Instance.UseDirectSQL)
                criteria.Query = ProductList.SELECT();
            
			ProductList list = DataPortal.Fetch<ProductList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static ProductList GetList(bool childs, bool cache)
		{
			ProductList list;

			if (!Cache.Instance.Contains(typeof(ProductList)))
			{
				list = ProductList.GetList(childs);
				Cache.Instance.Save(typeof(ProductList), list);
			}
			else
				list = Cache.Instance.Get(typeof(ProductList)) as ProductList;

			return list;
		}

		public static ProductList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(ProductList.SELECT(conditions), childs);
		}
	
		public static ProductList GetList(HashOidList oidList, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				OidList = oidList.ToList()
			};

			return GetList(ProductList.SELECT(conditions), childs);
		}
		public static ProductList GetList(HashOidList oidList, bool childs, bool cache)
		{
			ProductList list;

			if (!Cache.Instance.Contains(typeof(ProductList)))
			{
				list = ProductList.GetList(oidList, childs);
				Cache.Instance.Save(typeof(ProductList), list);
			}
			else
				list = Cache.Instance.Get(typeof(ProductList)) as ProductList;

			return list;
		}

        public static ProductList GetListFromList(string list, bool childs)
        {
            if (!Expedient.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
            criteria.Childs = childs;

            criteria.Query = ProductList.SELECT_FROM_LIST(list);

            Product.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<ProductList>(criteria);
        }

        public static ProductList GetListBySupplier(long oid, bool childs)
        {
            CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
            criteria.Childs = childs;

            ProveedorInfo item = Proveedor.New().GetInfo();
            item.Oid = oid;

            criteria.Query = ProductList.SELECT(item);

            ProductList list = DataPortal.Fetch<ProductList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }
		
		public static ProductList GetList(IAcreedorInfo provider, SerieInfo serie, bool childs)
        {
			QueryConditions conditions = new QueryConditions
			{
				Acreedor = provider,
 				TipoAcreedor = new ETipoAcreedor[1] { provider.ETipoAcreedor },
				Serie = serie
			};

			return GetList(ProductList.SELECT(conditions), childs);
        }
		public static ProductList GetList(IAcreedorInfo acreedor, SerieInfo serie, bool childs, bool cache)
		{
			ProductList list;

			if (!Cache.Instance.Contains(typeof(ProductList)))
			{
				list = ProductList.GetList(acreedor, serie, childs);
				Cache.Instance.Save(typeof(ProductList), list);
			}
			else
				list = Cache.Instance.Get(typeof(ProductList)) as ProductList;

			return list;
		}
		public static ProductList GetList(long oidClient, long oidSerie, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
            conditions.Entities.Add(oidClient, (long)ETipoEntidad.Cliente); 
            conditions.Entities.Add(oidSerie, (long)ETipoEntidad.Serie);

			return GetList(ProductList.SELECT(conditions), childs);
		}
		public static ProductList GetList(long oidClient, long oidSerie, bool childs, bool cache)
		{
			ProductList list;

			if (!Cache.Instance.Contains(typeof(ProductList)))
			{
				list = ProductList.GetList(oidClient, oidSerie, childs);
				Cache.Instance.Save(typeof(ProductList), list);
			}
			else
				list = Cache.Instance.Get(typeof(ProductList)) as ProductList;

			return list;
		}

		public static ProductList GetByCustomCodeList(string customCode, bool clustered, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Producto = ProductInfo.New(),
				Orders = new OrderList()
			};
			
			conditions.Producto.CodigoAduanero = customCode;

			conditions.Orders.NewOrder("CodigoAduanero", ListSortDirection.Ascending, typeof(Product));

			if (clustered)
			{
				conditions.Groups = new GroupList();
				conditions.Groups.NewGroup("CodigoAduanero", typeof(Product));			
			}

			return GetList(SELECT(conditions), false);
		}

		public static ProductList GetListByExpediente(long oid, bool childs)
		{
			CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;

			ExpedientInfo item = Expedient.New().GetInfo();
			item.Oid = oid;
			criteria.Query = ProductList.SELECT(item);

			ProductList list = DataPortal.Fetch<ProductList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static ProductList GetListByAlmacen(long oid, bool childs)
		{
			CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;

			StoreInfo item = Almacen.New().GetInfo();
			item.Oid = oid;
			criteria.Query = ProductList.SELECT(item);

			ProductList list = DataPortal.Fetch<ProductList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static ProductList GetListByProveedorByFamilia(long oid, ETipoFamilia familia, bool childs)
        {
            CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;

            ProveedorInfo item = Proveedor.New().GetInfo();
            item.Oid = oid;

            criteria.Query = ProductList.SELECT(item, familia);

            ProductList list = DataPortal.Fetch<ProductList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }

		public static ProductList GetListByTipoFamilia(ETipoFamilia familia, bool childs)
        {
            CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;

            criteria.Query = ProductList.SELECT(familia);

            ProductList list = DataPortal.Fetch<ProductList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }

        public static ProductList GetListBySerie(long oid_serie, bool childs)
        {
            CriteriaEx criteria = Batch.GetCriteria(Product.OpenSession());
            criteria.Childs = childs;

            SerieInfo item = Serie.Serie.New().GetInfo();
            item.Oid = oid_serie;

            criteria.Query = ProductList.SELECT(item);

            ProductList list = DataPortal.Fetch<ProductList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
        public static ProductList GetListBySerie(long oid_serie, bool childs, bool cache)
        {
            ProductList list;

            if (!Cache.Instance.Contains(typeof(ProductList)))
            {
                list = ProductList.GetListBySerie(oid_serie, childs);
                Cache.Instance.Save(typeof(ProductList), list);
            }
            else
                list = Cache.Instance.Get(typeof(ProductList)) as ProductList;

            return list;
        }

        public static ProductList GetElaboradosList(bool childs)
        {
            CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
            criteria.Childs = childs;

            criteria.Query = ProductList.SELECT_ELABORADOS();

            ProductList list = DataPortal.Fetch<ProductList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static ProductList GetKitList(bool isKit, bool childs)
		{
			return GetList(Product.SELECT_KIT(new QueryConditions(), isKit, false), childs);
		}

		private static ProductList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Product.GetCriteria(Product.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			ProductList list = DataPortal.Fetch<ProductList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static ProductList GetList(IList<Product> list) { return new ProductList(list,false); }
        public static ProductList GetList(IList<ProductInfo> list) { return new ProductList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ProductInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<ProductInfo> sortedList = new SortedBindingList<ProductInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<ProductInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ProductInfo> sortedList = new SortedBindingList<ProductInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Product> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Product item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(ProductInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(ProductInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Product.SELECT_COUNT(criteria), criteria.Session);
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
				
		#endregion

        #region SQL

        public static string SELECT() { return Product.SELECT(new QueryConditions(), false); }
        public static string SELECT(Library.Store.QueryConditions conditions) { return Product.SELECT(conditions, false); }
		public static string SELECT(StoreInfo item) { return Product.SELECT(item, false); }
		public static string SELECT(ExpedientInfo item) { return Product.SELECT(item, false); }
        public static string SELECT(ProveedorInfo item) { return Product.SELECT(item, false); }
        public static string SELECT(SerieInfo item) { return Product.SELECT(item, false); }
        public static string SELECT(ETipoFamilia item) { return Product.SELECT(item, false); }
        public static string SELECT(ProveedorInfo item, ETipoFamilia familia) { return Product.SELECT(item, familia, false); }
        public static string SELECT_FROM_LIST(string list)
        {
            string query = SELECT(new QueryConditions()) +
                           " WHERE P.\"OID\" IN (" + list + ")";
            return query;
        }
        public static string SELECT_ELABORADOS() { return Product.SELECT_ELABORADOS(false); }

        #endregion
    }
}

