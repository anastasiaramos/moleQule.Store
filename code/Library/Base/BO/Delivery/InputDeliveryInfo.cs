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
using moleQule.Hipatia;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class InputDeliveryInfo : ReadOnlyBaseEx<InputDeliveryInfo>, IAgenteHipatia
	{
        #region IAgenteHipatia

        public string IDHipatia { get { return Ano + "/" + Codigo; } }
        public string NombreHipatia { get { return NombreAcreedor; } }
		public Type TipoEntidad { get { return typeof(InputDelivery); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region Attributes

		public InputDeliveryBase _base = new InputDeliveryBase();
	
        protected InputDeliveryLineList _conceptos_albaranes = null;

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidAcreedor { get { return _base.Record.OidAcreedor; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
        public DateTime FechaRegistro { get { return _base.Record.FechaRegistro; } }
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
        public Decimal PIRPF { get { return _base.Record.PIrpf; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Impuestos { get { return _base.Record.Igic; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public bool Nota { get { return _base.Record.Nota; } }
		public long Ano { get { return _base.Record.Ano; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
        public Decimal Descuento { get { return _base.Record.Descuento; } }
        public long FormaPago { get { return _base.Record.FormaPago; } }
        public long DiasPago { get { return _base.Record.DiasPago; } }
        public long MedioPago { get { return _base.Record.MedioPago; } }
        public DateTime Prevision { get { return _base.Record.PrevisionPago; } }
        public bool Contado { get { return _base.Record.Contado; } }
        public bool Rectificativo { get { return _base.Record.Rectificativo; } }
        public virtual Decimal IRPF { get { return _base.Record.Irpf; } }
		
		public virtual InputDeliveryLineList ConceptoAlbaranes { get { return _conceptos_albaranes; } }

        //CAMPOS NO ENLAZADOS

		public virtual EEstado EEstado { get { return _base.EEstado; } set { _base.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string IDAlmacen { get { return _base._id_almacen; } set { _base._id_almacen = value; } }
		public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string NumeroAlbaran { get { return _base.NumeroAlbaran; } }
		public virtual string NumeroSerie { get { return _base._numero_serie; } set { _base._numero_serie = value; } }
		public virtual string NombreSerie { get { return _base._nombre_serie; } set { _base._nombre_serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string NumeroAcreedor { get { return _base._numero_acreedor; } set { _base._numero_acreedor = value; } }
		public virtual string NombreAcreedor { get { return _base._nombre_acreedor; } set { _base._nombre_acreedor = value; } }
		public virtual bool NAlbaranManual { get { return _base._n_albaran_manual; } set { _base._n_albaran_manual = value; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; }  }
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual bool Facturado { get { return _base._facturado; } set { _base._facturado = value; } }
		public virtual string NumeroFactura { get { return _base._numero_factura; } set { _base._numero_factura = value; } }
        public virtual long OidFactura { get { return _base._oid_factura; } }
        public virtual string Expediente { get { return _base.Expediente; } }

		#endregion
		
		#region Business Methods
        
        public void CopyFrom(InputDelivery source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected InputDeliveryInfo() { /* require use of factory methods */ }
		private InputDeliveryInfo(int sessionCode, IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal InputDeliveryInfo(InputDelivery item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_conceptos_albaranes = (item.Conceptos != null) ? InputDeliveryLineList.GetChildList(item.Conceptos) : null;
			}
		}
	
		public static InputDeliveryInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static InputDeliveryInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new InputDeliveryInfo(sessionCode, reader, childs); }

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(InputDeliveryLines))
				|| type.Equals(typeof(InputDeliveryLine))
				|| type.Equals(typeof(InputDeliveryLineInfo)))
			{
				_conceptos_albaranes = InputDeliveryLineList.GetChildList(this, childs);
			}
		}

		public static InputDeliveryInfo New(long oid = 0) { return new InputDeliveryInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods

        public static InputDeliveryInfo Get(long oid, ETipoAcreedor tipo) { return Get(oid, tipo ,false); }
		public static InputDeliveryInfo Get(long oid, ETipoAcreedor tipo, bool childs)
		{
			return Get(SELECT(oid, tipo), childs);
		}
		public static InputDeliveryInfo Get(QueryConditions conditions, bool childs)
		{
			return Get(SELECT(conditions), childs);
		}

		internal static InputDeliveryInfo Get(string query, bool childs)
		{
			CriteriaEx criteria = InputDelivery.GetCriteria(InputDelivery.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			InputDeliveryInfo obj = DataPortal.Fetch<InputDeliveryInfo>(criteria);
			InputDelivery.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static InputDeliveryInfo Exists(QueryConditions conditions, bool childs)
		{
			return Get(SELECT_EXISTS(conditions), childs);
		}

        public InputDeliveryPrint GetPrintObject()
        {
            return InputDeliveryPrint.New(this,   
                                    ProviderBaseInfo.Get(this.OidAcreedor, this.ETipoAcreedor), 
                                    SerieInfo.Get(OidSerie));
        }

		#endregion

		#region Common Data Access

		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);

				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					query = InputDeliveryLineList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_conceptos_albaranes = InputDeliveryLineList.GetChildList(SessionCode, reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
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
						
						query = InputDeliveryLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos_albaranes = InputDeliveryLineList.GetChildList(SessionCode, reader);
                    }
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid, ETipoAcreedor tipo) { return InputDelivery.SELECT(oid, tipo, false); }
		public static string SELECT(QueryConditions conditions) { return InputDelivery.SELECT(conditions); }
		public static string SELECT_EXISTS(QueryConditions conditions) { return InputDelivery.SELECT_EXISTS(conditions); }

        #endregion
    }
    
    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class InputDeliverySerialInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected InputDeliverySerialInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static InputDeliverySerialInfo Get(ETipoAlbaranes tipo, long oid_serie, int year, bool rectificativo)
        {
            CriteriaEx criteria = InputDelivery.GetCriteria(InputDelivery.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(tipo, oid_serie, year, rectificativo);

            InputDeliverySerialInfo obj = DataPortal.Fetch<InputDeliverySerialInfo>(criteria);
            InputDelivery.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(ETipoAlbaranes tipo, long oid_serie, int year, bool rectificativo)
        {
            return Get(tipo, oid_serie, year, rectificativo).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(ETipoAlbaranes tipo, long oid_serie, int year, bool rectificativo)
        {
            string id = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
            string query;

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + id + " AS A" +
                    " INNER JOIN " + se + " AS S ON A.\"OID_SERIE\" = S.\"OID\"" +
                    " WHERE A.\"OID_SERIE\" = " + oid_serie.ToString() + 
                    " AND A.\"ANO\" = " + year.ToString() +
                    " AND A.\"RECTIFICATIVO\" = " + rectificativo.ToString().ToUpper();

            switch (tipo)
            {
                case ETipoAlbaranes.Agrupados:
                    query += " AND A.\"CONTADO\" = TRUE";
                    break;

                case ETipoAlbaranes.Facturados:
                    query += " AND A.\"CONTADO\" = FALSE";
                    break;
            }

            return query;
        }

        #endregion

    }

}
