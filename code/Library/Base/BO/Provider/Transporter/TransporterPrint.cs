using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class TransporterPrint : TransporterInfo
    {
        #region Attributes & Properties
		
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(TransporterInfo source)
        {
            if (source == null) return;

            _base.CopyValues(source);
            Oid = source.Oid;			
        }

        #endregion

        #region Factory Methods

        protected TransporterPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TransporterPrint New(TransporterInfo source)
        {
            TransporterPrint item = new TransporterPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class TransportistaPrint : TransporterPrint
    {
        #region Factory Methods

        private TransportistaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static TransportistaPrint New(TransporterInfo source)
        {
            TransportistaPrint item = new TransportistaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
