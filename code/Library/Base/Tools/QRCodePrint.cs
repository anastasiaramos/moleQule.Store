using System;
using System.Globalization;
using System.Text;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;

namespace moleQule.Library.Store
{
	public class QRCodePrint : QRCodePrintBase
	{
		#region Business Methods

		public string Encode(QREncodeVersion version, ETipoEntidad tipo, object source)
		{
			String txtEncodeData = string.Empty;

			txtEncodeData = "<l v=" + ((long)version).ToString() + " t=" + ((long)tipo).ToString() + " oid=OID_ITEM>";

			switch (tipo)
			{ 
				case moleQule.Common.Structs.ETipoEntidad.FacturaRecibida:
					{
						InputInvoicePrint item = (InputInvoicePrint)source;

						txtEncodeData = txtEncodeData.Replace("OID_ITEM", item.Oid.ToString());

						txtEncodeData += "<p>" 
										+ item.Codigo + "|" 
										+ item.NSerie + "|" 
										+ item.Fecha.ToShortDateString() + "|" 
										+ item.VatNumber + "|" 
										+ item.NumeroAcreedor + "|" 
										+ item.Acreedor + "|" 
										+ item.BaseImponible.ToString() + "|" 
										+ item.ImporteIgic.ToString() + "|" 
										+ item.Total.ToString() + "|"
										+ "</p>";
					}
					break;

				case moleQule.Common.Structs.ETipoEntidad.Pago:
					{
						PaymentPrint item = (PaymentPrint)source;

						txtEncodeData = txtEncodeData.Replace("OID_ITEM", item.Oid.ToString());

						txtEncodeData += "<p>"
										+ item.Codigo + "|"
										+ item.Fecha.ToShortDateString() + "|"
										+ item.IDPagoS + "|"
										+ item.EMedioPagoPrintLabel + "|"
										+ item.CodigoAgente + "|"
										+ item.Agente + "|"
										+ item.Importe + "|"
										+ item.CuentaBancaria + "|"
										+ "</p>";

						if (item.Operations != null)
						{
							txtEncodeData += "<sl t=" + ((long)moleQule.Common.Structs.ETipoEntidad.PagoFactura).ToString() + ">";

							foreach (TransactionPaymentInfo cf in item.Operations)
								txtEncodeData += "<p>"
												+ cf.CodigoFactura + "|"
												+ cf.Cantidad + "|"
												+ "</p>";

							txtEncodeData += "</sl>";
						}



					}
					break;
			}

			txtEncodeData += "</l>";
			return txtEncodeData;
		}

		#endregion
	}	

}
