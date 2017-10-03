using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class PedidoProveedorInfo : ReadOnlyBaseEx<PedidoProveedorInfo>
	{	
		#region Attributes

		public InputOrderBase _base = new InputOrderBase();

		protected LineaPedidoProveeedorList _lineas = null;

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidAcreedor { get { return _base.Record.OidAcreedor; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Serial { get { return _base.Record.Serial; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Descuento { get { return _base.Record.Descuento; } }
		public Decimal Impuestos { get { return _base.Record.Impuestos; } }
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
		public Decimal Total { get { return _base.Record.Total; } }

		public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

		public LineaPedidoProveeedorList Lineas { get { return _lineas; } }

        //NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EEstado; } set { _base.EEstado = value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string NSerie { get { return _base._n_serie; } set { _base._n_serie = value; } }
		public virtual string Serie { get { return _base._serie; } set { _base._serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string IDAcreedor { get { return _base._id_acreedor; } set { _base._id_acreedor = value; } }
		public virtual string Acreedor { get { return _base._acreedor; } set { _base._acreedor = value; } }
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual string IDAlmacen { get { return _base._id_almacen; } set { _base._id_almacen = value; } }
		public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(PedidoProveedor source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected PedidoProveedorInfo() { /* require use of factory methods */ }
		private PedidoProveedorInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal PedidoProveedorInfo(PedidoProveedor item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_lineas = (item.Lineas != null) ? LineaPedidoProveeedorList.GetChildList(item.Lineas) : null;				
			}
		}
	
		public static PedidoProveedorInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static PedidoProveedorInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new PedidoProveedorInfo(sessionCode, reader, childs);
		}

		public virtual void LoadPendiente() { LoadPendiente(true); }
		public virtual void LoadPendiente(bool childs)
		{
			_lineas = LineaPedidoProveeedorList.GetPendientesChildList(this, childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
        public static PedidoProveedorInfo Get(long oid, ETipoAcreedor tipo) { return Get(oid, tipo, true); }
		public static PedidoProveedorInfo Get(long oid, ETipoAcreedor tipo, bool childs)
		{
			CriteriaEx criteria = PedidoProveedor.GetCriteria(PedidoProveedor.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = PedidoProveedorInfo.SELECT(oid, tipo);
	
			PedidoProveedorInfo obj = DataPortal.Fetch<PedidoProveedorInfo>(criteria);
			PedidoProveedor.CloseSession(criteria.SessionCode);
			return obj;
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
	                    
						query = LineaPedidoProveeedorList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = LineaPedidoProveeedorList.GetChildList(reader);						
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
					
					query = LineaPedidoProveeedorList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _lineas = LineaPedidoProveeedorList.GetChildList(reader);					
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

		#region SQL

		public static string SELECT(long oid, ETipoAcreedor tipo) { return PedidoProveedor.SELECT(oid, tipo, false); }

		#endregion
	}
}
