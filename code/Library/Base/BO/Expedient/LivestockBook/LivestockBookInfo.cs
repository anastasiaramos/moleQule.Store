using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class LivestockBookInfo : ReadOnlyBaseEx<LivestockBookInfo>
	{	
		#region Attributes

        public LivestockBookBase _base = new LivestockBookBase();

		protected LivestockBookLineList _lineas = null;
		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public Decimal Balance { get { return _base.Record.Balance; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		
		public LivestockBookLineList Lineas { get { return _lineas; } }

		public EEstado EEstado { get { return (EEstado)_base.Record.Estado; } }
		public string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }

		#endregion
		
		#region Business Methods
						
		public void CopyFrom(LivestockBook source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected LivestockBookInfo() { /* require use of factory methods */ }
		private LivestockBookInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal LivestockBookInfo(LivestockBook item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_lineas = (item.Lineas != null) ? LivestockBookLineList.GetChildList(item.Lineas) : null;
				
			}
		}
		
		public static LivestockBookInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new LivestockBookInfo(sessionCode, reader, childs);
		}

        public static LivestockBookInfo New(long oid = 0) { return new LivestockBookInfo() { Oid = oid }; }


 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static LivestockBookInfo Get(long oid)
        {
            return Get(oid, false);
        }
		
        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static LivestockBookInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = LivestockBook.GetCriteria(LivestockBook.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LivestockBookInfo.SELECT(oid);
	
			LivestockBookInfo obj = DataPortal.Fetch<LivestockBookInfo>(criteria);
			LivestockBook.CloseSession(criteria.SessionCode);
			return obj;
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
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = LivestockBookLineList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_lineas = LivestockBookLineList.GetChildList(SessionCode, reader);

					_lineas.UpdateBalance();
				}
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
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;
	                    
						query = LivestockBookLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = LivestockBookLineList.GetChildList(SessionCode, reader);

						_lineas.UpdateBalance();
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return LivestockBook.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return LivestockBook.SELECT(conditions, false); }
		
        #endregion		
	}
}
