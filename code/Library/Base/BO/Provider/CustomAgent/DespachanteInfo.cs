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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class DespachanteInfo : ReadOnlyBaseEx<DespachanteInfo>, IAcreedorInfo, IAgenteHipatia, ITitular
	{
		#region IUser

		public virtual long OidUser { get { return _base.ProviderBase.OidUser; } set { _base.ProviderBase.OidUser = value; } }
		public virtual string Username { get { return _base.ProviderBase.Username; } set { _base.ProviderBase.Username = value; } }
		public virtual EEstadoItem EUserStatus { get { return _base.ProviderBase.EUserStatus; } set { _base.ProviderBase.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.ProviderBase.UserStatusLabel; } }
		public virtual DateTime CreationDate { get { return _base.ProviderBase.CreationDate; } set { _base.ProviderBase.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _base.ProviderBase.LastLoginDate; } set { _base.ProviderBase.LastLoginDate = value; } }

		#endregion

        #region ITitular

        public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Proveedor; } }

        #endregion

		#region IAcreedorInfo

		public ProviderBase ProviderBase { get { return _base.ProviderBase; } }

        public string NombreTipoAcreedor { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		public string ETipoAcreedorLabel { get { return NombreTipoAcreedor; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
        public ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_base.Record.TipoAcreedor; } set { _base.Record.TipoAcreedor = (long)value; } }

		#endregion

        #region IAgenteHipatia

        public string NombreHipatia { get { return Nombre; } }
        public string IDHipatia { get { return Codigo; } }
		public Type TipoEntidad { get { return typeof(Despachante); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region Attributes

		protected CustomAgentBase _base = new CustomAgentBase();

        protected PuertoDespachanteList _puerto_despachantes = null;
		
		#endregion
		
		#region Properties

		public CustomAgentBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
        public string ID { get { return _base.Record.Identificador; } }
		public long TipoId { get { return _base.Record.TipoId; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string Alias { get { return _base.Record.Alias; } }
		public string CodPostal { get { return _base.Record.CodPostal; } }
		public string Localidad { get { return _base.Record.Localidad; } }
		public string Municipio { get { return _base.Record.Municipio; } }
		public string Provincia { get { return _base.Record.Provincia; } }
		public string Telefono { get { return _base.Record.Telefono; } }
		public string Pais { get { return _base.Record.Pais; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public string Swift { get { return _base.Record.Swift; } }
		public string Contacto { get { return _base.Record.Contacto; } }
		public string Email { get { return _base.Record.Email; } }
		public string Direccion { get { return _base.Record.Direccion; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long OidCuentaBAsociada { get { return _base.Record.OidCuentaBAsociada; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long OidTarjetaAsociada { get { return _base.Record.OidTarjetaAsociada; } }
		public Decimal PIRPF { get { return Decimal.Round(_base.Record.PIRPF, 2); } }

		public ProductoProveedorList Productos { get { return _base.ProviderBase._producto_proveedores_list; } }
		public PaymentList Pagos { get { return _base.ProviderBase._pagos_list; } }
		public PuertoDespachanteList PuertoDespachantes { get { return _puerto_despachantes; } }

        //NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.ProviderBase.EStatus; } set { _base.ProviderBase.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.ProviderBase.StatusLabel; } }
		public virtual long OidAcreedor { get { return _base.ProviderBase.OidAcreedor; } }
		public virtual string CuentaAsociada { get { return _base.ProviderBase.CuentaAsociada; } set { _base.ProviderBase.CuentaAsociada = value; } }
		public virtual EFormaPago EFormaPago { get { return _base.ProviderBase.EFormaPago; } set { _base.ProviderBase.EFormaPago = value; } }
		public virtual EMedioPago EMedioPago { get { return _base.ProviderBase.EMedioPago; } set { _base.ProviderBase.EMedioPago = value; } }
		public virtual string FormaPagoLabel { get { return _base.ProviderBase.FormaPagoLabel; } }
		public virtual string MedioPagoLabel { get { return _base.ProviderBase.MedioPagoLabel; } }
		public virtual string Impuesto { get { return _base.ProviderBase.Impuesto; } }
		public virtual decimal PImpuesto { get { return _base.ProviderBase.PImpuesto; } }
		public virtual string TarjetaAsociada { get { return _base.ProviderBase.TarjetaAsociada; } set { _base.ProviderBase.TarjetaAsociada = value; } }

		#endregion
		
		#region Business Methods
		
		protected void CopyValues(Despachante source)
		{
			if (source == null) return;

            _base.CopyValues(source);
            ETipoAcreedor = ETipoAcreedor.Despachante;
            Oid = source.Oid;
		}
		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _base.CopyValues(source);
            ETipoAcreedor = ETipoAcreedor.Despachante;
            Oid = Format.DataReader.GetInt64(source, "OID");
		}

        public void CopyFrom(Despachante source) { CopyValues(source); }

		public Decimal GetPrecio(ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo)
		{
			if (Productos == null) LoadChilds(typeof(ProductoProveedor), false);
			return _base.ProviderBase.GetPrecio(producto, partida, tipo);
		}
		public Decimal GetDescuento(ProductInfo producto, BatchInfo partida)
		{
			if (Productos == null) LoadChilds(typeof(ProductoProveedor), false);
			return _base.ProviderBase.GetDescuento(producto, partida);
		}

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected DespachanteInfo() { /* require use of factory methods */ }
		private DespachanteInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal DespachanteInfo(Despachante item, bool childs)
		{
			CopyValues(item);
			
			if (childs)
			{
				_base.ProviderBase._producto_proveedores_list = (item.Productos != null) ? ProductoProveedorList.GetChildList(item.Productos) : null;
				_puerto_despachantes = (item.PuertoDespachantes != null) ? PuertoDespachanteList.GetChildList(item.PuertoDespachantes) : null;
				_base.ProviderBase._pagos_list = (item.Pagos != null) ? PaymentList.GetChildList(item.Pagos) : null;				
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
		public static DespachanteInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static DespachanteInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new DespachanteInfo(sessionCode, reader, childs);
		}

		public void LoadChilds(Type type, bool get_childs)
		{
			if (type.Equals(typeof(ProductoProveedor)))
			{
				_base.ProviderBase._producto_proveedores_list = ProductoProveedorList.GetChildList(this, get_childs);
			}
		}

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static DespachanteInfo Get(long oid)
        {
            return Get(oid, false);
        }
		
        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static DespachanteInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = Despachante.GetCriteria(Despachante.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = DespachanteInfo.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
	
			DespachanteInfo obj = DataPortal.Fetch<DespachanteInfo>(criteria);
			Despachante.CloseSession(criteria.SessionCode);
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
						CopyValues(reader);

					if (Childs)
					{
						string query = string.Empty;

						query = ProductoProveedorList.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_base.ProviderBase._producto_proveedores_list = ProductoProveedorList.GetChildList(SessionCode, reader);

                        //query = PagoList.SELECT(this);
                        //reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        //_proveedor_base._pagos_list = PagoList.GetChildList(SessionCode, reader);

						query = PuertoDespachanteList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_puerto_despachantes = PuertoDespachanteList.GetChildList(reader);
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
				CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					query = ProductoProveedorList.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_base.ProviderBase._producto_proveedores_list = ProductoProveedorList.GetChildList(SessionCode, reader);

                    //query = PagoList.SELECT(this);
                    //reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    //_proveedor_base._pagos_list = PagoList.GetChildList(SessionCode, reader);	

					query = PuertoDespachanteList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _puerto_despachantes = PuertoDespachanteList.GetChildList(reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid) { return Despachante.SELECT(oid, false); }

        #endregion
    }
}
