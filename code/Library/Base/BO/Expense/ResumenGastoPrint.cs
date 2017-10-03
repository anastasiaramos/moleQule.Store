using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ResumenGastoPrint
    {              
        #region Attributes & Properties

		public long Oid { get; set; }
		public string Expediente { get; set; }
		public string Proveedor { get; set; }
		public string Naviera { get; set; }
		public string Despachante { get; set; }
		public string TransportistaOrigen { get; set; }
		public string TransportistaDestino { get; set; }

		#endregion

		#region Business Methods

		protected void CopyValues(ExpedientInfo source, ExpenseList list
                            , InputDeliveryLineList conceptos
                            , InputDeliveryList albaranes)
        {
            if (source == null) return;

			Oid = source.Oid;
			Expediente = source.Codigo;

			foreach (ExpenseInfo item in list)
			{
				if (item.OidExpediente == source.Oid)
					CheckGasto(item);
			}

            foreach (InputDeliveryLineInfo item in conceptos)
            {
                if (item.OidExpediente == source.Oid)
                    CheckGasto(list, albaranes.GetItem(item.OidAlbaran));
            }
            
			if (Proveedor != null) Proveedor = Proveedor.Substring(0, Proveedor.Length - 1);
			if (Naviera != null) Naviera = Naviera.Substring(0, Naviera.Length - 1);
			if (Despachante != null) Despachante = Despachante.Substring(0, Despachante.Length - 1);
			if (TransportistaOrigen != null) TransportistaOrigen = TransportistaOrigen.Substring(0, TransportistaOrigen.Length - 1);
			if (TransportistaDestino != null) TransportistaDestino = TransportistaDestino.Substring(0, TransportistaDestino.Length - 1);
		}

		private void CheckGasto(ExpenseInfo gasto)
		{
			if ((gasto.ECategoriaGasto != ECategoriaGasto.Stock) && 
				(gasto.ECategoriaGasto != ECategoriaGasto.GeneralesExpediente) &&
                (gasto.ECategoriaGasto != ECategoriaGasto.OtrosExpediente)) return;

			switch (gasto.ETipoAcreedor)
			{
				case ETipoAcreedor.Proveedor:
					Proveedor += gasto.NFactura + " - " + gasto.Acreedor + "\n";
					break;

				case ETipoAcreedor.Naviera:
					Naviera += gasto.NFactura + " - " + gasto.Acreedor + "\n";
					break;

				case ETipoAcreedor.Despachante:
					Despachante += gasto.NFactura + " - " + gasto.Acreedor + "\n";
					break;

				case ETipoAcreedor.TransportistaOrigen:
					TransportistaOrigen += gasto.NFactura + " - " + gasto.Acreedor + "\n";
					break;

				case ETipoAcreedor.TransportistaDestino:
					TransportistaDestino += gasto.NFactura + " - " + gasto.Acreedor + "\n";
					break;
			}
		}

        private void CheckGasto(ExpenseList gastos, InputDeliveryInfo albaran)
        {
            foreach (ExpenseInfo info in gastos)
            {
                if (info.OidFactura == albaran.OidFactura)
                    return;
            }

            switch (albaran.ETipoAcreedor)
            {
                case ETipoAcreedor.Proveedor:
                    Proveedor += albaran.Codigo + " - " + albaran.NombreAcreedor + "\n";
                    break;

                case ETipoAcreedor.Naviera:
                    Naviera += albaran.Codigo + " - " + albaran.NombreAcreedor + "\n";
                    break;

                case ETipoAcreedor.Despachante:
                    Despachante += albaran.Codigo + " - " + albaran.NombreAcreedor + "\n";
                    break;

                case ETipoAcreedor.TransportistaOrigen:
                    TransportistaOrigen += albaran.Codigo + " - " + albaran.NombreAcreedor + "\n";
                    break;

                case ETipoAcreedor.TransportistaDestino:
                    TransportistaDestino += albaran.Codigo + " - " + albaran.NombreAcreedor + "\n";
                    break;
            }
        }
        
        #endregion

        #region Factory Methods

		private ResumenGastoPrint() { /* require use of factory methods */ }

		public static ResumenGastoPrint New(ExpedientInfo source, ExpenseList list
                                        , InputDeliveryLineList conceptos
                                        , InputDeliveryList albaranes)
		{
			ResumenGastoPrint item = new ResumenGastoPrint();
			item.CopyValues(source, list, conceptos, albaranes);

			return item;
		}

        #endregion

    }
}
