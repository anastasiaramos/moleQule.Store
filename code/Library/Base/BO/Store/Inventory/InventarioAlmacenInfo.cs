using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 

using moleQule;

using NHibernate;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class InventarioAlmacenInfo : ReadOnlyBaseEx<InventarioAlmacenInfo>
	{
        #region Attributes

        public InventarioAlmacenBase _base = new InventarioAlmacenBase();
		
		protected LineaInventarioList _lineainventarios = null;
        		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } /*set { _oid_almacen = value; }*/ }
		public string Nombre { get { return _base.Record.Nombre; } /*set { _nombre = value; }*/ }
		public DateTime Fecha { get { return _base.Record.Fecha; } /*set { _fecha = value; }*/ }
		public string Observaciones { get { return _base.Record.Observaciones; } /*set { _observaciones = value; }*/ }
		
		public LineaInventarioList LineaInventarios { get { return _lineainventarios; } }
		
        //unlinked properties
        public string Almacen { get { return _base.Almacen; } /*set { _almacen = value; }*/ }

		#endregion
		
		#region Business Methods
				
        public void CopyFrom(InventarioAlmacen source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected InventarioAlmacenInfo() { /* require use of factory methods */ }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> origen de los datos</param>
        /// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		private InventarioAlmacenInfo(IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			Fetch(reader);
		}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="BusinessBaseEx"/> origen</param>
        /// <param name="copy_childs">Flag para copiar los hijos</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		internal InventarioAlmacenInfo(InventarioAlmacen item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_lineainventarios = (item.LineaInventarios != null) ? LineaInventarioList.GetChildList(item.LineaInventarios) : null;
				
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static InventarioAlmacenInfo GetChild(IDataReader reader)
        {
			return GetChild(reader, false);
		}
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		/// <remarks>La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista<remarks/>
		public static InventarioAlmacenInfo GetChild(IDataReader reader, bool retrieve_childs)
        {
			return new InventarioAlmacenInfo(reader, retrieve_childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static InventarioAlmacenInfo Get(long oid)
        {
            return Get(oid, false);
        }
		
        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static InventarioAlmacenInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = InventarioAlmacen.GetCriteria(InventarioAlmacen.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = InventarioAlmacenInfo.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
	
			InventarioAlmacenInfo obj = DataPortal.Fetch<InventarioAlmacenInfo>(criteria);
			InventarioAlmacen.CloseSession(criteria.SessionCode);
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
	                    
						query = LineaInventarioList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _lineainventarios = LineaInventarioList.GetChildList(reader);						
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
					
					query = LineaInventarioList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _lineainventarios = LineaInventarioList.GetChildList(reader);					
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        /// <summary>
        /// Construye el SELECT para traer todos los cobros asociados a una factura
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT(long oid)
        {
            string tabla = nHManager.Instance.GetSQLTable(typeof(InventarioAlmacenRecord));
            string tinner1 = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string query = string.Empty;

            query = "SELECT c.*, cl.\"NOMBRE\" AS \"ALMACEN\"" +
            " FROM " + tabla + " AS c" +
            " INNER JOIN " + tinner1 + " AS cl ON c.\"OID_ALMACEN\" = cl.\"OID\"";

            if (oid > 0)
                query += " WHERE c.\"OID\" = " + oid;

            return query;
        }

        #endregion
		
	}
}
