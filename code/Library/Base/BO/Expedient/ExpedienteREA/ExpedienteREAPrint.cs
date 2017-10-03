using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.CslaEx; 

using moleQule;
using moleQule.Common;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ExpedienteREAPrint : ExpedienteREAInfo
    {

        #region Attributes & Properties
        
		#endregion
		
		#region Business Methods

        protected void CopyValues(ExpedienteREAInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private ExpedienteREAPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ExpedienteREAPrint New(ExpedienteREAInfo source)
        {
            ExpedienteREAPrint item = new ExpedienteREAPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
