using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InputInvoiceLinePrint : InputInvoiceLineInfo
    {              
        #region Business Methods

        public new string Concepto
        {
            get { return _base.Record.Concepto; }
            set { _base.Record.Concepto = value; }
        }

        /*DEPRECATED*/
        public Decimal Cantidad { get { return _base.Record.CantidadKilos; } }

        protected void CopyValues(InputInvoiceLineInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);           
        }

        #endregion

        #region Factory Methods

        private InputInvoiceLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static InputInvoiceLinePrint New(InputInvoiceLineInfo source)
        {
            InputInvoiceLinePrint item = new InputInvoiceLinePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}