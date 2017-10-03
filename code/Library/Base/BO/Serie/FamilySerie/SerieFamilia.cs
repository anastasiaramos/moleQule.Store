using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class SerieFamiliaRecord : RecordBase
	{
		#region Attributes

		private long _oid_serie;
		private long _oid_familia;

		#endregion

		#region Properties

		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual long OidFamilia { get { return _oid_familia; } set { _oid_familia = value; } }

		#endregion

		#region Business Methods

		public SerieFamiliaRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");

		}
		public virtual void CopyValues(SerieFamiliaRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_serie = source.OidSerie;
			_oid_familia = source.OidFamilia;
		}

		#endregion
	}

	[Serializable()]
	public class SerieFamiliaBase
	{
		#region Attributes

		private SerieFamiliaRecord _record = new SerieFamiliaRecord();

		private string _familia;

		#endregion

		#region Properties

		public SerieFamiliaRecord Record { get { return _record; } }

		public virtual string Familia { get { return _familia; } set { _familia = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_familia = Format.DataReader.GetString(source, "FAMILIA");
		}
		internal void CopyValues(SerieFamilia source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_familia = source.Familia;
		}
		internal void CopyValues(SerieFamiliaInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_familia = source.Familia;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
	[Serializable()]
	public class SerieFamilia : BusinessBaseEx<SerieFamilia>
	{
		#region Attributes

		protected SerieFamiliaBase _base = new SerieFamiliaBase();

		#endregion

		#region Properties

		public SerieFamiliaBase Base { get { return _base; } }

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
		public virtual long OidFamilia
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidFamilia;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidFamilia.Equals(value))
				{
					_base.Record.OidFamilia = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual string Familia { get { return _base.Familia; } set { _base.Familia = value; } }

		#endregion
		 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		//Descomentar en caso de existir reglas de validación
		protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(
                Csla.Validation.CommonRules.MinValue<Decimal>,
                new Csla.Validation.CommonRules.MinValueRuleArgs<Decimal>("Precio", 0));
        }
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_RECIBIDA);
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public SerieFamilia() 
		{ 
			MarkAsChild();
            Oid = (long)(new Random()).Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private SerieFamilia(SerieFamilia source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		private SerieFamilia(int sessionCode, IDataReader reader, bool childs)
		{
			MarkAsChild();
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static SerieFamilia NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new SerieFamilia();
		}
		
		public static SerieFamilia NewChild(Serie parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			SerieFamilia obj = new SerieFamilia();
			obj.OidSerie = parent.Oid;
			
			return obj;
		}
		
		internal static SerieFamilia GetChild(SerieFamilia source)
		{
			return new SerieFamilia(source);
		}
		
		internal static SerieFamilia GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new SerieFamilia(sessionCode, reader, childs);
		}

        public virtual SerieFamiliaInfo GetInfo()
		{
            return new SerieFamiliaInfo(this);
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
		public override SerieFamilia Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}

        /// <summary>
        /// Devuelve el ProductoCliente con oid
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static SerieFamilia Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = SerieFamilia.GetCriteria(SerieFamilia.OpenSession());
            
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SerieFamilia.SELECT(oid);

            SerieFamilia.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<SerieFamilia>(criteria);
        }
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(SerieFamilia source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Serie parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidSerie = parent.Oid;

			try
			{	
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void Update(Serie parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidSerie = parent.Oid;

			try
			{
				SessionCode = parent.SessionCode;
				SerieFamiliaRecord obj = Session().Get<SerieFamiliaRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

        internal void DeleteSelf(Serie parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<SerieFamiliaRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		#endregion

        #region SQL

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT SF.*" +
                    "       ,FM.\"NOMBRE\" AS \"FAMILIA\"";

            return query;
        }

        internal static string SELECT(long oid, bool lockTable)
        {
            string sf = nHManager.Instance.GetSQLTable(typeof(SerieFamiliaRecord));
            string fm = nHManager.Instance.GetSQLTable(typeof(FamilyRecord));
            string query;

            query = SELECT_FIELDS() +
                    " FROM " + sf + " AS SF" +
                    " INNER JOIN " + fm + " AS FM ON SF.\"OID_FAMILIA\" = FM.\"OID\"";

            if (oid > 0) query += " WHERE SF.\"OID\" = " + oid;

            //if (lockTable) query += " FOR UPDATE OF S NOWAIT;";

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

            query = SELECT(0, false);

            if (conditions.Serie != null) query += " AND SF.\"OID_SERIE\" = " + conditions.Serie.Oid;
            if (conditions.Familia != null) query += " AND SF.\"OID_FAMILIA\" = " + conditions.Familia.Oid;

            //if (lockTable) query += " FOR UPDATE OF SF NOWAIT";

            return query;
        }

        #endregion
	}
}

