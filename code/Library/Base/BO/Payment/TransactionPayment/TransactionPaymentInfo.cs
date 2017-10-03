using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class TransactionPaymentInfo : ReadOnlyBaseEx<TransactionPaymentInfo>
	{	
		#region Attributes

        public TransactionPaymentBase _base = new TransactionPaymentBase();

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidPago { get { return _base.Record.OidPago; } }
		public long TipoPago { get { return _base.Record.TipoPago; } }
        public long OidOperation { get { return _base.Record.OidOperacion; } }
        public long OidExpediente { get { return _base.Record.OidExpediente; } }
        public long TipoAcreedor { get { return _base.Record.TipoAgente; } }
		public Decimal Cantidad { get { return _base.Record.Cantidad; } }

        //Campo no enlazdo
        public ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } }
        public string NExpediente { get { return _base.NExpediente; } }
		public string NSerie { get { return _base.NSerie; } }
		public string CodigoFactura { get { return _base.NFactura; } }
		public DateTime FechaFactura { get { return _base.FechaFactura; } }
        public string NFactura { get { return _base.NFactura; } }
        public Decimal ImporteFactura { get { return _base.ImporteFactura; } }
        public Decimal OtherPayments { get { return _base.OtherPayments; } } 

		#endregion
		
		#region Business Methods
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected TransactionPaymentInfo() { /* require use of factory methods */ }
		private TransactionPaymentInfo(int sessionCode, IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal TransactionPaymentInfo(TransactionPayment item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
	
		public static TransactionPaymentInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static TransactionPaymentInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new TransactionPaymentInfo(sessionCode, reader, childs);
		}
		
 		#endregion
		
		#region Child Data Access
		
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}
		
		#endregion		
	}
}