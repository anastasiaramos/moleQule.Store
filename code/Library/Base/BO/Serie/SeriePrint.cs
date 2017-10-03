using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class SeriePrint : SerieInfo
    {

        #region Attributes & Properties
		
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(SerieInfo source)
        {
            if (source == null) return;

			
			
        }

        #endregion

        #region Factory Methods

        private SeriePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static SeriePrint New(SerieInfo source)
        {
            SeriePrint item = new SeriePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
