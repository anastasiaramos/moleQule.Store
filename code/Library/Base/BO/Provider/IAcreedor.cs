using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
	/// Agente Acreedor
	/// </summary>
	public interface IAcreedor : IUser
	{
		ProviderBase ProviderBase { get; }

		long Oid { get; set; }
		long OidAcreedor { get; set; }
		long TipoAcreedor { get; set; }
        ETipoAcreedor ETipoAcreedor { get; set; }
		long Serial { get; set; }
		string Codigo { get; set; }
		long Estado { get; set; }
		string ID { get; set; }
		long TipoId { get; set; }
		string Nombre { get; set; }
		string Alias { get; set; }
		string CodPostal { get; set; }
		string Direccion { get; set; }
		string Localidad { get; set; }
		string Municipio { get; set; }
		string Provincia { get; set; }
		string Telefono { get; set; }
		string Observaciones { get; set; }
		long DiasPago { get; set; }
		long FormaPago { get; set; }
		long MedioPago { get; set; }
		string CuentaBancaria { get; set; }
		long OidCuentaBAsociada { get; set; }
		string Pais { get; set; }
		string Contacto { get; set; }
		string Email { get; set; }
		string CuentaContable { get; set; }
		long OidImpuesto { get; set; }
		long OidTarjetaAsociada { get; set; }
		decimal PIRPF { get; set; }

		ProductoProveedores Productos { get; }
		Payments Pagos { get; }

        string CuentaAsociada { get; }
        string TarjetaAsociada { get; }
        string Impuesto { get; }
        decimal PImpuesto { get; }

		bool CloseSessions { get; set; }
		int SessionCode { get; }
        ISession Session();

        void BeginEdit();
        void ApplyEdit();
        void CancelEdit();
        void CloseSession();
        IAcreedor IClone();
        IAcreedor ISave();
        IAcreedor ISave(Payment item);
        IAcreedorInfo IGetInfo();
        void NewTransaction();
        ITransaction Transaction();

        void LoadChilds(Type type, bool childs);
	}

    public interface IAcreedorInfo : IUser
    {
		ProviderBase ProviderBase { get; }

        long Oid { get; }
		long OidAcreedor { get; }
        long TipoAcreedor { get; }
        ETipoAcreedor ETipoAcreedor { get; }
		string ETipoAcreedorLabel { get; }
        long Serial { get;  }
        string Codigo { get; }
		long Estado { get; }
        string Nombre { get; }
        string Alias { get; }
        string ID { get; }
        long TipoId { get; }
        string CodPostal { get; }
        string Direccion { get; }
        string Localidad { get; }
        string Municipio { get; }
        string Provincia { get; }
        string Telefono { get;  }
        string Observaciones { get; }
        long DiasPago { get;  }
        long FormaPago { get; }
        long MedioPago { get; }
        string CuentaBancaria { get; }
        long OidCuentaBAsociada { get; }
        string Pais { get; }
        string Contacto { get; }
        string Email { get; }
        string CuentaContable { get; }
        long OidImpuesto { get; }
        long OidTarjetaAsociada { get; }
        decimal PIRPF { get; }

		ProductoProveedorList Productos { get; }
		PaymentList Pagos { get; }

        string CuentaAsociada { get; }
        string TarjetaAsociada { get; }
        string Impuesto { get; }
        decimal PImpuesto { get; }

		Decimal GetPrecio(ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo);
		Decimal GetDescuento(ProductInfo producto, BatchInfo partida);

		void LoadChilds(Type type, bool get_childs);
    }

    /// <summary>
    /// Agente Acreedor
    /// </summary>
    public interface IAcreedorPrint
    {
		long Oid { get; }
		long OidAcreedor { get; }
        long TipoAcreedor { get; }
        ETipoAcreedor ETipoAcreedor { get; set;}
        string Codigo { get; }
        string Nombre { get; }
        Payments Pagos { get; }
    }
}