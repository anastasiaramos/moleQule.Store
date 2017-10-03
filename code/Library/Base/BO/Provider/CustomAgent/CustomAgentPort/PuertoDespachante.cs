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
	public class PuertoDespachanteRecord : RecordBase
	{
		#region Attributes

		private long _oid_puerto;
		private long _oid_despachante;

		#endregion

		#region Properties

		public virtual long OidPuerto { get { return _oid_puerto; } set { _oid_puerto = value; } }
		public virtual long OidDespachante { get { return _oid_despachante; } set { _oid_despachante = value; } }

		#endregion

		#region Business Methods

		public PuertoDespachanteRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_puerto = Format.DataReader.GetInt64(source, "OID_PUERTO");
			_oid_despachante = Format.DataReader.GetInt64(source, "OID_DESPACHANTE");

		}
		public virtual void CopyValues(PuertoDespachanteRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_puerto = source.OidPuerto;
			_oid_despachante = source.OidDespachante;
		}

		#endregion
	}

	[Serializable()]
	public class PuertoDespachanteBase
	{
		#region Attributes

		private PuertoDespachanteRecord _record = new PuertoDespachanteRecord();

		#endregion

		#region Properties

		public PuertoDespachanteRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(PuertoDespachante source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(PuertoDespachanteInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
	[Serializable()]
	public class PuertoDespachante : BusinessBaseEx<PuertoDespachante>
	{
		#region Attributes

		protected PuertoDespachanteBase _base = new PuertoDespachanteBase();

		#endregion

		#region Properties

		public PuertoDespachanteBase Base { get { return _base; } }

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
		public virtual long OidPuerto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPuerto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPuerto.Equals(value))
				{
					_base.Record.OidPuerto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidDespachante
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidDespachante;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidDespachante.Equals(value))
				{
					_base.Record.OidDespachante = value;
					PropertyHasChanged();
				}
			}
		}

		#endregion
	
	    #region Business Methods
		 
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
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public PuertoDespachante() 
		{ 
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
		}	
		
		private PuertoDespachante(PuertoDespachante source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private PuertoDespachante(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static PuertoDespachante NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PuertoDespachante();
		}		
		public static PuertoDespachante NewChild(Despachante parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			PuertoDespachante obj = new PuertoDespachante();
			obj.OidDespachante = parent.Oid;
			
			return obj;
		}		
		public static PuertoDespachante NewChild(Puerto parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			PuertoDespachante obj = new PuertoDespachante();
			obj.OidPuerto = parent.Oid;
			
			return obj;
		}
        public static PuertoDespachante NewChild(DespachanteInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            PuertoDespachante obj = new PuertoDespachante();
            obj.OidDespachante = parent.Oid;

            return obj;
        }
        public static PuertoDespachante NewChild(PuertoInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            PuertoDespachante obj = new PuertoDespachante();
            obj.OidPuerto = parent.Oid;

            return obj;
        }
		
		
		internal static PuertoDespachante GetChild(PuertoDespachante source)
		{
			return new PuertoDespachante(source);
		}		
		internal static PuertoDespachante GetChild(IDataReader reader)
		{
			return new PuertoDespachante(reader);
		}
		
		public virtual PuertoDespachanteInfo GetInfo()
		{
			return new PuertoDespachanteInfo(this);
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
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override PuertoDespachante Save()
		{
			throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(PuertoDespachante source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Despachante parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidDespachante = parent.Oid;

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

		internal void Update(Despachante parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidDespachante = parent.Oid;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				SessionCode = parent.SessionCode;
				PuertoDespachanteRecord obj = Session().Get<PuertoDespachanteRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Despachante parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PuertoDespachanteRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(Puerto parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidPuerto = parent.Oid;

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

		internal void Update(Puerto parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidPuerto = parent.Oid;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);
				
				SessionCode = parent.SessionCode;
				PuertoDespachanteRecord obj = Session().Get<PuertoDespachanteRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Puerto parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<PuertoDespachanteRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion

		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT PU.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string pu = nHManager.Instance.GetSQLTable(typeof(PuertoDespachanteRecord));

			string query;

			query = @"
				FROM " + pu + @" AS PU";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "PU", ForeignFields());

//            query += @" 
//				AND (PU.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "PU");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "PU");

			if (conditions.PuertoDespachante != null)
				query += @"
					AND PU.""OID"" = " + conditions.PuertoDespachante.Oid;

			if (conditions.Acreedor != null)
				query += @"
					AND PU.""OID_DESPACHANTE"" = " + conditions.Acreedor.Oid;
			
			if (conditions.Puerto != null)
				query += @"
					AND PU.""OID_PUERTO"" = " + conditions.Puerto.Oid;

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
				query += ORDER(conditions.Orders, "PU", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("PU", lockTable);

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

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { PuertoDespachante = PuertoDespachanteInfo.New(oid) }, lockTable);
		}

		#endregion	
	}
}

