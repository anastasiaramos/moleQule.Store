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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class ExpedienteREAInfo : ReadOnlyBaseEx<ExpedienteREAInfo>, IEntidadRegistroInfo
	{
		#region IEntidadRegistroInfo

		public moleQule.Common.Structs.ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.ExpedienteREA; } }
		public string DescripcionRegistro { get { return "EXPEDIENTE REA Nº " + Codigo + ". Exp: " + IDExpediente; } }

		#endregion

		#region Attributes

		public REAExpedientBase _base = new REAExpedientBase();

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string CodigoAduanero { get { return _base.Record.CodigoAduanero; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string NExpedienteREA { get { return _base.Record.ExpedienteRea; } }
		public string CertificadoREA { get { return _base.Record.CertificadoRea; } }
		public string NDUA { get { return _base.Record.NDua; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		
		//NO ENLAZADAS
		public EEstado EEstado { get { return _base.EEstado; } }
		public string EstadoLabel { get { return _base.EstadoLabel; } }
		public string CuentaCobro { get { return _base._cuenta_cobro; } }
		public DateTime FechaCobro { get { return _base._fecha_cobro; } }
		public decimal AyudaEstimada { get { return _base._ayuda_estimada; } set { _base._ayuda_estimada = value; } }
		public decimal AyudaCobrada { get { return _base._ayuda_cobrada; } set { _base._ayuda_cobrada = value; } }
		public decimal AyudaPendiente { get { return _base.AyudaPendiente; } set { _base._ayuda_pendiente = value; } }
		public decimal AyudaDesestimada { get { return _base.AyudaDesestimada; } }
		public string CodigoExpediente { get { return _base._id_expediente; } } /* DEPRECATED */
		public string IDExpediente { get { return _base._id_expediente; } }
		public long TipoExpediente { get { return _base._tipo_expediente; } }
		public ETipoExpediente ETipoExpediente { get { return _base.ETipoExpediente; } }
		public string TipoExpedienteLabel { get { return _base.TipoExpedienteLabel; } }
        public bool Cobrado { get { return (EEstado == EEstado.Charged || EEstado == EEstado.Exportado); } }

		#endregion
		
		#region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			long query = Format.DataReader.GetInt64(source, "QUERY");

			_base.CopyValues(source);

			if (query == 1)
			{
				Oid = Format.DataReader.GetInt64(source, "OID");
				_base.Record.OidExpediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE_EX");

                string oid = Oid.ToString("00000") + _base.Record.OidExpediente.ToString("00000");
                Oid = Convert.ToInt64(oid);
			}
		}
						
		public void CopyFrom(REAExpedient source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected ExpedienteREAInfo() { /* require use of factory methods */ }
		private ExpedienteREAInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		internal ExpedienteREAInfo(REAExpedient item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
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
		public static ExpedienteREAInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static ExpedienteREAInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new ExpedienteREAInfo(sessionCode, reader, childs);
		}
		
 		#endregion

		#region Common Data Access

		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				CopyValues(source);

				if (Childs)
				{
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return REAExpedient.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return REAExpedient.SELECT(conditions, false); }
		
		public static string SELECT(ExpedientInfo item) { return SELECT(new Library.Store.QueryConditions { Expedient = item }); }			
		
        #endregion		
	}
}
