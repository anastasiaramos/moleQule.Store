using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
	public class ProviderBase
	{
		#region IAcreedor

		//public virtual long TipoAcreedor { get { return _record.TipoAcreedor; } set { _record.TipoAcreedor = value; } }
        //public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAcreedor; } 
        //    set { _record.TipoAcreedor = (long)value; } }
        public ProductoProveedores Productos { get { return _producto_proveedores; } set { _producto_proveedores = value; } }
        public Payments Pagos { get { return _pagos; } set { _pagos = value; } }
		public int SessionCode { get { return -1; } }
		public bool CloseSessions { get; set; }	

		public virtual IAcreedor IClone() { return null; }
        public virtual IAcreedor ISave() { return null; }
        public virtual IAcreedor ISave(Payment item) { return null; }
		public virtual IAcreedorInfo IGetInfo() { return (IAcreedorInfo)GetInfo(false); }
		public void BeginEdit() {}
		public void ApplyEdit() { }
		public void CancelEdit() { }
		public void CloseSession() { }
		public ISession Session() { return null; }

		#endregion

		#region Attributes

		protected ProviderBaseRecord _record = new ProviderBaseRecord();
		private User _user = User.New();

		protected long _oid_acreedor;
		protected string _cuenta_asociada = string.Empty;
		protected string _tarjeta_asociada = string.Empty;
		protected string _impuesto = string.Empty;
		protected decimal _p_impuesto;

		public ProductoProveedores _producto_proveedores = Library.Store.ProductoProveedores.NewChildList();
		public Payments _pagos = Library.Store.Payments.NewChildList();

		public ProductoProveedorList _producto_proveedores_list = null;
		public PaymentList _pagos_list = null;

		#endregion

		#region Properties

		public ProviderBaseRecord Record { get { return _record; } set { _record = value; } }

		public long OidAcreedor { get { return _oid_acreedor; } set { _oid_acreedor = value; } }
		public EEstado EStatus { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		public string StatusLabel { get { return Base.EnumText<EEstado>.GetLabel(EStatus); } }
		public ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAcreedor; } }
		public string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		public EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } set { _record.FormaPago = (long)value; } }
		public string FormaPagoLabel { get { return moleQule.Common.Structs.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
		public EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } set { _record.MedioPago = (long)value; } }
		public string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public string CuentaAsociada { get { return _cuenta_asociada; } set { _cuenta_asociada = value; } }
		public string Impuesto { get { return (_record.OidImpuesto != 0) ? _impuesto : moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } set { _impuesto = value; } }
		public decimal PImpuesto { get { return _p_impuesto; } set { _p_impuesto = value; } }
        public string TarjetaAsociada { get { return _tarjeta_asociada; } set { _tarjeta_asociada = value; } }

		// IUser
		public virtual long OidUser { get { return _user.Oid; } set { _user.Oid = value; } }
		public virtual string Username { get { return _user.Name; } set { _user.Name = value; } }
		public virtual EEstadoItem EUserStatus { get { return _user.EEstado; } set { _user.EEstado = value; } }
		public virtual string UserStatusLabel { get { return _user.EstadoLabel; } }
		public virtual DateTime CreationDate { get { return _user.CreationDate; } set { _user.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _user.LastLoginDate; } set { _user.LastLoginDate = value; } }

		#endregion

		#region Business Methods

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_oid_acreedor = _record.Oid;

			string oid = ((long)(_record.TipoAcreedor + 1)).ToString("00") + "00000" + Format.DataReader.GetInt64(source, "OID").ToString();
			_record.Oid = Convert.ToInt64(oid);

			CopyCommonValues(source);
		}
		public virtual void CopyValues(ProviderBase source)
		{
			if (source == null) return;

			_record.CopyValues(source.Record);
			CopyCommonValues(source);
		}
		public virtual void CopyValues(IAcreedor source)
		{
			if (source == null) return;

			_record.CopyValues(source.ProviderBase.Record);
			CopyCommonValues(source);
		}
		public virtual void CopyValues(IAcreedorInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.ProviderBase.Record);
			CopyCommonValues(source);
		}

		public void CopyCommonValues(IDataReader source)
		{
            if (source == null) return;

			_cuenta_asociada = Format.DataReader.GetString(source, "CUENTA_ASOCIADA");
			_tarjeta_asociada = Format.DataReader.GetString(source, "TARJETA_ASOCIADA");
			_impuesto = Format.DataReader.GetString(source, "IMPUESTO");
			_p_impuesto = Format.DataReader.GetDecimal(source, "P_IMPUESTO");

			//IUser
			_user.Oid = Format.DataReader.GetInt64(source, "OID_USER");
			_user.Name = Format.DataReader.GetString(source, "USERNAME");
			_user.Estado = Format.DataReader.GetInt64(source, "USER_STATUS");
			_user.CreationDate = Format.DataReader.GetDateTime(source, "CREATION_DATE");
			_user.LastLoginDate = Format.DataReader.GetDateTime(source, "LAST_LOGIN_DATE");
		}
		public void CopyCommonValues(ProviderBase source)
		{
			if (source == null) return;

			_oid_acreedor = source.OidAcreedor;
			_cuenta_asociada = source.CuentaAsociada;
			_tarjeta_asociada = source.TarjetaAsociada;
			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;

			//IUser
			_user.Oid = source.OidUser;
			_user.Name = source.Username;
			_user.EEstado = source.EUserStatus;
			_user.CreationDate = source.CreationDate;
			_user.LastLoginDate = source.LastLoginDate;
		}
		public void CopyCommonValues(IAcreedor source)
		{
			if (source == null) return;

			_oid_acreedor = source.OidAcreedor;
			_cuenta_asociada = source.CuentaAsociada;
			_tarjeta_asociada = source.TarjetaAsociada;
			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;

			//IUser
			_user.Oid = source.OidUser;
			_user.Name = source.Username;
			_user.EEstado = source.EUserStatus;
			_user.CreationDate = source.CreationDate;
			_user.LastLoginDate = source.LastLoginDate;
		}
		public void CopyCommonValues(IAcreedorInfo source)
		{
			if (source == null) return;

			_oid_acreedor = source.OidAcreedor;
			_cuenta_asociada = source.CuentaAsociada;
			_tarjeta_asociada = source.TarjetaAsociada;
			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;

			//IUser
			_user.Oid = source.OidUser;
			_user.Name = source.Username;
			_user.EEstado = source.EUserStatus;
			_user.CreationDate = source.CreationDate;
			_user.LastLoginDate = source.LastLoginDate;
		}

		public Decimal GetPrecio(ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo)
		{
			long oid_producto = (producto != null) ? producto.Oid : partida.OidProducto;

			producto = (producto != null) ? producto : ProductInfo.Get(oid_producto, false, true);
			ProductoProveedorInfo producto_prov = _producto_proveedores_list.GetItemByProducto(oid_producto);

			Decimal precio = producto.GetPrecioCompra(producto_prov, partida, tipo);

			return precio;
		}
		public Decimal GetDescuento(ProductInfo producto, BatchInfo partida)
		{
			long oid_producto = (producto != null) ? producto.Oid : partida.Oid;
			ProductoProveedorInfo producto_cliente = _producto_proveedores_list.GetItemByProducto(oid_producto);

			return ProductoProveedorInfo.GetDescuentoProveedor(producto_cliente, 0);
		}

		public static ETipoAcreedor GetProviderType(long entityType) { return moleQule.Store.Structs.EnumConvert.ToETipoAcreedor((ETipoEntidad)AppContext.User.EntityType); }
		public static ETipoAcreedor GetProviderType(ETipoEntidad entityType) { return GetProviderType((long)entityType); }

		#endregion

		#region Autorization Rules

		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.PROVEEDOR);
		}
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.PROVEEDOR);
		}
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.PROVEEDOR);
		}
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.PROVEEDOR);
		}

		public static bool CanEditCuentaContable()
		{
			return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE);
		}

		public static void IsPosibleDelete(long oid, ETipoAcreedor providerType)
		{
			QueryConditions conditions = new QueryConditions
			{
                TipoAcreedor = new ETipoAcreedor[1] { providerType },
				Acreedor = ProviderBaseInfo.New(oid, providerType),
				Estado = EEstado.NoAnulado
			};

			InputDeliveryList albaranes = InputDeliveryList.GetList(conditions, false);

			if (albaranes.Count > 0)
				throw new iQException(Resources.Messages.ALBARANES_ASOCIADOS);

			InputInvoiceList facturas = InputInvoiceList.GetList(conditions, false);

			if (facturas.Count > 0)
				throw new iQException(Resources.Messages.FACTURAS_ASOCIADAS);

			conditions.PaymentType = ETipoPago.Factura;

			PaymentList pagos = PaymentList.GetList(conditions, false);

			if (pagos.Count > 0)
				throw new iQException(Resources.Messages.PAGOS_ASOCIADOS);
		}

		#endregion
				
		#region Common Factory Methods

		public static ProviderBase New() { return new ProviderBase(); }
		public static ProviderBase New(IAcreedorInfo source)
		{
			ProviderBase obj = new ProviderBase();
			obj.CopyValues(source);

			return obj;
		}
		public static ProviderBase New(IAcreedor source)
		{
			ProviderBase obj = new ProviderBase();
			obj.CopyValues(source);

			return obj;
		}

		public virtual ProviderBaseInfo GetInfo(bool childs) { return new ProviderBaseInfo(this, childs); }
	
		#endregion		
	}

	public struct TProviderBase
	{
		public bool Active { get { return Table != string.Empty; } }
		public ETipoAcreedor Tipo;
		public string Table;
	}
}

