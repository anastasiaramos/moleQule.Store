using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class WorkReportCategoryInfo : ReadOnlyBaseEx<WorkReportCategoryInfo, WorkReportCategory>
	{	
		#region Attributes

		protected WorkReportCategoryBase _base = new WorkReportCategoryBase();

		
		#endregion
		
		#region Properties
		
		public WorkReportCategoryBase Base { get { return _base; } }		
		
		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; }}
		public string Name { get { return _base.Record.Name; } }
		public string Comments { get { return _base.Record.Comments; } }			
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(WorkReportCategory source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected WorkReportCategoryInfo() { /* require use of factory methods */ }
		private WorkReportCategoryInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal WorkReportCategoryInfo(WorkReportCategory item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static WorkReportCategoryInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new WorkReportCategoryInfo(sessionCode, reader, childs);
		}
		
		public static WorkReportCategoryInfo New(long oid = 0) { return new WorkReportCategoryInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static WorkReportCategoryInfo Get(long oid, bool childs = false) 
		{ 
            if (!WorkReportCategory.CanGetObject()) throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			return Get(WorkReportCategory.SELECT(oid, false), childs); 
		}
		
		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
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
					
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion			
	}
}
