using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;

namespace moleQule.Library.WorkReport
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class WorkReportInfo : ReadOnlyBaseEx<WorkReportInfo, WorkReport>
	{	
		#region Attributes

		protected WorkReportBase _base = new WorkReportBase();

		protected WorkReportResourceList _resources = null;
		
		#endregion
		
		#region Properties
		
		public WorkReportBase Base { get { return _base; } }
				
		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; }}
		public long OidOwner { get { return _base.Record.OidOwner; } }
		public long OidExpedient { get { return _base.Record.OidExpedient; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Code { get { return _base.Record.Code; } }
		public long Status { get { return _base.Record.Status; } }
		public DateTime Date { get { return _base.Record.Date; } }
		public DateTime From { get { return _base.Record.From; } }
		public DateTime Till { get { return _base.Record.Till; } }
		public Decimal Hours { get { return _base.Record.Hours; } }
		public decimal Total { get { return _base.Record.Total; } }
		public long Category { get { return _base.Record.Category; } }
		public string Comments { get { return _base.Record.Comments; } }
		
		public WorkReportResourceList Lines { get { return _resources; } }
		
		//LINKED
		public EEstado EStatus { get { return _base.EStatus; } }
		public string StatusLabel { get { return _base.StatusLabel; } }
		public string Owner { get { return _base.Owner; } set { _base.Owner = value; } }
		public string Expedient { get { return _base.Expedient; } set { _base.Expedient = value; } }
		public string CategoryName { get { return _base.CategoryName; } set { _base.CategoryName = value; } }

		#endregion
		
		#region Business Methods
						
		public void CopyFrom(WorkReport source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected WorkReportInfo() { /* require use of factory methods */ }
		private WorkReportInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal WorkReportInfo(WorkReport item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_resources = (item.Lines != null) ? WorkReportResourceList.GetChildList(item.Lines) : null;				
			}
		}
		
		public static WorkReportInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new WorkReportInfo(sessionCode, reader, childs);
		}

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(WorkReportResources)))
			{
				_resources = WorkReportResourceList.GetChildList(this, childs);
			}
		}

		public static WorkReportInfo New(long oid = 0) { return new WorkReportInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static WorkReportInfo Get(long oid, bool childs = false) 
		{ 
            if (!WorkReport.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			return Get(WorkReport.SELECT(oid, false), childs); 
		}

		public static WorkReportInfo GetByResource(long oidResource, ETipoEntidad entityType, bool childs = true)
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

			return Get(WorkReport.SELECT(conditions, false), childs);
		}

		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = WorkReportResourceList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _resources = WorkReportResourceList.GetChildList(SessionCode, reader);
					
				}
			}
            catch (Exception ex) { throw ex; }
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
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;
	                    
						query = WorkReportResourceList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_resources = WorkReportResourceList.GetChildList(SessionCode, reader);						
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion			
	}
}
