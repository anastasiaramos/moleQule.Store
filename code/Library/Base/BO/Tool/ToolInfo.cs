using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
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
	public class ToolInfo : ReadOnlyBaseEx<ToolInfo, Tool>, IWorkResource
	{
		#region IWorkResource

		public long EntityType { get { return (long)moleQule.Common.Structs.ETipoEntidad.Tool; } set { } }
		public moleQule.Common.Structs.ETipoEntidad EEntityType { get { return moleQule.Common.Structs.ETipoEntidad.Tool; } set { } }

		#endregion

		#region Attributes

		protected ToolBase _base = new ToolBase();

		
		#endregion
		
		#region Properties
		
		public ToolBase Base { get { return _base; } }		
		
		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; }}
		public long Serial { get { return _base.Record.Serial; } }
		public string ID { get { return _base.Record.ID; } }
		public long Status { get { return _base.Record.Status; } }
		public string Name { get { return _base.Record.Name; } }
		public string Description { get { return _base.Record.Description; } }
		public DateTime From { get { return _base.Record.From; } }
		public DateTime Till { get { return _base.Record.Till; } }
		public Decimal Cost { get { return _base.Record.Cost; } }
		public string Location { get { return _base.Record.Location; } }
		public string Comments { get { return _base.Record.Comments; } }		
		
		//LINKED
		public virtual EEstado EStatus { get { return _base.EStatus; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }		
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(Tool source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected ToolInfo() { /* require use of factory methods */ }
		private ToolInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal ToolInfo(Tool item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static ToolInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new ToolInfo(sessionCode, reader, childs);
		}
		
		public static ToolInfo New(long oid = 0) { return new ToolInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static ToolInfo Get(long oid, bool childs = false) 
		{ 
            if (!Tool.CanGetObject()) throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			return Get(Tool.SELECT(oid, false), childs); 
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