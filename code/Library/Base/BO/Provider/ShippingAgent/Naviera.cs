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

namespace moleQule.Library.Store
{
	[Serializable()]
	public class ShippingCompanyBase
	{
		#region Attributes

		protected moleQule.Store.Data.ShippingCompanyRecord _record = new moleQule.Store.Data.ShippingCompanyRecord();
        protected ProviderBase _provider_base = new ProviderBase();

		#endregion

		#region Properties

		public moleQule.Store.Data.ShippingCompanyRecord Record { get { return _record; } }
        public ProviderBase ProviderBase { get { return _provider_base; } }

		#endregion

		#region Business Methods

		public void CopyValues(IDataReader source)
		{
            if (source == null) return;

			_provider_base.Record = _record;

            _record.CopyValues(source);

            _provider_base.OidAcreedor = _record.Oid;

            string oid = ((long)(_record.TipoAcreedor + 1)).ToString("00") + "00000" + Format.DataReader.GetInt64(source, "OID").ToString();
            _record.Oid = Convert.ToInt64(oid);

            _provider_base.CopyCommonValues(source);
		}
		public void CopyValues(Naviera source)
		{
            if (source == null) return;

            _record.CopyValues(source.Base.Record);
            _provider_base.CopyCommonValues(source);

            _provider_base.OidAcreedor = _record.Oid;
		}
		public void CopyValues(NavieraInfo source)
		{
            if (source == null) return;

            _record.CopyValues(source.Base.Record);
            _provider_base.CopyCommonValues(source);

            _provider_base.OidAcreedor = _record.Oid;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
	public class Naviera : BusinessBaseEx<Naviera>, IAcreedor, ITitular
    {
		#region IUser

		public virtual long OidUser { get { return _base.ProviderBase.OidUser; } set { _base.ProviderBase.OidUser = value; } }
		public virtual string Username { get { return _base.ProviderBase.Username; } set { _base.ProviderBase.Username = value; } }
		public virtual EEstadoItem EUserStatus { get { return _base.ProviderBase.EUserStatus; } set { _base.ProviderBase.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.ProviderBase.UserStatusLabel; } }
		public virtual DateTime CreationDate { get { return _base.ProviderBase.CreationDate; } set { _base.ProviderBase.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _base.ProviderBase.LastLoginDate; } set { _base.ProviderBase.LastLoginDate = value; } }

		#endregion

        #region IAcreedor

		public ProviderBase ProviderBase { get { return _base.ProviderBase; } }

		public virtual long TipoAcreedor { get { return _base.Record.TipoAcreedor; } set { _base.Record.TipoAcreedor = value; } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_base.Record.TipoAcreedor; } set { _base.Record.TipoAcreedor = (long)value; } }
        public virtual IAcreedor IClone () { return Clone() as IAcreedor; }
        public virtual IAcreedor ISave() { return Save() as IAcreedor; }
        public virtual IAcreedor ISave(Payment item) { return Save(item) as IAcreedor; }
		public virtual IAcreedorInfo IGetInfo() { return GetInfo(false) as IAcreedorInfo; }

        #endregion

        #region ITitular

        public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Proveedor; } }

        #endregion

		#region Attributes

		ShippingCompanyBase _base = new ShippingCompanyBase();
		
		private PrecioTrayectos _precio_trayectos = PrecioTrayectos.NewChildList();
        private Payments _pagos = Payments.NewChildList();

        #endregion

        #region Properties

		public ShippingCompanyBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string ID
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Identificador;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

                if (!_base.Record.Identificador.Equals(value))
				{
                    _base.Record.Identificador = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoId
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoId;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoId.Equals(value))
				{
					_base.Record.TipoId = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nombre;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Nombre.Equals(value))
				{
					_base.Record.Nombre = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Alias
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Alias;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Alias.Equals(value))
				{
					_base.Record.Alias = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CodPostal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodPostal;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodPostal.Equals(value))
				{
					_base.Record.CodPostal = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Localidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Localidad;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Localidad.Equals(value))
				{
					_base.Record.Localidad = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Municipio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Municipio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Municipio.Equals(value))
				{
					_base.Record.Municipio = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Provincia
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Provincia;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Provincia.Equals(value))
				{
					_base.Record.Provincia = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Telefono
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Telefono;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Telefono.Equals(value))
				{
					_base.Record.Telefono = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Pais
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Pais;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Pais.Equals(value))
				{
					_base.Record.Pais = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long MedioPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.MedioPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.MedioPago.Equals(value))
				{
					_base.Record.MedioPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long FormaPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FormaPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FormaPago.Equals(value))
				{
					_base.Record.FormaPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long DiasPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.DiasPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.DiasPago.Equals(value))
				{
					_base.Record.DiasPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaBancaria
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaBancaria;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaBancaria.Equals(value))
				{
					_base.Record.CuentaBancaria = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Swift
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Swift;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Swift.Equals(value))
				{
					_base.Record.Swift = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Contacto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Contacto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Contacto.Equals(value))
				{
					_base.Record.Contacto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Email
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Email;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Email.Equals(value))
				{
					_base.Record.Email = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Direccion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Direccion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Direccion.Equals(value))
				{
					_base.Record.Direccion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCuentaBAsociada
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaBAsociada;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaBAsociada.Equals(value))
				{
					_base.Record.OidCuentaBAsociada = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaContable.Equals(value))
				{
					_base.Record.CuentaContable = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidImpuesto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidImpuesto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidImpuesto.Equals(value))
				{
					_base.Record.OidImpuesto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Estado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidTarjetaAsociada
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTarjetaAsociada;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidTarjetaAsociada.Equals(value))
				{
					_base.Record.OidTarjetaAsociada = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PIRPF;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PIRPF.Equals(value))
				{
					_base.Record.PIRPF = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual ProductoProveedores Productos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.ProviderBase.Productos;
			}
		}
		public virtual Payments Pagos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.ProviderBase.Pagos;
			}
		}
		public virtual PrecioTrayectos PrecioTrayectos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _precio_trayectos;
			}
		}

        //NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.ProviderBase.EStatus; } set { _base.ProviderBase.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.ProviderBase.StatusLabel; } }
		public virtual long OidAcreedor { get { return _base.ProviderBase.OidAcreedor; } set { _base.ProviderBase.OidAcreedor = value; } }
		public virtual string CuentaAsociada { get { return _base.ProviderBase.CuentaAsociada; } set { _base.ProviderBase.CuentaAsociada = value; } }
		public virtual EFormaPago EFormaPago { get { return _base.ProviderBase.EFormaPago; } set { FormaPago = (long)value; } }
		public virtual EMedioPago EMedioPago { get { return _base.ProviderBase.EMedioPago; } set { MedioPago = (long)value; } }
		public virtual string FormaPagoLabel { get { return _base.ProviderBase.FormaPagoLabel; } }
		public virtual string MedioPagoLabel { get { return _base.ProviderBase.MedioPagoLabel; } }
		public virtual string Impuesto { get { return _base.ProviderBase.Impuesto; } }
		public virtual decimal PImpuesto { get { return _base.ProviderBase.PImpuesto; } }
		public virtual string TarjetaAsociada { get { return _base.ProviderBase.TarjetaAsociada; } set { _base.ProviderBase.TarjetaAsociada = value; } }

		public override bool IsValid
		{
			get { return base.IsValid
							&& _base.ProviderBase.Productos.IsValid
							&& _precio_trayectos.IsValid
							&& _base.ProviderBase.Pagos.IsValid;
        }
		}		
		public override bool IsDirty
		{
			get { return base.IsDirty
					|| _base.ProviderBase.Productos.IsDirty
					|| _precio_trayectos.IsDirty
					|| _base.ProviderBase.Pagos.IsDirty;
            }
        }

        #endregion

        #region Business Methods

		public virtual Naviera CloneAsNew()
		{
			Naviera clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.Codigo = (0).ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			clon.SessionCode = Naviera.OpenSession();
			Naviera.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.PrecioTrayectos.MarkAsNew();
            clon.Pagos.MarkAsNew();
			
			return clon;
		}

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_base.CopyValues(source);
			Oid = Format.DataReader.GetInt64(source, "OID");
		}
		protected void CopyValues(Naviera source)
		{
			if (source == null) return;

            _base.CopyValues(source);
            Oid = source.Oid;
		}

        public virtual void SetImpuesto(ImpuestoInfo source)
        {
            if (source == null)
            {
                OidImpuesto = 0;
                _base.ProviderBase.Impuesto = moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto);
                _base.ProviderBase.PImpuesto = 0;
            }
            else
            {
                OidImpuesto = source.Oid;
                _base.ProviderBase.Impuesto = source.Nombre;
                _base.ProviderBase.PImpuesto = source.Porcentaje;
            }
        }

        public virtual void GetNewCode()
        {
            _base.Record.Serial = SerialInfo.GetNext(typeof(Naviera));
            _base.Record.Codigo = _base.Record.Serial.ToString(Resources.Defaults.NAVIERA_CODE_FORMAT);
        }		
				
		#endregion
		 
	    #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //Nombre
            if (Nombre == string.Empty)
            {
                e.Description = Resources.Messages.NO_NAME;
                throw new iQValidationException(e.Description, string.Empty, "Nombre");
            }

            AgenteBase.ValidateInput((ETipoID)TipoId, "ID", ID);
            
            return true;
        }
		 
		#endregion

		#region Autorization Rules

		public static bool CanAddObject()
		{
			return ProviderBase.CanAddObject();
		}

		public static bool CanGetObject()
		{
			return ProviderBase.CanAddObject();
		}

		public static bool CanDeleteObject()
		{
			return ProviderBase.CanAddObject();
		}

		public static bool CanEditObject()
		{
			return ProviderBase.CanAddObject();
		}

		public static bool CanEditCuentaContable()
		{
			return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE);
		}

		#endregion
		 
		#region Common Factory Methods

		private Naviera(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}

		internal static Naviera GetChild(int sessionCode, IDataReader reader) { return new Naviera(sessionCode, reader); }

		public virtual NavieraInfo GetInfo() { return GetInfo(true); }
		public virtual NavieraInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new NavieraInfo(this, childs);
		}

        public void LoadChilds(Type type, bool get_childs)
        {
            if (type.Equals(typeof(Payment)))
            {
                _base.ProviderBase.Pagos = Payments.GetChildList(this, get_childs);
            }
        }

		#endregion

		#region Root Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero es protected por exigencia de NHibernate.
		/// </summary>
		protected Naviera () {}			
		
		public static Naviera New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Naviera>(new CriteriaCs(-1));
		}
			
		public static Naviera Get(long oid)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Naviera.GetCriteria(Naviera.OpenSession());
		
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Naviera.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
			
			Naviera.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Naviera>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			ProviderBase.IsPosibleDelete(oid, ETipoAcreedor.Naviera);

			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Naviera. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Naviera.OpenSession();
			ISession sess = Naviera.Session(sessCode);
			ITransaction trans = Naviera.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from moleQule.Store.Data.ShippingCompanyRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Naviera.CloseSession(sessCode);
			}
		}
		
		public override Naviera Save()
		{
			// Por la posible doble interfaz Root/Child
            if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

				_base.ProviderBase.Productos.Update(this);
				_base.ProviderBase.Pagos.Update(this);
				_precio_trayectos.Update(this);
				
				Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                if (CloseSessions) CloseSession(); 
				else BeginTransaction();
            }
        }

        public virtual Naviera Save(Payment item)
        {
            // Por la posible doble interfaz Root/Child
            if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

                //_proveedor_base._producto_proveedores.Update(this);
                _base.ProviderBase.Pagos.Update(this, item);
                //_precio_trayectos.Update(this);

                Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                if (CloseSessions) CloseSession();
                else BeginTransaction();
            }
        }
				
		#endregion
		
		#region Common Data Access
		 
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			Oid = (long)(new Random().Next());
            GetNewCode();

			ETipoAcreedor = ETipoAcreedor.Naviera;
			EMedioPago = EMedioPago.Efectivo;
			EFormaPago = EFormaPago.Contado;

			_precio_trayectos = PrecioTrayectos.NewChildList();			
		}
		 
		#endregion
		 
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Naviera.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;

						ProductoProveedor.DoLOCK(Session());
						query = ProductoProveedores.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_base.ProviderBase.Productos = ProductoProveedores.GetChildList(SessionCode, reader);

						PrecioTrayecto.DoLOCK(Session());
						query = Store.PrecioTrayectos.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _precio_trayectos = PrecioTrayectos.GetChildList(reader);

                        //Pago.DoLOCK(Session());
                        //query = Pagos.SELECT(this);
                        //reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        //_proveedor_base._pagos = Pagos.GetChildList(SessionCode, reader);
					
 					}
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		//Fetch independiente de DataPortal para generar un Naviera a partir de un IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				CopyValues(source);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		 
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
				//si hay codigo o serial, hay que obtenerlos aquí
                GetNewCode();
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					moleQule.Store.Data.ShippingCompanyRecord obj = Session().Get<moleQule.Store.Data.ShippingCompanyRecord>(Oid);
					obj.CopyValues(Base.Record);
					Session().Update(obj);
				}
				catch (Exception ex)
				{
					iQExceptionHandler.TreatException(ex);
				}
			}
		}
		
		//Deferred deletion
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				SessionCode = OpenSession();
				BeginTransaction();

				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);

				moleQule.Store.Data.ShippingCompanyRecord obj = (moleQule.Store.Data.ShippingCompanyRecord)(criterio.UniqueResult());
				_base.Record.CopyValues(obj);

                _base.ProviderBase.Productos = ProductoProveedores.GetChildList(this, false);
                _base.ProviderBase.Productos.Clear();
                _base.ProviderBase.Productos.Update(this);

				Session().Delete(obj);
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion
		 
		#region Commands
		 
		#endregion

        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, true); }

        internal static string SELECT(long oid, bool lockTable) { return ProviderBaseInfo.SELECT_BASE(oid, ETipoAcreedor.Naviera, lockTable); }

        #endregion
    }
}

