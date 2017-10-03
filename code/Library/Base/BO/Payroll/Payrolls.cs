using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
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
    public class Payrolls : BusinessListBaseEx<Payrolls, Payroll>
    {
        #region IEntidadRegistro

        public static PayrollBatchs GetChildList(int sessionCode, List<long> oid_list, bool childs)
        {
            return PayrollBatchs.GetChildList(sessionCode, oid_list, childs);
        }

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        public Payroll NewItem(PayrollBatch parent) { return NewItem(parent, null); }
		public Payroll NewItem(PayrollBatch parent, EmployeeInfo employee)
		{
			Payroll item = Payroll.NewChild(parent, employee);
			this.AddItem(item);
            SetNextCode(item);
			return item;
		}

		public override void Remove(long oid)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }
        public new void Remove(Payroll item)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }
		public void Remove(PayrollBatch parent, Payroll item)
		{
			if (item.OidPago != 0) return;
			parent.CalculateTotal();
			base.Remove(item);
		}

		public void SetNextCode(Payroll item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode();
				MaxSerial = item.Serial;
			}
			else
			{
				item.Serial = MaxSerial + 1;
                item.Codigo = item.Serial.ToString("0000") + "/" + item.Fecha.ToString("yy");
                item.Fecha = item.Fecha.AddSeconds(index);
				MaxSerial++;
			}
		}

		public void UpdatePagoValues(Payment pago)
		{
			Payroll item;
			decimal acumulado;

			for (int i = 0; i < Items.Count; i++)
			{
				item = Items[i];

				if (i == 0) acumulado = 0;
				else acumulado = Items[i - 1].Acumulado;

				item.Acumulado = acumulado + item.Pendiente;
				item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
			}
		}

		public static void DeleteItems(PayrollBatch parent)
		{
			QueryConditions conditions = new QueryConditions { RemesaNomina = parent.GetInfo(false) };
			Payrolls list = Payrolls.GetList(conditions, false);
			list.Clear();
			list.Save();
			list.CloseSession();
		}
        
		#endregion

		#region Common Factory Methods

		private Payrolls() { }

		#endregion

		#region Root Factory Methods

		public static Payrolls NewList() { return new Payrolls(); }

		public static Payrolls GetList() { return GetList(new QueryConditions(), true); }
		public static Payrolls GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SELECT(conditions), childs);
		}
		public static Payrolls GetList(string query, bool childs)
		{
			CriteriaEx criteria = Payroll.GetCriteria(Payroll.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			Payroll.BeginTransaction(criteria.Session);

			Payrolls list = DataPortal.Fetch<Payrolls>(criteria);
			return list;
		}

		public static Payrolls GetPendientesList(bool childs)
		{
			return GetPendientesList(null, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static Payrolls GetPendientesList(Payment pago, bool childs)
		{
			return GetPendientesList(pago, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static Payrolls GetPendientesList(Payment pago, DateTime f_ini, DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Payment = (pago != null) ? pago.GetInfo(false) : null,
				FechaIni = f_ini,
				FechaFin = f_fin,
			};

			return GetPendientesList(conditions, childs);
		}
		public static Payrolls GetPendientesList(QueryConditions conditions, bool childs)
		{
			return GetList(Payrolls.SELECT_PENDIENTES(conditions), childs);
		}

		public static Payrolls GetByPagoAndPendientesList(Payment pago, bool childs)
		{
			Payrolls byPago = GetByPagoList(pago, childs);
			Payrolls pendientes = GetPendientesList(childs);

			Payrolls list = Payrolls.NewList();

			foreach (Payroll item in byPago)
				list.AddItem(item);

			foreach (Payroll item in pendientes)
				if (list.GetItem(item.Oid) == null) list.AddItem(item);

			return list;
		}

		public static Payrolls GetByPagoList(Payment pago, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Payment = pago.GetInfo(false) };

			return GetList(SELECT_BY_PAGO(conditions), childs);
		}

		public void Save(Payment parent)
		{
			foreach (Payroll item in this)
			{
				if (item.Asignado == item.Total)
				{
					item.OidPago = parent.Oid;
					item.EEstado = EEstado.Pagado;
				}
				else
				{
					item.OidPago = 0;
					item.EEstado = EEstado.Abierto;
				}
			}

			ApplyEdit();
			Save();
		}
		
		#endregion

		#region Child Factory Methods

		public Payrolls(bool is_child)
        {
			if (is_child) MarkAsChild();
        }
        private Payrolls(IList<Payroll> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Payrolls(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static Payrolls NewChildList() { return new Payrolls(true); }

        public static Payrolls GetChildList(IList<Payroll> lista) { return new Payrolls(lista); }
		public static Payrolls GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static Payrolls GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Payrolls(sessionCode, reader, childs); }

        public static Payrolls GetChildList(Expedient parent, bool childs)
        {
            CriteriaEx criteria = Payroll.GetCriteria(parent.SessionCode);
            criteria.Query = Payrolls.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<Payrolls>(criteria);
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
                    Payroll.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					MaxSerial = 0;

                    while (reader.Read())
                    {
                        Payroll item = Payroll.GetChild(SessionCode, reader);
						this.AddItem(item);
						if (item.Serial > MaxSerial) MaxSerial = item.Serial;
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
			foreach (Payroll obj in DeletedList)
				obj.DeleteSelf(this);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (Payroll obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
						{
							if (this.IndexOf(obj) == 0) obj.GetNewCode();
							else SetNextCode(obj);

							obj.Insert(this);
						}
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

        // called to copy objects data from list
        private void Fetch(IList<Payroll> lista)
        {
            this.RaiseListChangedEvents = false;

			foreach (Payroll item in lista)
			{
				this.AddItem(Payroll.GetChild(item));
			}

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

			MaxSerial = 0;

			while (reader.Read())
			{
				Payroll item = Payroll.GetChild(SessionCode, reader);
				this.AddItem(item);
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
			}

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Expedient parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Payroll obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Payroll obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }
		internal void Update(PayrollBatch parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (Payroll obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (Payroll obj in this)
			{
                if (obj.IsNew)
                {
                    SetNextCode(obj);
                    obj.Insert(parent);
                }
                else
                    obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Payroll.SELECT(conditions, true); }
		public static string SELECT(Expedient source) { return Payroll.SELECT(new QueryConditions { Expedient = source.GetInfo(false) }, true); }
		public static string SELECT(PayrollBatch source) { return Payroll.SELECT(new QueryConditions { RemesaNomina = source.GetInfo(false) }, true); }
		public static string SELECT(Payment source) { return Payroll.SELECT(new QueryConditions { Payment = source.GetInfo(false) }, true); }
		public static string SELECT_BY_PAGO(QueryConditions conditions) { return Payroll.SELECT(conditions, true); }
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return Payroll.SELECT_PENDIENTES(conditions, true); }

        #endregion
    }
}
