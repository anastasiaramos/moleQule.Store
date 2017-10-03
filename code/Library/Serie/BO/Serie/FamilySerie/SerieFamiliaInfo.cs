using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule;

namespace moleQule.Serie
{
    /// <summary>
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class SerieFamiliaInfo : ReadOnlyBaseEx<SerieFamiliaInfo>
    {
		#region Attributes

		protected SerieFamiliaBase _base = new SerieFamiliaBase();

		#endregion

		#region Properties

		public SerieFamiliaBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidFamilia { get { return _base.Record.OidFamilia; } }

		public virtual string Familia { get { return _base.Familia; } set { _base.Familia = value; } }
		
		#endregion

		#region Business Methods

		public void CopyFrom(SerieFamilia source) { _base.CopyValues(source); }

		#endregion		

        #region Factory Methods

        protected SerieFamiliaInfo() { /* require use of factory methods */ }

        private SerieFamiliaInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        internal SerieFamiliaInfo(SerieFamilia source)
        {
			_base.CopyValues(source);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static SerieFamiliaInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
            return new SerieFamiliaInfo(sessionCode, reader, childs);
        }

		public static SerieFamiliaInfo New(long oid = 0) { return new SerieFamiliaInfo() { Oid = oid }; }

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

