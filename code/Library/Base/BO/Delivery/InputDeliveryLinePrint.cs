using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InputDeliveryLinePrint : InputDeliveryLineInfo
    {              
        #region Business Methods

        public new string Concepto
        {
            get { return _base.Record.Concepto; }
            set { _base.Record.Concepto = value; }
        }

        protected void CopyValues(InputDeliveryLineInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        protected InputDeliveryLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static InputDeliveryLinePrint New(InputDeliveryLineInfo source)
        {
            InputDeliveryLinePrint item = new InputDeliveryLinePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class ConceptoAlbaranProveedorPrint : InputDeliveryLinePrint
    {
        #region Factory Methods

        private ConceptoAlbaranProveedorPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static ConceptoAlbaranProveedorPrint New(InputDeliveryLineInfo source)
        {
            ConceptoAlbaranProveedorPrint item = new ConceptoAlbaranProveedorPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
