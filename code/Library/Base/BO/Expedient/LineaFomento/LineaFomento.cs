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
using moleQule.Invoice.Structs;
using moleQule;
using moleQule.CslaEx;
using moleQule.Common;
using moleQule.Hipatia;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class LineaFomentoBase
	{
		#region Attributes

        private LineaFomentoRecord _record = new LineaFomentoRecord();

		//NO ENLAZADAS
		internal Decimal _subvencion;
		internal string _id_partida = string.Empty;
		internal string _producto = string.Empty;
		internal string _id_expediente = string.Empty;
		internal string _naviera = string.Empty;
        internal DateTime _fecha_naviera;
		internal string _id_factura = string.Empty;
		internal string _codigo_aduanero = string.Empty;
		internal string _contenedor = string.Empty;
        internal long _tipo_expediente;

        internal decimal _asignado;
        internal string _activo = Resources.Labels.SET_PAGO;
        internal decimal _acumulado = 0;
        internal DateTime _fecha_asignacion;
        internal decimal _importe_cobrado = 0;
        internal DateTime _fecha_cobro;
        internal string _id_cobro = string.Empty;
        internal bool _cobrado = false;

		#endregion

		#region Properties

        public LineaFomentoRecord Record { get { return _record; } set { _record = value; } }

		internal virtual EEstado EEstado { get { return _record.Estado == (long)EEstado.Abierto && _cobrado ? EEstado.Charged : (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		internal virtual string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal virtual string Teus
		{
			get
			{
				if (_record.Teus20) return Resources.Labels.TEUS20;
				else return Resources.Labels.TEUS40;
			}

			set
			{
				if (value == Resources.Labels.TEUS20)
				{
					_record.Teus20 = true;
					_record.Teus40 = false;
				}
				else
				{
					_record.Teus40 = true;
					_record.Teus20 = false;
				}
			}
		}
		internal Decimal T3 { get { return _record.T3Origen + _record.T3Destino; } }
		internal Decimal THC { get { return _record.ThcOrigen + _record.ThcDestino; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_subvencion = Format.DataReader.GetDecimal(source, "SUBVENCION");
			_id_partida = Format.DataReader.GetString(source, "ID_PARTIDA");
			_id_expediente = Format.DataReader.GetString(source, "ID_EXPEDIENTE");
			_contenedor = Format.DataReader.GetString(source, "CONTENEDOR");
			_producto = Format.DataReader.GetString(source, "PRODUCTO");
			_codigo_aduanero = Format.DataReader.GetString(source, "CODIGO_ADUANERO");
			_naviera = Format.DataReader.GetString(source, "NAVIERA");
            _fecha_naviera = Format.DataReader.GetDateTime(source, "FECHA_NAVIERA");
            _tipo_expediente = Format.DataReader.GetInt64(source, "TIPO_EXPEDIENTE");
			//_id_factura = Format.DataReader.GetString(source, "ID_FACTURA");

            _asignado = Format.DataReader.GetDecimal(source, "ASIGNADO");
            _importe_cobrado = Format.DataReader.GetDecimal(source, "COBRADO");
            _fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
            _id_cobro = Format.DataReader.GetString(source, "ID_COBRO");
            _fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");
            _cobrado = _id_cobro != string.Empty ? true : false;

            _activo = (_asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
            _importe_cobrado = (_importe_cobrado - _asignado) > 0 ? (_importe_cobrado - _asignado) : 0;
		}
		internal void CopyValues(LineaFomento source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_subvencion = source.Subvencion;
			_id_partida = source.IDPartida;
			_id_expediente = source.IDExpediente;
			_producto = source.Producto;
			_naviera = source.Naviera;
            _fecha_naviera = source.FechaNaviera;
			_id_factura = source.IDFactura;
			_codigo_aduanero = source.CodigoAduanero;
		}
		internal void CopyValues(LineaFomentoInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_subvencion = source.Subvencion;
			_id_partida = source.IDPartida;
			_id_expediente = source.IDExpediente;
			_producto = source.Producto;
			_naviera = source.Naviera;
            _fecha_naviera = source.FechaNaviera;
			_id_factura = source.IDFactura;
			_codigo_aduanero = source.CodigoAduanero;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class LineaFomento : BusinessBaseEx<LineaFomento>, IEntidadRegistro, IAgenteHipatia
	{
		#region IAgenteHipatia

		public string NombreHipatia { get { return Codigo + "/" + IDExpediente; } }
		public string IDHipatia { get { return Codigo; } }
		public Type TipoEntidad { get { return typeof(LineaFomento); } }
		public string ObservacionesHipatia { get { return Observaciones; } }

		#endregion

		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.LineaFomento; } }
		public string DescripcionRegistro { get { return "LINEA DE FOMENTO Nº " + Codigo + ". Exp: " + IDExpediente + ". Producto: " + Producto; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public virtual void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			LineaFomentoRecord obj = Session().Get<LineaFomentoRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion
	 
		#region Attributes

		public LineaFomentoBase _base = new LineaFomentoBase();

		#endregion
		
		#region Properties

        public LineaFomentoBase Base { get { return _base; } }

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
                // CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
                    _base.Record.Oid = value;
                    //PropertyHasChanged();
                }
            }
        }

		public virtual long OidPartida
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPartida;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidPartida.Equals(value))
				{
					_base.Record.OidPartida = value;
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
		public virtual long OidNaviera
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidNaviera;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidNaviera.Equals(value))
				{
					_base.Record.OidNaviera = value;
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
		public virtual string Descripcion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descripcion;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Descripcion.Equals(value))
				{
					_base.Record.Descripcion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string IDEnvio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.IdEnvio;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.IdEnvio.Equals(value))
				{
					_base.Record.IdEnvio = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Conocimiento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Conocimiento;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Conocimiento.Equals(value))
				{
					_base.Record.Conocimiento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaConocimiento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaConocimiento;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.FechaConocimiento.Equals(value))
				{
					_base.Record.FechaConocimiento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Teus20
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Teus20;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Teus20.Equals(value))
				{
					_base.Record.Teus20 = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Teus40
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Teus40;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Teus40.Equals(value))
				{
					_base.Record.Teus40 = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Kilos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Kilos;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Kilos.Equals(value))
				{
					_base.Record.Kilos = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal FleteNeto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FleteNeto;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.FleteNeto.Equals(value))
				{
					_base.Record.FleteNeto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal BAF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Baf;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Baf.Equals(value))
				{
					_base.Record.Baf = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal T3Origen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.T3Origen;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.T3Origen.Equals(value))
				{
					_base.Record.T3Origen = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal T3Destino
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.T3Destino;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.T3Destino.Equals(value))
				{
					_base.Record.T3Destino = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal THCOrigen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ThcOrigen;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ThcOrigen.Equals(value))
				{
					_base.Record.ThcOrigen = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal THCDestino
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ThcDestino;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ThcDestino.Equals(value))
				{
					_base.Record.ThcDestino = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal ISPS
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Isps;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Isps.Equals(value))
				{
					_base.Record.Isps = value;
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
					_base.Record.Total = value;
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
		public virtual DateTime FechaSolicitud
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
		public virtual string IDSolicitud
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.IdSolicitud;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.IdSolicitud.Equals(value))
				{
					_base.Record.IdSolicitud = value;
					PropertyHasChanged();
				}
			}
		}	
		public virtual string DUA
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Dua;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Dua.Equals(value))
				{
					_base.Record.Dua = value;
					PropertyHasChanged();
				}
			}
		}

		//NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual Decimal Subvencion { get { return Decimal.Round(_base._subvencion, 2); } set { _base._subvencion = value; } }
		public virtual string IDPartida { get { return _base._id_partida; } set { _base._id_partida = value; } }
		public virtual string IDExpediente { get { return _base._id_expediente; } }
		public virtual string IDFactura { get { return _base._id_factura; } set { _base._id_factura = value; } }
		public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
		public virtual string Naviera { get { return _base._naviera; } set { _base._naviera = value; } }
        public virtual DateTime FechaNaviera { get { return _base._fecha_naviera; } set { _base._fecha_naviera = value; } }
		public virtual string Teus { get { return _base.Teus; } set { _base.Teus = value; PropertyHasChanged(); } }
		public virtual string CodigoAduanero { get { return _base._codigo_aduanero; } set { _base._codigo_aduanero = value; } }
        public virtual long TipoExpediente { get { return _base._tipo_expediente; } }
        public virtual ETipoExpediente ETipoExpediente { get { return (ETipoExpediente)_base._tipo_expediente; } }
        public virtual string ETipoExpedienteLabel { get { return moleQule.Store.Structs.EnumText<ETipoExpediente>.GetLabel(ETipoExpediente); } }

        public virtual decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
        public virtual string Vinculado { get { return _base._activo; } set { _base._activo = value; } }
        public virtual decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
        public virtual string FechaAsignacion
        {
            get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; }
            set { _base._fecha_asignacion = DateTime.Parse(value); }
        }
        public virtual decimal ImporteCobrado { get { return _base._importe_cobrado; } set { _base._importe_cobrado = value; } }
        public virtual decimal TotalAyuda { get { return Decimal.Round(_base._subvencion, 2); } }
        public virtual string FechaCobro
        {
            get { return (_base._fecha_cobro != DateTime.MinValue) ? _base._fecha_cobro.ToShortDateString() : "---"; }
        }
        public virtual string IDCobro { get { return _base._id_cobro; } }
        public virtual bool Cobrado { get { return _base._cobrado; } set { _base._cobrado = value; } }
        public virtual decimal Pendiente { get { return Decimal.Round(_base._subvencion, 2) - _base._asignado; } set { } }
        public virtual int Ano { get { return _base.Record.FechaConocimiento.Year; } }

		#endregion
		
		#region Business Methods
		
		public virtual LineaFomento CloneAsNew()
		{
			LineaFomento clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = LineaFomento.OpenSession();
			LineaFomento.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
        
		protected virtual void CopyFrom(LineaFomentoInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}
		public virtual void CopyFrom(Expedient parent)
        {
            OidExpediente = parent.Oid;
			OidNaviera = parent.OidNaviera;
			Teus20 = parent.Teus20;
			Teus40 = parent.Teus40;

			Naviera = parent.Naviera;
        }
		public virtual void CopyFrom(Batch source) { CopyFrom(source.GetInfo(false)); }
		public virtual void CopyFrom(BatchInfo source)
		{
			if (source == null) return;

			OidPartida = source.Oid;
			OidExpediente = source.OidExpediente;
			Kilos = source.KilosIniciales;
			
			IDPartida = source.Codigo;			
			Producto = source.TipoMercancia;
			CodigoAduanero = source.CodigoAduanero;
		}

        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(LineaFomento));
            Codigo = Serial.ToString(Resources.Defaults.EXPEDIENTE_FOMENTO_CODE_FORMAT);
        }

		public virtual void UpdateTotal()
		{
			Total = FleteNeto + ISPS + BAF + THCDestino + THCOrigen + T3Destino + T3Origen;
		}

		public virtual void SetValues(InputInvoiceInfo factura) { SetValues(factura, 1); }
		public virtual void SetValues(InputInvoiceInfo factura, decimal factor)
		{
			IDFactura = factura.Codigo;
			FechaConocimiento = factura.Fecha;
			Conocimiento = factura.NFactura;

			InputInvoiceLineInfo cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultFleteSetting());
			FleteNeto = (cfp != null) ? cfp.BaseImponible * factor : 0;

			cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultT3OrigenSetting());
			T3Origen = (cfp != null) ? cfp.BaseImponible * factor : 0;

			cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultT3DestinoSetting());
			T3Destino = (cfp != null) ? cfp.BaseImponible * factor : 0;

			cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultTHCOrigenSetting());
			THCOrigen = (cfp != null) ? cfp.BaseImponible * factor : 0;

			cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultTHCDestinoSetting());
			THCDestino = (cfp != null) ? cfp.BaseImponible * factor : 0;

			cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultISPSSetting());
			ISPS = (cfp != null) ? cfp.BaseImponible * factor : 0;

			cfp = factura.Conceptos.GetItemByProducto(ModulePrincipal.GetDefaultBAFSetting());
			BAF = (cfp != null) ? cfp.BaseImponible * factor : 0;

			UpdateTotal();
		}

		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
						
			
			//Propiedad
			/*if (Propiedad <= 0)
			{
				e.Description = string.Format(Resources.Messages.NO_PROPIEDAD_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EXPEDIENTE);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EXPEDIENTE);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EXPEDIENTE);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EXPEDIENTE);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected LineaFomento()
		{
			GetNewCode();
		}
		private LineaFomento(LineaFomento source, bool retrieve_childs)
        {
			MarkAsChild();
			Childs = retrieve_childs;
            Fetch(source);
        }
        private LineaFomento(IDataReader source, bool retrieve_childs)
        {
            MarkAsChild();	
			Childs = retrieve_childs;
            Fetch(source);
        }

		public static LineaFomento NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			LineaFomento obj = DataPortal.Create<LineaFomento>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">ExpedienteFomento con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(ExpedienteFomento source, bool childs)
		/// <remarks/>
		internal static LineaFomento GetChild(LineaFomento source)
		{
			return new LineaFomento(source, false);
		}
		internal static LineaFomento GetChild(LineaFomento source, bool childs)
		{
			return new LineaFomento(source, childs);
		}
        internal static LineaFomento GetChild(IDataReader source)
        {
            return new LineaFomento(source, false);
        }
        internal static LineaFomento GetChild(IDataReader source, bool childs)
        {
            return new LineaFomento(source, childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual LineaFomentoInfo GetInfo() { return GetInfo(true); }	
		public virtual LineaFomentoInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LineaFomentoInfo(this, childs);
		}
		
		#endregion				
		
		#region Root Factory Methods

		public static LineaFomento Get(long oid, bool childs = true) 
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = LineaFomento.GetCriteria(LineaFomento.OpenSession());
			criteria.Childs = childs;

			criteria.Query = LineaFomento.SELECT(oid);

			LineaFomento.BeginTransaction(criteria.Session);

			return DataPortal.Fetch<LineaFomento>(criteria);
		}

		public override LineaFomento Save()
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

		#region Child Factory Methods

		public static LineaFomento NewChild(Expedient parent, Batch source)
		{
			return NewChild(parent, source.GetInfo(false));
		}
		public static LineaFomento NewChild(Expedient parent, BatchInfo source)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			LineaFomento obj = new LineaFomento();
			obj.MarkAsChild();
			obj.CopyFrom(parent);
			obj.CopyFrom(source);
			obj.FechaConocimiento = DateTime.Today;
			return obj;
		}
				
		/// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
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
			FechaConocimiento = DateTime.MinValue;
			EEstado = EEstado.Abierto;			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(LineaFomento source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);		   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(LineasFomento parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
	
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(LineasFomento parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			LineaFomentoRecord obj = Session().Get<LineaFomentoRecord>(Oid);
			obj.CopyValues(this.Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(LineasFomento parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<LineaFomentoRecord>(Oid));
		
			MarkNew(); 
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
					LineaFomento.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);
				}

				MarkOld();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
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
			if (!IsDirty) return;

			LineaFomentoRecord obj = Session().Get<LineaFomentoRecord>(Oid);

			obj.CopyValues(this._base.Record);
			Session().Update(obj);
		}

		#endregion

		#region Child Data Access
		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidExpediente = parent.Oid;	
			
			ValidationRules.CheckRules();
			
			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);			

			MarkOld();
		}

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidExpediente = parent.Oid;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			LineaFomentoRecord obj = parent.Session().Get<LineaFomentoRecord>(Oid);
			obj.CopyValues(this._base.Record);
			parent.Session().Update(obj);			

			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<LineaFomentoRecord>(Oid));

			MarkNew();
		}
		
		#endregion		
		
        #region SQL

		internal enum ETipoQuery { GENERAL = 0, AGRUPADO = 1, PENDIENTES = 2, COBRO = 3 }

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		public static string SELECT(Expedient item) 
		{ 
			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { Expedient = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}			
		
        internal static string FIELDS(ETipoQuery queryType)
        {
            string query = @"
			SELECT LF.*
					,CASE WHEN (COALESCE(AP.""TIPO_AYUDA"", 2) = " + (long)ETipoDescuento.Porcentaje + @")
						THEN (""TOTAL"" * COALESCE(AP.""PORCENTAJE"", 0) / 100)
						ELSE COALESCE(AP.""CANTIDAD"", 0)
						END AS ""SUBVENCION""
					,PT.""CODIGO"" AS ""ID_PARTIDA""
					,PT.""TIPO_MERCANCIA"" AS ""PRODUCTO""
					,PR.""CODIGO_ADUANERO"" AS ""CODIGO_ADUANERO""
					,EX.""CODIGO"" AS ""ID_EXPEDIENTE""
					,EX.""CONTENEDOR"" AS ""CONTENEDOR""
					,EX.""TIPO_EXPEDIENTE""
					,COALESCE(NV.""NOMBRE"", '') AS ""NAVIERA""                  
					,COALESCE(FP.""FECHA_NAVIERA"", NULL) AS ""FECHA_NAVIERA""";

			switch (queryType)
			{ 
				case ETipoQuery.GENERAL:
					query += @"
						,COALESCE(OC.""COBRADO"", 0) AS ""COBRADO""
						,0 AS ""ASIGNADO""
						,COALESCE(OC1.""FECHA_ASIGNACION"", NULL) AS ""FECHA_ASIGNACION""
						,COALESCE(C.""FECHA"", NULL) AS ""FECHA_COBRO""
						,COALESCE(C.""CODIGO"", '') AS ""ID_COBRO""";
					break;

				case ETipoQuery.COBRO:
					query += @"
						,OC.""COBRADO"" AS ""COBRADO""
						,OC1.""CANTIDAD"" AS ""ASIGNADO""
						,OC1.""FECHA_ASIGNACION""
						,C.""FECHA"" AS ""FECHA_COBRO""
						,C.""CODIGO"" AS ""ID_COBRO""";
					break;

				case ETipoQuery.PENDIENTES:
					query += @"
						,0 AS ""COBRADO""     
						,0 AS ""ASIGNADO""
						,NULL AS ""FECHA_ASIGNACION""     
						,NULL AS ""FECHA_COBRO"" 
						,'' AS ""ID_COBRO""";
					break;
			}

            return query;
        }

        internal static string JOIN(QueryConditions conditions)
        {
            string ef = nHManager.Instance.GetSQLTable(typeof(LineaFomentoRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string pt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string ap = nHManager.Instance.GetSQLTable(typeof(GrantPeriodRecord));
            string nv = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ShippingCompanyRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
            string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
            string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));

            long oid_ayuda_fomento = ModulePrincipal.GetAyudaFomentoSetting();

            string query = @"
            FROM " + ef + @" AS LF
			INNER JOIN " + pt + @" AS PT ON PT.""OID"" = LF.""OID_PARTIDA""
            INNER JOIN " + ex + @" AS EX ON EX.""OID"" = LF.""OID_EXPEDIENTE""
            LEFT JOIN " + nv + @" AS NV ON NV.""OID"" = LF.""OID_NAVIERA""
            INNER JOIN " + pr + @" AS PR ON PR.""OID"" = PT.""OID_PRODUCTO""
            LEFT JOIN (SELECT AP.""TIPO_DESCUENTO"" AS ""TIPO_AYUDA""
							,AP.""CANTIDAD"" AS ""CANTIDAD""
							,AP.""PORCENTAJE"" AS ""PORCENTAJE""
							,AP.""FECHA_INI"" AS ""FECHA_INI""
							,AP.""FECHA_FIN"" AS ""FECHA_FIN""
						FROM " + ap + @" AS AP
						WHERE AP.""OID_AYUDA"" = " + oid_ayuda_fomento + @"
						    AND ""ESTADO"" != " + (long)EEstado.Anulado + ")" + @"
				AS AP ON LF.""FECHA_CONOCIMIENTO"" BETWEEN AP.""FECHA_INI"" AND AP.""FECHA_FIN""
			LEFT JOIN (SELECT FP.""OID_ACREEDOR""
                            ,FP.""OID_EXPEDIENTE""
                            ,MIN(FP.""FECHA"") AS ""FECHA_NAVIERA""
						FROM " + fp + @" AS FP
						WHERE FP.""TIPO_ACREEDOR"" = " + (long)ETipoAcreedor.Naviera + @" 
                            AND FP.""ESTADO"" != " + (long)EEstado.Anulado + @"
						GROUP BY FP.""OID_ACREEDOR"", FP.""OID_EXPEDIENTE"")
				AS FP ON FP.""OID_ACREEDOR"" = LF.""OID_NAVIERA"" AND FP.""OID_EXPEDIENTE"" = EX.""OID""";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @"
            WHERE (LF.""FECHA_CONOCIMIENTO"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.OidList != null)
				query += @"
                AND LF.""OID"" IN " + Common.EntityBase.GET_IN_STRING(conditions.OidList);

			if (conditions.ExpedienteFomento != null) 
                query += @"
                AND LF.""OID"" = " + conditions.ExpedienteFomento.Oid;
			
            if (conditions.Partida != null) 
                query += @"
                AND LF.""OID_PARTIDA"" = " + conditions.Partida.Oid;
			
            if ((conditions.Expedient != null) && (conditions.Expedient.Oid != 0)) 
                query += @" 
                AND LF.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;
			
            if (conditions.Naviera != null) 
                query += @"
                AND LF.""OID_NAVIERA"" = " + conditions.Naviera.Oid;

            foreach (IQueryableEntity entity in conditions.Entities)
            {
                switch ((ETipoEntidad)entity.EntityType)
                {
                    case ETipoEntidad.Cobro:

                        query += @"
                        AND OC1.""OID_COBRO"" = " + entity.Oid;

                        break;

                    case ETipoEntidad.Naviera:

                        query += @"
                        AND LF.""OID_NAVIERA"" = " + entity.Oid;

                        break;
                }
            }

            //DEPRECATED
            switch (conditions.EntityType)
            {
                case ETipoEntidad.Cobro:
                    
                    query += @"
                        AND OC1.""OID_COBRO"" = " + conditions.OidEntity;

                    break;

                case ETipoEntidad.Naviera:

                    query += @"
                        AND LF.""OID_NAVIERA"" = " + conditions.OidEntity;            

                    break;
            }

			return query;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string cb = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string oc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OperationChargeRecord));
            
			string query = 
				FIELDS(ETipoQuery.GENERAL) +
                JOIN(conditions) + @"
                LEFT JOIN (SELECT OC.""OID""
								,OC.""OID_OPERATION""
                                ,SUM(OC.""CANTIDAD"") AS ""COBRADO""
							FROM " + oc + @" AS OC
                            INNER JOIN " + cb + @" AS CB ON CB.""OID"" = OC.""OID_COBRO"" 
                                AND CB.""TIPO_COBRO"" = " + (long)ETipoCobro.Fomento + @" 
                                AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            WHERE OC.""ENTITY_TYPE"" = " + (long)moleQule.Common.Structs.ETipoEntidad.LineaFomento + @"
                            GROUP BY OC.""OID"", OC.""OID_OPERATION"")
                       AS OC ON OC.""OID_OPERATION"" = LF.""OID""
                LEFT JOIN " + oc + @" AS OC1 ON OC1.""OID"" = OC.""OID""
                LEFT JOIN " + cb + @" AS C ON C.""OID"" = OC1.""OID_COBRO""" +
				WHERE(conditions);	
		
			//if (lockTable) query += " FOR UPDATE OF E NOWAIT";

            return query;
        }

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { ExpedienteFomento = LineaFomento.NewChild().GetInfo(false) };
			conditions.ExpedienteFomento.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

        public static string SELECT_LINEAS_FOMENTO(moleQule.Common.QueryConditions conditions, ETipoEntidad tipo)
        {
			string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));

            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string ap = nHManager.Instance.GetSQLTable(typeof(GrantPeriodRecord));
            
            conditions.Estado = EEstado.Todos;

            long oid_ayuda_fomento = ModulePrincipal.GetAyudaFomentoSetting();
            
            string query = @"
				SELECT 1 AS ""TIPO_QUERY""
						,LR.*
						,CASE WHEN (COALESCE(AP.""TIPO_AYUDA"", 2) = " + (long)ETipoDescuento.Porcentaje + @")
                    		THEN (""TOTAL"" * COALESCE(AP.""PORCENTAJE"", 0) / 100)
                    		ELSE COALESCE(AP.""CANTIDAD"", 0)
                    		END AS ""SUBVENCION""
						,RG.""FECHA"" AS ""FECHA_REGISTRO""
                    	,RG.""CODIGO"" AS ""CODIGO_REGISTRO""
						,RG.""TIPO_REGISTRO"" AS ""TIPO_REGISTRO""
                    	,COALESCE(EN.""CODIGO"", '') AS ""CODIGO_ENTIDAD""
						,COALESCE(EN.""ESTADO"", 0) AS ""ESTADO_ENTIDAD""
                        ,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENTE""
						,COALESCE(PA.""TIPO_MERCANCIA"", '') AS ""PRODUCTO""
                        ,EN.""FECHA_CONOCIMIENTO"" AS ""CONOCIMIENTO""
						,EN.""CODIGO"" AS ""LINEA_FOMENTO""
				FROM " + lr + @" AS LR
                INNER JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""
                INNER JOIN " + TABLE(tipo) + @" AS EN ON EN.""OID"" = LR.""OID_ENTIDAD"" AND LR.""TIPO_ENTIDAD"" = " + (long)tipo + @"
                LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = EN.""OID_EXPEDIENTE""
                LEFT JOIN " + pa + @" AS PA ON PA.""OID"" = EN.""OID_PARTIDA""
                LEFT JOIN (SELECT AP.""TIPO_DESCUENTO"" AS ""TIPO_AYUDA""
								,AP.""CANTIDAD"" AS ""CANTIDAD""
								,AP.""PORCENTAJE"" AS ""PORCENTAJE""
								,AP.""FECHA_INI"" AS ""FECHA_INI""
								,AP.""FECHA_FIN"" AS ""FECHA_FIN""
							FROM " + ap + @" AS AP
							WHERE AP.""OID_AYUDA"" = " + oid_ayuda_fomento + @"
                    		AND ""ESTADO"" != " + (long)EEstado.Anulado + @")
                    AS AP ON EN.""FECHA_CONOCIMIENTO"" BETWEEN AP.""FECHA_INI"" AND AP.""FECHA_FIN""
                WHERE (RG.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            if (conditions.Registro != null) 
				query += @"
					AND LR.""OID_REGISTRO"" = " + conditions.Registro.Oid;	

            return query;
        }

		internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lockTable)
        {
            string cb = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string oc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OperationChargeRecord));

            string query = 
			FIELDS(ETipoQuery.PENDIENTES) + 
            JOIN(conditions) +
            WHERE(conditions);
            
			query += @"
				AND LF.""ESTADO"" NOT IN (" + (long)EEstado.Anulado + 
											"," + (long)EEstado.Desestimado +
											"," + (long)EEstado.Charged + @")
				AND LF.""OID"" NOT IN (	SELECT OC.""OID_OPERATION""
										FROM " + oc + @" AS OC
										INNER JOIN " + cb + @" AS CB ON CB.""OID"" = OC.""OID_COBRO"" 
                                            AND CB.""TIPO_COBRO"" = " + (long)ETipoCobro.Fomento + @" 
                                            AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                        WHERE OC.""ENTITY_TYPE"" = " + (long)moleQule.Common.Structs.ETipoEntidad.LineaFomento + @")
            ORDER BY ""FECHA_ASIGNACION"", ""ID_EXPEDIENTE""";
            
			//if (lockTable) query += " FOR UPDATE OF E NOWAIT";
			return query;
		}

        internal static string SELECT_BY_COBRO(QueryConditions conditions, bool lockTable)
        {
            string ef = nHManager.Instance.GetSQLTable(typeof(LineaFomentoRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string oc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OperationChargeRecord));

            string query =
			FIELDS(ETipoQuery.COBRO) +
            JOIN(conditions) + @"
            INNER JOIN (SELECT OC.""OID_OPERATION""
								,SUM(OC.""CANTIDAD"") AS ""COBRADO""
						FROM " + oc + @" AS OC
						INNER JOIN " + cb + @" AS CB ON CB.""OID"" = OC.""OID_COBRO"" 
                            AND CB.""TIPO_COBRO"" = " + (long)ETipoCobro.Fomento + @" 
                            AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        WHERE OC.""ENTITY_TYPE"" = " + (long)moleQule.Common.Structs.ETipoEntidad.LineaFomento + @"
						GROUP BY OC.""OID_OPERATION"")
				AS OC ON OC.""OID_OPERATION"" = LF.""OID""
            INNER JOIN " + oc + @" AS OC1 ON OC1.""OID_OPERATION"" = OC.""OID_OPERATION""
            INNER JOIN " + cb + @" AS C ON C.""OID"" = OC1.""OID_COBRO""" +
            WHERE(conditions);

            //if (lockTable) query += " FOR UPDATE OF E NOWAIT";

            return query;
        }

        internal static string SELECT_BY_COBRO_AND_PENDIENTES(QueryConditions conditions, bool lockTable)
        {
            string query;

            query = SELECT_BY_COBRO(conditions, lockTable);

            conditions.Entity.EntityType = 0;
            query += @"
			UNION " +
            SELECT_PENDIENTES(conditions, lockTable);

            //if (lockTable) query += " FOR UPDATE OF E NOWAIT";

            return query;
        }

		internal static string SELECT_INFORME_FOMENTO(QueryConditions conditions, bool lockTable)
		{
			string query;

			query = SELECT(conditions, false);

			query += @"
				AND LF.""ESTADO"" != " + (long)EEstado.Anulado;

			if (conditions.Producto != null)
				if (conditions.Producto.CodigoAduanero != string.Empty) 
					query += @"
						AND PR.""CODIGO_ADUANERO"" = '" + conditions.Producto.CodigoAduanero + "'";

			if (conditions.Expedient != null)
			{
				if (conditions.Expedient.PuertoOrigen != string.Empty) 
					query += @"
						AND EX.""PUERTO_ORIGEN"" = '" + conditions.Expedient.PuertoOrigen + "'";

				if (conditions.Expedient.PuertoDestino != string.Empty) 
					query += @"
						AND EX.""PUERTO_DESTINO"" = '" + conditions.Expedient.PuertoDestino + "'";
			}

			//if (lockTable) query += " FOR UPDATE OF E NOWAIT";

			return query;
		}
        
        internal static string TABLE(ETipoEntidad tipo)
        {
            return Common.ModuleController.Instance.ActiveEntidades[tipo].Table;
        }

		#endregion
	}
}