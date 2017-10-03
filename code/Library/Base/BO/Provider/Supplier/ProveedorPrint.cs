using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ProveedorPrint : ProveedorInfo
    {
              
        #region Business Method

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(ProveedorInfo source)
        {
            if (source == null) return;

            _base.CopyValues(source);
            Oid = source.Oid;
        }

        #endregion

        #region Factory Methods

        private ProveedorPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ProveedorPrint New(ProveedorInfo source)
        {
            ProveedorPrint item = new ProveedorPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
