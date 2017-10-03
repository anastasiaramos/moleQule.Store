using System;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class StockRecord : RecordBase
	{
		#region Attributes

		private long _oid_partida;
		private long _oid_expediente;
		private long _oid_producto;
		private long _oid_albaran;
		private long _oid_concepto_albaran;
		private long _oid_stock;
		private long _oid_kit;
		private string _concepto = string.Empty;
		private Decimal _bultos;
		private Decimal _kilos;
		private DateTime _fecha;
		private string _observaciones = string.Empty;
		private long _tipo;
		private bool _inicial = false;
		private long _oid_linea_pedido;
		private long _oid_almacen;
		private int _oid_usuario;
		private long _oid_enlace;

		#endregion

		#region Properties

		public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
		public virtual long OidConceptoAlbaran { get { return _oid_concepto_albaran; } set { _oid_concepto_albaran = value; } }
		public virtual long OidStock { get { return _oid_stock; } set { _oid_stock = value; } }
		public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual Decimal Bultos { get { return _bultos; } set { _bultos = value; } }
		public virtual Decimal Kilos { get { return _kilos; } set { _kilos = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual bool Inicial { get { return _inicial; } set { _inicial = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
		public virtual long OidLineaPedido { get { return _oid_linea_pedido; } set { _oid_linea_pedido = value; } }
		public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
		public virtual long OidEnlace { get { return _oid_almacen; } set { _oid_enlace = value; } }
		public virtual int OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }

		#endregion

		#region Business Methods

		public StockRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			//_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
			_oid_concepto_albaran = Format.DataReader.GetInt64(source, "OID_CONCEPTO_ALBARAN");
			_oid_stock = Format.DataReader.GetInt64(source, "OID_STOCK");
			_oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
			_concepto = Format.DataReader.GetString(source, "CONCEPTO");
			_bultos = Format.DataReader.GetDecimal(source, "BULTOS");
			_kilos = Format.DataReader.GetDecimal(source, "KILOS");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_tipo = Format.DataReader.GetInt64(source, "TIPO");
			_inicial = Format.DataReader.GetBool(source, "INICIAL");
			_oid_linea_pedido = Format.DataReader.GetInt64(source, "OID_LINEA_PEDIDO");
			_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_oid_enlace = Format.DataReader.GetInt64(source, "OID_ENLACE");
			_oid_usuario = Format.DataReader.GetInt32(source, "OID_USUARIO");

		}
		public virtual void CopyValues(StockRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_partida = source.OidPartida;
			_oid_expediente = source.OidExpediente;
			_oid_producto = source.OidProducto;
			_oid_albaran = source.OidAlbaran;
			_oid_concepto_albaran = source.OidConceptoAlbaran;
			_oid_stock = source.OidStock;
			_oid_kit = source.OidKit;
			_concepto = source.Concepto;
			_bultos = source.Bultos;
			_kilos = source.Kilos;
			_fecha = source.Fecha;
			_observaciones = source.Observaciones;
			_tipo = source.Tipo;
			_inicial = source.Inicial;
			_oid_linea_pedido = source.OidLineaPedido;
			_oid_almacen = source.OidAlmacen;
			_oid_enlace = source.OidEnlace;
			_oid_usuario = source.OidUsuario;
		}

		#endregion
	}
}