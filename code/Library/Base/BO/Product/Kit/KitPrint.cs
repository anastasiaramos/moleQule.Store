using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;

namespace moleQule.Library.moleQule.Libary.Store
{
    [Serializable()]
    public class KitPrint : KitInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(KitInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private KitPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static KitPrint New(KitInfo source)
        {
            KitPrint item = new KitPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
