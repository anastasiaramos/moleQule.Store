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
    public class LivestockBookLinePrint : LivestockBookLineInfo
    {
        #region Attributes & Properties

        public new string Crotal { get { return _base.Record.Crotal; } set { _base.Record.Crotal = value; } }

		#endregion
		
		#region Business Methods

        protected void CopyValues(LivestockBookLineInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);

            if (!source.Explotacion) Crotal = Crotal + "*";
        }

        #endregion

        #region Factory Methods

        protected LivestockBookLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LivestockBookLinePrint New(LivestockBookLineInfo source)
        {
            LivestockBookLinePrint item = new LivestockBookLinePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }

    /*DEPRECATED*/
    [Serializable()]
    public class LineaLibroGanaderoPrint : LivestockBookLinePrint
    {
        #region Factory Methods

        private LineaLibroGanaderoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static LineaLibroGanaderoPrint New(LivestockBookLineInfo source)
        {
            LineaLibroGanaderoPrint item = new LineaLibroGanaderoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
