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
    public class LineaInventarioPrint : LineaInventarioInfo
    {

        #region Attributes & Properties
		
		protected string _inventario = string.Empty;
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(LineaInventarioInfo source)
        {
            if (source == null) return;

            InventarioAlmacenInfo inventario = InventarioAlmacenInfo.Get(OidInventario, false);
            if (inventario != null)
                _inventario = inventario.Nombre;			
			
        }

        #endregion

        #region Factory Methods

        private LineaInventarioPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LineaInventarioPrint New(LineaInventarioInfo source)
        {
            LineaInventarioPrint item = new LineaInventarioPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
