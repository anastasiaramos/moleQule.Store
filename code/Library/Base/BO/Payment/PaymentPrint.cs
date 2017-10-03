using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class PaymentPrint : PaymentInfo
    {
        #region Attributes & Properties

        protected string _codigo_acreedor = string.Empty;
		private QRCodePrint _qr_code_print = new QRCodePrint();

        public string IDPagoS { get { return _base.Record.IdPago.ToString(Resources.Defaults.PAGO_CODE_FORMAT); } }
        public string CodigoAcreedor { get { return _codigo_acreedor; } }
		public string EEstadoPrintLabel { get { return Base.EnumText<EEstado>.GetPrintLabel(EEstado); } }
		public string EMedioPagoPrintLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }
		public string ETipoAcreedorPrintLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetPrintLabel(ETipoAcreedor); } }
		public byte[] QRCode { get { return _qr_code_print.QRCode; } }

        public new string Agente { get { return _base.Agent; } }

        #endregion

        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(PaymentInfo source, IAcreedor acreedor, bool get_QRCode)
        {
            if (source == null) return;

            _base.Record.OidAgente = source.OidAgente;
            _base.Record.OidCuentaBancaria = source.OidCuentaBancaria;
            _base.Record.Estado = source.Estado;
            _base.Record.IdPago = source.IdPago;
            _base.Record.Codigo = source.Codigo;
            _base.Record.TipoAgente = source.TipoAgente;
            _base.Record.Fecha = source.Fecha;
            _base.Record.Importe = source.Importe;
            _base.Record.MedioPago = source.MedioPago;
            _base.Record.Vencimiento = source.Vencimiento;
            _base.Record.Observaciones = source.Observaciones;
            _base.Pendiente = source.Pendiente;
            _base.Record.Tipo = source.TipoPago;

            _base.CashLineID = source.IDLineaCaja;
            _base.BankLineID = source.IDMovimientoBanco;
            _base.AccountingLineID = source.IDMovimientoContable;
            _base.BankAccount = source.Entidad + Environment.NewLine + source.CuentaBancaria;
            _base.Agent = source.Agente;
            _base.AgentID = source.CodigoAgente;

            if (acreedor != null)
            {
                _codigo_acreedor = acreedor.Codigo;
                _base.Agent = acreedor.ETipoAcreedor == ETipoAcreedor.Empleado ? (acreedor as Employee).NombreCompleto : acreedor.Nombre;
                _base.AgentID = acreedor.Codigo;
            }

			if (get_QRCode)
			{
				_operations = source.Operations;
				_qr_code_print.LoadQRCode(_qr_code_print.Encode(QREncodeVersion.v1, moleQule.Common.Structs.ETipoEntidad.Pago, this), QRCodeVersion.v15);
			}
        }

        #endregion

        #region Factory Methods

        protected PaymentPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static PaymentPrint New(PaymentInfo source, IAcreedor acreedor, bool get_QRCode)
        {
            PaymentPrint item = new PaymentPrint();
            item.CopyValues(source, acreedor, get_QRCode);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class PagoPrint : PaymentPrint
    {
        #region Factory Methods

        private PagoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static PagoPrint New(PaymentInfo source, IAcreedor acreedor, bool get_QRCode)
        {
            PagoPrint item = new PagoPrint();
            item.CopyValues(source, acreedor, get_QRCode);

            return item;
        }

        #endregion
    }
}
