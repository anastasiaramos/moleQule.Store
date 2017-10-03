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
    public class PedidoProveedorPrint : PedidoProveedorInfo
    {

        #region Attributes & Properties		
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(PedidoProveedorInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);			
        }

        #endregion

        #region Factory Methods

        private PedidoProveedorPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static PedidoProveedorPrint New(PedidoProveedorInfo source)
        {
            PedidoProveedorPrint item = new PedidoProveedorPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
