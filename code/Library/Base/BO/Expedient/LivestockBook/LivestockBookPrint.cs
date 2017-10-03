using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LivestockBookPrint : LivestockBookInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods
        
        #endregion

        #region Factory Methods

        protected LivestockBookPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LivestockBookPrint New(LivestockBookInfo source)
        {
            LivestockBookPrint item = new LivestockBookPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion
    }

    /*DEPRECATED*/
    [Serializable()]
    public class LibroGanaderoPrint : LivestockBookPrint
    {
        #region Factory Methods

        private LibroGanaderoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static LibroGanaderoPrint New(LivestockBookInfo source)
        {
            LibroGanaderoPrint item = new LibroGanaderoPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion
    }
}
