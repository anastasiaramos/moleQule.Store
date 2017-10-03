using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class StockPrint : StockInfo
    {
		#region Attributes & Properties

		public string ETipoStockPrintLabel { get { return moleQule.Store.Structs.EnumText<ETipoStock>.GetPrintLabel(ETipoStock); } }

		#endregion

		#region Factory Methods

		private StockPrint() { /* require use of factory methods */ }

		// called to load data from source
		public static StockPrint New(StockInfo source)
		{
			StockPrint item = new StockPrint();
			item.CopyValues(source);

			return item;
		}

		#endregion

		#region Business Methods

        protected void CopyValues(StockInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);

			if (_base._titular == string.Empty) _base._titular = Proveedor;
        }

        #endregion
    }
}