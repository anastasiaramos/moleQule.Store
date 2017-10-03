using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class REAExpedients : BusinessListBaseEx<REAExpedients, REAExpedient>, IEntidadRegistroList
	{
		#region IEntidadRegistroList

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (REAExpedient obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region Common Business Methods

		public void SetNextCode(REAExpedient item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode();
			}
			else
			{
				item.Serial = this[index - 1].Serial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			}
		}

		public REAExpedient GetItemByPartida(BatchInfo source)
		{
			foreach (REAExpedient item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;

				if (item.CodigoAduanero == source.CodigoAduanero)
					return item;
			}
			return null;
		}

		public decimal GetTotalAyudas(string codigoAduanero)
		{
			decimal ayudas = 0;

			foreach (REAExpedient item in this)
			{
				if (item.CodigoAduanero == codigoAduanero)
					ayudas += item.AyudaCobrada;
			}

			return ayudas;
		}

		public void RemoveItem(REAExpedient item, Expedient parent)
		{
			if (item.FechaCobro != DateTime.MinValue)
				throw new iQException(Resources.Messages.DELETE_REA_NOT_ALLOWED);

			Remove(item.Oid);
			parent.UpdateAyudas();
		}

		public void SetValues(InputInvoiceInfo invoice)
		{
			if (Items.Count != 1) return;

			foreach(REAExpedient item in this)
				item.CopyFrom(invoice);
		}

		#endregion

		#region Root Business Methods

		public REAExpedient NewItem()
        {
			REAExpedient item = REAExpedient.NewChild();
			SetNextCode(item);
			this.AddItem(item);
			return item;
        }

        #endregion
		
		#region Child Business Methods
		
		public REAExpedient NewItem(Expedient parent, BatchInfo source)
		{
			if (GetItemByPartida(source) != null)
				throw new iQException(String.Format(Resources.Messages.EXPEDIENTE_REA_DUPLICATED, source.CodigoAduanero));

			REAExpedient item = REAExpedient.NewChild(parent, source);
			this.NewItem(item);
			SetNextCode(item);

			parent.UpdateAyudas();

			return item;
		}

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private REAExpedients() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static REAExpedients NewList() 
		{ 	
			if (!REAExpedient.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new REAExpedients(); 
		}

        internal static REAExpedients GetList(bool childs, string query)
        {
            if (!REAExpedient.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = REAExpedient.GetCriteria(REAExpedient.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            REAExpedient.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<REAExpedients>(criteria);
        }
        public static REAExpedients GetList(QueryConditions conditions, bool childs)
        {
            return GetList(childs, REAExpedients.SELECT(conditions));
        }
        public static REAExpedients GetList(bool childs = true) 
		{
			return GetList(childs, REAExpedients.SELECT());
		}

        public static REAExpedients GetByChargeList(long oidCharge, bool childs = true)
        {
            //QueryConditions conditions = new QueryConditions() 
            //{ 
            //    Cobro = ChargeInfo.New(oidCharge, ETipoCobro.REA) 
            //};

            QueryConditions conditions = new QueryConditions(oidCharge, ETipoEntidad.CobroREA); 
            return GetList(childs, REAExpedient.SELECT(conditions, false));
        }
        
        public static REAExpedients GetByChargeAndUnlinkedList(long oidCharge, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions(oidCharge, ETipoEntidad.CobroREA); 
            return GetList(childs, REAExpedient.SELECT_BY_CHARGE_AND_UNLINKED(conditions, false));
        }

        public static REAExpedients GetUnlinkedList(bool childs = true)
        {
            return GetList(childs, REAExpedient.SELECT_UNLINKED(new QueryConditions(), false));
        }

        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private REAExpedients(IList<REAExpedient> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
		private REAExpedients(int sessionCode, IDataReader reader, bool childs)
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
        public static REAExpedients NewChildList() 
        { 
            REAExpedients list = new REAExpedients(); 
            list.MarkAsChild(); 
            return list; 
        }

		public static REAExpedients GetChildList(Expedient parent, bool childs)
		{
			CriteriaEx criteria = REAExpedient.GetCriteria(parent.SessionCode);
			criteria.Query = REAExpedients.SELECT(parent);
			criteria.Childs = childs;

			REAExpedients list = DataPortal.Fetch<REAExpedients>(criteria);

			parent.UpdateTotalCostesCompensables();

			return list;
		}
		public static REAExpedients GetChildList(IList<REAExpedient> lista) { return new REAExpedients(lista); }	
        public static REAExpedients GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static REAExpedients GetChildList(int sessionCode, IDataReader reader, bool childs) { return new REAExpedients(sessionCode, reader, childs); }
		public static REAExpedients GetChildList(int sessionCode, List<long> oidList, bool childs)
		{
			return GetChildList(sessionCode, SELECT(new QueryConditions { OidList = oidList }), childs);
		}

		internal static REAExpedients GetChildList(int sessionCode, string query, bool childs)
		{
			if (!REAExpedient.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = REAExpedients.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<REAExpedients>(criteria);
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
                    REAExpedient.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
						this.AddItem(REAExpedient.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
            foreach (REAExpedient obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (REAExpedient obj in this)
				{
					if (!this.Contains(obj))
					{
						if (this.IndexOf(obj) == 0) obj.GetNewCode();
						else SetNextCode(obj);

						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
				}

                if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SharedTransaction) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<REAExpedient> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (REAExpedient item in lista)
				this.AddItem(REAExpedient.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
				this.AddItem(REAExpedient.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Expedient parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (REAExpedient obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (REAExpedient obj in this)
			{	
				if (!this.Contains(obj))
				{
					if (obj.IsNew)
					{
						SetNextCode(obj);
						obj.Insert(parent);
					}
					else
						obj.Update(parent);
				}
			}

			this.RaiseListChangedEvents = true;
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return REAExpedient.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return REAExpedient.SELECT(conditions, true); }
		public static string SELECT(Expedient parent) { return SELECT(new QueryConditions{ Expedient = parent.GetInfo(false) }); }
				
		#endregion
    }
}