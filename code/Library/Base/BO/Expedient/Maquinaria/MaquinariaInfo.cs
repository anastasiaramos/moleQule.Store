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
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class MaquinariaInfo : ReadOnlyBaseEx<MaquinariaInfo>
    {
        #region Attributes

        public MaquinariaBase _base = new MaquinariaBase();

		#endregion

		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
        public long OidPartida { get { return _base.Record.OidBatch; } }
        public string Codigo { get { return _base.Record.Codigo; } }
        public string Identificador { get { return _base.Record.Identificador; } }
        public string Descripcion { get { return _base.Record.Descripcion; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }

        //Propiedades no enlazadas
        public decimal PrecioCompra { get { return _base.PrecioCompra; } }
        public decimal PrecioVenta { get { return _base.PrecioVenta; } }
        public decimal Ayuda { get { return _base.Ayuda; } }
        public decimal Coste { get { return _base.Coste; } }
        public decimal Stock { get { return _base.Stock; } }
		public string NAlbaran { get { return _base.NAlbaran; } }

		#endregion

		#region Bussines Methods

        public void CopyFrom(Maquinaria source) { _base.CopyValues(source); }

        #endregion

        #region Factory Methods

        protected MaquinariaInfo() { /* require use of factory methods */ }

        private MaquinariaInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        internal MaquinariaInfo(Maquinaria source)
        {
            _base.CopyValues(source);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static MaquinariaInfo Get(IDataReader reader, bool childs)
        {
            return new MaquinariaInfo(reader, childs);
        }

        #endregion

        #region Data Access

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

