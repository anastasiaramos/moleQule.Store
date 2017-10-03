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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class TransactionPayments : BusinessListBaseEx<TransactionPayments, TransactionPayment>
    {		
		#region Business Methods

		public TransactionPayment NewItem(Payment parent, ITransactionPayment iPagoFactura, ETipoPago tipo)
		{
			this.NewItem(TransactionPayment.NewChild(parent, iPagoFactura, tipo));
			return this[Count - 1];            
		}

        public decimal GetTotal()
        {
            decimal suma = 0.0m;

            foreach (TransactionPayment c in this)
                suma += c.Cantidad;

            return suma;
        }

        public TransactionPayment GetItemByFactura(long oid_factura)
        {
            foreach (TransactionPayment item in this)
                if (item.OidOperation == oid_factura)
                    return item;
            return null;
        }

		public TransactionPayment GetItemByITransaction(ITransactionPayment source, ETipoPago tipo)
		{
			foreach (TransactionPayment item in this)
                if ((item.OidOperation == source.Oid) && (item.ETipoPago == tipo))
					return item;
			return null;
		}

        public TransactionPayment GetItemByAgenteExpediente(IAcreedor agente, long oid_expediente)
        {
            foreach (TransactionPayment item in this)
            {
                if ((item.OidExpediente == oid_expediente) && (item.ETipoAcreedor == agente.ETipoAcreedor))
                    return item;
            }

            return null;
        }

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private TransactionPayments() { }
		private TransactionPayments(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Childs = childs;
			Fetch(reader);
		}

		public static TransactionPayments GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static TransactionPayments GetChildList(int sessionCode, IDataReader reader, bool childs) { return new TransactionPayments(sessionCode, reader, childs); }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private TransactionPayments(IList<TransactionPayment> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}

		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static TransactionPayments NewChildList() 
        { 
            TransactionPayments list = new TransactionPayments(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		public static TransactionPayments GetChildList(IList<TransactionPayment> lista) { return new TransactionPayments(lista); }
        public static TransactionPayments GetChildList(Payment parent, bool childs)
        {
            CriteriaEx criteria = TransactionPayment.GetCriteria(parent.SessionCode);
            criteria.Query = SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<TransactionPayments>(criteria);
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
                    TransactionPayment.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query);

                    while (reader.Read())
                        this.AddItem(TransactionPayment.GetChild(SessionCode, reader, Childs));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (TransactionPayment obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (TransactionPayment obj in this)
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

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<TransactionPayment> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (TransactionPayment item in lista)
			{
				if (this.GetItem(item.Oid) == null) this.AddItem(item);
			}

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
			{
				TransactionPayment item = TransactionPayment.GetChild(SessionCode, reader, Childs);
				if (this.GetItem(item.Oid) == null) this.AddItem(item);
			}

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Payment parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (TransactionPayment obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (TransactionPayment obj in this)
			{	
				if (!this.Contains(obj))
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}

			this.RaiseListChangedEvents = true;
		}
		
		#endregion

        #region SQL

        public static string SELECT(Library.Store.QueryConditions conditions) { return TransactionPayment.SELECT(conditions, true); }
        public static string SELECT(Payment item) { return SELECT(new Library.Store.QueryConditions { Payment = item.GetInfo(false), PaymentType = item.ETipoPago }); }
        public static string SELECT(InputInvoice item) 
		{ 
			return SELECT(new Library.Store.QueryConditions { FacturaRecibida = item.GetInfo(), PaymentType = ETipoPago.Factura }); 
		}
		public static string SELECT(Expense item)
		{
			return SELECT(new Library.Store.QueryConditions { Gasto = item.GetInfo(), PaymentType = EnumConvert.ToETipoPago(item.ECategoriaGasto) });
		}

        #endregion
    }
}