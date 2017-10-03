using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class RemesaNominaPrint : PayrollBatchInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(PayrollBatchInfo source)
        {
            if (source == null) return;

           Oid = source.Oid;

		   _base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private RemesaNominaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static RemesaNominaPrint New(PayrollBatchInfo source)
        {
            RemesaNominaPrint item = new RemesaNominaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
