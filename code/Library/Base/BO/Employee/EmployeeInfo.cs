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
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class EmployeeInfo : ReadOnlyBaseEx<EmployeeInfo>, IAcreedorInfo, IAgenteHipatia, IWorkResource
	{
		#region IUser

		public virtual long OidUser { get { return _base.ProviderBase.OidUser; } set { _base.ProviderBase.OidUser = value; } }
		public virtual string Username { get { return _base.ProviderBase.Username; } set { _base.ProviderBase.Username = value; } }
		public virtual EEstadoItem EUserStatus { get { return _base.ProviderBase.EUserStatus; } set { _base.ProviderBase.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.ProviderBase.UserStatusLabel; } }
		public virtual DateTime CreationDate { get { return _base.ProviderBase.CreationDate; } set { _base.ProviderBase.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _base.ProviderBase.LastLoginDate; } set { _base.ProviderBase.LastLoginDate = value; } }

		#endregion

		#region IAcreedorInfo

		public ProviderBase ProviderBase { get { return _base.ProviderBase; } }

        public string NombreTipoAcreedor { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		public string ETipoAcreedorLabel { get { return NombreTipoAcreedor; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
        public ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_base.Record.TipoAcreedor; } }

		#endregion

        #region IAgenteHipatia

        public string NombreHipatia { get { return Apellidos + ", " + Nombre; } }
        public string IDHipatia { get { return Codigo; } }
		public Type TipoEntidad { get { return typeof(Employee); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region IWorkResource

		public long EntityType { get { return (long)moleQule.Common.Structs.ETipoEntidad.Empleado; } }
		public moleQule.Common.Structs.ETipoEntidad EEntityType { get { return moleQule.Common.Structs.ETipoEntidad.Empleado; } }
		public string Name { get { return Apellidos + ", " + Nombre; } }
		public decimal Cost { get { return _base.CostByHour; } }
		
		#endregion

		#region Attributes

		protected EmployeeBase _base = new EmployeeBase();

        protected PaymentList _pagos = null;

		#endregion
		
		#region Properties

		public EmployeeBase Base { get { return _base; } }

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

		public string Apellidos { get { return _base.Record.Apellidos; } }
		public string Foto { get { return _base.Record.Foto; } }
		public long Perfil { get { return _base.Record.Perfil; } }
		public DateTime InicioContrato { get { return _base.Record.InicioContrato; } }
		public DateTime FinContrato { get { return _base.Record.FinContrato; } }
		public string NivelEstudios { get { return _base.Record.NivelEstudios; } }
		public Decimal SueldoBruto { get { return _base.Record.SueldoBruto; } }
		public Decimal SueldoNeto { get { return _base.Record.SueldoNeto; } }
		public Decimal BaseIRPF { get { return _base.Record.BaseIrpf; } }
		public Decimal Descuentos { get { return _base.Record.Descuentos; } }
		public Decimal Seguro { get { return _base.Record.Seguro; } }
		public long OidCrew { get { return _base.Record.OidCrew; } set { _base.Record.OidCrew = value; } }
        public long PayrollMethod { get { return _base.Record.PayrollMethod; } set { _base.Record.PayrollMethod = value; } }

		public ProductoProveedorList Productos { get { return _base.ProviderBase._producto_proveedores_list; } }
		public PaymentList Pagos { get { return _base.ProviderBase._pagos_list; } }

        //NO ENLAZADAS
		public virtual string NombreCompleto { get { return _base.Record.Apellidos + ", " + _base.Record.Nombre; } }
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
        public virtual EPayrollMethod EPayrollMethod { get { return _base.EPayrollMethod; } }
        public virtual string PayrollMethodLabel { get { return _base.PayrollMethodLabel; } }

		#endregion
		
		#region Business Methods

        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _base.CopyValues(source);
			Oid = Format.DataReader.GetInt64(source, "OID");

			//Pte. de quitar de aqui cuando se adapten todos los Acreedores
			_base.Record.Estado = Format.DataReader.GetInt64(source, "ESTADO");
        }
		protected void CopyValues(Employee source)
		{
			if (source == null) return;

			_base.CopyValues(source);
			Oid = source.Oid;

			//Pte. de quitar de aqui cuando se adapten todos los Acreedores
			_base.Record.Estado = source.Estado;
		}

        public void CopyFrom(Employee source) { CopyValues(source); }

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
		protected EmployeeInfo() { /* require use of factory methods */ }
		private EmployeeInfo(int sessionCode, IDataReader reader, bool retrieve_childs)
		{
			SessionCode = sessionCode;
			Childs = retrieve_childs;
			Fetch(reader);
		}
		internal EmployeeInfo(Employee item, bool childs)
		{
			CopyValues(item);

			if (childs)
			{
				_base.ProviderBase._producto_proveedores_list = (item.Productos != null) ? ProductoProveedorList.GetChildList(item.Productos) : null;
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
		public static EmployeeInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static EmployeeInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new EmployeeInfo(sessionCode, reader, childs);
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
		
        public static EmployeeInfo Get(long oid) { return Get(oid, false); }		
		public static EmployeeInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = Employee.GetCriteria(Employee.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = EmployeeInfo.SELECT(oid);
	
			EmployeeInfo obj = DataPortal.Fetch<EmployeeInfo>(criteria);
			Employee.CloseSession(criteria.SessionCode);
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

						query = PaymentList.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_base.ProviderBase._pagos_list = PaymentList.GetChildList(SessionCode, reader);
					}					
				}
				else
				{
					CopyValues((Employee)(criteria.UniqueResult()));
					
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

					query = PaymentList.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_base.ProviderBase._pagos_list = PaymentList.GetChildList(SessionCode, reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

		#region SQL

		public static string SELECT(long oid) { return Employee.SELECT(oid, false); }

		#endregion
	}
}
