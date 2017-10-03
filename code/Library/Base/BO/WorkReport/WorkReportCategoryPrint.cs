using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class WorkReportCategoryPrint : WorkReportCategoryInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(WorkReportCategoryInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private WorkReportCategoryPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static WorkReportCategoryPrint New(WorkReportCategoryInfo source)
        {
            WorkReportCategoryPrint item = new WorkReportCategoryPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
