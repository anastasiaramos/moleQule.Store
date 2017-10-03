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
using moleQule.Hipatia;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class StoreInfo : ReadOnlyBaseEx<StoreInfo>, IAgenteHipatia
	{
		#region IAgenteHipatia

		public string NombreHipatia { get { return Nombre; } }
		public string IDHipatia { get { return string.Empty; } }
		public Type TipoEntidad { get { return typeof(Almacen); } }
		public string ObservacionesHipatia { get { return Observaciones; } }

		#endregion

		#region Attributes

		public AlmacenBase _base = new AlmacenBase();

		protected InventarioAlmacenList _inventarios = null;
		protected BatchList _partidas = null;
		protected StockList _stocks = null;
		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string Ubicacion { get { return _base.Record.Ubicacion; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		
		public InventarioAlmacenList InventarioAlmacenes { get { return _inventarios; } }
		public BatchList Partidas { get { return _partidas; } }
		public StockList Stocks { get { return _stocks; } }

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EEstado; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }

		#endregion
		
		#region Business Methods
        
        public void CopyFrom(Almacen source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected StoreInfo() { /* require use of factory methods */ }
		private StoreInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal StoreInfo(Almacen item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_inventarios = (item.InventarioAlmacenes != null) ? InventarioAlmacenList.GetChildList(item.InventarioAlmacenes) : null;
				_partidas = (item.Partidas != null) ? BatchList.GetChildList(item.Partidas) : null;
				_stocks = (item.Stocks != null) ? StockList.GetChildList(item.Stocks) : null;				
			}
		}
	
		public static StoreInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static StoreInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new StoreInfo(sessionCode, reader, childs); }

		public virtual void LoadPartidasByProducto(long oidProducto, bool childs)
		{
			if (Partidas == null) _partidas = BatchList.NewList();

			if (Partidas.GetItemByProducto(oidProducto) == null)
			{
				BatchList partidas = BatchList.GetChildListByProducto(this, oidProducto, childs);

				foreach (BatchInfo item in partidas)
					Partidas.AddItem(item);
			}
		}

		public static StoreInfo New(long oid = 0) { return new StoreInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static StoreInfo Get(long oid) { return Get(oid, false); }
		public static StoreInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Almacen.GetCriteria(Almacen.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = StoreInfo.SELECT(oid);
	
			StoreInfo obj = DataPortal.Fetch<StoreInfo>(criteria);
			Almacen.CloseSession(criteria.SessionCode);
			return obj;
		}
		public static StoreInfo Get(long oid, bool childs, bool cache)
		{
			StoreInfo item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(StoreList)))
			{
				StoreList items = StoreList.NewList();

				item = StoreInfo.Get(oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(StoreList), items);
			}
			else
			{
				StoreList items = Cache.Instance.Get(typeof(StoreList)) as StoreList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = StoreInfo.Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(StoreList), items);
				}
			}

			return item;
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
					
                    if (Childs)
					{
						string query = string.Empty;
	                    
						query = InventarioAlmacenList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _inventarios = InventarioAlmacenList.GetChildList(reader);
						
						query = BatchList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _partidas = BatchList.GetChildList(SessionCode, reader, Childs);

						query = StockList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_stocks = StockList.GetChildList(reader);						
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion
		
		#region Child Data Access
		
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = InventarioAlmacenList.SELECT();
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _inventarios = InventarioAlmacenList.GetChildList(reader);

					query = BatchList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_partidas = BatchList.GetChildList(SessionCode, reader, Childs);

					query = StockList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_stocks = StockList.GetChildList(reader);						
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

		#region SQL

		public static string SELECT(long oid) { return Almacen.SELECT(oid, false); }

		#endregion
	}
}
