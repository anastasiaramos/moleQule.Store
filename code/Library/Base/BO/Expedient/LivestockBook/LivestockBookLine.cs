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
using moleQule.CslaEx;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class LivestockBookLineRecord : RecordBase
    {
        #region Attributes

        private long _oid_book;
        private long _oid_batch;
        private long _oid_delivery_line;
        private long _oid_pair;
        private long _estado;
        private long _serial;
        private string _codigo = string.Empty;
        private string _crotal = string.Empty;
        private DateTime _fecha;
        private long _sexo;
        private long _edad;
        private string _raza = string.Empty;
        private string _causa = string.Empty;
        private string _procedencia = string.Empty;
        private Decimal _balance;
        private string _observaciones = string.Empty;
        private long _tipo;
        private bool _explotacion;

        #endregion

        #region Properties

        public virtual long OidBook { get { return _oid_book; } set { _oid_book = value; } }
        public virtual long OidBatch { get { return _oid_batch; } set { _oid_batch = value; } }
        public virtual long OidDeliveryLine { get { return _oid_delivery_line; } set { _oid_delivery_line = value; } }
        public virtual long OidPair { get { return _oid_pair; } set { _oid_pair = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Crotal { get { return _crotal; } set { _crotal = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual long Sexo { get { return _sexo; } set { _sexo = value; } }
        public virtual long Edad { get { return _edad; } set { _edad = value; } }
        public virtual string Raza { get { return _raza; } set { _raza = value; } }
        public virtual string Causa { get { return _causa; } set { _causa = value; } }
        public virtual string Procedencia { get { return _procedencia; } set { _procedencia = value; } }
        public virtual Decimal Balance { get { return _balance; } set { _balance = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
        public virtual bool Explotacion { get { return _explotacion; } set { _explotacion = value; } }

        #endregion

        #region Business Methods

        public LivestockBookLineRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_book = Format.DataReader.GetInt64(source, "OID_LIBRO");
            _oid_batch = Format.DataReader.GetInt64(source, "OID_PARTIDA");
            _oid_delivery_line = Format.DataReader.GetInt64(source, "OID_CONCEPTO");
            _oid_pair = Format.DataReader.GetInt64(source, "OID_PAIR");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _crotal = Format.DataReader.GetString(source, "CROTAL");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _sexo = Format.DataReader.GetInt64(source, "SEXO");
            _edad = Format.DataReader.GetInt64(source, "EDAD");
            _raza = Format.DataReader.GetString(source, "RAZA");
            _causa = Format.DataReader.GetString(source, "CAUSA");
            _procedencia = Format.DataReader.GetString(source, "PROCEDENCIA");
            _balance = Format.DataReader.GetDecimal(source, "BALANCE");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _tipo = Format.DataReader.GetInt64(source, "TIPO");
            _explotacion = Format.DataReader.GetBool(source, "EXPLOTACION");
        }
        public virtual void CopyValues(LivestockBookLineRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_book = source.OidBook;
            _oid_batch = source.OidBatch;
            _oid_delivery_line = source.OidDeliveryLine;
            _oid_pair = source.OidPair;
            _estado = source.Estado;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _crotal = source.Crotal;
            _fecha = source.Fecha;
            _sexo = source.Sexo;
            _edad = source.Edad;
            _raza = source.Raza;
            _causa = source.Causa;
            _procedencia = source.Procedencia;
            _balance = source.Balance;
            _observaciones = source.Observaciones;
            _tipo = source.Tipo;
            _explotacion = source.Explotacion;
        }

        #endregion
    }

	[Serializable()]
	public class LivestockBookLineBase
	{
		#region Attributes

        private LivestockBookLineRecord _record = new LivestockBookLineRecord();

		internal string _id_partida = string.Empty;
		internal long _oid_expediente;
		internal string _expediente = string.Empty;
        internal string _id_factura = string.Empty;
        internal string _cliente_proveedor = string.Empty;
        internal string _clean_crotal = string.Empty;
        internal string _concepto = string.Empty;
        internal string _pair_id = string.Empty;

		#endregion

		#region Properties

        public LivestockBookLineRecord Record { get { return _record; } set { _record = value; } }

		internal virtual EEstado EEstado { get { return (EEstado)_record.Estado; } }
		internal virtual string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal virtual ETipoLineaLibroGanadero ETipo { get { return (ETipoLineaLibroGanadero)_record.Tipo; } }
		internal virtual string TipoLabel { get { return moleQule.Store.Structs.EnumText<ETipoLineaLibroGanadero>.GetLabel(ETipo); } }
		internal virtual ESexo ESexo { get { return (ESexo)_record.Sexo; } }
		internal virtual string SexoLabel { get { return moleQule.Common.Structs.EnumText<ESexo>.GetLabel(ESexo); } }
        internal virtual string IdFactura { get { return _id_factura; } set { _id_factura = value; } }
        internal virtual string ClienteProveedor { get { return _cliente_proveedor; } set { _cliente_proveedor = value; } }
        internal virtual string CleanCrotal { get { return _clean_crotal; } set { _clean_crotal = value; } }
        internal virtual string VisibleCrotal { get { return _clean_crotal.Length >= 5 ? _clean_crotal.Substring(_clean_crotal.Length - 5, 4) + "-" + _clean_crotal.Substring(_clean_crotal.Length - 1) : _clean_crotal; } }
        internal virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
        internal virtual string PairID { get { return _pair_id; } set { _pair_id = value; } }

		#endregion

		#region Business Methods

		internal virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_id_partida = Format.DataReader.GetString(source, "ID_PARTIDA");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
            _id_factura = Format.DataReader.GetString(source, "ID_FACTURA");
            _cliente_proveedor = Format.DataReader.GetString(source, "CLIENTE");
            _clean_crotal = Format.DataReader.GetString(source, "REGEX_CROTAL");
            _concepto = Format.DataReader.GetString(source, "CONCEPTO");
            _pair_id = Format.DataReader.GetString(source, "PAIR_ID");
		}
		internal virtual void CopyValues(LivestockBookLine source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_id_partida = source.IDPartida;
			_oid_expediente = source.OidExpediente;
			_expediente = source.Expediente;
            _id_factura = source.IdFactura;
            _cliente_proveedor = source.ClienteProveedor;
            _clean_crotal = source.CleanCrotal;
            _concepto = source.Concepto;
            _pair_id = source.PairID;
		}
		internal virtual void CopyValues(LivestockBookLineInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_id_partida = source.IDPartida;
			_oid_expediente = source.OidExpediente;
            _expediente = source.Expediente;
            _id_factura = source.IdFactura;
            _cliente_proveedor = source.ClienteProveedor;
            _clean_crotal = source.CleanCrotal;
            _concepto = source.Concepto;
            _pair_id = source.PairID;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class LivestockBookLine : BusinessBaseEx<LivestockBookLine>, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return _base.Record.Fecha; } set { Fecha = value; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((LivestockBookLine)source); }
        public void DifferentYearChecks() { }
        public virtual void DifferentYearTasks(IEntityBase oldItem) { }
        public void SameYearTasks(IEntityBase newItem) { }
        public virtual void IEntityBaseSave(object parent) { Save(); }

        #endregion

		#region Attributes

		public LivestockBookLineBase _base = new LivestockBookLineBase();

		#endregion
		
		#region Properties

        public LivestockBookLineBase Base { get { return _base; } }

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

		public virtual long OidLibro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidBook;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidBook.Equals(value))
				{
					_base.Record.OidBook = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidPartida
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidBatch;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidBatch.Equals(value))
				{
					_base.Record.OidBatch = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidConceptoAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidDeliveryLine;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidDeliveryLine.Equals(value))
				{
					_base.Record.OidDeliveryLine = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidPair
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidPair;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidPair.Equals(value))
                {
                    _base.Record.OidPair = value;
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
		public virtual long Tipo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Tipo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Tipo.Equals(value))
				{
					_base.Record.Tipo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Crotal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Crotal;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Crotal.Equals(value))
				{
					_base.Record.Crotal = value;
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
		public virtual long Sexo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Sexo;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Sexo.Equals(value))
				{
					_base.Record.Sexo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Edad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Edad;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Edad.Equals(value))
				{
					_base.Record.Edad = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Raza
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Raza;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Raza.Equals(value))
				{
					_base.Record.Raza = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Causa
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Causa;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Causa.Equals(value))
				{
					_base.Record.Causa = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Procedencia
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Procedencia;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Procedencia.Equals(value))
				{
					_base.Record.Procedencia = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Balance
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Balance;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Balance.Equals(value))
				{
					_base.Record.Balance = value;
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
        public virtual bool Explotacion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Explotacion;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Explotacion.Equals(value))
                {
                    _base.Record.Explotacion = value;
                    PropertyHasChanged();
                }
            }
        }

		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoLineaLibroGanadero ETipo { get { return _base.ETipo; } set { Tipo = (long)value; } }
		public virtual string TipoLabel { get { return _base.TipoLabel; } }
		public virtual ESexo ESexo { get { return _base.ESexo; } set { Sexo = (long)value; } }
		public virtual string SexoLabel { get { return _base.SexoLabel; } }
		public virtual string IDPartida { get { return _base._id_partida; } set { _base._id_partida = value; } }
		public virtual long OidExpediente { get { return _base._oid_expediente; } set { _base._oid_expediente = value; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
        public virtual string IdFactura { get { return _base.IdFactura; } set { _base.IdFactura = value; } }
        public virtual string ClienteProveedor { get { return _base.ClienteProveedor; } set { _base.ClienteProveedor = value; } }
        public virtual string CleanCrotal { get { return _base.CleanCrotal; } set { _base.CleanCrotal = value; } }
        public virtual string VisibleCrotal { get { return _base.VisibleCrotal; } }
        public virtual string Concepto { get { return _base.Concepto; } }
        public virtual string PairID { get { return _base.PairID; } set { _base.PairID = value; PropertyHasChanged(); } }

		#endregion
		
		#region Business Methods
		
		public virtual LivestockBookLine CloneAsNew()
		{
			LivestockBookLine clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode(OidLibro);
			
			clon.SessionCode = LivestockBookLine.OpenSession();
			LivestockBookLine.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		public virtual void CopyFrom(LivestockBookLine source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidLibro = source.OidLibro;
			OidPartida = source.OidPartida;
			OidConceptoAlbaran = source.OidConceptoAlbaran;
			Estado = source.Estado;
			Serial = source.Serial;
			Codigo = source.Codigo;
			Crotal = source.Crotal;
			Fecha = source.Fecha;
			Sexo = source.Sexo;
			Edad = source.Edad;
			Raza = source.Raza;
			Causa = source.Causa;
			Procedencia = source.Procedencia;
			Balance = source.Balance;
			Observaciones = source.Observaciones;
            Explotacion = source.Explotacion;

			IDPartida = source.IDPartida;
			OidExpediente = source.OidExpediente;
			Expediente = source.Expediente;
            IdFactura = source.IdFactura;
            CleanCrotal = source.CleanCrotal;
            ClienteProveedor = source.ClienteProveedor;
		}
		public virtual void CopyFrom(LivestockBookLineInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			OidLibro = source.OidLibro;
			OidPartida = source.OidPartida;
			OidConceptoAlbaran = source.OidConceptoAlbaran;
			Estado = source.Estado;
			Serial = source.Serial;
			Codigo = source.Codigo;
			Crotal = source.Crotal;
			Fecha = source.Fecha;
			Sexo = source.Sexo;
			Edad = source.Edad;
			Raza = source.Raza;
			Causa = source.Causa;
			Procedencia = source.Procedencia;
			Balance = source.Balance;
			Observaciones = source.Observaciones;
            Explotacion = source.Explotacion;

			IDPartida = source.IDPartida;
			OidExpediente = source.OidExpediente;
            Expediente = source.Expediente;
            IdFactura = source.IdFactura;
            CleanCrotal = source.CleanCrotal;
            ClienteProveedor = source.ClienteProveedor;
		}
		public virtual void CopyFrom(LivestockBook source)
		{
			OidLibro = source.Oid;
		}

        public virtual void CopyFromPair(LivestockBookLineInfo source)
        {
            if (source == null) return;

            OidPair = source.Oid;
            OidLibro = source.OidLibro;
            OidPartida = source.OidPartida;
            Crotal = source.Crotal;
            Sexo = source.Sexo;
            Edad = source.Edad;
            Raza = source.Raza;
            Explotacion = source.Explotacion;

            PairID = source.Codigo;
            IDPartida = source.IDPartida;
            OidExpediente = source.OidExpediente;
            Expediente = source.Expediente;
            CleanCrotal = source.CleanCrotal;  
        }
        
        public virtual void GetNewCode(long OidLibro)
        {
            Serial = SerialInfo.GetNextByYear(typeof(LivestockBookLine), Fecha.Year);
            Codigo = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
        }

        public static void SetAsExternalHead(long oid, ETipoLineaLibroGanadero lineType)
        {
            LivestockBookLine line = LivestockBookLine.Get(oid, lineType);
            line.Explotacion = false;

            switch (line.EEstado)
            {
                case EEstado.Baja:

                    LivestockBookLine entryLine = LivestockBookLine.Get(line.OidPair, false, line.SessionCode);
                    entryLine.Explotacion = false;
                    entryLine.Save();

                    break;

                case EEstado.Alta:

                    LivestockBookLine exitLine = LivestockBookLine.GetExitLine(line.Oid, false, line.SessionCode);
                    if (exitLine != null)
                    {
                        exitLine.Explotacion = false;
                        exitLine.Save();
                    }

                    break;
            }

            line.Save();
            line.CloseSession();
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
			if (EEstado == EEstado.Baja && OidPair == 0)
			{
				e.Description = String.Format(moleQule.Resources.Messages.NO_VALUE_SELECTED, "Línea origen");
				throw new iQValidationException(e.Description, string.Empty);
			}

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

        public bool CanChangeType()
        {
            return !new List<ETipoLineaLibroGanadero>() 
            { 
                ETipoLineaLibroGanadero.Importacion, 
                ETipoLineaLibroGanadero.Venta 
            }.Contains(ETipo);
        }

		public static void IsPosibleDelete(long oid)
		{

		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected LivestockBookLine ()	{}		
		private LivestockBookLine(LivestockBookLine source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private LivestockBookLine(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static LivestockBookLine NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			LivestockBookLine obj = DataPortal.Create<LivestockBookLine>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		internal static LivestockBookLine GetChild(LivestockBookLine source, bool childs = false) { return new LivestockBookLine(source, childs); }
        internal static LivestockBookLine GetChild(int sessionCode, IDataReader source, bool childs = false) { return new LivestockBookLine(sessionCode, source, childs); }
		
		public virtual LivestockBookLineInfo GetInfo(bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LivestockBookLineInfo(this, childs);
		}
		
		#endregion				
		
		#region Root Factory Methods

		public static LivestockBookLine New(long oidLibro)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			LivestockBookLine obj = DataPortal.Create<LivestockBookLine>(new CriteriaCs(-1));
			obj.OidLibro = oidLibro;
			return obj;
		}

        public static LivestockBookLine Get(long oid) { return Get(oid, ETipoLineaLibroGanadero.Todos); }
		public static LivestockBookLine Get(long oid, ETipoLineaLibroGanadero tipo) { return Get(oid, tipo, true); }
		public static LivestockBookLine Get(long oid, ETipoLineaLibroGanadero tipo, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid, tipo);

			BeginTransaction(criteria.Session);

			return DataPortal.Fetch<LivestockBookLine>(criteria);
		}
		public static LivestockBookLine Get(long oid, bool childs, bool cache)
		{
			LivestockBookLine item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(LivestockBookLines)))
			{
				LivestockBookLines items = LivestockBookLines.NewList();

				item = LivestockBookLine.GetChild(items.SessionCode, oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(LivestockBookLines), items);
			}
			else
			{
				LivestockBookLines items = Cache.Instance.Get(typeof(LivestockBookLines)) as LivestockBookLines;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = LivestockBookLine.GetChild(items.SessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(LivestockBooks), items);
				}
			}

			return item;
		}

        public static LivestockBookLine GetExitLine(long oidPair, bool childs, int sessionCode)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = GetCriteria(Session(sessionCode));
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT_EXIT_LINE(oidPair, true);

            BeginTransaction(criteria.Session);

            LivestockBookLine obj = DataPortal.Fetch<LivestockBookLine>(criteria);
            obj.SetSharedSession(sessionCode);

            return obj;
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

			IsPosibleDelete(oid);

			DataPortal.Delete(new CriteriaCs(oid));
		}

		/// <summary>
		/// Elimina todos los LibroGanadero. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = LivestockBookLine.OpenSession();
			ISession sess = LivestockBookLine.Session(sessCode);
			ITransaction trans = LivestockBookLine.BeginTransaction(sessCode);

			try
			{
                sess.Delete("from LineaLibroGanaderoRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				LivestockBookLine.CloseSession(sessCode);
			}
		}

		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override LivestockBookLine Save()
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

				if (!SharedTransaction) Transaction().Commit();
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
                if (!SharedTransaction)
                {
                    if (CloseSessions) CloseSession();
                    else BeginTransaction();
                }
			}
		}

		#endregion				

		#region Child Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static LivestockBookLine NewChild(LivestockBook parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			LivestockBookLine obj = DataPortal.Create<LivestockBookLine>(new CriteriaCs(-1));
			obj.CopyFrom(parent);
            obj.GetNewCode(parent.Oid);
            obj.Explotacion = true;
			obj.MarkAsChild();

			return obj;
		}

		internal static LivestockBookLine GetChild(int sessionCode, long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid);

			LivestockBookLine obj = DataPortal.Fetch<LivestockBookLine>(criteria);
			obj.MarkAsChild();

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
			Oid = (long)new Random().Next();
			ESexo = ESexo.Hembra;
			EEstado = EEstado.Pendiente;
			Fecha = DateTime.Now;
            Explotacion = true;
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(LivestockBookLine source)
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
		internal void Insert(LivestockBookLines parent)
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
		internal void Update(LivestockBookLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;

            LivestockBookLine obj = null;

            try
            {

                LivestockBookLineRecord record = Session().Get<LivestockBookLineRecord>(this.Oid);
                obj = LivestockBookLine.Get(this.Oid, true, SessionCode);

                if (Common.EntityBase.UpdateByYear(obj, this, parent))
                {
                    obj.Save();

                    parent.Transaction().Commit();
                    //parent.CloseSession();
                    parent.NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    parent.Session().Update(record);
                    //obj.CloseSession();
                }
            }
            catch (Exception ex)
            {
                //if (obj != null) obj.CloseSession();
                throw ex;
            }
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(LivestockBookLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<LivestockBookLineRecord>(Oid));
		
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
					DoLOCK(Session());
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

		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
        {
            if (EEstado == EEstado.Alta) OidPair = 0;

            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			
			GetNewCode(OidLibro);

			Session().Save(Base.Record);
		}

		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

            LivestockBookLine obj = null;

            try
            {
                if (EEstado == EEstado.Alta) OidPair = 0;

                LivestockBookLineRecord record = Session().Get<LivestockBookLineRecord>(this.Oid);
                obj = LivestockBookLine.Get(this.Oid, true, SessionCode);

                if (Common.EntityBase.UpdateByYear(obj, this, null))
                {
                    obj.Save();
                    Transaction().Commit();
                    CloseSession();
                    NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    Session().Update(record);
                    //obj.CloseSession();
                }
            }
            catch (Exception ex)
            {
                //if (obj != null) obj.CloseSession();
                iQExceptionHandler.TreatException(ex);
            }

			MarkOld();

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
				Session().Delete((LivestockBookLineRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CloseSession();
			}
		}

		#endregion			

		#region Child Data Access
		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(LivestockBook parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsNew) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidLibro = parent.Oid;			

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
		internal void Update(LivestockBook parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidLibro = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            LivestockBookLine obj = null;

            try
            {

                LivestockBookLineRecord record = Session().Get<LivestockBookLineRecord>(this.Oid);
                obj = LivestockBookLine.Get(this.Oid, true, SessionCode);

                if (Common.EntityBase.UpdateByYear(obj, this, parent))
                {
                    obj.Save();

                    parent.Transaction().Commit();
                    //parent.CloseSession();
                    parent.NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    parent.Session().Update(record);
                    //obj.CloseSession();
                }
            }
            catch (Exception ex)
            {
                //if (obj != null) obj.CloseSession();
                throw ex;
            }	

			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(LivestockBook parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<LivestockBookLineRecord>(Oid));

			MarkNew();
		}
		
		#endregion		
		
        #region SQL

        internal enum EQueryType { GENERAL = 0, IMPORT = 1, SALE = 2, BAJA = 3 }

        public new static string SELECT(long oid) { return SELECT(oid, ETipoLineaLibroGanadero.Todos, true); }
        public static string SELECT(long oid, ETipoLineaLibroGanadero tipo) { return SELECT(oid, tipo, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		public static string SELECT(LivestockBook item) 
		{
			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { LibroGanadero = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}

        internal static string SELECT_FIELDS(EQueryType queryType)
        {
            string query;

            query = @"
            SELECT LL.*
				    ,COALESCE(PA.""CODIGO"", '') AS ""ID_PARTIDA""
				    ,COALESCE(EX.""OID"", 0) AS ""OID_EXPEDIENTE""
				    ,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENTE""
                    ,CASE WHEN (SUBSTRING(LL.""CROTAL"", '.[ ]?.[- ]?\d{9}\d*') != '') THEN SUBSTRING(LL.""CROTAL"", '.[ ]?.[- ]?\d{9}\d*')
                        ELSE LL.""CROTAL"" END
                        AS ""REGEX_CROTAL""
                    ,COALESCE(LL1.""CODIGO"", '') AS ""PAIR_ID""";

            switch (queryType)
            {
                case EQueryType.GENERAL:

                    query += @" 
                    ,'' AS ""CLIENTE""
	                ,'' AS ""ID_FACTURA""
                    ,'' AS ""CONCEPTO""";

                    break;

                case EQueryType.IMPORT:

                    query += @" 
                    ,PR.""NOMBRE"" AS ""CLIENTE""
	                ,COALESCE(AP.""CODIGO"", '') || '/' || COALESCE(FP.""CODIGO"", '') AS ""ID_FACTURA""
                    ,COALESCE(CA.""CONCEPTO"", '') AS ""CONCEPTO""";

                    break;

                case EQueryType.BAJA:

                    query += @" 
                    ,'MERMA' AS ""CLIENTE""
	                ,'' AS ""ID_FACTURA""
                    ,'' AS ""CONCEPTO""";

                    break;

                case EQueryType.SALE:

                    query += @" 
                    ,CL.""NOMBRE"" AS ""CLIENTE""
	                ,COALESCE(AE.""CODIGO"", '') || '/' || COALESCE(FE.""CODIGO"", '') AS ""ID_FACTURA""
                    ,COALESCE(CA.""CONCEPTO"", '') AS ""CONCEPTO""";

                    break;
            }

            return query;
        }
		
        internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = @"
            WHERE (LL.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "LL", "ESTADO");

			switch (conditions.Estado)
			{
				case EEstado.Todos:
					break;

				case EEstado.NoAnulado:
					query += @"
                    AND LL.""ESTADO"" != " + (long)EEstado.Anulado;
					break;

				default:
					query += @"
                    AND LL.""ESTADO"" = " + (long)conditions.Estado;
					break;
			}

            if (conditions.LineaLibroGanadero != null)
		       if (conditions.LineaLibroGanadero.Oid != 0)
                   query += @"
                    AND LL.""OID"" = " + conditions.LineaLibroGanadero.Oid;				
			
            if (conditions.LibroGanadero != null) 
                query += @"
                AND LL.""OID_LIBRO"" = " + conditions.LibroGanadero.Oid;

			if (conditions.Partida != null) 
                query += @"
                AND LL.""OID_PARTIDA"" = " + conditions.Partida.Oid;

			if (conditions.Expedient != null)
                query += @"
                AND EX.""OID"" = " + conditions.Expedient.Oid;

            return query + " " + conditions.ExtraWhere;
		}

        internal static string INNER(ETipoLineaLibroGanadero tipo)
        {
            string ll = nHManager.Instance.GetSQLTable(typeof(LivestockBookLineRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));

            string query = @"
            FROM " + ll + @" AS LL
            LEFT JOIN " + ba + @" AS PA ON PA.""OID"" = LL.""OID_PARTIDA"" 
            LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = PA.""OID_EXPEDIENTE"" 
            LEFT JOIN " + st + @" AS ST ON ST.""OID_BATCH"" = PA.""OID"" AND ST.""TIPO"" = " + (long)tipo + @"
            LEFT JOIN " + ll + @" AS LL1 ON LL1.""OID"" = LL.""OID_PAIR""";

            switch (tipo)
            {
                case ETipoLineaLibroGanadero.Importacion:
                    { 
                        string il = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
                        string id = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryRecord));
                        string af = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
                        string fp = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
                        string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));

                        query += @" 
                        LEFT JOIN " + il + @" AS CA ON CA.""OID"" = LL.""OID_CONCEPTO""
                        LEFT JOIN " + id + @" AS AP ON AP.""OID"" = CA.""OID_ALBARAN"" AND AP.""TIPO_ACREEDOR"" = " + (long)ETipoAcreedor.Proveedor + @"
                        LEFT JOIN " + af + @" AS AF ON AF.""OID_ALBARAN"" = AP.""OID""
                        LEFT JOIN " + fp + @" AS FP ON FP.""OID"" = AF.""OID_FACTURA""
                        LEFT JOIN " + pr + @" AS PR ON PR.""OID"" = AP.""OID_ACREEDOR""";
                    }
                    break;

                case ETipoLineaLibroGanadero.Venta:
                    {
                        string ol = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputDeliveryLineRecord));
                        string od = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputDeliveryRecord));
                        string af = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputDeliveryInvoiceRecord));
                        string fc = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.OutputInvoiceRecord));
                        string cl = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ClientRecord));

                        query += @"
                        LEFT JOIN " + ol + @" AS CA ON CA.""OID"" = LL.""OID_CONCEPTO""
                        LEFT JOIN " + od + @" AS AE ON AE.""OID"" = CA.""OID_ALBARAN""
                        LEFT JOIN " + af + @" AS AF ON AF.""OID_ALBARAN"" = AE.""OID""
                        LEFT JOIN " + fc + @" AS FE ON FE.""OID"" = AF.""OID_FACTURA""
                        LEFT JOIN " + cl + @" AS CL ON CL.""OID"" = AE.""OID_HOLDER""";
                    }
                    break;
            }

            return query;
        }

        internal static string SELECT_IMPORTACIONES(QueryConditions conditions)
        {
            conditions.ExtraWhere = @"
                 AND LL.""TIPO"" = " + (long)ETipoLineaLibroGanadero.Importacion;

            string query =
            SELECT_FIELDS(EQueryType.IMPORT) +
            INNER(ETipoLineaLibroGanadero.Importacion) +
            WHERE(conditions);
            
            return query;
        }

        internal static string SELECT_VENTAS(QueryConditions conditions)
        {
            conditions.ExtraWhere = @"
                 AND LL.""TIPO"" = " + (long)ETipoLineaLibroGanadero.Venta;

            string query =
            SELECT_FIELDS(EQueryType.SALE) +
            INNER(ETipoLineaLibroGanadero.Venta) +
            WHERE(conditions);

            return query;
        }

        internal static string SELECT_DEFAULT(QueryConditions conditions)
        {
            conditions.ExtraWhere = @"
                 AND LL.""TIPO"" NOT IN( " +
                        (long)ETipoLineaLibroGanadero.Importacion +
                        @"," + (long)ETipoLineaLibroGanadero.Venta +
                        @"," + (long)ETipoLineaLibroGanadero.Muerte +
                    @")";

            string query =
            SELECT_FIELDS(EQueryType.GENERAL) +
            INNER(ETipoLineaLibroGanadero.Todos) +
            WHERE(conditions);

            return query;
        }

        internal static string SELECT_BAJAS(QueryConditions conditions)
        {
            conditions.ExtraWhere = @"
                 AND LL.""TIPO"" = " + (long)ETipoLineaLibroGanadero.Muerte;

            string query =
            SELECT_FIELDS(EQueryType.BAJA) +
            INNER(ETipoLineaLibroGanadero.Muerte) +
            WHERE(conditions);

            return query;
        }

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string query;

            switch(conditions.TipoLineaLibroGanadero)
            {
                case ETipoLineaLibroGanadero.Importacion:
                    query = SELECT_IMPORTACIONES(conditions);
                    break;

                case ETipoLineaLibroGanadero.Venta:
                    query = SELECT_VENTAS(conditions);
                    break;

                case ETipoLineaLibroGanadero.Muerte:
                    query = SELECT_BAJAS(conditions);
                    break;

                default:
                    query = 
                    SELECT_DEFAULT(conditions) + @"
                    UNION " +
                    SELECT_IMPORTACIONES(conditions) + @"
                    UNION " +
                    SELECT_VENTAS(conditions) + @"
                    UNION " +
                    SELECT_BAJAS(conditions);
                    break;
            }

			return query;
		}

        internal static string SELECT(long oid, bool lockTable) { return SELECT(oid, ETipoLineaLibroGanadero.Todos, lockTable); }
        internal static string SELECT(long oid, ETipoLineaLibroGanadero tipo, bool lockTable)
        {			
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { LineaLibroGanadero = LivestockBookLine.NewChild().GetInfo(false) };
			conditions.LineaLibroGanadero.Oid = oid;
            conditions.TipoLineaLibroGanadero = tipo;

			query = SELECT(conditions, lockTable);

			return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = 
            SELECT_BASE(conditions);

			query += @"
            ORDER BY ""FECHA"", ""SERIAL""";

			//if (lockTable) query += " FOR UPDATE OF LL NOWAIT";

            return query;
        }

        internal static string SELECT_AVAILABLE(QueryConditions conditions, bool lockTable)
        {
            string ll = nHManager.Instance.GetSQLTable(typeof(LivestockBookLineRecord));

            conditions.ExtraWhere = @"
                AND LL.""OID"" NOT IN (SELECT ""OID_PAIR""
                                        FROM " + ll + @"
                                        WHERE ""TIPO"" IN ( " +
                                                            (long)ETipoLineaLibroGanadero.Muerte +
                                                            @"," + (long)ETipoLineaLibroGanadero.Venta +
                                                            @"," + (long)ETipoLineaLibroGanadero.TraspasoExplotacion +
                                                        @"))
                AND LL.""TIPO"" IN ( " +
                                    (long)ETipoLineaLibroGanadero.Importacion +
                                    @"," + (long)ETipoLineaLibroGanadero.Nacimiento +
                                    @"," + (long)ETipoLineaLibroGanadero.TraspasoExplotacion +
                                    @")";

            string query =
            SELECT_FIELDS(EQueryType.GENERAL) +
            INNER(ETipoLineaLibroGanadero.Todos) +
            WHERE(conditions);

            return query;
        }

		internal static string SELECT_BY_EXPEDIENTE(QueryConditions conditions, bool lockTable)
		{
			string query;

            conditions.ExtraWhere = @"
                AND LL.""ESTADO"" IN (" + (long)EEstado.Pendiente + "," + (long)EEstado.Alta + ")";

			query = 
            SELECT_BASE(conditions) +
			WHERE(conditions);

			query += @"
            ORDER BY LL.""FECHA"", LL.""SERIAL""";

            Common.EntityBase.LOCK("LL", lockTable);

			return query;
		}

        internal static string SELECT_EXIT_LINE(long oidPair, bool lockTable)
        {
            string ll = nHManager.Instance.GetSQLTable(typeof(LivestockBookLineRecord));

            QueryConditions conditions = new QueryConditions();

            conditions.ExtraWhere = @"
                AND LL.""OID_PAIR"" = " + oidPair;

            string query =
            SELECT_FIELDS(EQueryType.GENERAL) +
            INNER(ETipoLineaLibroGanadero.Todos) +
            WHERE(conditions);

            return query;
        }

		#endregion
	}
}
