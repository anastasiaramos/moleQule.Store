using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    #region ExpedientePrint

    [Serializable()]
    public class ExpedientePrint : ExpedientInfo
    {
        #region Attributes & Properties

        protected string _trans_orig = string.Empty;
        protected string _trans_dest = string.Empty;
        protected string _contenedor_teus20 = string.Empty;
        protected string _contenedor_teus40 = string.Empty;

        public string TransDest { get { return _trans_dest; } }
        public string TransOrig { get { return _trans_orig; } }
        public decimal TotalKilos { get { return _base.KilosTotal; } }
        public string ContenedorTeus20 { get { return _contenedor_teus20; } set { _contenedor_teus20 = value; } }
        public string ContenedorTeus40 { get { return _contenedor_teus40; } set { _contenedor_teus40 = value; } }

        public new decimal StockKilos { get { return _base.StockKilos; } }
        public new decimal StockBultos { get { return _base.StockBultos; } }

		#endregion

		#region Business Methods

        protected void CopyValues(ExpedientInfo source)
        {
            if (source == null) return;

            _base.CopyValues(source);

            if (source.Teus20)
                _contenedor_teus20 = source.Contenedor;
            else
                _contenedor_teus40 = source.Contenedor;
            			
			_base.AyudaEstimada = source.AyudaEstimada;
			_base.AyudaCobrada = source.AyudaCobrada;
			_base.AyudaPendiente = source.AyudaPendiente;

            _base.StockBultos = source.StockBultos;
            _base.StockKilos = source.StockKilos;

            _base.KilosTotal = source.Kilos;
            _base.BultosTotal = source.Bultos;
            _base.Proveedor = source.Proveedor;
            _base.Despachante = source.Despachante;
            _base.NombreTransDest = source.NombreTransDest;
            _base.NombreTransOrig = source.NombreTransOrig;
            _base.Naviera = source.Naviera;
        }

        #endregion

		#region Factory Methods

		private ExpedientePrint() { /* require use of factory methods */ }

		// called to load data from source
		public static ExpedientePrint New(ExpedientInfo source)
		{
			ExpedientePrint item = new ExpedientePrint();
			item.CopyValues(source);

			return item;
		}

		#endregion
    }

    #endregion  
}