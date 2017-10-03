using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class InputInvoiceLines : BusinessListBaseEx<InputInvoiceLines, InputInvoiceLine>
    {
        #region Business Methods
	
        public InputInvoiceLine NewItem(InputInvoice parent)
        {
            this.NewItem(InputInvoiceLine.NewChild(parent));
            return this[Count - 1];
        }

        public bool ContainsPartida(long oidBatch)
        {
            foreach (InputInvoiceLine obj in this)
                if (obj.OidPartida == oidBatch)
                    return true;

            return false;
        }

        public void Remove(InputDeliveryLineInfo line)
        {
            foreach (InputInvoiceLine item in this)
                if (item.OidConceptoAlbaran == line.Oid)
                {
                    this.Remove(item);
                    break;
                }
        }

        public InputInvoiceLine GetItemByOidPartida(long oidBatch)
        {
            foreach (InputInvoiceLine item in this)
                if (item.OidPartida == oidBatch)
                    return item;

            return null;
        }

		public decimal GetSubTotalByExpediente(long oidExpedient)
		{
			decimal total = 0;

			foreach (InputInvoiceLine item in this)
				if (item.OidExpediente == oidExpedient) total += item.Subtotal;

			return total;
		}		

        #endregion

        #region Factory Methods

        private InputInvoiceLines()
        {
            MarkAsChild();
        }
		private InputInvoiceLines(InputInvoiceLineList lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private InputInvoiceLines(IList<InputInvoiceLine> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private InputInvoiceLines(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }
		
        public static InputInvoiceLines NewChildList() { return new InputInvoiceLines(); }

		public static InputInvoiceLines GetChildList(InputInvoiceLineList lista) { return new InputInvoiceLines(lista); }
        public static InputInvoiceLines GetChildList(IList<InputInvoiceLine> lista) { return new InputInvoiceLines(lista); }
        public static InputInvoiceLines GetChildList(int sessionCode, IDataReader reader, bool childs) { return new InputInvoiceLines(sessionCode, reader, childs); }
		public static InputInvoiceLines GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }

        public static InputInvoiceLines GetChildList(InputInvoice parent, bool childs)
        {
            CriteriaEx criteria = InputInvoiceLine.GetCriteria(parent.SessionCode);

            criteria.Query = SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<InputInvoiceLines>(criteria);
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
                    InputInvoiceLine.DoLOCK(Session());
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        InputInvoiceLine obj = InputInvoiceLine.GetChild(SessionCode, reader);
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
        private void Fetch(IList<InputInvoiceLine> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (InputInvoiceLine item in lista)
                this.AddItem(InputInvoiceLine.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

		private void Fetch(InputInvoiceLineList lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (InputInvoiceLineInfo item in lista)
				this.AddItem(InputInvoiceLine.GetChild(item));

			this.RaiseListChangedEvents = true;
		}

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(InputInvoiceLine.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(InputInvoice parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (InputInvoiceLine obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (InputInvoiceLine obj in this)
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

		public static string SELECT(InputInvoice factura)
		{
			string query;

			QueryConditions conditions = new QueryConditions { FacturaRecibida = factura.GetInfo(false) };
			query = InputInvoiceLine.SELECT(conditions, true);

			return query;
		}

		#endregion
	}
}
