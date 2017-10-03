using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Common.Structs;
using moleQule.Common; 
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ControlPagosPrint : InputInvoiceInfo
    {

		#region Attributes & Properties

		protected string _tipo_elemento;
		protected string _nombre_elemento = string.Empty;
		protected DateTime _fecha_fac;
		protected DateTime _fecha_prev;
		protected string _detalles = string.Empty;
		protected string _contenedor = "---";
		protected decimal _total_estimado;

		public string Contenedor { get { return _contenedor; } }
		public string NombreElemento { get { return _nombre_elemento; } }
		public string Detalles { get { return _detalles; } }
		public string FechaFac { get { return _fecha_fac == DateTime.MinValue ? "" : _fecha_fac.ToString("d"); } }
		public string FechaPrev { get { return _fecha_prev == DateTime.MinValue ? "" : _fecha_prev.ToString("d"); } }
		public string TipoElemento { get { return _tipo_elemento; } }
		public decimal PendienteEstimado { get { return _total_estimado; } }

		#endregion

		#region Business Methods

		protected void CopyValues(InputInvoiceInfo factura, ExpedientInfo expediente, PaymentSummary resumen)
		{
			if (factura == null) return;

			_base.Record.NFactura = factura.NFactura;
			_fecha_fac = factura.Fecha;
			_fecha_prev = factura.Prevision;
			_base._fecha_pago = factura.FechaPagoFactura;
			_base._id_pago = factura.IDPago;
			_base.Record.Total = factura.Total;
			_base.Pagado = factura.Pagado;
			_base.Pendiente = factura.Pendiente;
			_detalles = factura.CuentaBancaria;
			_nombre_elemento = factura.Acreedor;
			_tipo_elemento = moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(factura.ETipoAcreedor);
			_base._efectos_negociados = factura.PendienteVencimiento;
			_base._efectos_devueltos = factura.Vencido;
			_base._efectos_pendientes_vto = factura.EfectosPendientesVto;

			if (expediente != null)
			{
				_contenedor = expediente.Contenedor;
				_base._expediente = expediente.Codigo;
			}
			else
			{
				_contenedor = "---";
				_base._expediente = "No Asignado";
			}

			if (resumen != null)
			{
				_total_estimado = resumen.TotalEstimado;
			}
		}

		#endregion

		#region Factory Methods

		private ControlPagosPrint() { /* require use of factory methods */ }

		public static ControlPagosPrint New(InputInvoiceInfo factura, ExpedientInfo exp, PaymentSummary resumen)
		{
			ControlPagosPrint item = new ControlPagosPrint();
			item.CopyValues(factura, exp, resumen);

			return item;
		}

		#endregion
    }
}
