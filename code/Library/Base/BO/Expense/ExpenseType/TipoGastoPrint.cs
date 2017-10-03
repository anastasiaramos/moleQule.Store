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
    public class TipoGastoPrint : TipoGastoInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(TipoGastoInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);			
        }

        #endregion

        #region Factory Methods

        private TipoGastoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TipoGastoPrint New(TipoGastoInfo source)
        {
            TipoGastoPrint item = new TipoGastoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
