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
    public class LineaFomentoPrint : LineaFomentoInfo
    {
        #region Attributes & Properties

		protected string _contenedor_teus20 = string.Empty;
		protected string _contenedor_teus40 = string.Empty;

		public string ContenedorTeus20 { get { return _contenedor_teus20; } set { _contenedor_teus20 = value; } }
		public string ContenedorTeus40 { get { return _contenedor_teus40; } set { _contenedor_teus40 = value; } }
		public DateTime Fecha { get { return FechaSolicitud; } } /*DEPRECATED*/

		#endregion

		#region Factory Methods

		private LineaFomentoPrint() { /* require use of factory methods */ }

		// called to load data from source
		public static LineaFomentoPrint New(LineaFomentoInfo source)
		{
			LineaFomentoPrint item = new LineaFomentoPrint();
			item.CopyValues(source);

			return item;
		}

		#endregion

		#region Business Methods

        protected void CopyValues(LineaFomentoInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);

			if (source.Teus20)
				_contenedor_teus20 = source.Contenedor;
			else
				_contenedor_teus40 = source.Contenedor;
        }

        #endregion
    }
}
