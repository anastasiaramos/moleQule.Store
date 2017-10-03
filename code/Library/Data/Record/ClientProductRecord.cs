using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class ClientProductRecord : RecordBase
	{
		#region Attributes

		private long _oid_producto;
		private long _oid_cliente;
		private Decimal _precio;
		private bool _facturacion_bulto = false;
		private Decimal _p_descuento;
		private long _tipo_descuento;
		private Decimal _precio_compra;
		private bool _facturar = false;
		private DateTime _fecha_validez;
		#endregion

		#region Properties

		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual long TipoDescuento { get { return _tipo_descuento; } set { _tipo_descuento = value; } }
		public virtual Decimal PrecioCompra { get { return _precio_compra; } set { _precio_compra = value; } }
		public virtual bool Facturar { get { return _facturar; } set { _facturar = value; } }
		public virtual DateTime FechaValidez { get { return _fecha_validez; } set { _fecha_validez = value; } }

		#endregion

		#region Business Methods

		public ClientProductRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");
			_facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_tipo_descuento = Format.DataReader.GetInt64(source, "TIPO_DESCUENTO");
			_precio_compra = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
			_facturar = Format.DataReader.GetBool(source, "FACTURAR");
			_fecha_validez = Format.DataReader.GetDateTime(source, "FECHA_VALIDEZ");

		}
		public virtual void CopyValues(ClientProductRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_producto = source.OidProducto;
			_oid_cliente = source.OidCliente;
			_precio = source.Precio;
			_facturacion_bulto = source.FacturacionBulto;
			_p_descuento = source.PDescuento;
			_tipo_descuento = source.TipoDescuento;
			_precio_compra = source.PrecioCompra;
			_facturar = source.Facturar;
			_fecha_validez = source.FechaValidez;
		}

		#endregion
	}
}