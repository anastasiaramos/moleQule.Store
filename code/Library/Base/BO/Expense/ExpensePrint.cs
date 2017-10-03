using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ExpensePrint : ExpenseInfo
    {              
        #region Attributes & Properties

		public string CategoriaGastoPrintLabel { get { return moleQule.Store.Structs.EnumText<ECategoriaGasto>.GetPrintLabel(ECategoriaGasto); } }
        public string ETipoAcreedorPrintLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetPrintLabel(ETipoAcreedor); } }
		public string EMedioPagoPrintLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }

		#endregion

		#region Business Methods

		protected void CopyValues(ExpenseInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}

        #endregion

        #region Factory Methods

        protected ExpensePrint() { /* require use of factory methods */ }

        public static ExpensePrint New(ExpenseInfo source)
		{
            ExpensePrint item = new ExpensePrint();
			item.CopyValues(source);

			return item;
		}

        #endregion
    }

    [Serializable()]
    public class GastoPrint : ExpensePrint
    {
        #region Factory Methods

        private GastoPrint() { /* require use of factory methods */ }

        public new static GastoPrint New(ExpenseInfo source)
        {
            GastoPrint item = new GastoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
