using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
	public class InputInvoices : BusinessListBaseEx<InputInvoices, InputInvoice>, IEntidadRegistroList
	{
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (InputInvoice obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region Properties

		protected new Hashtable MaxSerial = new Hashtable();

		#endregion

        #region Business Methods

        /// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public InputInvoice NewItem()
        {
            this.AddItem(InputInvoice.NewChild());
            return this[Count - 1];
        }

		public InputInvoice NewItem(InputDeliveryInfo albaran)
		{
			InputInvoice item = NewItem();

			ProviderBaseInfo acreedor = ProviderBaseInfo.Get(albaran.OidAcreedor, albaran.ETipoAcreedor, true);

			item.CopyFrom(acreedor);
			item.CopyFrom(albaran);
			item.Insert(albaran);
			item.Fecha = albaran.Fecha;
			item.NFactura = "SIN NUMERO";

			SetNextCode(item);

			return item;
		}

		public void NewItems(List<InputDeliveryInfo> albaranes)
		{
			foreach (InputDeliveryInfo item in albaranes)
			{
				if (item.EEstado != EEstado.Abierto) continue;
				if (item.Contado) continue;

				InputInvoice newItem = NewItem(item);
				item.EEstado = EEstado.Billed;
				item.NumeroFactura = newItem.Codigo;
			}
		}

		public void SetNextCode(InputInvoice item)
		{
			int index = this.IndexOf(item);

            if (index == 0)
			{
				item.GetNewCode();

				if (MaxSerial[1] == null) MaxSerial.Add(1, item.Serial);
					MaxSerial[1] = item.Serial;
			}
			else
			{
				item.Serial = (long)MaxSerial[1] + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
				item.Fecha = item.Fecha.AddSeconds(item.Serial - this[0].Serial);
				MaxSerial[1] = (long)MaxSerial[1] + 1;
			}
		}

        #endregion

		#region Child Factory Methods

		public static InputInvoices GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, InputInvoiceSQL.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}

		internal static InputInvoices GetChildList(int sessionCode, string query, bool childs)
		{
			if (!InputInvoice.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = InputInvoice.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<InputInvoices>(criteria);
		}

		#endregion

        #region Root Factory Methods

        private InputInvoices() { }

		public static InputInvoices NewList()
		{
			InputInvoices list = new InputInvoices();
			list.SessionCode = InputInvoices.OpenSession();
			BeginTransaction(list.SessionCode);
			return list;
		}

		public static InputInvoices GetList(bool childs = true)
		{
            return GetList(childs, InputInvoiceSQL.SELECT());
		}
		public static InputInvoices GetList(Library.Store.QueryConditions conditions, bool childs)
		{
            return GetList(childs, InputInvoiceSQL.SELECT(conditions));
		}
        public static InputInvoices GetList(Library.Store.QueryConditions conditions, bool childs, int sessionCode)
        {
            return GetList(childs, InputInvoiceSQL.SELECT(conditions), sessionCode);
        }
		
		internal static InputInvoices GetList(bool childs, string query)
		{
			if (!InputInvoice.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			InputInvoice.BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<InputInvoices>(criteria);
		}

        internal static InputInvoices GetList(bool childs, string query, int sessionCode)
        {
            if (!InputInvoice.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = InputInvoice.GetCriteria(sessionCode);
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            return DataPortal.Fetch<InputInvoices>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    InputInvoice.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						InputInvoice item = InputInvoice.GetChild(SessionCode, reader, _childs);
						if (this.GetItem(item.Oid) == null) this.AddItem(item);
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;
            
            // update (thus deleting) any deleted child objects
            foreach (InputInvoice obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (InputInvoice obj in this)
                {
                    if (obj.IsNew)
                    {
                        SetNextCode(obj);
                        obj.Insert(this);
                    }
                    else
                        obj.Update(this);
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
    }
}
