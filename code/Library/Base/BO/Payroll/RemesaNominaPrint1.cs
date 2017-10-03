using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using CslaEx;

using moleQule.Library;

namespace moleQule.Library.moleQule.Libary.Store
{
    [Serializable()]
    public class RemesaNominaPrint : RemesaNominaInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(RemesaNominaInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private RemesaNominaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static RemesaNominaPrint New(RemesaNominaInfo source)
        {
            RemesaNominaPrint item = new RemesaNominaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
