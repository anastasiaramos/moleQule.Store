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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InputOrderRecord : RecordBase
    {
        #region Attributes

        private long _oid_usuario;
        private long _oid_acreedor;
        private long _serial;
        private string _codigo = string.Empty;
        private DateTime _fecha;
        private long _estado;
        private long _tipo_acreedor;
        private string _observaciones = string.Empty;
        private long _oid_serie;
        private Decimal _p_descuento;
        private Decimal _descuento;
        private Decimal _base_imponible;
        private Decimal _impuestos;
        private Decimal _total;
        private long _oid_expediente;
        private long _oid_almacen;

        #endregion

        #region Properties
        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
        public virtual long OidAcreedor { get { return _oid_acreedor; } set { _oid_acreedor = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual long TipoAcreedor { get { return _tipo_acreedor; } set { _tipo_acreedor = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
        public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
        public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
        public virtual Decimal BaseImponible { get { return _base_imponible; } set { _base_imponible = value; } }
        public virtual Decimal Impuestos { get { return _impuestos; } set { _impuestos = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }

        #endregion

        #region Business Methods

        public InputOrderRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
            _oid_acreedor = Format.DataReader.GetInt64(source, "OID_ACREEDOR");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _tipo_acreedor = Format.DataReader.GetInt64(source, "TIPO_ACREEDOR");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
            _descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
            _base_imponible = Format.DataReader.GetDecimal(source, "BASE_IMPONIBLE");
            _impuestos = Format.DataReader.GetDecimal(source, "IMPUESTOS");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");

        }

        public virtual void CopyValues(InputOrderRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_usuario = source.OidUsuario;
            _oid_acreedor = source.OidAcreedor;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _fecha = source.Fecha;
            _estado = source.Estado;
            _tipo_acreedor = source.TipoAcreedor;
            _observaciones = source.Observaciones;
            _oid_serie = source.OidSerie;
            _p_descuento = source.PDescuento;
            _descuento = source.Descuento;
            _base_imponible = source.BaseImponible;
            _impuestos = source.Impuestos;
            _total = source.Total;
            _oid_expediente = source.OidExpediente;
            _oid_almacen = source.OidAlmacen;
        }
        #endregion
    }

	[Serializable()]
	public class InputOrderBase
	{
		#region Attributes

        private InputOrderRecord _record = new InputOrderRecord();
        
		internal string _usuario = string.Empty;
		internal string _n_serie = string.Empty;
		internal string _serie = string.Empty;
		internal string _id_acreedor = string.Empty;
		internal string _acreedor = string.Empty;
		internal bool _id_manual = false;
		internal string _expediente = string.Empty;
		internal string _almacen = string.Empty;
		internal string _id_almacen = string.Empty;

		#endregion

		#region Properties

        public InputOrderRecord Record { get { return _record; } set { _record = value; } }

		internal EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		internal string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAcreedor; } set { _record.TipoAcreedor = (long)value; } }
		internal Decimal Subtotal { get { return _record.BaseImponible + _record.Descuento; } }
		internal virtual string NSerieSerie { get { return _n_serie + " - " + _serie; } }
		internal virtual string IDAlmacenAlmacen { get { return _id_almacen + " - " + _almacen; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_usuario = Format.DataReader.GetString(source, "USUARIO");
			_id_acreedor = Format.DataReader.GetString(source, "CODIGO_ACREEDOR");
			_acreedor = Format.DataReader.GetString(source, "ACREEDOR");
			_serie = Format.DataReader.GetString(source, "SERIE");
			_n_serie = Format.DataReader.GetString(source, "N_SERIE");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
			_almacen = Format.DataReader.GetString(source, "ALMACEN");
			_id_almacen = Format.DataReader.GetString(source, "ID_ALMACEN");
		}
		internal void CopyValues(PedidoProveedor source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_id_acreedor = source.IDAcreedor;
			_acreedor = source.Acreedor;
			_usuario = source.Usuario;
			_n_serie = source.NSerie;
			_serie = source.Serie;
			_expediente = source.Expediente;
			_almacen = source.Almacen;
			_id_almacen = source.IDAlmacen;
		}
		internal void CopyValues(PedidoProveedorInfo source)
		{
            if (source == null) return;

            _record.CopyValues(source._base.Record);

			_id_acreedor = source.IDAcreedor;
			_acreedor = source.Acreedor;
			_usuario = source.Usuario;
			_n_serie = source.NSerie;
			_serie = source.Serie;
			_expediente = source.Expediente;
			_almacen = source.Almacen;
			_id_almacen = source.IDAlmacen;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class PedidoProveedor : BusinessBaseEx<PedidoProveedor>
	{	 
		#region Attributes

		public InputOrderBase _base = new InputOrderBase();

		private LineaPedidoProveedores _lineas = LineaPedidoProveedores.NewChildList();

		#endregion
		
		#region Properties

		public InputOrderBase Base { get { return _base; } }

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
				}
			}
		}
		public virtual long OidUsuario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUsuario;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);


				if (!_base.Record.OidUsuario.Equals(value))
				{
					_base.Record.OidUsuario = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidSerie
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidSerie;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidSerie.Equals(value))
				{
					_base.Record.OidSerie = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidAlmacen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlmacen;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidAlmacen.Equals(value))
				{
					_base.Record.OidAlmacen = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidExpediente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpediente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidExpediente.Equals(value))
				{
					_base.Record.OidExpediente = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAcreedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);


				if (!_base.Record.OidAcreedor.Equals(value))
				{
					_base.Record.OidAcreedor = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoAcreedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);


				if (!_base.Record.TipoAcreedor.Equals(value))
				{
					_base.Record.TipoAcreedor = value;
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
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PDescuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PDescuento;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.PDescuento.Equals(value))
				{
					_base.Record.PDescuento = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Descuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descuento;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Descuento.Equals(value))
				{
					_base.Record.Descuento = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal BaseImponible
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BaseImponible;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.BaseImponible.Equals(value))
				{
					_base.Record.BaseImponible = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Impuestos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Impuestos;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Impuestos.Equals(value))
				{
					_base.Record.Impuestos = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Total
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Total;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Total.Equals(value))
				{
					_base.Record.Total = Decimal.Round(value, 2);
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

		public virtual LineaPedidoProveedores Lineas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _lineas;
			}
		}

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { _base.EEstado = value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { _base.ETipoAcreedor = value; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string NSerie { get { return _base._n_serie; } set { _base._n_serie = value; } }
		public virtual string Serie { get { return _base._serie; } set { _base._serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string IDAcreedor { get { return _base._id_acreedor; } set { _base._id_acreedor = value; } }
		public virtual string Acreedor { get { return _base._acreedor; } set { _base._acreedor = value; } }
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual string IDAlmacen { get { return _base._id_almacen; } set { _base._id_almacen = value; } }
		public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }

		public override bool IsValid
		{
			get { return base.IsValid
						 && _lineas.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lineas.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual PedidoProveedor CloneAsNew()
		{
			PedidoProveedor clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.Codigo = (0).ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			
			clon.SessionCode = PedidoProveedor.OpenSession();
			PedidoProveedor.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Lineas.MarkAsNew();
			
			return clon;
		}
        
		public virtual void CopyFrom(PedidoProveedorInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}
		public virtual void CopyFrom(IAcreedorInfo source)
		{
			if (source == null) return;

			OidAcreedor = source.OidAcreedor;
			TipoAcreedor = source.TipoAcreedor;
			Acreedor = source.Nombre;
			IDAcreedor = source.Codigo;
		}

		public virtual void GetNewCode()
		{
			// Obtenemos el último serial de servicio
			Serial = SerialInfo.GetNextByYear(typeof(PedidoProveedor), Fecha.Year);

			Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
		}

		protected virtual void SetNewCode()
		{
			try
			{
				PedidoProveedorList list = PedidoProveedorList.GetList(ETipoAcreedor.Todos, Fecha.Year, false);

				if (list.GetItemByProperty("Codigo", Codigo) != null)
					throw new iQException("Número de Pedido de Proveedor duplicado");
				
				Serial = Convert.ToInt64(Codigo);
			}
			catch
			{
				throw new iQException("Número de Pedido de Proveedor incorrecto." + System.Environment.NewLine +
										"Debe tener el formato " + Resources.Defaults.FACTURA_CODE_FORMAT);
			}
		}

		public virtual void CalculaTotal()
		{
			BaseImponible = 0;
			Descuento = 0;
			Impuestos = 0;
			Total = 0;

			foreach (LineaPedidoProveedor linea in Lineas)
			{
				if (!linea.IsKitComponent)
				{
					linea.CalculateTotal();

					BaseImponible += linea.BaseImponible;
					Impuestos += linea.Impuestos;
				}
			}

			Descuento = BaseImponible * PDescuento / 100;
			BaseImponible -= Descuento;
			Total = BaseImponible + Impuestos;			
		}

		public virtual void SetAlmacen(StoreInfo source)
		{
			foreach (LineaPedidoProveedor item in Lineas)
			{
				item.OidAlmacen = source.Oid;
				item.Almacen = source.Nombre;
			}
		}
		public virtual void SetExpediente(ExpedientInfo source)
		{
			foreach (LineaPedidoProveedor item in Lineas)
			{
				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;
			}
		}

		#endregion
		 
	    #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			//Codigo
			if (Codigo == string.Empty)
			{
				e.Description = Resources.Messages.NO_ID_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Serie
			if (OidSerie == 0)
			{
				e.Description = Resources.Messages.NO_SERIE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//OidAcreedor
			if (OidAcreedor == 0)
			{
				e.Description = Resources.Messages.NO_ACREEDOR_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			return true;
		}

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

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected PedidoProveedor() {}		
		private PedidoProveedor(PedidoProveedor source, bool retrieve_childs)
        {
			MarkAsChild();
			Childs = retrieve_childs;
            Fetch(source);
        }
        private PedidoProveedor(int sessionCode, IDataReader source, bool retrieve_childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();	
			Childs = retrieve_childs;
            Fetch(source);
        }

		public static PedidoProveedor NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<PedidoProveedor>(new CriteriaCs(-1));
		}
		
		internal static PedidoProveedor GetChild(PedidoProveedor source)
		{
			return new PedidoProveedor(source, false);
		}
		internal static PedidoProveedor GetChild(PedidoProveedor source, bool childs)
		{
			return new PedidoProveedor(source, childs);
		}
        internal static PedidoProveedor GetChild(int sessionCode, IDataReader source)
        {
            return new PedidoProveedor(sessionCode, source, false);
        }
		internal static PedidoProveedor GetChild(int sessionCode, IDataReader source, bool childs)
        {
			return new PedidoProveedor(sessionCode, source, childs);
        }
		
		public virtual PedidoProveedorInfo GetInfo() { return GetInfo(true); }
		public virtual PedidoProveedorInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			return new PedidoProveedorInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static PedidoProveedor New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<PedidoProveedor>(new CriteriaCs(-1));
		}
		
		public static PedidoProveedor Get(long oid, ETipoAcreedor tipo) { return Get(oid, tipo, true); }
		public static PedidoProveedor Get(long oid, ETipoAcreedor tipo, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = PedidoProveedor.GetCriteria(PedidoProveedor.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = PedidoProveedor.SELECT(oid, tipo);
			
			PedidoProveedor.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<PedidoProveedor>(criteria);
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
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los PedidoProveedor. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = PedidoProveedor.OpenSession();
			ISession sess = PedidoProveedor.Session(sessCode);
			ITransaction trans = PedidoProveedor.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from InputOrderRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				PedidoProveedor.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override PedidoProveedor Save()
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
				
				_lineas.Update(this);				
				
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
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			GetNewCode();
			Fecha = DateTime.Now;
			EEstado = EEstado.Abierto;
			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;
			OidSerie = Library.Store.ModulePrincipal.GetDefaultSerieSetting();
			OidAlmacen = Library.Store.ModulePrincipal.GetDefaultAlmacenSetting();
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(PedidoProveedor source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);
	
				if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {
                        
						LineaPedidoProveedor.DoLOCK(Session());
                        string query = LineaPedidoProveedores.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = LineaPedidoProveedores.GetChildList(reader, false);						
                    }
                }
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
                _base.CopyValues(source);

                if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {
                        
						LineaPedidoProveedor.DoLOCK(Session());
                        string query = LineaPedidoProveedores.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = LineaPedidoProveedores.GetChildList(reader, false);
						
                    }
                }
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
		internal void Insert(PedidoProveedores parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

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
		internal void Update(PedidoProveedores parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				InputOrderRecord obj = Session().Get<InputOrderRecord>(Oid);
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
		internal void DeleteSelf(PedidoProveedores parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<InputOrderRecord>(Oid));
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
					PedidoProveedor.DoLOCK( Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						LineaPedidoProveedor.DoLOCK( Session());
						query = LineaPedidoProveedores.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_lineas = LineaPedidoProveedores.GetChildList(reader);						
 					} 
				}
				MarkOld();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
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
				if (!IDManual)
					GetNewCode();
				else
					SetNewCode();

                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
				
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
					if (IDManual) SetNewCode();

					InputOrderRecord obj = Session().Get<InputOrderRecord>(Oid);
					obj.CopyValues(this._base.Record);
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
				Session().Delete((InputOrderRecord)(criterio.UniqueResult()));
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
		
		#region SQL

		public static ProviderBaseInfo.SelectLocalCaller local_caller = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE);

		public static string SELECT(long oid, ETipoAcreedor tipo) { return SELECT(oid, tipo, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT PP.*" +
					"       ,COALESCE(A.\"NOMBRE\", '') AS \"ACREEDOR\"" +
					"		,COALESCE(A.\"CODIGO\", '') AS \"CODIGO_ACREEDOR\"" +
					"		,US.\"NAME\" AS \"USUARIO\"" +
					"       ,SE.\"IDENTIFICADOR\" AS \"N_SERIE\"" +
					"       ,SE.\"NOMBRE\" AS \"SERIE\"" +
					"       ,COALESCE(AL.\"CODIGO\", '') AS \"ID_ALMACEN\"" +
					"       ,COALESCE(AL.\"NOMBRE\", '') AS \"ALMACEN\"" +
					"       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\""; 

			return query;
		}

		internal static string INNER_ACREEDOR(ETipoAcreedor tipo)
		{
			string query = string.Empty;

			if (tipo != ETipoAcreedor.Todos)
				query = " INNER JOIN " + ProviderBaseInfo.TABLE(tipo) + " AS A ON PP.\"OID_ACREEDOR\" = A.\"OID\"";
			else
				query = " LEFT JOIN " + ProviderBaseInfo.TABLE(ETipoAcreedor.Proveedor) + " AS A ON AP.\"OID_ACREEDOR\" = A.\"OID\"";

			return query;
		}

		internal static string WHERE(Library.Store.QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query += " WHERE (PP.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "PP");

			if (conditions.PedidoProveedor != null)
			{
				if (conditions.PedidoProveedor.Oid != 0)
					query += " AND PP.\"OID\" = " + conditions.PedidoProveedor.Oid;
			}

			if ((conditions.Acreedor != null) && (conditions.Acreedor.OidAcreedor != 0)) query += " AND PP.\"OID_ACREEDOR\" = " + conditions.Acreedor.OidAcreedor;
			if ((conditions.TipoAcreedor[0] != ETipoAcreedor.Todos)) query += " AND PP.\"TIPO_ACREEDOR\" = " + (long)conditions.TipoAcreedor[0];

			if (conditions.Almacen != null) query += " AND PP.\"OID_ALMACEN\" = " + conditions.Almacen.Oid;
			if (conditions.Expedient != null) query += " AND PP.\"OID_EXPEDIENTE\" = " + conditions.Expedient.Oid;
			if (conditions.User != null) query += " AND PP.\"OID_USUARIO\" = " + conditions.User.Oid;

			return query;
		}

		internal static string SELECT_BASE(Library.Store.QueryConditions conditions, ETipoAcreedor tipo)
		{
			string pp = nHManager.Instance.GetSQLTable(typeof(InputOrderRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));

			string query = string.Empty;

			query = SELECT_FIELDS() +
					" FROM " + pp + " AS PP" +
					" INNER JOIN " + se + " AS SE ON SE.\"OID\" = PP.\"OID_SERIE\"" +
					" LEFT JOIN " + us + " AS US ON US.\"OID\" = PP.\"OID_USUARIO\"" +
					" LEFT JOIN " + al + " AS AL ON AL.\"OID\" = PP.\"OID_ALMACEN\"" +
					" LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = PP.\"OID_EXPEDIENTE\"" +
					INNER_ACREEDOR(tipo);

			return query;
		}

		internal static string SELECT(Library.Store.QueryConditions conditions)
		{
			string query = string.Empty;

			switch (conditions.TipoAlbaranes)
			{
				case ETipoAlbaranes.Todos:
				case ETipoAlbaranes.Facturados:
				case ETipoAlbaranes.NoFacturados:
					{
						if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
							query = ProviderBaseInfo.SELECT_BUILDER(local_caller, conditions);
						else
						{
							query = SELECT_BASE(conditions, conditions.TipoAcreedor[0]);
							query += WHERE(conditions);
						}
					}
					break;
			}

			query += " ORDER BY \"FECHA\", \"CODIGO\"";

			return query;
		}

		internal static string SELECT_PENDIENTES(Library.Store.QueryConditions conditions, bool lockTable)
		{
            string lpc = nHManager.Instance.GetSQLTable(typeof(InputOrderLineRecord));
            string cap = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));

			string query = string.Empty;

			conditions.Estado = EEstado.NoAnulado;

			query = SELECT_BASE(conditions, conditions.TipoAcreedor[0]);

			query += " INNER JOIN (SELECT LPC.\"OID_PEDIDO\"" +
					 "				FROM " + lpc + " AS LPC" +
					 "				WHERE (LPC.\"OID\", LPC.\"CANTIDAD\") NOT IN (SELECT \"OID_LINEA_PEDIDO\"" +
					 "																	,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
					 "																FROM " + cap + " AS CAP" +
					 "																WHERE CAP.\"OID_LINEA_PEDIDO\" != 0" +
					 "																GROUP BY CAP.\"OID_LINEA_PEDIDO\")" +
					 "				GROUP BY LPC.\"OID_PEDIDO\")" +
					 "		AS LPC ON LPC.\"OID_PEDIDO\" = PP.\"OID\"";

			query += WHERE(conditions);

			query += " ORDER BY PP.\"FECHA\", PP.\"CODIGO\"";

			if (lockTable) query += " FOR UPDATE OF PP NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, ETipoAcreedor tipo, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { PedidoProveedor = PedidoProveedor.New().GetInfo(false) };
			conditions.PedidoProveedor.Oid = oid;
			conditions.TipoAcreedor[0] = tipo;

			query = SELECT_BASE(conditions, tipo);
			
			query += WHERE(conditions);

			if (lockTable) query += " FOR UPDATE OF PP NOWAIT";

			return query;
		}

		public static string UPDATE_TIPO(QueryConditions conditions)
		{
            string ap = nHManager.Instance.GetSQLTable(typeof(InputOrderRecord));
			string query = string.Empty;

			query = "UPDATE " + ap + " AS PP SET \"TIPO_ACREEDOR\" = " + conditions.Acreedor.TipoAcreedor.ToString();

			query += WHERE(conditions);

			return query;
		}

		#endregion		
	}
}

