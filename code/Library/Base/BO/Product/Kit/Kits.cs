using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class Kits : BusinessListBaseEx<Kits, Kit>
    {
		#region Business Methods
	
		public Kit NewItem(Product kit, ProductInfo component)
		{
			this.NewItem(Kit.NewChild(kit, component));
			Kit item = this[Count - 1];
			return item;
		}

		public Kit GetItemByComponent(long oidComponent)
		{
			return this.FirstOrDefault(item => item.OidProduct == oidComponent);
		}
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private Kits() { }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private Kits(IList<Kit> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private Kits(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Kits NewChildList() 
        { 
            Kits list = new Kits(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static Kits GetChildList(IList<Kit> lista) { return new Kits(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static Kits GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new Kits(sessionCode, reader, childs); }		
		public static Kits GetChildList(Product parent, bool childs)
		{
			CriteriaEx criteria = Kit.GetCriteria(parent.SessionCode);

			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<Kits>(criteria);
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
					Kit.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						Kit item = Kit.GetChild(SessionCode, reader);
						this.AddItem(item);
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

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<Kit> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (Kit item in lista)
				this.AddItem(Kit.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
		
        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen con los elementos a insertar</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Kit.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Product parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;
				
				// update (thus deleting) any deleted child objects
				foreach (Kit obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (Kit obj in this)
				{	
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Kit.SELECT(conditions, true); }
		
		public static string SELECT(Product parent) { return Kit.SELECT(new QueryConditions{ Kit = parent.GetInfo(false) }, true); }
			
		#endregion
    }
}

