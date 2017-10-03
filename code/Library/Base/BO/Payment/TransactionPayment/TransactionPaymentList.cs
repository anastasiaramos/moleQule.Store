using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
    /// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class TransactionPaymentList : ReadOnlyListBaseEx<TransactionPaymentList, TransactionPaymentInfo>
	{	
		#region Business Methods

        public TransactionPaymentInfo GetItemByExpediente(long oid_expediente)
        {
            foreach (TransactionPaymentInfo item in this)
                if (item.OidExpediente == oid_expediente)
                    return item;
            return null;
        }

        public TransactionPaymentInfo GetItemByFactura(long oid_factura)
        {
            foreach (TransactionPaymentInfo item in this)
                if (item.OidOperation == oid_factura)
                    return item;
            return null;
        }

        public decimal GetTotal()
        {
            decimal suma = 0.0m;

            foreach (TransactionPaymentInfo c in this)
            {
                suma += c.Cantidad;
            }
            return suma;
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private TransactionPaymentList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private TransactionPaymentList(IList<TransactionPayment> list, bool retrieve_childs)
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
		private TransactionPaymentList(IDataReader reader, bool retrieve_childs)
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
		private TransactionPaymentList(IList<TransactionPaymentInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion

        #region Root Factory Methods

        public static TransactionPaymentList GetListByFactura(long oid_factura)
        {
            CriteriaEx criteria = TransactionPayment.GetCriteria(TransactionPayment.OpenSession());
            criteria.Childs = false;

            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { FacturaRecibida = InputInvoice.New().GetInfo() };
            conditions.FacturaRecibida.Oid = oid_factura;
            criteria.Query = TransactionPaymentList.SELECT(conditions);

            TransactionPaymentList list = DataPortal.Fetch<TransactionPaymentList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static TransactionPaymentList GetList() { return TransactionPaymentList.GetList(true); }
		public static TransactionPaymentList GetList(bool childs) { return GetList(new QueryConditions(), childs); }
		public static TransactionPaymentList GetList(ETipoPago tipo, bool childs) { return GetList(new QueryConditions { PaymentType = tipo }, childs); }
		public static TransactionPaymentList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = TransactionPayment.GetCriteria(TransactionPayment.OpenSession());
			criteria.Childs = childs;

			criteria.Query = TransactionPaymentList.SELECT(conditions);

			TransactionPaymentList list = DataPortal.Fetch<TransactionPaymentList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        #endregion

        #region Child Factory Methods

        /// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static TransactionPaymentList GetChildList(IList<TransactionPayment> list) { return new TransactionPaymentList(list, false); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		public static TransactionPaymentList GetChildList(IList<TransactionPayment> list, bool retrieve_childs) { return new TransactionPaymentList(list, retrieve_childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static TransactionPaymentList GetChildList(IDataReader reader) { return new TransactionPaymentList(reader, false); } 
		
		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        public static TransactionPaymentList GetChildList(IDataReader reader, bool retrieve_childs) { return new TransactionPaymentList(reader, retrieve_childs); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static TransactionPaymentList GetChildList(IList<TransactionPaymentInfo> list) { return new TransactionPaymentList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<TransactionPayment> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (TransactionPayment item in lista)
				if (this.GetItem(item.Oid) == null)	this.AddItem(item.GetInfo(Childs));

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
			{
				TransactionPaymentInfo item = TransactionPaymentInfo.GetChild(SessionCode, reader, Childs);
				if (this.GetItem(item.Oid) == null) this.AddItem(item);
			}

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

        #region Root Data Access

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
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

					while (reader.Read())
					{
						TransactionPaymentInfo item = TransactionPaymentInfo.GetChild(SessionCode, reader, Childs);
						if (this.GetItem(item.Oid) == null) this.AddItem(item);
					}
 
                    IsReadOnly = true;
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

        public static string SELECT(Library.Store.QueryConditions conditions) { return TransactionPayment.SELECT(conditions, false); }
        public static string SELECT(PaymentInfo item) { return SELECT(new Library.Store.QueryConditions { Payment = item, PaymentType = item.ETipoPago }); }
        public static string SELECT(InputInvoiceInfo item) { return SELECT(new Library.Store.QueryConditions { FacturaRecibida = item }); }
           
        #endregion
	}
}