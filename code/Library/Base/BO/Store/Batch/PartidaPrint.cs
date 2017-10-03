using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PartidaPrint : BatchInfo
    {
        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(BatchInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        // called to load data from source
        public static PartidaPrint New(BatchInfo source)
        {
            PartidaPrint item = new PartidaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
