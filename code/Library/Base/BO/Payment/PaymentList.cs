using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Read Only Root Collection of Business Objects With Child Collection
	/// Read Only Child Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
    public class PaymentList : ReadOnlyListBaseEx<PaymentList, PaymentInfo, Payment>
    {
        #region Business Methods

        public PaymentInfo GetItemByDueDate(DateTime dueDate)
        {
            return Items.FirstOrDefault(x => x.Vencimiento == dueDate);
        }

        public decimal Charges()
        {
            return this.Sum(x => x.GastosBancarios);
        }

        public decimal TotalPagado()
        {
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.Importe);
        }

		public decimal Total()
		{
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.Importe + x.GastosBancarios);
		}

        public void UpdatePagoValues(Payment pago)
        {
            PaymentInfo item;
            decimal acumulado;

            for (int i = 0; i < Items.Count; i++)
            {
                item = Items[i];

                /*if (item.OidPago != pago.Oid)
                    item.Asignado = 0;*/

                if (i == 0) acumulado = 0;
                else acumulado = Items[i - 1].Acumulado;

                item.Acumulado = acumulado + item.Pendiente;
                item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
            }
        }

        #endregion

        #region Common Factory Methods

        private PaymentList() { }
		private PaymentList(IList<Payment> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
        private PaymentList(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            Fetch(reader);
        }
        private PaymentList(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		#endregion

        #region Root Factory Methods

		public static PaymentList NewList() { return new PaymentList(); }

        public static PaymentList GetList(Library.Store.QueryConditions conditions, bool childs)
        {
            return GetList(PaymentList.SELECT(conditions), childs);
        }
		public static PaymentList GetList(bool childs)
		{
			return GetList(DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static PaymentList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static PaymentList GetList(DateTime from, DateTime till, bool childs)
		{
			return GetList(ETipoPago.Todos, from, till, childs);
		}

		public static PaymentList GetList(ETipoPago tipo, bool childs)
		{
			return GetList(tipo, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static PaymentList GetList(ETipoPago tipo, int year, bool childs)
		{
			return GetList(tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static PaymentList GetList(ETipoPago tipo, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				PaymentType = tipo,
				FechaIni = from,
				FechaFin = till
			};

			return GetList(conditions, childs);
		}

		public static PaymentList GetListByAgente(long oid, ETipoAcreedor tipo, bool childs)
		{
			return GetList(PaymentList.SELECT(ProviderBaseInfo.New(oid, tipo)), childs);
		}
		public static PaymentList GetList(ETipoAcreedor providerType, CriteriaEx criteria, bool childs)
		{
			return GetList(providerType, 0, criteria, childs);
		}
		public static PaymentList GetList(ETipoAcreedor providerType, long oidProvider, CriteriaEx criteria, bool childs)
		{
			return GetList(providerType, oidProvider, DateTime.MinValue, DateTime.MaxValue, EStepGraph.None, criteria, childs);
		}
		public static PaymentList GetList(ETipoAcreedor providerType,
											long oidProvider,
											DateTime from,
											DateTime till,
											EStepGraph step,
											CriteriaEx criteria,
											bool childs)
		{
			QueryConditions conditions = new QueryConditions()
			{
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null,
				TipoAcreedor = new ETipoAcreedor[1] { providerType },
				Acreedor = (oidProvider != 0) ? ProviderBaseInfo.New(oidProvider, providerType) : null,
				PaymentType = providerType == ETipoAcreedor.Empleado ? ETipoPago.Nomina : ETipoPago.Factura,
				FechaIni = from,
				FechaFin = till,
				Step = step,
			};

			string query = SELECT(conditions);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}

        public static PaymentList GetByCreditCardStatement(long oidStatement, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                CreditCardStatement = (oidStatement) != 0 ? CreditCardStatementInfo.New(oidStatement) : null,
                MedioPago = EMedioPago.Tarjeta
            };

            return GetList(Payment.SELECT_BY_CREDIT_CARD_STATEMENT(conditions, false), childs);
        }

		public static PaymentList GetListByVencimiento(DateTime f_vto_ini, DateTime f_vto_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaAuxIni = f_vto_ini,
				FechaAuxFin = f_vto_fin
			};

			return GetListByVencimiento(conditions, childs);
		}
		public static PaymentList GetListByVencimiento(QueryConditions conditions, bool childs)
		{
			return GetList(Payment.SELECT_VENCIMIENTO(conditions), childs);
		}

		public static PaymentList GetListByVencimientoPrestamo(DateTime f_vto_ini, DateTime f_vto_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaAuxIni = f_vto_ini,
				FechaAuxFin = f_vto_fin
			};

			return GetListByVencimientoPrestamo(conditions, childs);
		}
		public static PaymentList GetListByVencimientoPrestamo(QueryConditions conditions, bool childs)
		{
			conditions.MedioPago = EMedioPago.ComercioExterior;

			return GetList(Payment.SELECT_VENCIMIENTO_PRESTAMO(conditions), childs);
		}

		public static PaymentList GetListByVencimientoTarjeta(DateTime f_vto_ini, DateTime f_vto_fin, CreditCardInfo tarjeta, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaAuxIni = f_vto_ini,
				FechaAuxFin = f_vto_fin,
				TarjetaCredito = tarjeta
			};

			return GetListByVencimientoTarjeta(conditions, childs);
		}

        public static PaymentList GetListByVencimientoTarjeta(PaymentInfo pago, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Payment = pago,
                FechaAuxIni = pago.Vencimiento,
                FechaAuxFin = pago.Vencimiento,
                TarjetaCredito = CreditCardInfo.New(pago.OidTarjetaCredito),
                MedioPago = EMedioPago.Tarjeta
            };

            return GetList(Payment.SELECT_VENCIMIENTO_TARJETA_BY_PAGO(conditions), childs);
        }
		public static PaymentList GetListByVencimientoTarjeta(QueryConditions conditions, bool childs)
		{
			conditions.MedioPago = EMedioPago.Tarjeta;

			return GetList(Payment.SELECT_VENCIMIENTO_TARJETA(conditions), childs);
		}

		public static PaymentList GetListByVencimientoSinApunte(QueryConditions conditions, bool childs)
		{
			return GetList(Payment.SELECT_VENCIMIENTO_SIN_APUNTE(conditions), childs);
		}

        /*public static PaymentList GetListByMovimiento(BankLineInfo mov, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				BankLine = mov,
				MedioPago = Common.EMedioPago.Tarjeta
			};

			return GetList(Pago.SELECT_EXTRACTO(conditions), childs);
		}*/

        public static PaymentList GetOrderedByFechaList(Library.Store.QueryConditions conditions, bool childs)
        {
            conditions.OrderFields = new List<string>();

            conditions.OrderFields.Add("FECHA_ORDENACION");
            conditions.OrderFields.Add("CODIGO");

            return GetList(PaymentList.SELECT(conditions), childs);
        }

		public static PaymentList GetListInNomina(Library.Store.QueryConditions conditions, bool childs)
		{
			return GetList(PaymentList.SELECT_IN_NOMINA(conditions), childs);
		}

        public static PaymentList GetListByPrestamo(LoanInfo item, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                Prestamo = item,
                PaymentType = ETipoPago.Prestamo
            };

            return GetList(Payment.SELECT_BY_PRESTAMO(conditions), childs);
        }

		private static PaymentList GetList(string query, bool childs)
		{
            CriteriaEx criteria = null;

            try
            {
                criteria = Payment.GetCriteria(Payment.OpenSession());
                criteria.Childs = childs;

                criteria.Query = query;
                PaymentList list = DataPortal.Fetch<PaymentList>(criteria);
                return list;
            }
            finally
            {
                if (criteria != null) CloseSession(criteria.SessionCode);
            }			
		}

        public static PaymentList GetList(IList<Payment> list, bool get_childs)
        {
            PaymentList flist = new PaymentList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Payment item in list)
                    flist.AddItem(item.GetInfo(get_childs));

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static PaymentList GetList(IList<PaymentInfo> list)
        {
            PaymentList flist = new PaymentList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (PaymentInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

		public void Merge(PaymentList list)
		{
			IsReadOnly = false;

			foreach (PaymentInfo item in list)
				AddItem(item);

			IsReadOnly = true;
		}

        #endregion
		
		#region Child Factory Methods

		public static PaymentList GetChildList(IList<PaymentInfo> list)
		{
			PaymentList flist = new PaymentList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (PaymentInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}
        public static PaymentList GetChildList(IList<Payment> list) { return PaymentList.GetChildList(list, true); }
		public static PaymentList GetChildList(IList<Payment> list, bool childs) { return new PaymentList(list, childs); }

		public static PaymentList GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static PaymentList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new PaymentList(sessionCode, reader, childs); }

        #endregion

        #region Data Access

        // called to retrieve data from database
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
                        this.AddItem(PaymentInfo.GetChild(SessionCode, reader, Childs));

                    IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Payment.SELECT_COUNT(criteria), criteria.Session);
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

		private void Fetch(IList<Payment> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;

			foreach (Payment item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
					this.AddItem(PaymentInfo.GetChild(SessionCode, reader, Childs));

                IsReadOnly = true;
       
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

		public static string SELECT() { return Payment.SELECT(new QueryConditions(), false); }
		public static string SELECT(Library.Store.QueryConditions conditions) { return Payment.SELECT(conditions, false); }
		public static string SELECT_FACTURAS(Library.Store.QueryConditions conditions) { return Payment.SELECT_FACTURAS(conditions); }
		public static string SELECT_GASTOS(Library.Store.QueryConditions conditions) { return Payment.SELECT_GASTOS(conditions); }
        public static string SELECT(IAcreedorInfo source) { return Payment.SELECT(source, false); }
        public static string SELECT(LoanInfo source) { return Payment.SELECT(source, false); }
        public static string SELECT_IN_NOMINA(Library.Store.QueryConditions conditions) { return Payment.SELECT_IN_NOMINA(conditions, false); }

        #endregion
    }
}



