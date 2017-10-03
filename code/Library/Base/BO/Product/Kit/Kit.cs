using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class KitRecord : RecordBase
	{
		#region Attributes

		private long _oid_kit;
		private long _oid_product;
		private Decimal _amount;
  
		#endregion
		
		#region Properties
		
		public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
		public virtual long OidProduct { get { return _oid_product; } set { _oid_product = value; } }
		public virtual Decimal Amount { get { return _amount; } set { _amount = value; } }

		#endregion
		
		#region Business Methods
		
		public KitRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
			_oid_product = Format.DataReader.GetInt64(source, "OID_PRODUCT");
			_amount = Format.DataReader.GetDecimal(source, "AMOUNT");

		}		
		public virtual void CopyValues(KitRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_kit = source.OidKit;
			_oid_product = source.OidProduct;
			_amount = source.Amount;
		}
		
		#endregion	
	}

    [Serializable()]
	public class KitBase 
	{	 
		#region Attributes
		
		private KitRecord _record = new KitRecord();
		private string _product = string.Empty;
        private decimal _purchase_price = 0;

		#endregion
		
		#region Properties
		
		public KitRecord Record { get { return _record; } }

		public string Product { get { return _product; } set { _product = value; } }
        public decimal PurchasePrice { get { return _purchase_price; } set { _purchase_price = value; } }

		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);

			_product = Format.DataReader.GetString(source, "PRODUCT");
            _purchase_price = Format.DataReader.GetDecimal(source, "PURCHASE_PRICE");
		}		
		public void CopyValues(Kit source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);

			_product = source.Product;
            _purchase_price = source.PurchasePrice;
		}
		public void CopyValues(KitInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);

			_product = source.Product;
            _purchase_price = source.PurchasePrice;
		}
		
		#endregion	
	}
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class Kit : BusinessBaseEx<Kit>
	{	 
		#region Attributes
		
		protected KitBase _base = new KitBase();
		

		#endregion
		
		#region Properties
		
		public KitBase Base { get { return _base; } }
		
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
		public virtual long OidKit
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidKit;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidKit.Equals(value))
				{
					_base.Record.OidKit = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidProduct
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidProduct;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidProduct.Equals(value))
				{
					_base.Record.OidProduct = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Amount
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Amount;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Amount.Equals(value))
				{
					_base.Record.Amount = value;
					PropertyHasChanged();
				}
			}
		}

		public string Product { get { return _base.Product; } set { _base.Product = value; } }
        public decimal PurchasePrice { get { return _base.PurchasePrice; } set { _base.PurchasePrice = value; } }

		#endregion
		
		#region Business Methods
		
		public virtual Kit CloneAsNew()
		{
			Kit clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = Kit.OpenSession();
			Kit.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom(KitInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidKit = source.OidKit;
			OidProduct = source.OidProduct;
			Amount = source.Amount;

			Product = source.Product;
            PurchasePrice = source.PurchasePrice;
		}
		protected virtual void CopyFrom(Product kit, ProductInfo component)
		{
			if (kit == null) return;

			OidKit = kit.Oid;
			OidProduct = component.Oid;
			Product = component.Nombre;
            PurchasePrice = component.PrecioCompra;
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
				e.Description = String.Format(moleQule.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
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
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected Kit ()
		{
			Oid = (long)(new Random()).Next();
			Amount = 1;
		}				
		private Kit(Kit source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Kit(int sessionCode, IDataReader source, bool childs)
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
		public static Kit NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			Kit obj = DataPortal.Create<Kit>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		internal static Kit GetChild(Kit source) { return new Kit(source, false); }
		internal static Kit GetChild(Kit source, bool childs) { return new Kit(source, childs); }
        internal static Kit GetChild(int sessionCode, IDataReader source) { return new Kit(sessionCode, source, false); }
        internal static Kit GetChild(int sessionCode, IDataReader source, bool childs) { return new Kit(sessionCode, source, childs); }
		
		public virtual KitInfo GetInfo (bool childs = true) { return new KitInfo(this, childs); }
		
		#endregion				
		
		#region Child Factory Methods
			
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static Kit NewChild(Product kit, ProductInfo component)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			Kit obj = new Kit();
			obj.CopyFrom(kit, component);
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
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre, que
		/// debe utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override Kit Save()
		{
			throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
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
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Kit source)
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
		internal void Insert(Kits parent)
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
		internal void Update(Kits parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			KitRecord obj = Session().Get<KitRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Kits parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<KitRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
				
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidKit = parent.Oid;	
			
			ValidationRules.CheckRules();
			
			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(_base.Record);			
			
			MarkOld();
		}

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidKit = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			KitRecord obj = parent.Session().Get<KitRecord>(Oid);
			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);
			
			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<KitRecord>(Oid));

			MarkNew();
		}
		
		#endregion
				
        #region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		public static string SELECT(Product item) 
		{ 
			moleQule.Library.Store.QueryConditions conditions = new moleQule.Library.Store.QueryConditions { Producto = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}		
		
        internal static string SELECT_FIELDS()
        {
            string query;

			query = @"
			SELECT KI.* 
					,PT.""NOMBRE"" AS ""PRODUCT""
                    ,PT.""PRECIO_COMPRA"" AS ""PURCHASE_PRICE""";

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string ki = nHManager.Instance.GetSQLTable(typeof(KitRecord));
			string pt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));

			string query;

            query = @"
			FROM " + ki + @" AS KI
			INNER JOIN " + pt + @" AS PT ON PT.""OID"" = KI.""OID_PRODUCT""";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;
		
			string query;

            query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "KI", ForeignFields());				

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "KI");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "KI");
			
            if (conditions.Kit != null)
				query += @"
					AND KI.""OID_KIT"" = " + conditions.Kit.Oid;				
			
            if (conditions.Producto != null) 
				query += @"
					AND KI.""OID_PRODUCTO"" = " + conditions.Producto.Oid;
            
			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = 
			SELECT_FIELDS() + 
			JOIN(conditions) +
			WHERE(conditions);

            if (conditions != null) 
			{
				query += ORDER(conditions.Orders, "KI", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}				

			query += Common.EntityBase.LOCK("KI", lockTable);

            return query;
        }
		
		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}		
	
		#endregion
	}
}