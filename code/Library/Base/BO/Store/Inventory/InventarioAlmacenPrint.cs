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
    public class InventarioAlmacenPrint : InventarioAlmacenInfo
    {

        #region Attributes & Properties		
		
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(InventarioAlmacenInfo source)
        {
            if (source == null) return;

			
			StoreInfo almacen = StoreInfo.Get(OidAlmacen, false);
            if (almacen != null)
                _base.Almacen = almacen.Nombre;
			
			
        }

        #endregion

        #region Factory Methods

        private InventarioAlmacenPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static InventarioAlmacenPrint New(InventarioAlmacenInfo source)
        {
            InventarioAlmacenPrint item = new InventarioAlmacenPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
