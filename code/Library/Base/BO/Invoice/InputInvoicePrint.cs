using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.CslaEx; 
using moleQule.Common;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InputInvoicePrint : InputInvoiceInfo
    {              
        #region Attributes & Properties

		protected string _poblacion = string.Empty;
		protected string _telefonos = string.Empty;
        private string _nombre_transportista = string.Empty;
        private string _nombre_serie = string.Empty;
		private QRCodePrint _qr_code_print = new QRCodePrint();

        public decimal ImporteIgic { get { return Impuestos; } }
        public string Poblacion { get { return _poblacion; } }
        public string Fax { get; set; }
		public string EEstadoPrintLabel { get { return Base.EnumText<EEstado>.GetPrintLabel(EEstado); } }
		public string ETipoAcreedorPrintLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetPrintLabel(ETipoAcreedor); } }
		public byte[] QRCode { get { return _qr_code_print.QRCode; } }
		public EPeriodo EPeriodo
		{
			get
			{ 
				if ((Fecha >= new DateTime(Fecha.Year, 1 , 1)) && (Fecha < new DateTime(Fecha.Year, 4, 1))) return EPeriodo.Periodo1T;
				if ((Fecha >= new DateTime(Fecha.Year, 4 , 1)) && (Fecha < new DateTime(Fecha.Year, 7, 1))) return EPeriodo.Periodo2T;
				if ((Fecha >= new DateTime(Fecha.Year, 7 , 1)) && (Fecha < new DateTime(Fecha.Year, 10, 1))) return EPeriodo.Periodo3T;
				if ((Fecha >= new DateTime(Fecha.Year, 10 , 1)) && (Fecha <= new DateTime(Fecha.Year, 12, 31))) return EPeriodo.Periodo4T;

				return EPeriodo.Anual;
			}
		}
		public string PeriodoLabel { get { return moleQule.Common.Structs.EnumText<EPeriodo>.GetLabel(EPeriodo); } }

		#endregion

		#region Business Methods
		
		protected void CopyValues(InputInvoiceInfo factura, IAcreedorInfo acreedor, bool get_QRCode)
        {
            if (factura == null) return;

            Oid = factura.Oid;
			_base.CopyValues(factura);
            if (acreedor != null)
            {
                _telefonos = acreedor.Telefono;
            }

			if (get_QRCode)
			{
				_qr_code_print.LoadQRCode(_qr_code_print.Encode(QREncodeVersion.v1, moleQule.Common.Structs.ETipoEntidad.FacturaRecibida, this), QRCodeVersion.v8);
			}
		}

        #endregion

        #region Factory Methods

        protected InputInvoicePrint() { /* require use of factory methods */ }

		public static InputInvoicePrint New(InputInvoiceInfo factura)
		{
			InputInvoicePrint item = new InputInvoicePrint();
			item.CopyValues(factura, null, false);

			return item;
		}
		public static InputInvoicePrint New(InputInvoiceInfo factura, IAcreedorInfo acreedor)
		{
			InputInvoicePrint item = new InputInvoicePrint();
			item.CopyValues(factura, acreedor, true);

			return item;
		}
        public static InputInvoicePrint New(InputInvoiceInfo factura, IAcreedorInfo acreedor, bool get_QRCode)
        {
            InputInvoicePrint item = new InputInvoicePrint();
            item.CopyValues(factura, acreedor, get_QRCode);

            return item;
        }

        #endregion
    }

    [Serializable()]
    public class FacturaRecibidaPrint : InputInvoicePrint
    {
        #region Factory Methods

        protected FacturaRecibidaPrint() { /* require use of factory methods */ }

        public new static FacturaRecibidaPrint New(InputInvoiceInfo factura)
        {
            FacturaRecibidaPrint item = new FacturaRecibidaPrint();
            item.CopyValues(factura, null, false);

            return item;
        }
        public new static FacturaRecibidaPrint New(InputInvoiceInfo factura, IAcreedorInfo acreedor)
        {
            FacturaRecibidaPrint item = new FacturaRecibidaPrint();
            item.CopyValues(factura, acreedor, true);

            return item;
        }
        public new static FacturaRecibidaPrint New(InputInvoiceInfo factura, IAcreedorInfo acreedor, bool get_QRCode)
        {
            FacturaRecibidaPrint item = new FacturaRecibidaPrint();
            item.CopyValues(factura, acreedor, get_QRCode);

            return item;
        }

        #endregion
    }

	public class FacturaProveedorPrint : InputInvoicePrint
	{
		#region Factory Methods

		private FacturaProveedorPrint() { /* require use of factory methods */ }

		public new static FacturaProveedorPrint New(InputInvoiceInfo factura)
		{
			FacturaProveedorPrint item = new FacturaProveedorPrint();
			item.CopyValues(factura, null, false);

			return item;
		}
		public new static FacturaProveedorPrint New(InputInvoiceInfo factura, IAcreedorInfo acreedor)
		{
			FacturaProveedorPrint item = new FacturaProveedorPrint();
			item.CopyValues(factura, acreedor, true);

			return item;
		}
        public new static FacturaProveedorPrint New(InputInvoiceInfo factura, IAcreedorInfo acreedor, bool get_QRCode)
        {
            FacturaProveedorPrint item = new FacturaProveedorPrint();
            item.CopyValues(factura, acreedor, get_QRCode);

            return item;
        }

        #endregion
	}
}
