using System;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class CabezaRecord : RecordBase
    {
        #region Attributes

        private long _oid_expediente;
        private long _oid_batch;
        private string _codigo = string.Empty;
        private string _identificador = string.Empty;
        private string _raza = string.Empty;
        private string _tipo = string.Empty;
        private string _sexo = string.Empty;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties

        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidProductoExpediente { get { return _oid_batch; } set { _oid_batch = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Identificador { get { return _identificador; } set { _identificador = value; } }
        public virtual string Raza { get { return _raza; } set { _raza = value; } }
        public virtual string Tipo { get { return _tipo; } set { _tipo = value; } }
        public virtual string Sexo { get { return _sexo; } set { _sexo = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public CabezaRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_batch = Format.DataReader.GetInt64(source, "OID_BATCH");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _identificador = Format.DataReader.GetString(source, "IDENTIFICADOR");
            _raza = Format.DataReader.GetString(source, "RAZA");
            _tipo = Format.DataReader.GetString(source, "TIPO");
            _sexo = Format.DataReader.GetString(source, "SEXO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(CabezaRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_expediente = source.OidExpediente;
            _oid_batch = source.OidProductoExpediente;
            _codigo = source.Codigo;
            _identificador = source.Identificador;
            _raza = source.Raza;
            _tipo = source.Tipo;
            _sexo = source.Sexo;
            _observaciones = source.Observaciones;
        }

        #endregion
    }

    [Serializable()]
    public class CabezaBase
    {
        #region Attributes

        private CabezaRecord _record = new CabezaRecord();

        //Campos no enlazados
        private decimal _precio_compra;
        private decimal _precio_venta;
        private decimal _ayuda;
        private decimal _coste;
        private decimal _stock;
        private string _n_albaran;

        #endregion

        #region Properties

        public CabezaRecord Record { get { return _record; } }

        //Propiedades no enlazadas
        public virtual decimal PrecioCompra { get { return _precio_compra; } set { _precio_compra = value; } }
        public virtual decimal PrecioVenta { get { return _precio_venta; } set { _precio_venta = value; } }
        public virtual decimal Ayuda { get { return _ayuda; } set { _ayuda = value; } }
        public virtual decimal Coste { get { return _coste; } set { _coste = value; } }
        public virtual decimal Stock { get { return _stock; } set { _stock = value; } }
        public virtual string NAlbaran { get { return _n_albaran; } set { _n_albaran = value; } }


        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _precio_compra = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
            _precio_venta = Format.DataReader.GetDecimal(source, "PRECIO_VENTA");
            _ayuda = Format.DataReader.GetDecimal(source, "AYUDA");
            _coste = Format.DataReader.GetDecimal(source, "COSTE");
            _stock = Format.DataReader.GetDecimal(source, "STOCK");
            _n_albaran = Format.DataReader.GetString(source, "N_ALBARAN");
        }

        internal void CopyValues(Cabeza source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            //No enlazados
            _precio_compra = source.PrecioCompra;
            _precio_venta = source.PrecioVenta;
            _ayuda = source.Ayuda;
            _coste = source.Coste;
            _stock = source.Stock;
            _n_albaran = source.NAlbaran;
        }

        internal void CopyValues(CabezaInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            //No enlazados
            _precio_compra = source.PrecioCompra;
            _precio_venta = source.PrecioVenta;
            _ayuda = source.Ayuda;
            _coste = source.Coste;
            _stock = source.Stock;
            _n_albaran = source.NAlbaran;
        }
        #endregion
    }

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class Cabeza : BusinessBaseEx<Cabeza>
	{	
	    #region Attributes

        public CabezaBase _base = new CabezaBase();

        #endregion

        #region Properties

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
        public virtual long OidPartida
        {

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidProductoExpediente;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidProductoExpediente.Equals(value))
                {
                    _base.Record.OidProductoExpediente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Sexo
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
                if (value == null) value = string.Empty;
                if (!_base.Record.Sexo.Equals(value))
                {
                    _base.Record.Sexo = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string Identificador
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
		public virtual string Tipo
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
				if (value == null) value = string.Empty;
                if (!_base.Record.Tipo.Equals(value))
				{
                    _base.Record.Tipo = value;
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
		
        //Propiedades no enlazadas
        public virtual decimal PrecioCompra { get { return _base.PrecioCompra; } set { _base.PrecioCompra = value; } }
        public virtual decimal PrecioVenta { get { return _base.PrecioVenta; } set { _base.PrecioVenta = value; } }
        public virtual decimal Ayuda { get { return _base.Ayuda; } set { _base.Ayuda = value; } }
        public virtual decimal Coste { get { return _base.Coste; } set { _base.Coste = value; } }
        public virtual decimal Stock { get { return _base.Stock; } set { _base.Stock = value; } }
		public virtual string NAlbaran { get { return _base.NAlbaran; } set { _base.NAlbaran = value; } }

        #endregion

        #region Business Methods
        
		public virtual void CopyFrom(Cabeza source)
		{
			Identificador = source.Identificador;
			Raza = source.Raza;
			Tipo = source.Tipo;
			Sexo = source.Sexo;
			Observaciones = source.Observaciones;
			NAlbaran = source.NAlbaran;
		}
		public virtual void CopyFrom(Batch pe)
		{
			OidPartida = pe.Oid;
			PrecioCompra = pe.PrecioCompraKilo;
			PrecioVenta = pe.PrecioVentaKilo;
			Ayuda = pe.AyudaRecibidaKilo;
			Coste = pe.CosteKilo;
			Stock = 1;
		}
		
		#endregion
		 
	    #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(Csla.Validation.CommonRules.StringRequired, "Identificador");
		}

		#endregion

		#region Authorization Rules

		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.PRODUCTO);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.PRODUCTO);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.PRODUCTO);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.PRODUCTO);
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public Cabeza() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private Cabeza(Cabeza source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private Cabeza(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static Cabeza NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Cabeza();
		}
		
		public static Cabeza NewChild(Expedient parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Cabeza obj = new Cabeza();
			obj.OidExpediente = parent.Oid;
			
			return obj;
		}		
		
		internal static Cabeza GetChild(Cabeza source)
		{
			return new Cabeza(source);
		}
		
		internal static Cabeza GetChild(IDataReader reader)
		{
			return new Cabeza(reader);
		}

        public virtual CabezaInfo GetInfo() { return GetInfo(true); }

        public virtual CabezaInfo GetInfo(bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new CabezaInfo(this, childs);
        }
			
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override Cabeza Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(Cabeza source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            _base.Record.OidExpediente = parent.Oid;

			try
			{
                parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            _base.Record.OidExpediente = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				CabezaRecord obj = Session().Get<CabezaRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Expedient parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<CabezaRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		
		#endregion

        #region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

			query = " SELECT C.*" +
					"       ,PE.\"PRECIO_COMPRA_KILO\" AS \"PRECIO_COMPRA\"" +
					"       ,PE.\"PRECIO_VENTA_KILO\" AS \"PRECIO_VENTA\"" +
					"       ,PE.\"AYUDA_RECIBIDA_KILO\" AS \"AYUDA\"" +
					"       ,PE.\"COSTE_KILO\" AS \"COSTE\"" +
					"		,\"STOCK_K\" AS \"STOCK\"" +
					"		,AP.\"CODIGO\" AS \"N_ALBARAN\"";

            return query;
        }

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string ca = nHManager.Instance.GetSQLTable(typeof(CabezaRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
            string idl = nHManager.Instance.GetSQLTable(typeof(InputDeliveryLineRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
			string query;

			query = Cabeza.SELECT_FIELDS() +
					" FROM " + ca + " AS C" +
					" INNER JOIN " + ex + " AS E ON E.\"OID\" = C.\"OID_EXPEDIENTE\"" +
					" LEFT JOIN " + pe + " AS PE ON PE.\"OID\" = C.\"OID_BATCH\"" +
					" LEFT JOIN " + idl + " AS CA ON CA.\"OID_BATCH\" = PE.\"OID\"" +
					" LEFT JOIN " + id + " AS AP ON AP.\"OID\" = CA.\"OID_ALBARAN\"" +
					" LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
					"				FROM " + st + " GROUP BY \"OID_BATCH\")" +
					"		AS S ON S.\"OID_BATCH\" = PE.\"OID\"";

			query += WHERE(conditions);

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query = " WHERE TRUE";
			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;


			query = SELECT(conditions, lockTable);

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			query = SELECT_BASE(conditions);
						
			if (lockTable) query += " FOR UPDATE OF C NOWAIT";

			return query;
		}

        #endregion

    }
}

