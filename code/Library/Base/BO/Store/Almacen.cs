using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

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
    public class AlmacenRecord : RecordBase
    {
        #region Attributes

        private string _nombre = string.Empty;
        private string _ubicacion = string.Empty;
        private string _observaciones = string.Empty;
        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;

        #endregion

        #region Properties
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string Ubicacion { get { return _ubicacion; } set { _ubicacion = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }

        #endregion

        #region Business Methods

        public AlmacenRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _ubicacion = Format.DataReader.GetString(source, "UBICACION");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");

        }

        public virtual void CopyValues(AlmacenRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _nombre = source.Nombre;
            _ubicacion = source.Ubicacion;
            _observaciones = source.Observaciones;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
        }
        #endregion
    }

	[Serializable()]
	public class AlmacenBase
	{
		#region Attributes

        private AlmacenRecord _record = new AlmacenRecord();

		#endregion

		#region Properties

        public AlmacenRecord Record { get { return _record; } set { _record = value; } }

		internal EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		internal string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
		internal string IDAlmacenAlmacen { get { return _record.Codigo + " - " + _record.Nombre; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);
		}
		internal void CopyValues(Almacen source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);
		}
		internal void CopyValues(StoreInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class Almacen : BusinessBaseEx<Almacen>
	{	 
		#region Attributes

		public AlmacenBase _base = new AlmacenBase();

		private Batchs _partidas = Batchs.NewChildList();
		private Stocks _stocks = Stocks.NewChildList();
		private InventarioAlmacenes _inventarios = InventarioAlmacenes.NewChildList();		
				
		#endregion
		
		#region Properties

        public AlmacenBase Base { get { return _base; } }

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
		public virtual string Ubicacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Ubicacion;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

				if (!_base.Record.Ubicacion.Equals(value))
				{
					_base.Record.Ubicacion = value;
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
		
		public virtual InventarioAlmacenes InventarioAlmacenes
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _inventarios;
			}
		}		
		public virtual Batchs Partidas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _partidas;
			}
		}
		public virtual Stocks Stocks
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _stocks;
			}
		}

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }

		public override bool IsValid
		{
			get { return base.IsValid
						 && _inventarios.IsValid
						 && _partidas.IsValid 
						 && _stocks.IsValid; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _inventarios.IsDirty
						 || _partidas.IsDirty 
						 || _stocks.IsDirty; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual Almacen CloneAsNew()
		{
			Almacen clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = Almacen.OpenSession();
			Almacen.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.InventarioAlmacenes.MarkAsNew();
			clon.Partidas.MarkAsNew();
			
			return clon;
		}

		public virtual void GetNewCode()
		{
			Serial = SerialInfo.GetNext(typeof(Almacen));
			Codigo = Serial.ToString(Resources.Defaults.ALMACEN_CODE_FORMAT);
		}

        public virtual bool CheckStock(ETipoFacturacion saleType, long oidProduct, decimal amount, decimal reserved_amount = 0)
        {
            Stocks product_stock = Stocks.GetByProductList(oidProduct);

            switch (saleType)
            {
                case ETipoFacturacion.Peso: return (product_stock.TotalKgs() + reserved_amount >= amount);
                case ETipoFacturacion.Unidad: return (product_stock.TotalUds() + reserved_amount >= amount);
                case ETipoFacturacion.Unitaria: return (product_stock.TotalUds() + reserved_amount >= amount);
            }

            return false;
        }

        public virtual void RemoveStock(Stock item)
        {
            if (item == null) return;
            RemoveStock(item.Oid, item.OidPartida);
        }
        public virtual void RemoveStock(long oidStock, long oidPartida)
        {
            Stocks.Remove(oidStock);
            Batch partida = Partidas.GetItem(oidPartida);
            UpdateStocks(partida, true);
        }

        public virtual void UpdateStocks(bool throwStockException)
		{
            foreach (Batch item in Partidas)
                UpdateStocks(item, throwStockException);
		}

        public virtual void UpdateStocks(Batch partida, bool throwStockException)
		{
            _stocks.UpdateStocks(partida, throwStockException);
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
            if (Nombre == string.Empty)
            {
                e.Description = Resources.Messages.NO_NAME;
                throw new iQValidationException(e.Description, string.Empty, "Nombre");
            }

            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.STOCK);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.STOCK);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.STOCK);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.STOCK);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Almacen () {}		
		private Almacen(Almacen source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		private Almacen(int sessionCode, IDataReader source, bool childs)
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
		public static Almacen NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Almacen>(new CriteriaCs(-1));
		}

		internal static Almacen GetChild(long oid, bool childs)
		{
			Almacen obj = Get(oid, childs);
			obj.MarkAsChild();

			return obj;
		}
		internal static Almacen GetChild(Almacen source)
		{
			return new Almacen(source, false);
		}
		internal static Almacen GetChild(Almacen source, bool childs)
		{
			return new Almacen(source, childs);
		}
		internal static Almacen GetChild(int sessionCode, IDataReader source)
        {
			return new Almacen(sessionCode, source, false);
        }
		internal static Almacen GetChild(int sessionCode, IDataReader source, bool childs)
        {
			return new Almacen(sessionCode, source, childs);
        }
		internal static Almacen GetChild(int sessionCode, long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Almacen.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Almacen.SELECT(oid);

			Almacen obj = DataPortal.Fetch<Almacen>(criteria);
			obj.MarkAsChild();

			return obj;
		}

		public virtual StoreInfo GetInfo()
		{
			return GetInfo(true);
		}
		public virtual StoreInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new StoreInfo(this, get_childs);
		}

        public virtual void LoadChilds(Type type, bool get_childs, bool throwStockException)
		{
			if (IsNew) return;

			if (type.Equals(typeof(Batch)))
			{
				if (_partidas.Count > 0) return;

				_partidas = Batchs.GetChildList(this, get_childs);
			}
			else if (type.Equals(typeof(Stock)))
			{
				if (_stocks.Count > 0) return;

                _stocks = Stocks.GetChildList(this, get_childs, throwStockException);
                UpdateStocks(throwStockException);
			}
		}

		public virtual void LoadChildsFromList(Type type, string list, bool get_childs)
		{
			if (type.Equals(typeof(Batch)))
			{
				_partidas = Batchs.GetChildListFromList(this, list, get_childs);
			}
			else if (type.Equals(typeof(Stock)))
			{
				_stocks = Stocks.GetChildListByPartidaFromList(this, list, get_childs);
			}
		}

		public virtual void LoadPartidasByProducto(long oid, bool childs)
		{
			if (Partidas.GetItemByProducto(oid) == null)
			{
				Batchs partidas = Batchs.GetChildListByProducto(this, oid, childs);

				foreach (Batch item in partidas)
					if ((Partidas.GetItem(item.Oid) == null) && (!Partidas.ContainsDeleted(item.Oid)))
						Partidas.AddItem(item);
			}
		}
		public virtual void LoadPartidasByExpediente(long oid, bool childs)
		{
			Batchs partidas = Batchs.GetChildListByExpediente(this, oid, childs);

			foreach (Batch item in partidas)
				if ((Partidas.GetItem(item.Oid) == null) && (!Partidas.ContainsDeleted(item.Oid)))
					Partidas.AddItem(item);
		}
		public virtual void LoadPartidasByAlbaranProveedor(long oid, bool childs)
		{
			Batchs partidas = Batchs.GetChildListByAlbaranRecibido(this, oid, childs);

			foreach (Batch item in partidas)
				if ((Partidas.GetItem(item.Oid) == null) && (!Partidas.ContainsDeleted(item.Oid)))
					Partidas.AddItem(item);
		}
		public virtual void LoadStockByPartida(long oid, bool childs, bool throwStockException)
		{
			if (_stocks.GetItemByBatch(oid) == null)
			{
				Stocks stocks = Stocks.GetChildListByPartida(this, oid, childs);

				foreach (Stock item in stocks)
					_stocks.AddItem(item);

				_stocks.UpdateStocks(Partidas.GetItem(oid), throwStockException);
			}
		}
        public virtual void LoadStockByProducto(long oid, bool childs, bool throwStockException)
		{
			if (_stocks.GetItemByProduct(oid) == null)
			{
				Stocks stocks = Stocks.GetChildListByProducto(this, oid, childs);

				foreach (Stock item in stocks)
					_stocks.AddItem(item);

                _stocks.UpdateStocks(this, throwStockException);
			}
		}

		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Almacen New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Almacen>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static Almacen Get(long oid) { return Get(oid, true); }
		public static Almacen Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Almacen.GetCriteria(Almacen.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Almacen.SELECT(oid);
			
			Almacen.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Almacen>(criteria);
		}
		public static Almacen Get(long oid, bool childs, bool cache)
		{
			Almacen item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(Stores)))
			{
				Stores items = Stores.NewList();

				item = Almacen.GetChild(oid, childs);
				items.AddItem(item);
				items.SessionCode = item.SessionCode;
				Cache.Instance.Save(typeof(Stores), items);
			}
			else
			{
				Stores items = Cache.Instance.Get(typeof(Stores)) as Stores;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = Almacen.GetChild(items.SessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(Stores), items);
				}
			}

			return item;
		}
		public static Almacen Get(long oid, bool childs, bool cache, int sessionCode)
		{
			Almacen item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(Stores)))
			{
				Stores items = Stores.NewList();

				item = Almacen.GetChild(sessionCode, oid, childs);
				items.AddItem(item);
				items.SessionCode = item.SessionCode;
				Cache.Instance.Save(typeof(Stores), items);
			}
			else
			{
				Stores items = Cache.Instance.Get(typeof(Stores)) as Stores;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = Almacen.GetChild(items.SessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(Stores), items);
				}
			}

			return item;
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
		/// Elimina todos los Almacen. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Almacen.OpenSession();
			ISession sess = Almacen.Session(sessCode);
			ITransaction trans = Almacen.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from AlmacenRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Almacen.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Almacen Save()
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
				
				_inventarios.Update(this);				
				_partidas.Update(this);
				_stocks.Update(this);
				
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

		public override Almacen SaveAsChild()
		{
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				base.SaveAsChild();

				if (_save_childs)
				{
					_partidas.Update(this);
					_stocks.Update(this);
				}

				return this;
			}
			catch (Exception ex)
			{
				//if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally	{}
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
			Oid = (long)(new Random().Next());
			GetNewCode();
			EEstado = EEstado.Active;
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Almacen source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

				if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						/*InventarioAlmacen.DoLOCK(Session());
                        string query = InventarioAlmacenes.SELECT_BY_FIELD("OidAlmacen", this.Oid);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _inventarios = InventarioAlmacenes.GetChildList(reader, false);
						
						LineaAlmacen.DoLOCK(Session());
                        query = Partidas.SELECT_BY_FIELD("OidAlmacen", this.Oid);
                        reader = nHMng.SQLNativeSelect(query, Session());
							_partidas = Partidas.GetChildList(SessionCode, reader, false);*/						
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
						InventarioAlmacen.DoLOCK(Session());
                        string query = InventarioAlmacenes.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _inventarios = InventarioAlmacenes.GetChildList(reader, false);
						
						Batch.DoLOCK(Session());
                        query = Batchs.SELECT(this, true);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_partidas = Batchs.GetChildList(SessionCode, reader, false);

						Stock.DoLOCK(Session());
						query = Stocks.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_stocks = Stocks.GetChildList(SessionCode, reader, false);						
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
		internal void Insert(Stores parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);

				if (SaveChilds)
				{
					_partidas.Update(this);
					_stocks.Update(this);
				}
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
		internal void Update(Stores parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				AlmacenRecord obj = Session().Get<AlmacenRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
				
				if (SaveChilds)
				{
					_partidas.Update(this);
					_stocks.Update(this);
				}
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
		internal void DeleteSelf(Stores parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<AlmacenRecord>(Oid));
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
					Almacen.DoLOCK( Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						InventarioAlmacen.DoLOCK( Session());
						query = InventarioAlmacenes.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_inventarios = InventarioAlmacenes.GetChildList(reader);
						
						Batch.DoLOCK(Session());
                        query = Batchs.SELECT(this, true);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_partidas = Batchs.GetChildList(SessionCode, reader, false);

						Stock.DoLOCK(Session());
						query = Stocks.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_stocks = Stocks.GetChildList(SessionCode, reader, false);						
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
                GetNewCode();

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
					AlmacenRecord obj = Session().Get<AlmacenRecord>(Oid);
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
				Session().Delete(Session().Get<AlmacenRecord>(criteria.Oid));
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

		public new static string SELECT(long oid) { return SELECT(oid, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT AL.*";

			return query;
		}

		internal static string WHERE(Library.Store.QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query += " WHERE TRUE";

			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "AL");

			if (conditions.Almacen != null) query += " AND AL.\"OID\" = " + conditions.Almacen.Oid;

			return query;
		}

		internal static string SELECT(Library.Store.QueryConditions conditions, bool lockTable)
		{
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + al + " AS AL";

			query += WHERE(conditions);

			if (lockTable) query += " FOR UPDATE OF AL NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query;

			QueryConditions conditions = new QueryConditions { Almacen = Almacen.New().GetInfo(false) };
			conditions.Almacen.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

		#endregion		
	}
}

