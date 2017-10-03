using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class PayrollBatchs : BusinessListBaseEx<PayrollBatchs, PayrollBatch>, IEntidadRegistroList
    {
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (PayrollBatch obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion
		
		#region Root Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public PayrollBatch NewItem()
        {
            this.AddItem(PayrollBatch.NewChild());
            return this[Count - 1];
        }

        #endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private PayrollBatchs() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static PayrollBatchs NewList() 
		{ 	
			if (!PayrollBatch.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PayrollBatchs(); 
		}

        public static PayrollBatchs GetList() { return GetList(true); }		
		public static PayrollBatchs GetList(bool childs) 
		{
			return GetList(childs, PayrollBatchs.SELECT());
		}
		public static PayrollBatchs GetList(QueryConditions conditions, bool childs)
		{
			return GetList(childs, PayrollBatchs.SELECT(conditions));
		}

		internal static PayrollBatchs GetList(bool childs, string query)
		{
            if (!PayrollBatch.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = PayrollBatch.GetCriteria(PayrollBatch.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			PayrollBatch.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<PayrollBatchs>(criteria);
		}
		
        #endregion
		
		#region Child Factory Methods

        private PayrollBatchs(IList<PayrollBatch> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private PayrollBatchs(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static PayrollBatchs NewChildList() 
		{
			PayrollBatchs list = new PayrollBatchs(); 
			list.MarkAsChild(); 
			return list; 
		}

		public static PayrollBatchs GetChildList(IList<PayrollBatch> lista) { return new PayrollBatchs(lista); }
		public static PayrollBatchs GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static PayrollBatchs GetChildList(int sessionCode, IDataReader reader, bool childs) { return new PayrollBatchs(sessionCode, reader, childs); }

		public static PayrollBatchs GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, PayrollBatchs.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		internal static PayrollBatchs GetChildList(int sessionCode, string query, bool childs)
		{
			if (!PayrollBatch.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = PayrollBatch.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<PayrollBatchs>(criteria);
		}

        #endregion

		#region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    PayrollBatch.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(PayrollBatch.GetChild(SessionCode, reader, Childs));
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

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (PayrollBatch obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (PayrollBatch obj in this)
				{
					if (!this.Contains(obj))
					{
                        if (obj.IsNew)                       
                            obj.Insert(this);
                        else
                            obj.Update(this);
					}
				}

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
		#region Child Data Access

		// called to copy objects data from list and to retrieve child data from db
		private void Fetch(IList<PayrollBatch> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (PayrollBatch item in lista)
				this.AddItem(PayrollBatch.GetChild(item));

			this.RaiseListChangedEvents = true;
		}

		// called to copy objects data from list and to retrieve child data from db
		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			while (reader.Read())
				this.AddItem(PayrollBatch.GetChild(SessionCode, reader, Childs));

			this.RaiseListChangedEvents = true;
		}

		public void Update(IAcreedor parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (PayrollBatch obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (PayrollBatch obj in this)
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

        public static string SELECT() { return PayrollBatch.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return PayrollBatch.SELECT(conditions, true); }
		public static string SELECT(PayrollBatch parent) { return PayrollBatch.SELECT(new QueryConditions{ RemesaNomina = parent.GetInfo(false) }, true); }
				
		#endregion
    }
}

