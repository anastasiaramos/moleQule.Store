using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.CslaEx; 
using moleQule;

namespace moleQule.Library.Store
{
	/// <summary>
	/// Tabla auxiliar con hijos
	/// </summary>
	[Serializable()]
	public class PuertoInfo : ReadOnlyBaseEx<PuertoInfo>
	{
		#region Attributes

		protected PuertoBase _base = new PuertoBase();

		private PuertoDespachanteList _puerto_despachantes = null;		

		#endregion

		#region Properties

		public PuertoBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Valor { get { return _base.Record.Valor; } }
		public Decimal Precio { get { return _base.Record.Precio; } }

		public virtual PuertoDespachanteList PuertoDespachantes { get { return _puerto_despachantes; } }	

		#endregion

		#region Business Methods        		
		
        public void CopyFrom(Puerto source) { _base.CopyValues(source); }

		#endregion

		#region Factory Methods

		protected PuertoInfo() { /* require use of factory methods */ }

		private PuertoInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

		internal PuertoInfo(Puerto source)
			: this(source, false) { }		
		internal PuertoInfo(Puerto item, bool childs)
		{
			_base.CopyValues(item);		
			
			if (childs)
			{
				_puerto_despachantes = (item.PuertoDespachantes != null) ? PuertoDespachanteList.GetChildList(item.PuertoDespachantes) : null;
			}
		}

		public static PuertoInfo Get(long oid) { return Get(oid, false); }
		public static PuertoInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Puerto.GetCriteria(Puerto.OpenSession());
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Puerto.SELECT(oid);
				
			criteria.Childs = childs;
			PuertoInfo obj = DataPortal.Fetch<PuertoInfo>(criteria);
			Puerto.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		public static PuertoInfo Get(int sessionCode, IDataReader reader, bool childs) { return new PuertoInfo(sessionCode, reader, childs); }

		public static PuertoInfo New(long oid = 0) { return new PuertoInfo() { Oid = oid }; }

		#endregion

		#region Data Access

		// called to retrieve data from db
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
						
                        query = PuertoDespachanteList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _puerto_despachantes = PuertoDespachanteList.GetChildList(reader);
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);

				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = PuertoDespachanteList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _puerto_despachantes = PuertoDespachanteList.GetChildList(reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

	}
}



