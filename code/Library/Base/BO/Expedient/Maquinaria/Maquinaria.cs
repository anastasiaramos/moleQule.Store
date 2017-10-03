using System;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class MaquinariaRecord : RecordBase
    {
        #region Attributes

        private long _oid_expediente;
        private long _oid_batch;
        private string _codigo = string.Empty;
        private string _identificador = string.Empty;
        private string _descripcion = string.Empty;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties

        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual long OidBatch { get { return _oid_batch; } set { _oid_batch = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Identificador { get { return _identificador; } set { _identificador = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public MaquinariaRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
            _oid_batch = Format.DataReader.GetInt64(source, "OID_BATCH");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _identificador = Format.DataReader.GetString(source, "IDENTIFICADOR");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(MaquinariaRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_expediente = source.OidExpediente;
            _oid_batch = source.OidBatch;
            _codigo = source.Codigo;
            _identificador = source.Identificador;
            _descripcion = source.Descripcion;
            _observaciones = source.Observaciones;
        }
        #endregion
    }

    [Serializable()]
    public class MaquinariaBase
    {
        #region Attributes

        private MaquinariaRecord _record = new MaquinariaRecord();

        //Campos no enlazados
        private decimal _precio_compra;
        private decimal _precio_venta;
        private decimal _ayuda;
        private decimal _coste;
        private decimal _stock;
        private string _n_albaran;

        #endregion

        #region Properties

        public MaquinariaRecord Record { get { return _record; } }

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

        internal void CopyValues(Maquinaria source)
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
        internal void CopyValues(MaquinariaInfo source)
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
	public class Maquinaria : BusinessBaseEx<Maquinaria>
	{	
	    #region Attributes

        public MaquinariaBase _base = new MaquinariaBase();

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
        
		public virtual void CopyFrom(Maquinaria source)
		{
			Identificador = source.Identificador;
			Descripcion = source.Descripcion;
			Observaciones = source.Observaciones;
			NAlbaran = source.NAlbaran;
		}
		public virtual void CopyFrom(Batch partida)
		{
			OidPartida = partida.Oid;
			PrecioCompra = partida.PrecioCompraKilo;
			PrecioVenta = partida.PrecioVentaKilo;
			Ayuda = partida.AyudaRecibidaKilo;
			Coste = partida.CosteKilo;
			Stock = 1;
		}

        #endregion
 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		//Descomentar en caso de existir reglas de validación
		/*protected override void AddBusinessRules()
        {	
			//Agregar reglas de validación
        }*/
		
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
		public Maquinaria() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private Maquinaria(Maquinaria source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private Maquinaria(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static Maquinaria NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Maquinaria();
		}
		
		public static Maquinaria NewChild(Expedient parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Maquinaria obj = new Maquinaria();
			obj.OidExpediente = parent.Oid;
			
			return obj;
		}
		
		
		internal static Maquinaria GetChild(Maquinaria source)
		{
			return new Maquinaria(source);
		}
		
		internal static Maquinaria GetChild(IDataReader reader)
		{
			return new Maquinaria(reader);
		}
		
		public virtual MaquinariaInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new MaquinariaInfo(this);
		}
			
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override Maquinaria Save()
		{
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(Maquinaria source)
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

            OidExpediente = parent.Oid;

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

            OidExpediente = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				MaquinariaRecord obj = Session().Get<MaquinariaRecord>(Oid);
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
				Session().Delete(Session().Get<MaquinariaRecord>(Oid));
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

			query = " SELECT M.*" +
					"       ,PE.\"PRECIO_COMPRA_KILO\" AS \"PRECIO_COMPRA\"" +
					"		,PE.\"PRECIO_VENTA_KILO\" AS \"PRECIO_VENTA\"" +
					"       ,PE.\"AYUDA_RECIBIDA_KILO\" AS \"AYUDA\"" +
					"		,PE.\"COSTE_KILO\" AS \"COSTE\"" +
					"		,\"STOCK_K\" AS \"STOCK\"" +
					"		,AP.\"CODIGO\" AS \"N_ALBARAN\"";

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string m = nHManager.Instance.GetSQLTable(typeof(MaquinariaRecord));
            string e = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string pe = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string idl = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string id = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + m + " AS M" +
					" INNER JOIN " + e + " AS E ON E.\"OID\" = M.\"OID_EXPEDIENTE\"" +
					" LEFT JOIN " + pe + " AS PE ON PE.\"OID\" = M.\"OID_BATCH\"" +
					" LEFT JOIN " + idl + " AS CA ON CA.\"OID_BATCH\" = PE.\"OID\"" +
					" LEFT JOIN " + id + " AS AP ON AP.\"OID\" = CA.\"OID_ALBARAN\"" +
					" LEFT JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"STOCK_K\", SUM(\"BULTOS\") AS \"STOCK_B\"" +
					"				FROM " + s + " GROUP BY \"OID_BATCH\")" +
					"		AS S ON S.\"OID_BATCH\" = PE.\"OID\"";

			query += WHERE(conditions);

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query = " WHERE TRUE";

			if (conditions.Maquinaria != null) query += " AND M.\"OID\" = " + conditions.Maquinaria.Oid.ToString();
			if (conditions.Expedient != null) query += " AND M.\"OID_EXPEDIENTE\" = " + conditions.Expedient.Oid.ToString();

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Maquinaria = Maquinaria.NewChild().GetInfo() };
			conditions.Maquinaria.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			query = SELECT_BASE(conditions);

			if (lockTable) query += " FOR UPDATE OF M NOWAIT";

			return query;
		}

		#endregion
	}
}

