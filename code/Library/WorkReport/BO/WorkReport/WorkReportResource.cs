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
//using moleQule.Library.Invoice;

namespace moleQule.Library.WorkReport
{
	[Serializable()]
	public class WorkReportResourceRecord : RecordBase
	{
		#region Attributes

		private long _oid_work_report;
		private long _oid_resource;
		private long _entity_type;
		private DateTime _from;
		private DateTime _till;
		private Decimal _amount;
		private Decimal _cost;
		private Decimal _hours;
		private Decimal _extra_cost;
        private Decimal _total;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties

		public virtual long OidWorkReport { get { return _oid_work_report; } set { _oid_work_report = value; } }
		public virtual long OidResource { get { return _oid_resource; } set { _oid_resource = value; } }
		public virtual long EntityType { get { return _entity_type; } set { _entity_type = value; } }
		public virtual Decimal Amount { get { return _amount; } set { _amount = value; } }
		public virtual DateTime From { get { return _from; } set { _from = value; } }
		public virtual DateTime Till { get { return _till; } set { _till = value; } }
		public virtual Decimal Cost { get { return _cost; } set { _cost = value; } }
		public virtual Decimal Hours { get { return _hours; } set { _hours = value; } }
		public virtual Decimal ExtraCost { get { return _extra_cost; } set { _extra_cost = value; } }
        public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public WorkReportResourceRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_work_report = Format.DataReader.GetInt64(source, "OID_WORK_REPORT");
			_oid_resource = Format.DataReader.GetInt64(source, "OID_RESOURCE");
			_entity_type = Format.DataReader.GetInt64(source, "ENTITY_TYPE");
			_amount = Format.DataReader.GetDecimal(source, "AMOUNT");
			_cost = Format.DataReader.GetDecimal(source, "COST");
			_from = Format.DataReader.GetDateTime(source, "FROM");
			_till = Format.DataReader.GetDateTime(source, "TILL");
			_hours = Format.DataReader.GetDecimal(source, "HOURS");
			_extra_cost = Format.DataReader.GetDecimal(source, "EXTRA_COST");
            _total = Format.DataReader.GetDecimal(source, "TOTAL");
			_comments = Format.DataReader.GetString(source, "COMMENTS");
		}		
		public virtual void CopyValues(WorkReportResourceRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_work_report = source.OidWorkReport;
			_oid_resource = source.OidResource;
			_entity_type = source.EntityType;
			_amount = source.Amount;
			_cost = source.Cost;
			_from = source.From;
			_till = source.Till;
			_hours = source.Hours;
			_extra_cost = source.ExtraCost;
            _total = source.Total;
			_comments = source.Comments;
		}
		
		#endregion	
	}

    [Serializable()]
	public class WorkReportResourceBase 
	{	 
		#region Attributes
		
		private WorkReportResourceRecord _record = new WorkReportResourceRecord();

        private long _oid_category;
		private string _category = string.Empty;
		private string _resource = string.Empty;
		private string _resource_id = string.Empty;
        private string _month = string.Empty;
        private string _year = string.Empty;
        private string _work_report_id = string.Empty;
        private string _expedient_id = string.Empty;

		#endregion
		
		#region Properties
		
		public WorkReportResourceRecord Record { get { return _record; } }

		public ETipoEntidad EEntityType { get { return (ETipoEntidad)_record.EntityType; } }
		public string EntityTypeLabel { get { return Library.Common.EnumText<ETipoEntidad>.GetLabel(EEntityType); } }
        public long OidCategory { get { return _oid_category; } set { _oid_category = value; } }
        public string Category { get { return _category; } set { _category = value; } }
		public string Resource { get { return _resource; } set { _resource = value; } }
		public string ResourceID { get { return _resource_id; } set { _resource_id = value; } }
        public string Month { get { return _record.From.ToString("MMM").ToUpper(); } }
        public string Year { get { return _record.From.ToString("yyyy"); } }
        public string WorkReportID { get { return _work_report_id; } set { _work_report_id = value; } }
        public string ExpedientID { get { return _expedient_id; } set { _expedient_id = value; } }

		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);

            _oid_category = Format.DataReader.GetInt64(source, "OID_CATEGORY");
			_category = Format.DataReader.GetString(source, "CATEGORY");
			_resource = Format.DataReader.GetString(source, "RESOURCE");
			_resource_id = Format.DataReader.GetString(source, "RESOURCE_CODE");
            _work_report_id = Format.DataReader.GetString(source, "WORK_REPORT_ID");
            _expedient_id = Format.DataReader.GetString(source, "EXPEDIENT_ID");
		}		
		public void CopyValues(WorkReportResource source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);

            _oid_category = source.OidCategory;
			_category = source.Category;
			_resource = source.Resource;
			_resource_id = source.ResourceID;
            _work_report_id = source.WorkReportID;
            _expedient_id = source.ExpedientID;
		}
		public void CopyValues(WorkReportResourceInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);

            _oid_category = source.OidCategory;
			_category = source.Category;
			_resource = source.Resource;
			_resource_id = source.ResourceID;
            _work_report_id = source.WorkReportID;
            _expedient_id = source.ExpedientID;
		}

        public decimal CalculateTotal()
        {
            switch (EEntityType)
            {
                case ETipoEntidad.Empleado:
                    return (_record.Cost * _record.Hours) + _record.ExtraCost;

                case ETipoEntidad.Tool:
                    return (_record.Cost * _record.Amount * _record.Hours) + _record.ExtraCost;

                case ETipoEntidad.OutputDelivery:
                    return _record.Cost + _record.ExtraCost;
            }

            return 0;
        }

		#endregion	
	}
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class WorkReportResource : BusinessBaseEx<WorkReportResource>
	{	 
		#region Attributes
		
		protected WorkReportResourceBase _base = new WorkReportResourceBase();		

		#endregion
		
		#region Properties
		
		public WorkReportResourceBase Base { get { return _base; } }
		
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
		public long OidWorkReport
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidWorkReport;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidWorkReport.Equals(value))
				{
					_base.Record.OidWorkReport = value;
					PropertyHasChanged();
				}
			}
		}
		public long OidResource
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidResource;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidResource.Equals(value))
				{
					_base.Record.OidResource = value;
					PropertyHasChanged();
				}
			}
		}
		public long EntityType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.EntityType;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.EntityType.Equals(value))
				{
					_base.Record.EntityType = value;
					PropertyHasChanged();
				}
			}
		}
		public Decimal Amount
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
                    Total = _base.CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public Decimal Cost
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Cost;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Cost.Equals(value))
				{
					_base.Record.Cost = value;
                    Total = _base.CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public DateTime From
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.From;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.From.Equals(value))
				{
					_base.Record.From = value;
					PropertyHasChanged();
				}
			}
		}
		public DateTime Till
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Till;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Till.Equals(value))
				{
					_base.Record.Till = value;
					PropertyHasChanged();
				}
			}
		}
		public Decimal Hours
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Hours;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Hours.Equals(value))
				{
					_base.Record.Hours = value;
                    Total = _base.CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public Decimal ExtraCost
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ExtraCost;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ExtraCost.Equals(value))
				{
					_base.Record.ExtraCost = value;
                    Total = _base.CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
        public Decimal Total
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
		public string Comments
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Comments;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Comments.Equals(value))
				{
					_base.Record.Comments = value;
					PropertyHasChanged();
				}
			}
		}

		public ETipoEntidad EEntityType { get { return _base.EEntityType; } set { EntityType = (long)value; } }
		public string EntityTypeLabel { get { return _base.EntityTypeLabel; } }
        public long OidCategory { get { return _base.OidCategory; } set { _base.OidCategory = value; } }
        public string Category { get { return _base.Category; } set { _base.Category = value; } }
		public string Resource { get { return _base.Resource; } set { _base.Resource = value; } }
		public string ResourceID { get { return _base.ResourceID; } set { _base.ResourceID = value; } }
        public string WorkReportID { get { return _base.WorkReportID; } set { _base.WorkReportID = value; } }
        public string ExpedientID { get { return _base.ExpedientID; } set { _base.ExpedientID = value; } }
        public string Month { get { return _base.Month; } }
        public string Year { get { return _base.Year; } }

		#endregion
		
		#region Business Methods
		
		public virtual WorkReportResource CloneAsNew()
		{
			WorkReportResource clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = WorkReportResource.OpenSession();
			WorkReportResource.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		protected virtual void CopyFrom(WorkReport source)
		{
			if (source == null) return;

			OidWorkReport = source.Oid;
			From = source.From;
			Till = source.Till;
			Hours = source.Hours;
		}
		protected virtual void CopyFrom(WorkReportResourceInfo source)
		{
			if (source == null) return;

			OidResource = source.OidResource;
			EntityType = source.EntityType;
			Cost = source.Cost;
			Amount = source.Amount;
			Hours = source.Amount;

			ResourceID = source.ResourceID;
			Resource = source.Resource;
		}
		public virtual void CopyFrom(IWorkResource source)
		{
			if (source == null) return;

			OidResource = source.Oid;
			EntityType = source.EntityType;
			Cost = source.Cost;
			Resource = source.Name;
			ResourceID = source.ID;
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
				e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EMPLEADO);
        }
        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EMPLEADO);
        }
        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EMPLEADO);
        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EMPLEADO);
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
		protected WorkReportResource()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí		
		}
				
		private WorkReportResource(WorkReportResource source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private WorkReportResource(int sessionCode, IDataReader source, bool childs)
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
		public static WorkReportResource NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			WorkReportResource obj = DataPortal.Create<WorkReportResource>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">WorkReportResource con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(WorkReportResource source, bool childs)
		/// <remarks/>
		internal static WorkReportResource GetChild(WorkReportResource source) { return new WorkReportResource(source, false); }
		internal static WorkReportResource GetChild(WorkReportResource source, bool childs) { return new WorkReportResource(source, childs); }
        internal static WorkReportResource GetChild(int sessionCode, IDataReader source) { return new WorkReportResource(sessionCode, source, false); }
        internal static WorkReportResource GetChild(int sessionCode, IDataReader source, bool childs) { return new WorkReportResource(sessionCode, source, childs); }

		public virtual WorkReportResourceInfo GetInfo (bool childs = true) { return new WorkReportResourceInfo(this, childs); }
		
		#endregion				
		
		#region Child Factory Methods
	
		public static WorkReportResource NewChild(WorkReport parent, WorkReportResourceInfo item)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			WorkReportResource obj = DataPortal.Create<WorkReportResource>(new CriteriaCs(-1));
			obj.CopyFrom(parent);
			obj.CopyFrom(item);
			obj.MarkAsChild();
			return obj;
		}
		public static WorkReportResource NewChild(WorkReport parent, IWorkResource item = null)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			WorkReportResource obj = DataPortal.Create<WorkReportResource>(new CriteriaCs(-1));
			obj.CopyFrom(parent);
			obj.CopyFrom(item);
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
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre, que
		/// debe utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override WorkReportResource Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
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
			Oid = (long)(new Random()).Next();
			Amount = 1;
			From = DateTime.Now;
			Till = DateTime.Now;
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(WorkReportResource source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}
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
		internal void Insert(WorkReportResources parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
	
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(WorkReportResources parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			WorkReportResourceRecord obj = Session().Get<WorkReportResourceRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(WorkReportResources parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<WorkReportResourceRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(WorkReport parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidWorkReport = parent.Oid;	
			
			ValidationRules.CheckRules();
			
			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(_base.Record);			
			
			MarkOld();
		}

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(WorkReport parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidWorkReport = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			WorkReportResourceRecord obj = parent.Session().Get<WorkReportResourceRecord>(Oid);
			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);
			
			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(WorkReport parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			if (EEntityType == ETipoEntidad.OutputDelivery)
				OutputDelivery.Delete(OidResource, ETipoEntidad.WorkReport, parent.SessionCode);

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<WorkReportResourceRecord>(Oid));

			MarkNew();
		}
		
		#endregion
				
        #region SQL

		internal enum EQueryType { GENERAL = 0, CLUSTERED = 1, STAFF = 2, TOOLS = 3, DELIVERIES = 4, ALL = 5 }
		
		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>()
						{ 
							{
								"OidCategory", 
								new ForeignField() {                        
									Property = "Oid", 
									TableAlias = "WC", 
									Column = nHManager.Instance.GetTableIDColumn(typeof(WorkReportCategoryRecord))
								}
							},
                            							{
								"Category", 
								new ForeignField() {                        
									Property = "Name", 
									TableAlias = "WC", 
									Column = nHManager.Instance.GetTableColumn(typeof(WorkReportCategoryRecord), "Name")
								}
							},
							{
								"CategoryMax", 
								new ForeignField() {                        
									Property = "CATEGORY", 
									TableAlias = string.Empty, 
									Column = null
								}
							},
							{
								"EntityTypeMax", 
								new ForeignField() {                        
									Property = "ENTITY_TYPE", 
									TableAlias = string.Empty, 
									Column = null
								}
							},
							{
								"Year", 
								new ForeignField() {                        
									Property = "YEAR", 
									TableAlias = string.Empty, 
									Column = null
								}
							},
                            {
								"Month", 
								new ForeignField() {                        
									Property = "MONTH", 
									TableAlias = string.Empty, 
									Column = null
								}
							},
                            {
								"WorkReportID", 
								new ForeignField() {                        
									Property = "WORK_REPORT_ID", 
									TableAlias = string.Empty, 
									Column = null
								}
							},
						};
        }

		public static SelectCaller local_caller_BASE = new SelectCaller(SELECT);

		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		public static string SELECT(WorkReport item) 
		{ 
			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions { WorkReport = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}		
		
        internal static string SELECT_FIELDS(EQueryType queryType, QueryConditions conditions)
        {
			string query;
			
			if (conditions.Groups == null)
			{
				query = @"
					SELECT 
						WO.*
                        ,COALESCE(WR.""CODE"", '') AS ""WORK_REPORT_ID""
						,COALESCE(WC.""NAME"", '') AS ""CATEGORY""
                        ,COALESCE(WC.""OID"", 0) AS ""OID_CATEGORY""
                        ,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENT_ID""";
			}
			else
			{
				query = @"
					SELECT 
						(MAX(WO.""OID"")::varchar || '00' || MAX(WO.""ENTITY_TYPE"")::varchar)::bigint AS ""OID""
						,MAX(WO.""OID_RESOURCE"") AS ""OID_RESOURCE""
						,MAX(WO.""OID_WORK_REPORT"") AS ""OID_WORK_REPORT""
						,MAX(WO.""ENTITY_TYPE"") AS ""ENTITY_TYPE""
						,1 AS ""AMOUNT""
						,MAX(WO.""FROM"") AS ""FROM""
						,MAX(WO.""TILL"") AS ""TILL""
						,SUM(WO.""HOURS"" * WO.""AMOUNT"") AS ""HOURS""
						,AVG(WO.""COST"") AS ""COST""
						,SUM(WO.""EXTRA_COST"") AS ""EXTRA_COST""
                        ,SUM(WO.""TOTAL"") AS ""TOTAL""
						,'' AS ""COMMENTS""
                        ,MAX(WR.""CODE"") AS ""WORK_REPORT_ID""
                        ,COALESCE(MAX(WC.""OID""), 0) AS ""OID_CATEGORY""
						,COALESCE(MAX(WC.""NAME""), '') AS ""CATEGORY""
                        ,COALESCE(MAX(EX.""CODIGO""), '') AS ""EXPEDIENT_ID""";
			}

			switch (queryType)
			{
                case EQueryType.ALL:

                    query += @"
						,'' AS ""RESOURCE_CODE""
						,'' AS ""RESOURCE""";

                    break;

				case EQueryType.STAFF:

					if (conditions.Groups == null)
					{
						query += @"
							,RS.""CODIGO"" AS ""RESOURCE_CODE""
							,RS.""APELLIDOS"" || ', ' || RS.""NOMBRE"" AS ""RESOURCE""";
					}
					else
					{
						query += @"
							,MAX(RS.""CODIGO"") AS ""RESOURCE_CODE""
							,MAX(RS.""APELLIDOS"") || ', ' || MAX(RS.""NOMBRE"") AS ""RESOURCE""";
					}
					break;

				case EQueryType.TOOLS:

					if (conditions.Groups == null)
					{
						query += @"
							,RS.""CODE"" AS ""RESOURCE_CODE""
							,RS.""NAME"" AS ""RESOURCE""";
					}
					else
					{ 
						query += @"
							,MAX(RS.""CODE"") AS ""RESOURCE_CODE""
							,MAX(RS.""NAME"") AS ""RESOURCE""";
					}
					break;

				case EQueryType.DELIVERIES:

					if (conditions.Groups == null)
					{
						query += @"
							,RS.""CODIGO"" AS ""RESOURCE_CODE""
							,'ALBARAN DE OBRA' AS ""RESOURCE""";
					}
					else
					{
						query += @"
							,MAX(RS.""CODIGO"") AS ""RESOURCE_CODE""
							,'ALBARAN DE OBRA' AS ""RESOURCE""";
					}

					break;

				case EQueryType.CLUSTERED:
					query = @"
                    SELECT " + (long)queryType + @" AS ""QUERY_TYPE"" 
                        ,DATE_TRUNC('" + conditions.Step.ToString() + @"', WO.""DATE"") AS ""STEP""
						,SUM(WO.""TOTAL"") AS ""TOTAL""";

					break;
			}

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string wo = nHManager.Instance.GetSQLTable(typeof(WorkReportResourceRecord));
			string wr = nHManager.Instance.GetSQLTable(typeof(WorkReportRecord));
			string wc = nHManager.Instance.GetSQLTable(typeof(WorkReportCategoryRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = @"
			FROM " + wo + @" AS WO
			INNER JOIN " + wr + @" AS WR ON WR.""OID"" = WO.""OID_WORK_REPORT""
			LEFT JOIN " + wc + @" AS WC ON WC.""OID"" = WR.""CATEGORY""
            LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = WR.""OID_EXPEDIENT""";
				
			return query + " " + conditions.ExtraJoin;
		}
		internal static string JOIN_RESOURCE(ETipoEntidad entityType, QueryConditions conditions)
		{
			string query = string.Empty;

            switch (entityType)
			{
				case ETipoEntidad.OutputDelivery:
					{
						string rs = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
						query += @"
						INNER JOIN " + rs + @" AS RS ON RS.""OID"" = WO.""OID_RESOURCE"" AND WO.""ENTITY_TYPE"" = " + (long)entityType;
					}
					break;

				case ETipoEntidad.Empleado:
					{
						string rs = nHManager.Instance.GetSQLTable(typeof(EmployeeRecord));
						query += @"
						INNER JOIN " + rs + @" AS RS ON RS.""OID"" = WO.""OID_RESOURCE"" AND WO.""ENTITY_TYPE"" = " + (long)entityType;
					}
					break;

				case ETipoEntidad.Tool:
					{
						string rs = nHManager.Instance.GetSQLTable(typeof(ToolRecord));
						query += @"
						INNER JOIN " + rs + @" AS RS ON RS.""OID"" = WO.""OID_RESOURCE"" AND WO.""ENTITY_TYPE"" = " + (long)entityType;
					}
					break;
			}
			
			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;
		
			string query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "WO", ForeignFields());

			query += @" 
				AND (WO.""FROM"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "WO");
			
            if (conditions.WorkReport != null) 
				query += @"
					AND WO.""OID_WORK_REPORT"" = " + conditions.WorkReport.Oid;

			if (conditions.EntityType != ETipoEntidad.Todos)
				query += @"
					AND WO.""ENTITY_TYPE"" = " + (long)conditions.EntityType;

			if (conditions.Expedient != null)
				query += @"
					AND WR.""OID_EXPEDIENT"" = " + conditions.Expedient.Oid;

            if (conditions.WorkReportCategory != null)
                query += @"
					AND WC.""OID"" = " + conditions.WorkReportCategory.Oid;

			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = string.Empty;
		
			if (conditions.Step != EStepGraph.None)
			{
				query =
					SELECT_FIELDS(EQueryType.CLUSTERED, conditions) +
					JOIN(conditions);
			}
			else
			{
				switch (conditions.EntityType)
				{
					case ETipoEntidad.Empleado:
						
						query = SELECT_EMPLOYEES(conditions);

						break;

					case ETipoEntidad.OutputDelivery:
						
						query = SELECT_DELIVERIES(conditions);

						break;

					case ETipoEntidad.Tool:
						
						query = SELECT_TOOLS(conditions);

						break;

					case ETipoEntidad.Todos:
					default:

                        if (conditions.Groups != null
                            && conditions.Groups.Count == 1
                            && conditions.Groups.Contains("OidCategory"))
                        {
                            query =
                                SELECT_ALL(conditions);
                        }
                        else
                        {
                            query =
                                SELECT_EMPLOYEES(conditions) + @"
							UNION " +
                                SELECT_TOOLS(conditions) + @"
							UNION " +
                                SELECT_DELIVERIES(conditions);
                        }

						break;
				}
			}

			if (conditions.Groups != null)
			{
				query += ORDER(conditions.Orders, "WO", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}
			else
			{
				if (conditions.Step != EStepGraph.None)
				{
					query += @"
					GROUP BY ""STEP""
					ORDER BY ""STEP""";
				}
				else
				{
					query += ORDER(conditions.Orders, "WO", ForeignFields());
					query += LIMIT(conditions.PagingInfo);
				}
			}		

			query += Common.Common.EntityBase.LOCK("WO", lockTable);

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

        internal static string SELECT_ALL(QueryConditions conditions)
        {
            string query =
                SELECT_FIELDS(EQueryType.ALL, conditions) +
                JOIN(conditions) +
                WHERE(conditions);

            if (conditions.Groups != null)
                query += GROUPBY(conditions.Groups, "WO", ForeignFields());

            return query;
        }

		internal static string SELECT_DELIVERIES(QueryConditions conditions)
		{
			conditions.EntityType = ETipoEntidad.OutputDelivery;

			conditions.ExtraJoin = JOIN_RESOURCE(ETipoEntidad.OutputDelivery, conditions);

			string query =
				SELECT_FIELDS(EQueryType.DELIVERIES, conditions) +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions.Groups != null)
				query += GROUPBY(conditions.Groups, "WO", ForeignFields());

			return query;
		}

		internal static string SELECT_EMPLOYEES(QueryConditions conditions)
		{
			conditions.EntityType = ETipoEntidad.Empleado;

			conditions.ExtraJoin = JOIN_RESOURCE(ETipoEntidad.Empleado, conditions);

			string query =
				SELECT_FIELDS(EQueryType.STAFF, conditions) +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions.Groups != null)
				query += GROUPBY(conditions.Groups, "WO", ForeignFields());

			return query;
		}

		internal static string SELECT_TOOLS(QueryConditions conditions)
		{
			conditions.EntityType = ETipoEntidad.Tool;

			conditions.ExtraJoin = JOIN_RESOURCE(ETipoEntidad.Tool, conditions);

			string query =
				SELECT_FIELDS(EQueryType.TOOLS, conditions) +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions.Groups != null)
				query += GROUPBY(conditions.Groups, "WO", ForeignFields());

			return query;
		}

        internal static string SELECT_BY_EXPEDIENTS(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere += @"
                AND WR.""OID_EXPEDIENT"" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

            conditions.OidList = null;

            string query =
            SELECT(conditions, lockTable);

            return query;
        }

        internal static string SELECT_BY_EMPLOYEES(QueryConditions conditions, bool lockTable)
        {
            conditions.EntityType = ETipoEntidad.Empleado;

            conditions.ExtraWhere += @"
                AND WO.""OID_RESOURCE"" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

            conditions.OidList = null;

            conditions.ExtraJoin = JOIN_RESOURCE(ETipoEntidad.Empleado, conditions);

            string query =
            SELECT_FIELDS(EQueryType.STAFF, conditions) + @"
                ,DATE_TRUNC('" + EStepGraph.Year.ToString() + @"', WO.""FROM"") AS ""YEAR""
                ,DATE_TRUNC('" + EStepGraph.Month.ToString() + @"', WO.""FROM"") AS ""MONTH""" +
            JOIN(conditions) +
            WHERE(conditions) +
            GROUPBY(conditions.Groups, "WO", ForeignFields()) +
            ORDER(conditions.Orders, "WO", ForeignFields());

            query += Common.Common.EntityBase.LOCK("WO", lockTable);

            return query;
        }

		#endregion
	}
}
