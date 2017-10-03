using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using moleQule;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class AlmacenPrint : StoreInfo
    {

        #region Attributes & Properties
		
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(StoreInfo source)
        {
            if (source == null) return;

			
			
        }

        #endregion

        #region Factory Methods

        private AlmacenPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static AlmacenPrint New(StoreInfo source)
        {
            AlmacenPrint item = new AlmacenPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
