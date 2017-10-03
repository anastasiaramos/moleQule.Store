using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;

namespace moleQule.Library.WorkReport
{
	[Serializable()]
	public class WorkReportRecord : RecordBase
	{
		#region Attributes

		private long _oid_owner;
		private long _oid_expedient;
		private long _serial;
		private string _code = string.Empty;
		private long _status;
		private DateTime _date;
		private DateTime _from;
		private DateTime _till;
		private Decimal _hours;
		private Decimal _total;
		private long _category;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties
		
		public virtual long OidOwner { get { return _oid_owner; } set { _oid_owner = value; } }
		public virtual long OidExpedient { get { return _oid_expedient; } set { _oid_expedient = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Code { get { return _code; } set { _code = value; } }
		public virtual long Status { get { return _status; } set { _status = value; } }
		public virtual DateTime Date { get { return _date; } set { _date = value; } }
		public virtual DateTime From { get { return _from; } set { _from = value; } }
		public virtual DateTime Till { get { return _till; } set { _till = value; } }
		public virtual Decimal Hours { get { return _hours; } set { _hours = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual long Category { get { return _category; } set { _category = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public WorkReportRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_owner = Format.DataReader.GetInt64(source, "OID_OWNER");
			_oid_expedient = Format.DataReader.GetInt64(source, "OID_EXPEDIENT");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_code = Format.DataReader.GetString(source, "CODE");
			_status = Format.DataReader.GetInt64(source, "STATUS");
			_date = Format.DataReader.GetDateTime(source, "DATE");
			_from = Format.DataReader.GetDateTime(source, "FROM");
			_till = Format.DataReader.GetDateTime(source, "TILL");
			_hours = Format.DataReader.GetDecimal(source, "HOURS");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_category = Format.DataReader.GetInt64(source, "CATEGORY");
			_comments = Format.DataReader.GetString(source, "COMMENTS");
		}		
		public virtual void CopyValues(WorkReportRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_owner = source.OidOwner;
			_oid_expedient = source.OidExpedient;
			_serial = source.Serial;
			_code = source.Code;
			_status = source.Status;
			_date = source.Date;
			_from = source.From;
			_till = source.Till;
			_hours = source.Hours;
			_total = source.Total;
			_category = source.Category;
			_comments = source.Comments;
		}
		
		#endregion	
	}

    [Serializable()]
	public class WorkReportBase 
	{	 
		#region Attributes
		
		private WorkReportRecord _record = new WorkReportRecord();

		private string _expedient = string.Empty;
		private string _owner = string.Empty;
		private string _category_name = string.Empty;

		#endregion
		
		#region Properties
		
		public WorkReportRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Status; } }
		public string StatusLabel { get { return Base.EnumText<EEstado>.GetLabel(EStatus); } }
		public string Expedient { get { return _expedient; } set { _expedient = value; } }
		public string Owner { get { return _owner; } set { _owner = value; } }
		public long Hours { get { return DateAndTime.DateDiff(DateInterval.Hour, _record.From, _record.Till); } }
		public string CategoryName { get { return _category_name; } set { _category_name = value; } }

		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);

			_expedient = Format.DataReader.GetString(source, "EXPEDIENT");
			_owner = Format.DataReader.GetString(source, "OWNER");
			_category_name = Format.DataReader.GetString(source, "CATEGORY_NAME");
		}		
		public void CopyValues(WorkReport source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);

			_expedient = source.Expedient;
			_owner = source.Owner;
			_category_name = source.CategoryName;
		}
		public void CopyValues(WorkReportInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);

			_expedient = source.Expedient;
			_owner = source.Owner;
			_category_name = source.CategoryName;
		}
		
		#endregion	
	}
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class WorkReport : BusinessBaseEx<WorkReport>
	{	 
		#region Attributes
		
		protected WorkReportBase _base = new WorkReportBase();	

		private WorkReportResources _lines = WorkReportResources.NewChildList();

		#endregion
		
		#region Properties
		
		public WorkReportBase Base { get { return _base; } }
		
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
		public virtual long OidOwner
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidOwner;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidOwner.Equals(value))
				{
					_base.Record.OidOwner = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidExpedient
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpedient;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidExpedient.Equals(value))
				{
					_base.Record.OidExpedient = value;
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
		public virtual string Code
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Code;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Code.Equals(value))
				{
					_base.Record.Code = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Status
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Status;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Status.Equals(value))
				{
					_base.Record.Status = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Date
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Date;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Date.Equals(value))
				{
					_base.Record.Date = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime From
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
		public virtual DateTime Till
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
		public virtual Decimal Hours
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
					PropertyHasChanged();
				}
			}
		}
		public virtual decimal Total
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
		public virtual long Category
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Category;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Category.Equals(value))
				{
					_base.Record.Category = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Comments
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

		public virtual WorkReportResources Lines
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _lines;
			}
		}
			
		//LINKED
		public virtual EEstado EStatus { get { return _base.EStatus; } set { Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual string Owner { get { return _base.Owner; } set { _base.Owner = value; } }
		public virtual string Expedient { get { return _base.Expedient; } set { _base.Expedient = value; } }
		public virtual string CategoryName { get { return _base.CategoryName; } set { _base.CategoryName = value; } }
		
		/// <summary>
        /// Indica si el objeto está validado
        /// </summary>
		/// <remarks>Para añadir una lista: && _lista.IsValid<remarks/>
		public override bool IsValid
		{
			get { return base.IsValid
						 && _lines.IsValid ; }
		}
		
        /// <summary>
        /// Indica si el objeto está "sucio" (se ha modificado) y se debe actualizar en la base de datos
        /// </summary>
		/// <remarks>Para añadir una lista: || _lista.IsDirty<remarks/>
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lines.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods

		public static WorkReport CloneAsNew(WorkReportInfo source)
		{
			WorkReport clon = WorkReport.New();
			clon.CopyFrom(source);
		
			clon.GetNewCode();			
		
			clon.MarkNew();

			if (source.Lines == null) source.LoadChilds(typeof(WorkReportResources), false);

			foreach (WorkReportResourceInfo item in source.Lines)
			{
				if (item.EEntityType != ETipoEntidad.OutputDelivery)
					clon.Lines.NewItem(clon, item);
			}

			clon.Lines.MarkAsNew();
			
			return clon;
		}

		protected virtual void CopyFrom(WorkReportInfo source)
		{
			if (source == null) return;

			OidOwner = source.OidOwner;
			OidExpedient = source.OidExpedient;
			Serial = source.Serial;
			Code = source.Code;
		}

        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(WorkReport));
            Code = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
        }

		public void CalculateTotal()
		{
			Total = 0;

			foreach (WorkReportResource item in Lines)
				Total += item.Total;
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
			//Expedient
			if (OidExpedient <= 0)
			{
				e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, Resources.Labels.EXPEDIENT);
				throw new iQValidationException(e.Description, string.Empty);
			}

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
			/*QueryConditions conditions = new QueryConditions
			{
				WorkReport = WorkReportInfo.New(oid),
			};

			WorkReportResourceList resources = WorkReportResourceList.GetList(conditions, false);

			if (resources.Count > 0)
				throw new iQException(Resources.Messages.ASSOCIATED_WORKREPORTRESOURCE_INTERVALS);*/
		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected WorkReport() 
		{
			Oid = (long)(new Random()).Next();
			EStatus = EEstado.Abierto;	
		}				
		private WorkReport(WorkReport source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private WorkReport(int sessionCode, IDataReader source, bool childs)
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
		public static WorkReport NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			WorkReport obj = DataPortal.Create<WorkReport>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">WorkReport con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(WorkReport source, bool childs)
		/// <remarks/>
		internal static WorkReport GetChild(long oid, bool childs)
		{
			WorkReport obj = Get(oid, childs);
			obj.MarkAsChild();

			return obj;
		}
		public static WorkReport GetChild(int sessionCode, long oid, bool childs)
		{
			WorkReport obj = Get(oid, childs, sessionCode);
			obj.MarkAsChild();
			return obj;
		}
		internal static WorkReport GetChild(WorkReport source) { return new WorkReport(source, false); }
		internal static WorkReport GetChild(WorkReport source, bool childs) { return new WorkReport(source, childs); }
        internal static WorkReport GetChild(int sessionCode, IDataReader source) { return new WorkReport(sessionCode, source, false); }
        internal static WorkReport GetChild(int sessionCode, IDataReader source, bool childs) { return new WorkReport(sessionCode, source, childs); }

		public virtual WorkReportInfo GetInfo (bool childs = true) { return new WorkReportInfo(this, childs); }
		
		#endregion
		
		#region Root Factory Methods
		
		public static WorkReport New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			WorkReport obj = DataPortal.Create<WorkReport>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}
		
		public new static WorkReport Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<WorkReport>.Get(query, childs, -1);
		}		
		public static WorkReport Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		public static WorkReport Get(long oid, bool childs, bool cache, int sessionCode)
		{
			WorkReport item;

			if (!cache) return Get(oid, childs);

			//Está en la cache de listas
			if (Cache.Instance.Contains(typeof(WorkReports)))
			{
				WorkReports items = Cache.Instance.Get(typeof(WorkReports)) as WorkReports;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = WorkReport.GetChild(items.SessionCode, oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(WorkReports), items);
				}
			}
			//Está en la cache de objetos
			else if (Cache.Instance.Contains(typeof(WorkReport)))
			{
				item = Cache.Instance.Get(typeof(WorkReport)) as WorkReport;
			}
			else
			{
				WorkReports items = WorkReports.NewList();

				item = sessionCode == -1 ? WorkReport.GetChild(oid, childs) : WorkReport.GetChild(sessionCode, oid, childs);
				items.AddItem(item);
				items.SessionCode = item.SessionCode;
				Cache.Instance.Save(typeof(WorkReports), items);
			}

			return item;
		}

		public static WorkReport GetByResource(long oidResource, ETipoEntidad entityType, bool childs = true) 
		{ 
			QueryConditions conditions = new QueryConditions();

			switch (entityType)
			{
				case ETipoEntidad.Empleado:
					conditions.Acreedor = ProviderBaseInfo.New(oidResource, ETipoAcreedor.Empleado);
					break;

				case ETipoEntidad.OutputDelivery:
					conditions.OutputDelivery = OutputDeliveryInfo.New(oidResource);
					break;

				case ETipoEntidad.Tool:
					conditions.Tool = ToolInfo.New(oidResource);
					break;
				
			}

			return Get(SELECT(conditions, true), childs); 
		}		
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			IsPosibleDelete(oid);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}

		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = WorkReport.OpenSession();
			ISession sess = WorkReport.Session(sessCode);
			ITransaction trans = WorkReport.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from WorkReport");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				WorkReport.CloseSession(sessCode);
			}
		}

		public override WorkReport Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

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
				CalculateTotal();

				base.Save();				

				_lines.Update(this);				
				if (!SharedTransaction) Transaction().Commit();
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
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession(); 
					else BeginTransaction();
				}
			}
		}
				
		#endregion				
		
		#region Common Data Access
	
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			GetNewCode();
			OidOwner = (AppContext.User != null) ? AppContext.User.Oid : 0;
			Owner = (AppContext.User != null) ? AppContext.User.Name : string.Empty;
			Date = DateTime.Today;
			From = DateTime.Now;
			Till = DateTime.Now;
			Hours = 8;
		}
		
		private void Fetch(WorkReport source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					WorkReportResource.DoLOCK(Session());
					string query = WorkReportResources.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lines = WorkReportResources.GetChildList(SessionCode, reader);
                }
			} 

			MarkOld();
		}
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					
					WorkReportResource.DoLOCK(Session());
					string query = WorkReportResources.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lines = WorkReportResources.GetChildList(SessionCode, reader);					
				}
			}   

            MarkOld();
        }

		internal void Insert(WorkReports parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		internal void Update(WorkReports parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			WorkReportRecord obj = Session().Get<WorkReportRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		internal void DeleteSelf(WorkReports parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<WorkReportRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					//WorkReport.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);					
					
					if (Childs)
					{
						string query = string.Empty;
					
						//WorkReportResource.DoLOCK(Session());
						query = WorkReportResources.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_lines = WorkReportResources.GetChildList(SessionCode, reader);								
					}
				}

				MarkOld();
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			if (!SharedTransaction)
			{
				SessionCode = OpenSession();
				BeginTransaction();
			}			
			
			GetNewCode();
			
			Session().Save(_base.Record);
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			WorkReportRecord obj = Session().Get<WorkReportRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			MarkOld();
			
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

				WorkReport obj = Get(criteria.Oid, true, SessionCode);

				obj.Lines.Clear();
				obj.Save();

				Session().Delete(Session().Get<WorkReportRecord>(obj.Oid));
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
				
        #region SQL

		internal enum EQueryType { GENERAL = 0, CLUSTERED = 1 }

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() 
			{
				{
					"Owner", 
					new ForeignField() {                        
						Property = "Owner", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "Name")
					}
				},
				{
					"Expedient", 
					new ForeignField() {                        
						Property = "Expedient", 
                        TableAlias = "EX", 
                        Column = nHManager.Instance.GetTableColumn(typeof(SupplierRecord), "Codigo")
					}
				}
			};
        }
		
        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string FIELDS(EQueryType queryType, QueryConditions conditions)
        {
            string query = string.Empty;

			switch (queryType)
			{
				case EQueryType.GENERAL:

					query = @"
						SELECT WR.*
								,US.""NAME"" AS ""OWNER""
								,EX.""CODIGO"" AS ""EXPEDIENT""
								,COALESCE(WC.""NAME"", '') AS ""CATEGORY_NAME""";

					break;

				case EQueryType.CLUSTERED:

					query = @"
                        SELECT " + (long)queryType + @" AS ""QUERY_TYPE"" 
                            ,DATE_TRUNC('" + conditions.Step.ToString() + @"', WR.""DATE"") AS ""STEP""
							,SUM(WR.""TOTAL"") AS ""TOTAL""";

					break;
			}

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string wr = nHManager.Instance.GetSQLTable(typeof(WorkReportRecord));
			string ws = nHManager.Instance.GetSQLTable(typeof(WorkReportResourceRecord));
			string wc = nHManager.Instance.GetSQLTable(typeof(WorkReportCategoryRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = @"
				FROM " + wr + @" AS WR 
				LEFT JOIN " + wc + @" AS WC ON WC.""OID"" = WR.""CATEGORY""
				LEFT JOIN " + us + @" AS US ON US.""OID"" = WR.""OID_OWNER""
				INNER JOIN " + ex + @" AS EX ON EX.""OID"" = WR.""OID_EXPEDIENT""";

			if (conditions.OutputDelivery != null)
			{
				query += @"
					INNER JOIN " + ws + @" AS WS ON WS.""OID_WORK_REPORT"" = WR.""OID"" 
						AND WS.""ENTITY_TYPE"" = " + (long)ETipoEntidad.OutputDelivery + @"
						AND WS.""OID_RESOURCE"" = " + conditions.OutputDelivery.Oid;
			}

			if (conditions.Acreedor != null)
			{
				query += @"
					INNER JOIN " + ws + @" AS WS ON WS.""OID_WORK_REPORT"" = WR.""OID"" 
						AND WS.""ENTITY_TYPE"" = " + (long)ETipoEntidad.Empleado + @"
						AND WS.""OID_RESOURCE"" = " + conditions.Acreedor.Oid;
			}

			if (conditions.Tool != null)
			{
				query += @"
					INNER JOIN " + ws + @" AS WS ON WS.""OID_WORK_REPORT"" = WR.""OID"" 
						AND WS.""ENTITY_TYPE"" = " + (long)ETipoEntidad.Tool + @"
						AND WS.""OID_RESOURCE"" = " + conditions.Tool.Oid;
			}

			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;
		
			string query;

            query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "WR", ForeignFields());
				
			query += @" 
				AND (WR.""FROM"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += Common.EntityBase.NO_NULL_RECORDS_CONDITION("WR");
			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "WR");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "WR");
			
            if (conditions.WorkReport != null)
				query += @"
					AND WR.""OID"" = " + conditions.WorkReport.Oid;

			if (conditions.WorkReportCategory != null)
				query += @"
					AND WR.""CATEGORY"" = " + conditions.WorkReportCategory.Oid;

			if (conditions.Expedient != null)
				query += @"
					AND WR.""OID_EXPEDIENT"" = " + conditions.Expedient.Oid;	
			
			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query; 

			if (conditions.Step != EStepGraph.None)
			{
				query =
					FIELDS(EQueryType.CLUSTERED, conditions) +
					JOIN(conditions);
			}
			else
			{
				query =
					FIELDS(EQueryType.GENERAL, conditions) + 
					JOIN(conditions) +
					WHERE(conditions);
			}

            if (conditions != null) 
			{
				if (conditions.Step != EStepGraph.None)
				{
					query += @"
					GROUP BY ""STEP""
					ORDER BY ""STEP""";
				}
				else
				{
					query += ORDER(conditions.Orders, "WR", ForeignFields());
					query += LIMIT(conditions.PagingInfo);
				}
			}				

			query += Common.Common.EntityBase.LOCK("WR", lockTable);

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
			return SELECT(new QueryConditions { WorkReport = WorkReportInfo.New(oid) }, lockTable);
        }

        internal static string SELECT_BY_EXPEDIENTS(QueryConditions conditions, bool lockTable)
        {            
            conditions.ExtraWhere += @"
                AND WR.""OID_EXPEDIENT"" IN " + Common.EntityBase.GET_IN_STRING(conditions.OidList);
            
            conditions.OidList = null;

            conditions.Orders = new OrderList();
            conditions.Orders.Add(FilterMng.BuildOrderItem("Expedient", ListSortDirection.Ascending, typeof(Expedient)));
            conditions.Orders.Add(FilterMng.BuildOrderItem("Date", ListSortDirection.Descending, typeof(WorkReport)));

            string query =
            SELECT(conditions, lockTable);

            return query;
        }

		#endregion
	}
}