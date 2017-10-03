using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using moleQule;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class TipoAnimalRecord : RecordBase
	{
		#region Attributes

		private string _valor = string.Empty;

		#endregion

		#region Properties

		public virtual string Valor { get { return _valor; } set { _valor = value; } }

		#endregion

		#region Business Methods

		public TipoAnimalRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_valor = Format.DataReader.GetString(source, "VALOR");

		}
		public virtual void CopyValues(TipoAnimalRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_valor = source.Valor;
		}

		#endregion
	}

	[Serializable()]
	public class TipoAnimalBase
	{
		#region Attributes

		private TipoAnimalRecord _record = new TipoAnimalRecord();

		#endregion

		#region Properties

		public TipoAnimalRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(TipoAnimal source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(TipoAnimalInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class TipoAnimal : BusinessBaseEx<TipoAnimal>
	{
		#region Attributes

		protected TipoAnimalBase _base = new TipoAnimalBase();

		#endregion

		#region Properties

		public TipoAnimalBase Base { get { return _base; } }

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
		public virtual string Valor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Valor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Valor.Equals(value))
				{
					_base.Record.Valor = value;
					PropertyHasChanged();
				}
			}
		}

		#endregion
		 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		protected override void AddBusinessRules()
        {	
			ValidationRules.AddRule(CommonRules.StringRequired, "Valor");
			
			//Agregar otras reglas de validación
        }
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES);
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public TipoAnimal() 
		{ 
			MarkAsChild();
			Random r = new Random();
            Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private TipoAnimal(TipoAnimal source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private TipoAnimal(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static TipoAnimal NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TipoAnimal();
		}
		
		
		internal static TipoAnimal GetChild(TipoAnimal source)
		{
			return new TipoAnimal(source);
		}
		
		internal static TipoAnimal GetChild(IDataReader reader)
		{
			return new TipoAnimal(reader);
		}
		
		public virtual TipoAnimalInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
				  moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TipoAnimalInfo(this);
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
		public override TipoAnimal Save()
		{
			throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(TipoAnimal source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		
		internal void Insert(TipoAnimales parent)
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

		internal void Update(TipoAnimales parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				TipoAnimalRecord obj = Session().Get<TipoAnimalRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(TipoAnimales parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<TipoAnimalRecord>(Oid));
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
				SELECT TI.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string ti = nHManager.Instance.GetSQLTable(typeof(TipoAnimalRecord));

			string query;

			query = @"
				FROM " + ti + @" AS TI";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "TI", ForeignFields());

			query = @" 
				AND (TI.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "TI");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "TI");

			/*if (conditions.TIPOANIMAL != null)
				query += @"
					AND T.""OID"" = " + conditions.TIPOANIMAL.Oid;*/

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query;

			query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "TI", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("TI", lockTable);

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
			return SELECT(new QueryConditions { TipoAnimal = TipoAnimalInfo.New(oid) }, lockTable);
		}

		#endregion
	}

}

