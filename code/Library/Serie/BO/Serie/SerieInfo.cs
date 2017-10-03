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

namespace moleQule.Serie
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class SerieInfo : ReadOnlyBaseEx<SerieInfo, Serie>, IQueryableEntity
	{	
        #region IQueryableEntity

        public long EntityType { get { return (long)ETipoEntidad.Serie; } set {} }
    
        #endregion

		#region Attributes

		protected SerieBase _base = new SerieBase();

        private SerieFamiliaList _serie_familias;

		#endregion
		
		#region Properties

		public SerieBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string Identificador { get { return _base.Record.Identificador; } }
		public long Tipo { get { return _base.Record.Tipo; } }
		public string Cabecera { get { return _base.Record.Cabecera; } }
		public bool Resumen { get { return _base.Record.Resumen; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }		

        public virtual SerieFamiliaList SerieFamilias { get { return _serie_familias; } set { _serie_familias = value; } }

		public virtual ETipoSerie ETipoSerie { get { return _base.ETipoSerie; } set { _base.Record.Tipo = (long)value; } }
		public virtual string TipoSerieLabel { get { return _base.TipoSerieLabel;  } }
		public virtual string Impuesto { get { return _base.Impuesto; } }
		public virtual decimal PImpuesto { get { return _base.PImpuesto; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(Serie source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected SerieInfo() { /* require use of factory methods */ }
		private SerieInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal SerieInfo(Serie item, bool copy_childs)
		{
			_base.CopyValues(item);

            if (copy_childs)
            {
                _serie_familias = (item.SerieFamilias != null) ? SerieFamiliaList.GetChildList(item.SerieFamilias) : null;
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
		public static SerieInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false);	}
		public static SerieInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new SerieInfo(sessionCode, reader, childs); }

		public static SerieInfo New(long oid = 0) { return new SerieInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		public static SerieInfo Get(long oid, bool childs = false)
		{
			if (!Serie.CanGetObject()) throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			return Get(SELECT(oid), childs);
		}

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(SerieFamilia)))
			{
				_serie_familias = SerieFamiliaList.GetChildList(this, childs);
			}
		}

		#endregion
		
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
			try
			{
				_base.Record.Oid = 0;
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

                        query = SerieFamiliaList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _serie_familias = SerieFamiliaList.GetChildList(SessionCode, reader, Childs);
                    }
				}
			}
			catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
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

                    query = SerieFamiliaList.SELECT(this);
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _serie_familias = SerieFamiliaList.GetChildList(SessionCode, reader, Childs);
                }
			}
			catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid) { return Serie.SELECT(oid, false); }

        #endregion		
	}
}