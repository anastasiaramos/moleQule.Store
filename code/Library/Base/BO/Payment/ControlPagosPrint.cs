using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using CslaEx;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ControlPagosPrint : FacturaProveedorInfo
    {

        #region Business Method

        protected string _tipo_elemento;
        protected string _nombre_elemento = string.Empty;
        protected DateTime _fecha_fac;
        protected DateTime _fecha_prev;
        protected string _detalles = string.Empty;
        protected string _contenedor = "---";
        private decimal _total_estimado;

        public string Contenedor { get { return _contenedor; } }
        public string NombreElemento { get { return _nombre_elemento; } }
        public string Detalles { get { return _detalles; } }
        public string FechaFac { get { return _fecha_fac == DateTime.MinValue ? "" : _fecha_fac.ToString("d"); } }
        public string FechaPago { get { return (Pagado > 0) ? _fecha_pago.ToString("d") : "---"; } }
        public string FechaPrev { get { return _fecha_prev == DateTime.MinValue ? "" : _fecha_prev.ToString("d"); } }
        public string TipoElemento { get { return _tipo_elemento; } }
        public virtual decimal PendienteEstimado { get { return _total_estimado; } }

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(FacturaProveedorInfo factura, ExpedienteInfo expediente, PResumen resumen)
        {
            if (factura == null) return;

            _n_serie = factura.NSerie;
            _n_factura = factura.NFactura;
            _fecha_fac = factura.Fecha;
            _fecha_prev = factura.Prevision;
            _fecha_pago = factura.FechaPagoFactura;
            _total = factura.Total;
            _pagado = factura.Pagado;
            _pendiente = factura.Pendiente;
            _detalles = factura.CuentaBancaria;
            _nombre_elemento = factura.NombreAcreedor;
            _tipo_elemento = EnumText<ETipoAcreedor>.GetLabel(factura.ETipoAcreedor);
            _efectos_negociados = factura.PendienteVencimiento;
            _efectos_devueltos = factura.Vencido;
            _efectos_pendientes_vto = factura.EfectosPendientesVto;

            if (expediente != null)
            {
                _contenedor = expediente.Contenedor;
                _codigo_expediente = expediente.Codigo;
            }
            else
            {
                _contenedor = "---";
                _codigo_expediente = "No Asignado";
            }

            if (resumen != null)
            {
                _total_estimado = resumen.TotalEstimado;
            }
        }

        #endregion

        #region Factory Methods

        private ControlPagosPrint() { /* require use of factory methods */ }

        public static ControlPagosPrint New(FacturaProveedorInfo factura, ExpedienteInfo exp, PResumen resumen)
        {
            ControlPagosPrint item = new ControlPagosPrint();
            item.CopyValues(factura, exp, resumen);

            return item;
        }

        #endregion

    }

}
