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
    public class DespachantePrint : DespachanteInfo
    {

        #region Attributes & Properties
		
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(DespachanteInfo source)
        {
            if (source == null) return;

            _base.CopyValues(source);
            Oid = source.Oid;			
        }

        #endregion

        #region Factory Methods

        private DespachantePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static DespachantePrint New(DespachanteInfo source)
        {
            DespachantePrint item = new DespachantePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
