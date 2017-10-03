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
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class KitInfo : ReadOnlyBaseEx<KitInfo, Kit>
	{	
		#region Attributes

		protected KitBase _base = new KitBase();

		
		#endregion
		
		#region Properties
		
		public KitBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public long OidProduct { get { return _base.Record.OidProduct; } }
		public Decimal Amount { get { return _base.Record.Amount; } }

		public string Product { get { return _base.Product; } set { _base.Product = value; } }
        public decimal PurchasePrice { get { return _base.PurchasePrice; } set { _base.PurchasePrice = value; } }

		#endregion
		
		#region Business Methods
						
		public void CopyFrom(Kit source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected KitInfo() { /* require use of factory methods */ }
		private KitInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal KitInfo(Kit item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static KitInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new KitInfo(sessionCode, reader, childs);
		}
		
		public static KitInfo New(long oid = 0) { return new KitInfo(){ Oid = oid }; }
		
 		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion			
	}
}