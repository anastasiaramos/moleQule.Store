using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ToolPrint : ToolInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(ToolInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private ToolPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ToolPrint New(ToolInfo source)
        {
            ToolPrint item = new ToolPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
