using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class TransactionPaymentPrint : TransactionPaymentInfo
    {
        #region Attributes

        protected decimal _pagado;
        protected decimal _pendiente;
        protected DateTime _prevision;
        protected DateTime _fecha_pago;

        #endregion

        #region Properties

        /* DEPRECATED: Using OidOperation */
        public long OidFactura { get { return base.OidOperation; } }
        public bool Pagada { get { return (_pagado >= _base.ImporteFactura); } }
        public decimal Pagado { get { return _pagado; } set { _pagado = value; } }
        public decimal Pendiente { get { return _pendiente; } set { _pendiente = value; } }
        public long DiasTranscurridos
        {
            get
            {
                if (Pagada)
                    if (_fecha_pago != DateTime.MinValue)
                        return _fecha_pago.Subtract(_base.FechaFactura).Days;
                    else
                        return 0;
                else
                    return DateTime.Today.Subtract(_base.FechaFactura).Days;
            }
        }
        public DateTime Prevision { get { return _prevision; } }
        public DateTime FechaPagoFactura { get { return _fecha_pago; } }
        public decimal PagosAnteriores { get { return OtherPayments; } }

        #endregion

        #region Business Methods

        protected void CopyValues(TransactionPaymentInfo source, InputInvoiceInfo factura)
        {
            if (source == null) return;

            Oid = source.Oid;

            _base.CopyValues(source);

            if (factura != null)
            {
                _pagado = factura.Pagado;
                _pendiente = factura.Pendiente;
                _prevision = factura.Prevision;
				_fecha_pago = factura.FechaPagoFactura;
                if (_base.NExpediente == string.Empty && factura.Expediente != string.Empty) 
                    _base.NExpediente = factura.Expediente;
            }
        }
        protected void CopyValues(TransactionPaymentInfo source, NominaInfo nomina)
        {
            if (source == null) return;

            Oid = source.Oid;

            _base.CopyValues(source);

            if (nomina != null)
            {
                _base.NFactura = nomina.Descripcion;
                _base.NExpediente = nomina.Codigo;
                _base.NSerie = nomina.IDRemesa;
                _pagado = nomina.TotalPagado;
                _pendiente = nomina.Pendiente;
                _prevision = nomina.PrevisionPago;
                _fecha_pago = nomina.FechaPago;
            }
        }

        #endregion

        #region Factory Methods

        protected TransactionPaymentPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TransactionPaymentPrint New(TransactionPaymentInfo source, InputInvoiceInfo factura)
        {
            TransactionPaymentPrint item = new TransactionPaymentPrint();
            item.CopyValues(source, factura);

            return item;
        }

        // called to load data from source
        public static TransactionPaymentPrint New(TransactionPaymentInfo source, NominaInfo nomina)
        {
            TransactionPaymentPrint item = new TransactionPaymentPrint();
            item.CopyValues(source, nomina);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class PagoFacturaPrint : TransactionPaymentPrint
    {
        #region Factory Methods

        private PagoFacturaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static PagoFacturaPrint New(TransactionPaymentInfo source, InputInvoiceInfo factura)
        {
            PagoFacturaPrint item = new PagoFacturaPrint();
            item.CopyValues(source, factura);

            return item;
        }

        // called to load data from source
        public new static PagoFacturaPrint New(TransactionPaymentInfo source, NominaInfo nomina)
        {
            PagoFacturaPrint item = new PagoFacturaPrint();
            item.CopyValues(source, nomina);

            return item;
        }

        #endregion
    }
}