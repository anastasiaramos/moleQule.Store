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
using moleQule.CslaEx; 
using moleQule.Common;
using moleQule.Hipatia;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class LineaFomentoInfo : ReadOnlyBaseEx<LineaFomentoInfo>, IEntidadRegistroInfo, IAgenteHipatia
    {
        #region IAgenteHipatia

        public string NombreHipatia { get { return Codigo + "/" + IDExpediente; } }
        public string IDHipatia { get { return Codigo; } }
		public Type TipoEntidad { get { return typeof(LineaFomento); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region IEntidadRegistroInfo

		public moleQule.Common.Structs.ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.LineaFomento; } }
		public string DescripcionRegistro { get { return "LINEA DE FOMENTO Nº " + Codigo + ". Exp: " + IDExpediente + ". Producto: " + Producto; } }

		#endregion

		#region Attributes

        public LineaFomentoBase _base = new LineaFomentoBase();

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidNaviera { get { return _base.Record.OidNaviera; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public string IDEnvio { get { return _base.Record.IdEnvio; } }
		public string Conocimiento { get { return _base.Record.Conocimiento; } }
		public DateTime FechaConocimiento { get { return _base.Record.FechaConocimiento; } }
		public bool Teus20 { get { return _base.Record.Teus20; } }
		public bool Teus40 { get { return _base.Record.Teus40; } }
		public decimal Kilos { get { return _base.Record.Kilos; } }
		public Decimal FleteNeto { get { return _base.Record.FleteNeto; } }
		public Decimal BAF { get { return _base.Record.Baf; } }
		public Decimal T3Origen { get { return _base.Record.T3Origen; } }
		public Decimal T3Destino { get { return _base.Record.T3Destino; } }
		public Decimal THCOrigen { get { return _base.Record.ThcOrigen; } }
		public Decimal THCDestino { get { return _base.Record.ThcDestino; } }
		public Decimal ISPS { get { return _base.Record.Isps; } }
		public Decimal Total { get { return _base.Record.Total; } }
        public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public DateTime FechaSolicitud { get { return _base.Record.Fecha; } }
		public string IDSolicitud { get { return _base.Record.IdSolicitud; } }
		public string DUA { get { return _base.Record.Dua; } }

		//NO ENLAZADOS
		public EEstado EEstado { get { return _base.EEstado; } }
		public string EstadoLabel { get { return _base.EstadoLabel; } }
		public Decimal Subvencion { get { return Decimal.Round(_base._subvencion, 2); } }
		public string IDPartida { get { return _base._id_partida; } }
		public string IDExpediente { get { return _base._id_expediente; } }
		public string IDFactura { get { return _base._id_factura; } }
		public string Contenedor { get { return _base._contenedor; } }
		public string Producto { get { return _base._producto; } }
		public string CodigoAduanero { get { return _base._codigo_aduanero; } }
		public Decimal T3 { get { return _base.T3; } }
		public Decimal THC { get { return _base.THC; } }
        public string Naviera { get { return _base._naviera; } }
        public DateTime FechaNaviera { get { return _base._fecha_naviera; } }
        public string Teus { get { if (Teus20) return Resources.Labels.TEUS20; else return Resources.Labels.TEUS40; } }
        public virtual long TipoExpediente { get { return _base._tipo_expediente; } }
        public virtual ETipoExpediente ETipoExpediente { get { return (ETipoExpediente)_base._tipo_expediente; } }
        public virtual string ETipoExpedienteLabel { get { return moleQule.Store.Structs.EnumText<ETipoExpediente>.GetLabel(ETipoExpediente); } }

        public virtual decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
        public virtual string Vinculado { get { return _base._activo; } set { _base._activo = value; } }
        public decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
        public virtual string FechaAsignacion
        {
            get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; }
            set { _base._fecha_asignacion = DateTime.Parse(value); }
        }
        public virtual decimal ImporteCobrado { get { return _base._importe_cobrado; } set { _base._importe_cobrado = value; } }
        public virtual decimal TotalAyuda { get { return Decimal.Round(_base._subvencion, 2); } }
        public virtual string FechaCobro
        {
            get { return (_base._fecha_cobro != DateTime.MinValue) ? _base._fecha_cobro.ToShortDateString() : "---"; }
        }
        public virtual string IDCobro { get { return _base._id_cobro; } }
        public virtual bool Cobrado { get { return _base._cobrado; } set { _base._cobrado = value; } }
        public virtual decimal Pendiente { get { return Decimal.Round(_base._subvencion, 2) - _base._asignado; } set { } }
        public virtual int Ano { get { return _base.Record.FechaConocimiento.Year; } }
		
		#endregion
		
		#region Business Methods
        				
		public void CopyFrom(LineaFomento source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected LineaFomentoInfo() { /* require use of factory methods */ }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> origen de los datos</param>
        /// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		private LineaFomentoInfo(IDataReader reader, bool retrieve_childs)
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
		internal LineaFomentoInfo(LineaFomento item, bool copy_childs)
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
		public static LineaFomentoInfo GetChild(IDataReader reader)
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
		public static LineaFomentoInfo GetChild(IDataReader reader, bool retrieve_childs)
        {
			return new LineaFomentoInfo(reader, retrieve_childs);
		}
		
 		#endregion

		#region Child Data Access

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

        #region SQL

        public static string SELECT(long oid) { return LineaFomento.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return LineaFomento.SELECT(conditions, false); }
		
		public static string SELECT(ExpedientInfo item) { return SELECT(new Library.Store.QueryConditions { Expedient = item }); }			
		
        #endregion		
	}
}
