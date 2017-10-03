using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class WorkReportResourcePrint : WorkReportResourceInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(WorkReportResourceInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private WorkReportResourcePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static WorkReportResourcePrint New(WorkReportResourceInfo source)
        {
            WorkReportResourcePrint item = new WorkReportResourcePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
