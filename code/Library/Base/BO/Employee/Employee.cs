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
	public class EmployeeBase
	{
		#region Attributes

		protected EmployeeRecord _record = new EmployeeRecord();
        protected ProviderBase _provider_base = new ProviderBase();

		#endregion

		#region Properties

		public EmployeeRecord Record { get { return _record; } }
        public ProviderBase ProviderBase { get { return _provider_base; } }

		public decimal CostByHour { get { return (_record.SueldoBruto + _record.Seguro + _record.Descuentos) / 24 / 8; } }
        public virtual EPayrollMethod EPayrollMethod { get { return (EPayrollMethod)_record.PayrollMethod; } }
        public virtual string PayrollMethodLabel { get { return moleQule.Store.Structs.EnumText<EPayrollMethod>.GetLabel(EPayrollMethod); } }

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
			_provider_base.Record = Record;
		}
		public void CopyValues(Employee source)
		{
            if (source == null) return;

            _record.CopyValues(source.Base.Record);
            _provider_base.CopyCommonValues(source);

            _provider_base.OidAcreedor = _record.Oid;
		}
		public void CopyValues(EmployeeInfo source)
		{
            if (source == null) return;

            _record.CopyValues(source.Base.Record);
            _provider_base.CopyCommonValues(source);

            _provider_base.OidAcreedor = _record.Oid;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Employee : BusinessBaseEx<Employee>, IAcreedor, IWorkResource
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

		public virtual long TipoAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.TipoAcreedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.TipoAcreedor.Equals(value))
				{
                    _base.Record.TipoAcreedor = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_base.Record.TipoAcreedor; } set { _base.Record.TipoAcreedor = (long)value; } }
        public virtual IAcreedor IClone() { return Clone() as IAcreedor; }
        public virtual IAcreedor ISave() { return Save() as IAcreedor; }
        public virtual IAcreedor ISave(Payment item) { return Save(item) as IAcreedor; }
		public virtual IAcreedorInfo IGetInfo() { return GetInfo(false) as IAcreedorInfo; }

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

        #endregion

		#region IWorkResource

		public long EntityType { get { return (long)moleQule.Common.Structs.ETipoEntidad.Empleado; } }
		public moleQule.Common.Structs.ETipoEntidad EEntityType { get { return moleQule.Common.Structs.ETipoEntidad.Empleado; } }
		public string Name { get { return Apellidos + ", " + Nombre; } }
		public decimal Cost { get { return _base.CostByHour; } }

		#endregion

		#region Attributes

		EmployeeBase _base = new EmployeeBase();

		#endregion
		
		#region Properties

		public EmployeeBase Base { get { return _base; } }

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
                    _base.Record.PIRPF = Decimal.Round(value, 2);
                    SueldoNeto = _base.Record.BaseIrpf - Decimal.Round(_base.Record.BaseIrpf * _base.Record.PIRPF / 100, 2) - _base.Record.Descuentos - _base.Record.Seguro;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Apellidos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Apellidos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Apellidos.Equals(value))
				{
					_base.Record.Apellidos = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Foto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Foto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Foto.Equals(value))
				{
					_base.Record.Foto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Perfil
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Perfil;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Perfil.Equals(value))
				{
					_base.Record.Perfil = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime InicioContrato
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.InicioContrato;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.InicioContrato.Equals(value))
				{
					_base.Record.InicioContrato = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FinContrato
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FinContrato;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FinContrato.Equals(value))
				{
					_base.Record.FinContrato = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string NivelEstudios
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.NivelEstudios;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.NivelEstudios.Equals(value))
				{
					_base.Record.NivelEstudios = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal SueldoBruto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.SueldoBruto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.SueldoBruto.Equals(value))
                {
                    _base.Record.SueldoBruto = Decimal.Round(value, 2);
                    BaseIRPF = _base.Record.BaseIrpf == 0 ? value : _base.Record.BaseIrpf;
                    CalculateNeto();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal SueldoNeto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.SueldoNeto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.SueldoNeto.Equals(value))
				{
                    _base.Record.SueldoNeto = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal BaseIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BaseIrpf;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.BaseIrpf.Equals(value))
                {
                    _base.Record.BaseIrpf = Decimal.Round(value, 2);
                    CalculateNeto();
                    PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Descuentos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descuentos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Descuentos.Equals(value))
                {
                    _base.Record.Descuentos = Decimal.Round(value, 2);
                    CalculateNeto();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Seguro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Seguro;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Seguro.Equals(value))
				{
                    _base.Record.Seguro = Decimal.Round(value, 2);
                    CalculateNeto();
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCrew
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCrew;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCrew.Equals(value))
				{
					_base.Record.OidCrew = value;
					//PropertyHasChanged();
				}
			}
		}
        public virtual long PayrollMethod
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PayrollMethod;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.PayrollMethod.Equals(value))
                {
                    _base.Record.PayrollMethod = value;
                    PropertyHasChanged();
                }
            }
        }

        //NO ENLAZADAS
        public virtual string NombreCompleto { get { return _base.Record.Apellidos + ", " + _base.Record.Nombre; } }
		public virtual EEstado EEstado { get { return _base.ProviderBase.EStatus; } set { Estado = (long)value; } }
        public virtual string EstadoLabel { get { return _base.ProviderBase.StatusLabel; } }
		public virtual long OidAcreedor { get { return _base.ProviderBase.OidAcreedor; } set { _base.ProviderBase.OidAcreedor = value; } }
        public virtual string CuentaAsociada { get { return _base.ProviderBase.CuentaAsociada; } set { _base.ProviderBase.CuentaAsociada = value; PropertyHasChanged(); } }
        public virtual EFormaPago EFormaPago { get { return _base.ProviderBase.EFormaPago; } set { FormaPago = (long)value; } }
        public virtual EMedioPago EMedioPago { get { return _base.ProviderBase.EMedioPago; } set { MedioPago = (long)value; } }
        public virtual string FormaPagoLabel { get { return _base.ProviderBase.FormaPagoLabel; } }
        public virtual string MedioPagoLabel { get { return _base.ProviderBase.MedioPagoLabel; } }
        public virtual string Impuesto { get { return _base.ProviderBase.Impuesto; } }
        public virtual decimal PImpuesto { get { return _base.ProviderBase.PImpuesto; } }
        public virtual string TarjetaAsociada { get { return _base.ProviderBase.TarjetaAsociada; } set { _base.ProviderBase.TarjetaAsociada = value; PropertyHasChanged(); } }
        public virtual EPayrollMethod EPayrollMethod { get { return _base.EPayrollMethod; } set { PayrollMethod = (long)value; } }
        public virtual string PayrollMethodLabel { get { return _base.PayrollMethodLabel; } }
        
		public override bool IsValid
		{
			get
			{
				return base.IsValid
							&& _base.ProviderBase.Productos.IsValid
							&& _base.ProviderBase.Pagos.IsValid;
			}
		}
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty
					   || _base.ProviderBase.Productos.IsDirty
					   || _base.ProviderBase.Pagos.IsDirty;
			}
		}

		#endregion
		
		#region Business Methods
		
		public virtual Employee CloneAsNew()
		{
			Employee clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.Codigo = (0).ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			
			clon.SessionCode = Employee.OpenSession();
			Employee.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Productos.MarkAsNew();
			clon.Pagos.MarkAsNew();

			return clon;
		}

        protected void CopyValues(Employee source)
        {
            if (source == null) return;

            _base.CopyValues(source);

			//Pte. de quitar de aqui cuando se adapten todos los Acreedores
			_base.Record.Estado = source.Estado;
            Oid = source.Oid;
        }
        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _base.CopyValues(source);
			Oid = Format.DataReader.GetInt64(source, "OID");

            //Pte. de quitar de aqui cuando se adapten todos los Acreedores
            _base.Record.Estado = Format.DataReader.GetInt64(source, "ESTADO");
        }

		protected virtual void CopyFrom(EmployeeInfo source)
		{
            _base.CopyValues(source);

            Oid = source.Oid;
		}

        public void CalculateNeto()
        {
            SueldoNeto = SueldoBruto * Decimal.Round(BaseIRPF * PIRPF / 100, 2) - Descuentos - Seguro;
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
            _base.Record.Serial = SerialInfo.GetNext(typeof(Employee));
            _base.Record.Codigo = _base.Record.Serial.ToString(Resources.Defaults.EMPLEADO_CODE_FORMAT);
        }

		public virtual bool HasProfile(Perfil profile)
		{
			byte bit = Convert.ToByte(Math.Log(Convert.ToDouble((long)profile), 2));
			return ((Perfil >> bit) % 2) == 1;
		}

		#endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EMPLEADO);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EMPLEADO);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EMPLEADO);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EMPLEADO);
		}

		public static bool CanEditCuentaContable()
		{
			return AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
                TipoAcreedor = new ETipoAcreedor[1] { ETipoAcreedor.Empleado },
				Acreedor = ProviderBaseInfo.New(oid, ETipoAcreedor.Empleado),
				Estado = EEstado.NoAnulado,
				CategoriaGasto = ECategoriaGasto.Nomina
			};

			ExpenseList gastos = ExpenseList.GetList(conditions, false);

			if (gastos.Count > 0)
				throw new iQException(Resources.Messages.NOMINAS_ASOCIADAS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Employee () {}		
		private Employee(Employee source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Employee(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Employee NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Employee>(new CriteriaCs(-1));
		}
		
		internal static Employee GetChild(Employee source, bool childs = false)
		{
            return new Employee(source, childs);
		}
        internal static Employee GetChild(int sessionCode, IDataReader source)
        {
            return new Employee(sessionCode, source, false);
        }
		internal static Employee GetChild(int sessionCode, IDataReader source, bool childs)
        {
            return new Employee(sessionCode, source, childs);
        }
		
		public virtual EmployeeInfo GetInfo (bool childs = true)
		{			
			return new EmployeeInfo(this, childs);
		}

        public void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(Payment)))
            {
                _base.ProviderBase.Pagos = Payments.GetChildList(this, childs);
            }
        }
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Employee New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Employee>(new CriteriaCs(-1));
		}
		
		public static Employee Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Employee.GetCriteria(Employee.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Employee.SELECT(oid);
			
			Employee.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Employee>(criteria);
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

			ProviderBase.IsPosibleDelete(oid, ETipoAcreedor.Empleado);
			IsPosibleDelete(oid);

			EmployeeInfo item = EmployeeInfo.Get(oid, false);

			//Se elimina la foto
			Images.Delete(item.Foto, ModuleController.FOTOS_EMPLEADOS_PATH);

			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Empleado. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Employee.OpenSession();
			ISession sess = Employee.Session(sessCode);
			ITransaction trans = Employee.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from EmployeeRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Employee.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Employee Save()
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

				Transaction().Commit();
				return this;
			}
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                // Se abre la nueva transacción para bloquear el objeto
                if (CloseSessions) CloseSession(); 
				else BeginTransaction();
            }
        }

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public virtual Employee Save(Payment item)
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

                Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                // Se abre la nueva transacción para bloquear el objeto
                if (CloseSessions) CloseSession();
                else BeginTransaction();
            }
        }
				
		#endregion				
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			Random r = new Random();
			Oid = (long)r.Next();
			ETipoAcreedor = ETipoAcreedor.Empleado;
			GetNewCode();
            EPayrollMethod = EPayrollMethod.Month;  
	    }
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Employee source)
		{
            try
            {
                SessionCode = source.SessionCode;

                CopyValues(source);				
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
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

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Employees parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();
				GetNewCode();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Employees parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				EmployeeRecord obj = Session().Get<EmployeeRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Employees parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<EmployeeRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>Lo llama el DataPortal tras generar el objeto</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Employee.DoLOCK( Session());
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

						/*Pago.DoLOCK(Session());
						query = Pagos.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_proveedor_base._pagos = Pagos.GetChildList(reader);*/
					} 
				}
				else
				{
					Session().Lock(Session().Get<EmployeeRecord>(Oid), LockMode.UpgradeNoWait);
					CopyValues((Employee)(criteria.UniqueResult()));
				}
				MarkOld();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
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
                GetNewCode();
				
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					EmployeeRecord obj = Session().Get<EmployeeRecord>(Oid);
					obj.CopyValues(Base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					iQExceptionHandler.TreatException(ex);
				}
			}
		}
		
		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		/// <summary>
		/// Elimina un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal</remarks>
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
					
				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((EmployeeRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion		
		
		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		internal static string SELECT(long oid, bool lockTable) { return ProviderBaseInfo.SELECT_BASE(oid, ETipoAcreedor.Empleado, lockTable); }
        
        internal static string FIELDS()
        {
            string query = string.Empty;

            query = @"
            SELECT A.""OID"" AS ""OID_AGENTE""
                    ," + (long)ETipoAcreedor.Empleado + @" AS ""TIPO_AGENTE""
                    ,A.""CODIGO""
                    ,A.""NOMBRE"" || ' ' || A.""APELLIDOS"" AS ""NOMBRE""
                    ,A.""OBSERVACIONES"" AS ""OBSERVACIONES_ACREEDOR""
                    ,COALESCE(""TOTAL_FACTURADO"",0) AS ""TOTAL_FACTURADO""
                    ,0 AS ""TOTAL_ESTIMADO""
                    ,COALESCE(""TOTAL_PAGADO"",0) AS ""TOTAL_PAGADO""
                    ,COALESCE(""EFECTOS_NEGOCIADOS"",0) AS ""EFECTOS_NEGOCIADOS""
                    ,COALESCE(""EFECTOS_DEVUELTOS"",0) AS ""EFECTOS_DEVUELTOS""
                    ,COALESCE(""EFECTOS_PTES_VTO"",0) AS ""EFECTOS_PTES_VTO""";

            return query;
        }

        internal static string JOIN_PAGOS(ETipoAcreedor tipo)
        {
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PaymentRecord));
            string py = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string query = string.Empty;

            query = @"
            LEFT JOIN (	SELECT ""OID_EMPLEADO""
                                ,SUM(""NETO"") AS ""TOTAL_FACTURADO""
                        FROM " + py + @"
                        WHERE ""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY ""OID_EMPLEADO"")
                AS N ON N.""OID_EMPLEADO"" = A.""OID""
            LEFT JOIN (	SELECT ""OID_AGENTE""
                                ,""TIPO_AGENTE""
                                ,SUM(""TOTAL_PAGADO"") AS ""TOTAL_PAGADO""
                        FROM (SELECT PG.""OID_AGENTE""
                                    ,PG.""TIPO_AGENTE""
                                    ,SUM(PG.""IMPORTE"") AS ""TOTAL_PAGADO""
                                FROM " + pa + @" AS PG
                                WHERE PG.""TIPO_AGENTE"" = " + (long)ETipoAcreedor.Empleado + @"
                                    AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
                                    AND PG.""VENCIMIENTO"" <= '" + QueryConditions.GetFechaLabel(DateTime.Today) + "'" + @"
                                    AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                    AND PG.""OID"" NOT IN ( SELECT PO.""OID_PAGO""
                                                            FROM " + tp + @" AS PO
                                                            INNER JOIN " + py + @" AS NM ON NM.""OID"" = PO.""OID_OPERACION""
                                                            WHERE PO.""TIPO_PAGO"" = " + (long)ETipoPago.Nomina + @"
                                                                AND NM.""OID_EMPLEADO"" = PG.""OID_AGENTE"")
                        GROUP BY PG.""OID_AGENTE"", PG.""TIPO_AGENTE""";

            query +=
                    "                UNION" +
                    "                    SELECT N.\"OID_EMPLEADO\" AS \"OID_AGENTE\"" +
                    "                        , P.\"TIPO_AGENTE\"" +
                    "                        , SUM(PO.\"CANTIDAD\") AS \"TOTAL_PAGADO\"" +
                    "                    FROM " + pa + " AS P" +
                    "                    INNER JOIN " + tp + " AS PO ON PO.\"OID_PAGO\" = P.\"OID\"" +
                    "                    INNER JOIN " + py + " AS N ON N.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                    WHERE   P.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND P.\"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " AND P.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                        AND PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina +
                    "                    GROUP BY N.\"OID_EMPLEADO\", P.\"TIPO_AGENTE\"" +
                    "                    )	AS P" +
                    "                GROUP BY \"OID_AGENTE\", \"TIPO_AGENTE\")" +
                    "            AS P1 ON P1.\"OID_AGENTE\" = A.\"OID\"" +
                    "        LEFT JOIN (	SELECT \"OID_AGENTE\"" +
                    "                    ,\"TIPO_AGENTE\"" +
                    "                    , SUM(\"EFECTOS_NEGOCIADOS\") AS \"EFECTOS_NEGOCIADOS\"" +
                    "                FROM (" +
                    "                    SELECT PG.\"OID_AGENTE\"" +
                    "                        , PG.\"TIPO_AGENTE\"" +
                    "                        ,SUM(PG.\"IMPORTE\") AS \"EFECTOS_NEGOCIADOS\"" +
                    "                    FROM " + pa + " AS PG" +
                    "                    WHERE PG.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND PG.\"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " AND PG.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND PG.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                       AND PG.\"OID\" NOT IN ( SELECT PO.\"OID_PAGO\"" +
                    "                                               FROM " + tp + " AS PO" +
                    "                                               INNER JOIN " + py + " AS NM ON NM.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                                               WHERE PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina + " AND NM.\"OID_EMPLEADO\" = PG.\"OID_AGENTE\")" +
                    "                    GROUP BY PG.\"OID_AGENTE\", PG.\"TIPO_AGENTE\"" +
                    "                    UNION" +
                    "                    SELECT N.\"OID_EMPLEADO\" AS \"OID_AGENTE\"" +
                    "                        , P.\"TIPO_AGENTE\"" +
                    "                        , SUM(PO.\"CANTIDAD\") AS \"EFECTOS_NEGOCIADOS\"" +
                    "                    FROM " + pa + " AS P" +
                    "                    INNER JOIN " + tp + " AS PO ON PO.\"OID_PAGO\" = P.\"OID\"" +
                    "                    INNER JOIN " + py + "  AS N ON N.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                    WHERE   P.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND P.\"ESTADO_PAGO\" = " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                        AND PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina +
                    "                    GROUP BY N.\"OID_EMPLEADO\", P.\"TIPO_AGENTE\"" +
                    "                    )	AS P" +
                    "                GROUP BY \"OID_AGENTE\", \"TIPO_AGENTE\")" +
                    "            AS P2 ON P2.\"OID_AGENTE\" = A.\"OID\"" +
                    "        LEFT JOIN (	SELECT \"OID_AGENTE\"" +
                    "                    ,\"TIPO_AGENTE\"" +
                    "                    , SUM(\"EFECTOS_DEVUELTOS\") AS \"EFECTOS_DEVUELTOS\"" +
                    "                FROM (" +
                    "                    SELECT PG.\"OID_AGENTE\"" +
                    "                        , PG.\"TIPO_AGENTE\"" +
                    "                        ,SUM(PG.\"IMPORTE\") AS \"EFECTOS_DEVUELTOS\"" +
                    "                    FROM " + pa + " AS PG" +
                    "                    WHERE PG.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND PG.\"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " AND PG.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND PG.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                       AND PG.\"OID\" NOT IN ( SELECT PO.\"OID_PAGO\"" +
                    "                                               FROM " + tp + " AS PO" +
                    "                                               INNER JOIN " + py + " AS NM ON NM.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                                               WHERE PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina + " AND NM.\"OID_EMPLEADO\" = PG.\"OID_AGENTE\")" +
                    "                    GROUP BY PG.\"OID_AGENTE\", PG.\"TIPO_AGENTE\"" +
                    "                    UNION" +
                    "                    SELECT N.\"OID_EMPLEADO\" AS \"OID_AGENTE\"" +
                    "                        , P.\"TIPO_AGENTE\"" +
                    "                        , SUM(PO.\"CANTIDAD\") AS \"EFECTOS_DEVUELTOS\"" +
                    "                    FROM " + pa + " AS P" +
                    "                    INNER JOIN " + tp + " AS PO ON PO.\"OID_PAGO\" = P.\"OID\"" +
                    "                    INNER JOIN " + py + "  AS N ON N.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                    WHERE   P.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND \"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                        AND PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina +
                    "                    GROUP BY N.\"OID_EMPLEADO\", P.\"TIPO_AGENTE\"" +
                    "                    )	AS P" +
                    "                GROUP BY \"OID_AGENTE\", \"TIPO_AGENTE\")" +
                    "            AS P3 ON P3.\"OID_AGENTE\" = A.\"OID\"" +
                    "        LEFT JOIN (	SELECT \"OID_AGENTE\"" +
                    "                    ,\"TIPO_AGENTE\"" +
                    "                    , SUM(\"EFECTOS_PTES_VTO\") AS \"EFECTOS_PTES_VTO\"" +
                    "                FROM (" +
                    "                    SELECT PG.\"OID_AGENTE\"" +
                    "                        , PG.\"TIPO_AGENTE\"" +
                    "                        ,SUM(PG.\"IMPORTE\") AS \"EFECTOS_PTES_VTO\"" +
                    "                    FROM " + pa + " AS PG" +
                    "                    WHERE PG.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND PG.\"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " AND PG.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND PG.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                       AND PG.\"OID\" NOT IN ( SELECT PO.\"OID_PAGO\"" +
                    "                                               FROM " + tp + " AS PO" +
                    "                                               INNER JOIN " + py + " AS NM ON NM.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                                               WHERE PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina + " AND NM.\"OID_EMPLEADO\" = PG.\"OID_AGENTE\")" +
                    "                    GROUP BY PG.\"OID_AGENTE\", PG.\"TIPO_AGENTE\"" +
                    "                    UNION" +
                    "                    SELECT N.\"OID_EMPLEADO\" AS \"OID_AGENTE\"" +
                    "                        , P.\"TIPO_AGENTE\"" +
                    "                        , SUM(PO.\"CANTIDAD\") AS \"EFECTOS_PTES_VTO\"" +
                    "                    FROM " + pa + " AS P" +
                    "                    INNER JOIN " + tp + " AS PO ON PO.\"OID_PAGO\" = P.\"OID\"" +
                    "                    INNER JOIN " + py + "  AS N ON N.\"OID\" = PO.\"OID_OPERACION\"" +
                    "                    WHERE   P.\"TIPO_AGENTE\" = " + (long)ETipoAcreedor.Empleado +
                    "                        AND \"ESTADO_PAGO\" != " + (long)EEstado.Pagado + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "                        AND P.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "                        AND PO.\"TIPO_PAGO\" = " + (long)ETipoPago.Nomina +
                    "                    GROUP BY N.\"OID_EMPLEADO\", P.\"TIPO_AGENTE\"" +
                    "                    )	AS P" +
                    "                GROUP BY \"OID_AGENTE\", \"TIPO_AGENTE\")" +
                    "            AS P4 ON P4.\"OID_AGENTE\" = A.\"OID\"";

            return query;
        }

        public static string SELECT_PAGOS_NOMINAS() { return SELECT_PAGOS_NOMINAS(new QueryConditions()); }
        public static string SELECT_PAGOS_NOMINAS(QueryConditions conditions)
        {
            string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
            string nv = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ShippingCompanyRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.TransporterRecord));
            string dp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.CustomAgentRecord));

            //if (conditions.TipoAcreedor[0] == ETipoAcreedor.Empleado) return string.Empty;

            string query = string.Empty;

            query =
            FIELDS() + @"
            FROM " + ProviderBaseInfo.TABLE(ETipoAcreedor.Empleado) + @" AS A" +
            JOIN_PAGOS(ETipoAcreedor.Empleado) + @" 
            WHERE A.""TIPO"" = " + (long)ETipoAcreedor.Empleado;

            if (conditions.Acreedor != null && conditions.Acreedor.Oid != 0)
                query += @"
                AND A.""OID"" =  " + (long)conditions.Acreedor.Oid;

            return query;
        }
        
		#endregion		
	}
}