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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class LivestockBookLineInfo : ReadOnlyBaseEx<LivestockBookLineInfo>
	{
		#region Attributes

		public LivestockBookLineBase _base = new LivestockBookLineBase();

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidLibro { get { return _base.Record.OidBook; } }
		public long OidPartida { get { return _base.Record.OidBatch; } }
		public long OidConceptoAlbaran { get { return _base.Record.OidDeliveryLine; } }
        public long OidPair { get { return _base.Record.OidPair; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Tipo { get { return _base.Record.Tipo; } }
		public string Crotal { get { return _base.Record.Crotal; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public long Sexo { get { return _base.Record.Sexo; } }
		public long Edad { get { return _base.Record.Edad; } }
		public string Raza { get { return _base.Record.Raza; } }
		public string Causa { get { return _base.Record.Causa; } }
		public string Procedencia { get { return _base.Record.Procedencia; } }
		public Decimal Balance { get { return _base.Record.Balance; } set { _base.Record.Balance = value; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
        public bool Explotacion { get { return _base.Record.Explotacion; } }

		public EEstado EEstado { get { return _base.EEstado; } }
		public string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoLineaLibroGanadero ETipo { get { return _base.ETipo; } }
		public virtual string TipoLabel { get { return _base.TipoLabel; } }
		public ESexo ESexo { get { return _base.ESexo; } }
		public string SexoLabel { get { return _base.SexoLabel; } }
		public virtual string IDPartida { get { return _base._id_partida; } }
		public long OidExpediente { get { return _base._oid_expediente; } }
		public virtual string Expediente { get { return _base._expediente; } }
        public virtual string IdFactura { get { return _base.IdFactura; } }
        public virtual string ClienteProveedor { get { return _base.ClienteProveedor; } }
        public virtual string CleanCrotal { get { return _base.CleanCrotal; } }
        public virtual string VisibleCrotal { get { return _base.VisibleCrotal; } }
        public virtual string Concepto { get { return _base.Concepto; } }
        public virtual string PairID { get { return _base.PairID; } set { _base.PairID = value; } }

		#endregion
		
		#region Business Methods
        				
		public void CopyFrom(LivestockBookLine source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected LivestockBookLineInfo() { /* require use of factory methods */ }
		private LivestockBookLineInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal LivestockBookLineInfo(LivestockBookLine item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
		
		public static LivestockBookLineInfo GetChild(int session_code, IDataReader reader) { return GetChild(session_code, reader, false); }
		public static LivestockBookLineInfo GetChild(int session_code, IDataReader reader, bool childs)
        {
			return new LivestockBookLineInfo(session_code, reader, childs);
		}
		
 		#endregion			
		
		#region Root Factory Methods

		/// <summary>
		/// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
		/// </summary>
		/// <param name="oid">Oid del objeto</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static LivestockBookLineInfo Get(long oid) { return Get(oid, false); }
		public static LivestockBookLineInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = LivestockBookLine.GetCriteria(LivestockBookLine.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid);

			LivestockBookLineInfo obj = DataPortal.Fetch<LivestockBookLineInfo>(criteria);
			LivestockBookLine.CloseSession(criteria.SessionCode);
			return obj;
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
				}
			}
			catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return LivestockBookLine.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return LivestockBookLine.SELECT(conditions, false); }		
		public static string SELECT(LivestockBookInfo item) { return SELECT(new Library.Store.QueryConditions { LibroGanadero = item }); }			
		
        #endregion		
	}
}
