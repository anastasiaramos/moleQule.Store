using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using CslaEx;
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.moleQule.Libary.Store
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class RemesaNominaInfo : ReadOnlyBaseEx<RemesaNominaInfo, RemesaNomina>
	{	
		#region Attributes

		protected RemesaNominaBase _base = new RemesaNominaBase();

		
		#endregion
		
		#region Properties
		
		public RemesaNominaBase Base { get { return _base; } }
		
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Irpf { get { return _base.Record.Irpf; } }
		public Decimal SeguroEmpresa { get { return _base.Record.SeguroEmpresa; } }
		public Decimal SeguroPersonal { get { return _base.Record.SeguroPersonal; } }
		public DateTime PrevisionPago { get { return _base.Record.PrevisionPago; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public Decimal BaseIrpf { get { return _base.Record.BaseIrpf; } }
		public Decimal Descuentos { get { return _base.Record.Descuentos; } }
		
		
		//LINKED
		public virtual EEstado EStatus { get { return _base.EStatus; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(RemesaNomina source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected RemesaNominaInfo() { /* require use of factory methods */ }
		private RemesaNominaInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal RemesaNominaInfo(RemesaNomina item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static RemesaNominaInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new RemesaNominaInfo(sessionCode, reader, childs);
		}
		
		public static RemesaNominaInfo New(long oid = 0) { return new RemesaNominaInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static RemesaNominaInfo Get(long oid, bool childs = false) 
		{ 
            if (!RemesaNomina.CanGetObject()) throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			return Get(RemesaNomina.SELECT(oid, false), childs); 
		}
		
		#endregion
					
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
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
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
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
					
        #region SQL
		
        #endregion		
	}
}
