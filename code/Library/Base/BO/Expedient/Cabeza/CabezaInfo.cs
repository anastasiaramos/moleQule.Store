using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class CabezaInfo : ReadOnlyBaseEx<CabezaInfo>
    {
        #region Attributes

        public CabezaBase _base = new CabezaBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidExpediente
        {
            get
            {
                return _base.Record.OidExpediente;
            }
        }
        public long OidPartida { get { return _base.Record.OidProductoExpediente; } }
        public string Sexo
        {
            get
            {
                return _base.Record.Sexo;
            }
        }
        public string Identificador
        {
            get
            {
                return _base.Record.Identificador;
            }
        }
        public string Raza
        {
            get
            {
                return _base.Record.Raza;
            }
        }
        public string Tipo
        {
            get
            {
                return _base.Record.Tipo;
            }
        }
        public string Observaciones
        {
            get
            {
                return _base.Record.Observaciones;
            }
        }

        //Propiedades no enlazadas
        public decimal PrecioCompra { get { return _base.PrecioCompra; } }
        public decimal PrecioVenta { get { return _base.PrecioVenta; } }
        public decimal Ayuda { get { return _base.Ayuda; } set { _base.Ayuda = value; } }
        public decimal Coste { get { return _base.Coste; } set { _base.Coste = value; } }
        public decimal Stock { get { return _base.Stock; } set { _base.Stock = value; } }
		public string NAlbaran { get { return _base.NAlbaran; } }

        #endregion

        #region Business Methods
        
        public void CopyFrom(Cabeza source) { _base.CopyValues(source); }

        #endregion

        #region Common Factory Methods

        protected CabezaInfo() { /* require use of factory methods */ }

        private CabezaInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        internal CabezaInfo(Cabeza source, bool copy_childs)
        {
            _base.CopyValues(source);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static CabezaInfo GetChild(IDataReader reader)
        {
            return GetChild(reader, false);
        }

        public static CabezaInfo GetChild(IDataReader reader, bool retrieve_childs)
        {
            return new CabezaInfo(reader, retrieve_childs);
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Devuelve un ExpedienteInfo tras consultar la base de datos
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static CabezaInfo Get(long oid, bool childs)
        {
            CriteriaEx criteria = Cabeza.GetCriteria(Cabeza.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Cabeza.SELECT(oid, false);

            criteria.Childs = childs;
            CabezaInfo obj = DataPortal.Fetch<CabezaInfo>(criteria);
            Cabeza.CloseSession(criteria.SessionCode);
            return obj;
        }

        #endregion

        #region Root Data Access

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

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion
    }
}

