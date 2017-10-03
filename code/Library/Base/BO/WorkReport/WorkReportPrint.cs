using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class WorkReportPrint : WorkReportInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(WorkReportInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private WorkReportPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static WorkReportPrint New(WorkReportInfo source)
        {
            WorkReportPrint item = new WorkReportPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
