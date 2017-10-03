using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Root Collection of Businees Objects With Child Collection
    /// Editable Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
	public class Payments : BusinessListBaseEx<Payments, Payment>, IEntidadRegistroList
	{
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (Payment obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region Business Methods

        public void SetMaxSerial()
        {
            foreach (Payment item in this)
            {
                if (!MaxSerials.ContainsKey(item.Fecha.Year)) MaxSerials.Add(item.Fecha.Year, 0);
                if (item.Serial > MaxSerials[item.Fecha.Year]) MaxSerials[item.Fecha.Year] = item.Serial;
            }
        }
        public void SetNextCode(Payment item)
        {
            if (!MaxSerials.ContainsKey(item.Fecha.Year)) MaxSerials.Add(item.Fecha.Year, 0);

            int index = this.IndexOf(item);

            if (index == 0)
            {
                item.GetNewCode();
                MaxSerials[item.Fecha.Year] = item.Serial;
            }
            else
            {
                item.Serial = MaxSerials[item.Fecha.Year] + 1;
                item.Codigo = item.Serial.ToString(Resources.Defaults.PAGO_CODE_FORMAT);
                MaxSerials[item.Fecha.Year]++;
            }
        }

        /// <summary>
        /// Crea y añade un elemento a la lista
        /// </summary>
        /// <returns>Nuevo item</returns>
        public Payment NewItem(IAcreedor parent, ETipoPago tipo)
        {
            this.NewItem(Payment.NewChild(parent, tipo));
            return this[Count - 1];
        }
        public Payment NewItem(PaymentInfo pago, ETipoPago tipo)
        {
            this.NewItem(Payment.NewChild(pago, tipo));
            return this[Count - 1];
        }
		public Payment NewItem(PaymentInfo pago)
		{
			this.NewItem(Payment.NewChild(pago));
			return this[Count - 1];
		}
        public Payment NewItem(LoanInfo loan) { return NewItem(loan, DateTime.Now); } 
        public Payment NewItem(LoanInfo loan, DateTime date)
        {
            Payment obj = Payment.NewChild(loan, date);
            this.NewItem(obj);
            SetNextCode(obj);
            return obj;
        }

        public Payment GetItemByDueDate(DateTime dueDate)
        {
            return Items.FirstOrDefault(x => x.Vencimiento == dueDate);
        }

		public Payment GetItemByTarjetaCredito(long oidCreditCard, DateTime dueDate)
		{
            return Items.FirstOrDefault(x => x.OidTarjetaCredito == oidCreditCard && x.Vencimiento == dueDate);
		}

        public override void MarkAsNew()
        {
            foreach (Payment item in Items)
            {
                item.MarkItemNew();
                item.Operations.MarkAsNew();
            }
        }

        public decimal Charges()
        {
            return this.Sum(x => x.GastosBancarios);
        }
        public decimal GetTotal()
        {
            return Items.Sum(x => x.Operations.GetTotal());
        }
        public decimal GetTotalAmount()
        {
            return Items.Sum(x => x.Importe);
        }

		public void ChangeState(EEstado status, Payment item, IAcreedor parent)
		{
			Payment obj = GetItem(item.Oid);
			if (obj != null)
			{
				obj.ChangeEstado(status);
				//parent.UpdateCredito();
			}
		}

        public long GetLastBankLineSerial(Payment parent = null)
        {
            long last = -1;

            if (parent != null)
                last = parent.LastBankLineSerial > last ? parent.LastBankLineSerial : last;

            foreach (Payment item in this)
            {
                if (item.Pagos != null && item.Pagos.Count > 0)
                    last = item.Pagos.GetLastBankLineSerial() > last ? item.Pagos.GetLastBankLineSerial() : last;
                else
                    last = item.LastBankLineSerial > last ? item.LastBankLineSerial : last;
            }

            return last;
        }

        #endregion

		#region Common Factory Methods

		private Payments() {}
		
		#endregion

        #region Child Factory Methods

        private Payments(IList<Payment> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Payments(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static Payments NewChildList() 
		{ 
			Payments list = new Payments(); 
			list.MarkAsChild(); 
			return list; 
		}

        public static Payments GetChildList(IList<Payment> lista) { return new Payments(lista); }
        public static Payments GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new Payments(sessionCode, reader, childs); }		
		public static Payments GetChildList(int sessionCode, List<long> oidList, bool childs)
		{
			return GetChildList(sessionCode, Payments.SELECT(new QueryConditions { OidList = oidList }), childs);
		}
        public static Payments GetChildList(IAcreedor parent, bool childs)
        {
            CriteriaEx criteria = Payment.GetCriteria(parent.SessionCode);

            criteria.Query = SELECT(parent);
            criteria.Childs = childs;

            Payments list = DataPortal.Fetch<Payments>(criteria);

            return list;
        }
        public static Payments GetChildList(Loan parent, bool childs)
        {
            CriteriaEx criteria = Payment.GetCriteria(parent.SessionCode);

            QueryConditions conditions = new QueryConditions
            {
                Prestamo = parent.GetInfo(false),
                PaymentType = ETipoPago.Prestamo
            };

            criteria.Query = Payment.SELECT_BY_PRESTAMO(conditions);
            criteria.Childs = childs;

            Payments list = DataPortal.Fetch<Payments>(criteria);

            return list;
        }
        internal static Payments GetChildList(int sessionCode, string query, bool childs)
        {
            if (!Payment.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Payment.GetCriteria(sessionCode);
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            return DataPortal.Fetch<Payments>(criteria);
        }

        #endregion

		#region Root Factory Methods

		/// <summary>
		/// Crea una nueva lista vacía
		/// </summary>
		/// <returns>Lista vacía</returns>
		public static Payments NewList()
		{
			if (!Payment.CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new Payments();
		}

        internal static Payments GetList(string query, bool childs)
        {
            if (!Payment.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Payment.GetCriteria(Payment.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            Payment.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Payments>(criteria);
        }
		
        public static Payments GetList(bool childs = true)
		{
			return GetList(Payments.SELECT(), childs);
		}
		public static Payments GetList(QueryConditions conditions, bool childs)
		{
			return GetList(Payments.SELECT(conditions), childs);
		}
        public static Payments GetList(ETipoPago paymentType, bool childs)
        {
            return GetList(paymentType, DateTime.MinValue, DateTime.MaxValue, childs);
        }
        public static Payments GetList(ETipoPago paymentType, int year, bool childs)
        {
            return GetList(paymentType, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
        }
        public static Payments GetList(ETipoPago paymentType, DateTime from, DateTime till, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                PaymentType = paymentType,
                FechaIni = from,
                FechaFin = till
            };

            return GetList(conditions, childs);
        }

        public static Payments GetByCreditCardStatement(long oidStatement, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                CreditCardStatement = (oidStatement) != 0 ? CreditCardStatementInfo.New(oidStatement) : null,
                MedioPago = EMedioPago.Tarjeta
            };

            return GetList(Payment.SELECT_BY_CREDIT_CARD_STATEMENT(conditions, true), childs);
        }
        public static Payments GetCreditCardStatementsList(long oidCreditCard, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                TarjetaCredito = (oidCreditCard) != 0 ? CreditCardInfo.New(oidCreditCard) : null,
                PaymentType = ETipoPago.ExtractoTarjeta
            };

            return GetList(Payment.SELECT(conditions, true), childs);
        }

        public static Payments GetByLoanList(LoanInfo item, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Prestamo = item,
                PaymentType = ETipoPago.Prestamo
            };

            return GetList(Payment.SELECT_BY_PRESTAMO(conditions), childs);
        }

		public static Payments GetListInNomina(Library.Store.QueryConditions conditions, bool childs)
		{
			return GetList(Payment.SELECT_IN_NOMINA(conditions, true), childs);
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

		private void Fetch(CriteriaEx criteria)
		{
			try
			{
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
                MaxSerial = 0;

				if (nHMng.UseDirectSQL)
				{
					Payment.DoLOCK(Session());

					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        Payment item = Payment.GetChild(SessionCode, reader, Childs);
                        this.AddItem(item);
                        if (item.Serial > MaxSerial) MaxSerial = item.Serial;
                    }
				}
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
			foreach (Payment obj in DeletedList)
				obj.DeleteSelf(this);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (Payment obj in this)
				{
                    if (obj.IsNew)
                    {
                        obj.LastBankLineSerial = GetLastBankLineSerial();
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
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
        private void Fetch(IList<Payment> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Payment item in lista)
                this.AddItem(Payment.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list and to retrieve child data from db
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;
            
            MaxSerial = 0;

            while (reader.Read())
            {
                Payment item = Payment.GetChild(SessionCode, reader, Childs);
                this.AddItem(item);
                if (item.Serial > MaxSerial) MaxSerial = item.Serial;
            }

            this.RaiseListChangedEvents = true;
        }
        
        public void Update(IAcreedor parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Payment obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            // no se usa un foreach porque puede variar la cantidad de elementos
            // si se ha cambiado alguno de fecha ya que se crea uno nuevo y se anula el anterior
            for (int i = 0; i < Items.Count; i++)
            {
                Payment obj = Items[i];

                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }
        public void Update(IAcreedor parent, Payment item)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Payment obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            // no se usa un foreach porque puede variar la cantidad de elementos
            // si se ha cambiado alguno de fecha ya que se crea uno nuevo y se anula el anterior
            for (int i = 0; i < Items.Count; i++)
            {
                Payment obj = Items[i];

                if (obj.Oid != item.Oid) continue;

                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }
        public void Update(Loan parent)
        {
            this.RaiseListChangedEvents = false;

			SessionCode = parent.SessionCode;

            // update (thus deleting) any deleted child objects
            foreach (Payment obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            // no se usa un foreach porque puede variar la cantidad de elementos
            // si se ha cambiado alguno de fecha ya que se crea uno nuevo y se anula el anterior
            for (int i = 0; i < Items.Count; i++)
            {
                Payment obj = Items[i];

                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }
        public void Update(Payment parent)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = parent.SessionCode;

            // update (thus deleting) any deleted child objects
            foreach (Payment obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            // no se usa un foreach porque puede variar la cantidad de elementos
            // si se ha cambiado alguno de fecha ya que se crea uno nuevo y se anula el anterior
            for (int i = 0; i < Items.Count; i++)
            {
                Payment obj = Items[i];

                if (obj.IsNew)
                {
                    obj.LastBankLineSerial = GetLastBankLineSerial(parent);
                    obj.Insert(parent);
                    parent.LastBankLineSerial = obj.LastBankLineSerial;
                }
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Payment.SELECT(conditions, true); }
        public static string SELECT(IAcreedor source) { return Payment.SELECT(source.IGetInfo(), true); }
        public static string SELECT(Loan source) { return Payment.SELECT(source.GetInfo(), true); }

        #endregion
    }
}