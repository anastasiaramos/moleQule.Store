using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Common.Structs;
using moleQule.CslaEx; 
using moleQule.Common;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class NominaPrint : NominaInfo
    {              
        #region Attributes & Properties

		public string ETipoAcreedorPrintLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetPrintLabel(ETipoAcreedor); } }
		public string EMedioPagoPrintLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }

		#endregion

		#region Business Methods

		protected void CopyValues(NominaInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}

        #endregion

        #region Factory Methods

		private NominaPrint() { /* require use of factory methods */ }

		public static NominaPrint New(NominaInfo source)
		{
			NominaPrint item = new NominaPrint();
			item.CopyValues(source);

			return item;
		}

        #endregion
    }
}