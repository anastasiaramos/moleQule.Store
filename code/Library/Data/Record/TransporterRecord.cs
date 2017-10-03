using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class TransporterRecord : SupplierRecord
	{
		#region Attributes

		#endregion

		#region Properties

		#endregion

		#region Business Methods

		public TransporterRecord() { }

		public override void CopyValues(IDataReader source)
		{
			if (source == null) return;

			base.CopyValues(source);
		}
		public virtual void CopyValues(TransporterRecord source)
		{
			if (source == null) return;

			base.CopyValues(source);
		}

		#endregion
	}
}