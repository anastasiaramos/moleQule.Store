using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{ 
    [Serializable()]
    public class ProviderBaseInfo : ReadOnlyBaseEx<ProviderBaseInfo>, IAcreedorInfo
    {
		#region IUser

		public virtual long OidUser { get { return _base.OidUser; } set { _base.OidUser = value; } }
		public virtual string Username { get { return _base.Username; } set { _base.Username = value; } }
		public virtual EEstadoItem EUserStatus { get { return _base.EUserStatus; } set { _base.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.UserStatusLabel; } }
		public virtual DateTime CreationDate { get { return _base.CreationDate; } set { _base.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _base.LastLoginDate; } set { _base.LastLoginDate = value; } }

		#endregion

        #region Attributes

		private ProviderBase _base = new ProviderBase();

        #endregion

		#region Properties

		public ProviderBase ProviderBase { get { return _base as ProviderBase; } }
		public ProviderBase Base { get { return _base; } }

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
		public string Contacto { get { return _base.Record.Contacto; } }
		public string Email { get { return _base.Record.Email; } }
		public string Direccion { get { return _base.Record.Direccion; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long OidCuentaBAsociada { get { return _base.Record.OidCuentaBAsociada; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public string Swift { get { return _base.Record.Swift; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long OidTarjetaAsociada { get { return _base.Record.OidTarjetaAsociada; } }
		public Decimal PIRPF { get { return _base.Record.PIRPF; } }

		public ProductoProveedorList Productos { get { return _base._producto_proveedores_list; } }
		public PaymentList Pagos { get { return _base._pagos_list; } }			 

        //NO ENLAZADAS
		public EEstado EEstado { get { return (EEstado)Estado; } }
		public string EstadoLabel { get { return moleQule.Base.EnumText<EEstado>.GetLabel(EEstado); } }
		public long OidAcreedor { get { return _base.OidAcreedor; } set { _base.OidAcreedor = value; } }
		public ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_base.Record.TipoAcreedor; } set { _base.Record.TipoAcreedor = (long)value; } }
		public EFormaPago EFormaPago { get { return _base.EFormaPago; } set { _base.EFormaPago = value; } }
        public EMedioPago EMedioPago { get { return _base.EMedioPago; } set { _base.EMedioPago = value; } }
        public string NombreTipoAcreedor { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
		public string ETipoAcreedorLabel { get { return NombreTipoAcreedor; } }
        public string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public string CuentaAsociada { get { return _base.CuentaAsociada; } }
		public string Impuesto { get { return _base.Impuesto; } }
		public decimal PImpuesto { get { return _base.PImpuesto; } }
        public string TarjetaAsociada { get { return _base.TarjetaAsociada; } }

        #endregion

        #region Business Methods

        public void CopyFrom(IAcreedor source) { _base.CopyValues(source); }

        public void CopyFrom(ProviderBase source) { _base.CopyValues(source); }

		public Decimal GetPrecio(ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo)
		{
			if (Productos == null) LoadChilds(typeof(ProductoProveedor), false);
			return _base.GetPrecio(producto, partida, tipo);
		}
		public Decimal GetDescuento(ProductInfo producto, BatchInfo partida)
		{
			if (Productos == null) LoadChilds(typeof(ProductoProveedor), false);
			return _base.GetDescuento(producto, partida);
		}

        #endregion

	    #region Factory Methods

        public ProviderBaseInfo() { /* require use of factory methods */ }
        private ProviderBaseInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }
		internal ProviderBaseInfo(ProviderBase item) : this(item, false) { }
        internal ProviderBaseInfo(ProviderBase item, bool childs)
        {
            _base.CopyValues(item);

            if (childs)
            {
            }
        }

		public static ProviderBaseInfo New(long oid = 0, ETipoAcreedor providerType = ETipoAcreedor.Proveedor)
		{
			ProviderBaseInfo obj = new ProviderBaseInfo();
			obj.Oid = oid;
			obj.OidAcreedor = oid;
			obj.ETipoAcreedor = providerType;

			return obj;
		}

		public static ProviderBaseInfo Get(long oid, ETipoAcreedor providerType, bool childs = false)
        {
            CriteriaEx criteria = Proveedor.GetCriteria(Proveedor.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ProviderBaseInfo.SELECT(oid, providerType);
 
            criteria.Childs = childs;
            ProviderBaseInfo obj = DataPortal.Fetch<ProviderBaseInfo>(criteria);
            Proveedor.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ProviderBaseInfo Get(IDataReader reader, bool childs)
        {
            return new ProviderBaseInfo(reader, childs);
        }

		public void LoadChilds(Type type, bool get_childs)
		{
			if (type.Equals(typeof(ProductoProveedor)))
			{
				_base._producto_proveedores_list = ProductoProveedorList.GetChildList(this, get_childs);
			}
		}

        #endregion

        #region Data Access

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
						string query = string.Empty;

						query = ProductoProveedorList.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_base._producto_proveedores_list = ProductoProveedorList.GetChildList(SessionCode, reader);

						query = PaymentList.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_base._pagos_list = PaymentList.GetChildList(SessionCode, reader);	
                    }
                }    
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					query = ProductoProveedorList.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_base._producto_proveedores_list = ProductoProveedorList.GetChildList(SessionCode, reader);

					query = PaymentList.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_base._pagos_list = PaymentList.GetChildList(SessionCode, reader);
				}
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
            {
                { 
                    "Username", 
                    new ForeignField() { 
                        Property = "Username", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "Name")
                    } 
                },
                { 
                    "LastLoginDate", 
                    new ForeignField() { 
                        Property = "LastLoginDate", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "LastLoginDate")
                    }
                },
				{	
                    "CreationDate", 
                    new ForeignField() { 
                        Property = "CreationDate", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "CreationDate")
                    }
                }
            };
		}

		public delegate string SelectCaller(QueryConditions conditions);
		public delegate string SelectLocalCaller(QueryConditions conditions, ETipoAcreedor tipo);

		public static ProviderBaseInfo.SelectCaller provider_caller = new ProviderBaseInfo.SelectCaller(SELECT_PROVIDER);

		internal static string SELECT_BASE_FIELDS(ETipoAcreedor tipo)
		{
			string fields;

            fields = @"
				SELECT A.*";

			switch (tipo)
			{
				case ETipoAcreedor.Proveedor:
				case ETipoAcreedor.TransportistaOrigen:
				case ETipoAcreedor.TransportistaDestino:

					fields += @"
						, A.""TIPO"" AS ""TIPO_ACREEDOR""";
					break;

				default:

					fields += @"
						," + (long)tipo + @" AS ""TIPO_ACREEDOR""";
					break;
			}

			fields += @"
				,CB.""VALOR"" AS ""CUENTA_ASOCIADA""
				,TC.""NOMBRE"" AS ""TARJETA_ASOCIADA""
				,IP.""NOMBRE"" AS ""IMPUESTO""
				,IP.""PORCENTAJE"" AS ""P_IMPUESTO""";

            //IUser
            fields += @"
                ,COALESCE(US.""OID"", 0) AS ""OID_USER""
                ,COALESCE(US.""NAME"", '') AS ""USERNAME""
                ,COALESCE(US.""ESTADO"", 0) AS ""USER_STATUS""
                ,COALESCE(US.""CREATION_DATE"", NULL) AS ""CREATION_DATE""
                ,COALESCE(US.""LAST_LOGIN_DATE"", NULL) AS ""LAST_LOGIN_DATE""";

			return fields;
		}

		public static string SELECT_FIELDS(ETipoAcreedor tipo)
		{
			string query;

			query = @"
			SELECT A.""TIPO"" AS ""TIPO_ACREEDOR""
					,A.""OID""
					,A.""SERIAL"", A.""CODIGO""
					,A.""ID"", A.""TIPO_ID""
					,A.""NOMBRE"", A.""ALIAS""
					,A.""ESTADO""
					,A.""DIRECCION""
                    ,A.""TELEFONO""
                    ,A.""LOCALIDAD""
                    ,A.""MUNICIPIO""
					,A.""COD_POSTAL""
                    ,A.""PROVINCIA""
                    ,A.""PAIS""
                    ,A.""CONTACTO""
                    ,A.""EMAIL""
                    ,A.""OBSERVACIONES""
					,A.""FORMA_PAGO""
                    ,A.""DIAS_PAGO""
                    ,A.""MEDIO_PAGO""
					,A.""CUENTA_BANCARIA""
                    ,A.""OID_CUENTA_BANCARIA_ASOCIADA""
                    ,A.""SWIFT""
					,A.""CUENTA_CONTABLE""
					,A.""OID_IMPUESTO""
					,A.""OID_TARJETA_ASOCIADA""
					,A.""P_IRPF""
					,CB.""VALOR"" AS ""CUENTA_ASOCIADA""
                    ,TC.""NOMBRE"" AS ""TARJETA_ASOCIADA""
					,IP.""NOMBRE"" AS ""IMPUESTO""
					,IP.""PORCENTAJE"" AS ""P_IMPUESTO""";
			//IUser
			query += @"
                    ,COALESCE(US.""OID"",0) AS ""OID_USER""
                    ,COALESCE(US.""NAME"",'') AS ""USERNAME""
                    ,COALESCE(US.""ESTADO"",0) AS ""USER_STATUS""
                    ,COALESCE(US.""CREATION_DATE"",NULL) AS ""CREATION_DATE""
                    ,COALESCE(US.""LAST_LOGIN_DATE"",NULL) AS ""LAST_LOGIN_DATE""";

			/*switch (tipo)
			{
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Partner:
				case ETipoAcreedor.Proveedor:
					query += @"
						,COALESCE(US.""OID"", 0) AS ""OID_USER""
						,COALESCE(US.""NAME"", '') AS ""USERNAME""
						,COALESCE(US.""ESTADO"", 0) AS ""USER_STATUS""
						,COALESCE(US.""LAST_LOGIN_DATE"", NULL) AS ""LAST_LOGIN_DATE""";
					break;

				default:
					query += @"
						,0 AS ""OID_USER""
						,'' AS ""USERNAME""
						,0 AS ""USER_STATUS""
						,CAST (NULL AS TIMESTAMP) AS ""LAST_LOGIN_DATE""";
					break;
			}*/

			return query;
		}

		public static string TABLE(ETipoAcreedor tipo)
		{
			return ModuleController.Instance.ActiveAcreedores[tipo].Table;
		}

		public static string JOIN(QueryConditions conditions)
		{
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
			string tc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));

			string query = @"
			FROM " + TABLE(conditions.TipoAcreedor[0]) + @" AS A
			LEFT JOIN " + ip + @" AS IP ON IP.""OID"" = A.""OID_IMPUESTO""
			LEFT JOIN " + cb + @" AS CB ON A.""OID_CUENTA_BANCARIA_ASOCIADA"" = CB.""OID""
			LEFT JOIN " + tc + @" AS TC ON A.""OID_TARJETA_ASOCIADA"" = TC.""OID""";

			query += @"
			LEFT JOIN " + us + @" AS US ON US.""ENTITY_TYPE"" = " + (long)moleQule.Store.Structs.EnumConvert.ToETipoEntidad(conditions.TipoAcreedor[0]) + @" AND A.""OID"" = US.""OID_ENTITY""
			LEFT JOIN " + su + @" AS SU ON SU.""OID_USER"" = US.""OID"" AND SU.""OID_SCHEMA"" = " + AppContext.ActiveSchema.Oid;

			switch (conditions.TipoAcreedor[0])
			{
				case ETipoAcreedor.Despachante:
					string pd = nHManager.Instance.GetSQLTable(typeof(PuertoDespachanteRecord));
					if (conditions.Puerto != null)
						query += @"
						INNER JOIN " + pd + @" AS PD ON PD.""OID_DESPACHANTE"" = A.""OID"" AND PD.""OID_PUERTO"" = " + conditions.Puerto.Oid;
					break;
			}

			return query;
		}

		public static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query;

			query = @" 
			WHERE TRUE";

            query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "A", "ESTADO");
           
			if (conditions.Estado != EEstado.Todos) 
				query += @"
					AND A.""ESTADO"" = " + (long)conditions.Estado;

			if (conditions.Acreedor != null) 
				query += @"
					AND A.""OID"" = " + conditions.Acreedor.Oid;

			switch (conditions.TipoAcreedor[0])
			{
				case ETipoAcreedor.Partner:
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Proveedor:
					query += @"
						AND A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];
					break;

				case ETipoAcreedor.TransportistaDestino:
				case ETipoAcreedor.TransportistaOrigen:
					query += @"
						AND A.""TIPO"" = " + (long)conditions.TipoAcreedor[0];
					break;
			}

			/*if (AppContext.User.IsPartner)
			{
				switch (conditions.TipoAcreedor[0])
				{ 
					case ETipoAcreedor.Partner:
						query += Common.EntityBase.GET_IN_LIST_CONDITION(AppContext.Principal.Branches, "A");
						break;
				}
			}*/
			
			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions, ETipoAcreedor providerType)
		{
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
			string tc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));

			string query =
			SELECT_BASE_FIELDS(providerType) + @"
			FROM " + TABLE(providerType) + @" AS A
			LEFT JOIN " + ip + @" AS IP ON IP.""OID"" = A.""OID_IMPUESTO""
			LEFT JOIN " + cb + @" AS CB ON A.""OID_CUENTA_BANCARIA_ASOCIADA"" = CB.""OID""
            LEFT JOIN " + tc + @" AS TC ON A.""OID_TARJETA_ASOCIADA"" = TC.""OID""";

			//IUser
			query += @"
			LEFT JOIN " + us + @" AS US ON US.""ENTITY_TYPE"" = " + (long)moleQule.Store.Structs.EnumConvert.ToETipoEntidad(providerType) + @" 
                                            AND A.""OID"" = US.""OID_ENTITY""
			LEFT JOIN " + su + @" AS SU ON SU.""OID_USER"" = US.""OID"" AND SU.""OID_SCHEMA"" = " + AppContext.ActiveSchema.Oid;

			query += WHERE(conditions);

			return query;
		}

        public static string SELECT_BASE(long oid, ETipoAcreedor providerType, bool lockTable)
        {
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { TipoAcreedor = new ETipoAcreedor[1] { providerType } };
			if (oid != 0) conditions.Acreedor = ProviderBaseInfo.New(oid, providerType);

			query = SELECT_BASE(conditions, providerType);

            return query;
        }

		public static string SELECT(QueryConditions conditions, ETipoAcreedor providerType)
		{
			return SELECT(conditions, new ETipoAcreedor[1] { providerType });
		}
		public static string SELECT(QueryConditions conditions, ETipoAcreedor[] providerType)
		{
			conditions.TipoAcreedor = providerType;
			return SELECT(conditions);
		}

		public static string SELECT(QueryConditions conditions, ETipoAcreedor tipo, Dictionary<String, ForeignField> foreignFields)
		{
			conditions.TipoAcreedor[0] = tipo;

			string query = SELECT(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "A", foreignFields);
				query += LIMIT(conditions.PagingInfo);
			}

			return query;
		}

		public static string SELECT_PROVIDER(QueryConditions conditions)
		{
			string query =
				SELECT_FIELDS(conditions.TipoAcreedor[0]) +
				JOIN(conditions) +
				WHERE(conditions);

			return query;
		}

		public static string SELECT(QueryConditions conditions)
		{
			string query = string.Empty;

			if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos || conditions.TipoAcreedor.Length > 1)
			{
				query = ProviderBaseInfo.SELECT_BUILDER(provider_caller, conditions);
			}
			else
			{
				query = SELECT_PROVIDER(conditions);
			}

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, string.Empty, ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			return query;
		}

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions);
		}

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
            SELECT COUNT(*) AS ""TOTAL_ROWS""" +
			SELECT(conditions) +
			WHERE(conditions);

			return query;
		}

		public static string SELECT(long oid, ETipoAcreedor providerType) { return SELECT(oid, providerType, false); }

		internal static string SELECT(long oid, ETipoAcreedor providerType, bool lockTable)
        {
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));

            string query = string.Empty;
			
			QueryConditions conditions = new QueryConditions { TipoAcreedor = new ETipoAcreedor[1] { providerType } };
			if (oid != 0) conditions.Acreedor = ProviderBaseInfo.New(oid, providerType);

			query = SELECT(conditions, providerType);

            return query;
        }

		internal static string SELECT_LOCKEDOUT(QueryConditions conditions)
		{
			string query = string.Empty;

			conditions.ExtraWhere += @"
				AND US.""ESTADO"" IN (" + (long)EEstadoItem.LockedOut + ")";

			query = SELECT(conditions);

			return query;
		}

		internal static string SELECT_BUILDER(SelectLocalCaller caller, Library.Store.QueryConditions conditions)
		{
			string query = string.Empty;
			string union = @"
			UNION ";

			if (conditions.TipoAcreedor.Length == 1)
			{
				foreach (KeyValuePair<ETipoAcreedor, TProviderBase> item in ModuleController.Instance.ActiveAcreedores)
				{
					if (!item.Value.Active) continue;
					query += union + caller(conditions, item.Key);
				}

				conditions.TipoAcreedor[0] = ETipoAcreedor.Todos;
			}
			else
			{
				foreach (ETipoAcreedor item in conditions.TipoAcreedor)
				{
					query += union + caller(conditions, item);
				}
			}

			query = query.Substring(union.Length);

			return query;
		}

		internal static string SELECT_BUILDER(SelectCaller caller, Library.Store.QueryConditions conditions)
		{
			string query = string.Empty;
			string union = @"
			UNION ";

			if (conditions.TipoAcreedor.Length == 1)
			{
				foreach (KeyValuePair<ETipoAcreedor, TProviderBase> item in ModuleController.Instance.ActiveAcreedores)
				{
					if (!item.Value.Active) continue;

					conditions.TipoAcreedor[0] = item.Key;
					query += union + caller(conditions);
				}

				conditions.TipoAcreedor[0] = ETipoAcreedor.Todos;
			}
			else
			{
				ETipoAcreedor[] providerTypes = conditions.TipoAcreedor;

				foreach (ETipoAcreedor item in providerTypes)
				{
					conditions.TipoAcreedor = new ETipoAcreedor[1] { item };
					query += union + caller(conditions);
				}

				conditions.TipoAcreedor = providerTypes;
			}

			query = query.Substring(union.Length);

			return query;
		}

        #endregion
    }
	
	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
	public class SerialProviderBaseInfo : SerialInfo
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
		protected SerialProviderBaseInfo() { /* require use of factory methods */ }

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Obtiene el último serial de la entidad desde la base de datos
		/// </summary>
		/// <param name="oid">Oid del objeto</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static SerialProviderBaseInfo Get(ETipoAcreedor tipo)
		{
			CriteriaEx criteria = Proveedor.GetCriteria(Proveedor.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(tipo);

			SerialProviderBaseInfo obj = DataPortal.Fetch<SerialProviderBaseInfo>(criteria);
			Proveedor.CloseSession(criteria.SessionCode);
			return obj;
		}

		/// <summary>
		/// Obtiene el siguiente serial para una entidad desde la base de datos
		/// </summary>
		/// <param name="entity">Tipo de Entidad</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static long GetNext(ETipoAcreedor tipo) { return Get(tipo).Value + 1; }

		#endregion

		#region Root Data Access

		#endregion

		#region SQL

		public static string SELECT(ETipoAcreedor providerType)
		{
			string ac = ProviderBaseInfo.TABLE(providerType);
			string query;

			QueryConditions conditions = new QueryConditions
			{
				TipoAcreedor = new ETipoAcreedor[1] { providerType }
			};

			query = @"
				SELECT 0 AS ""OID"", MAX(""SERIAL"") AS ""SERIAL""
				FROM " + ac + @" AS AC
				WHERE TRUE";

			switch (providerType)
			{
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Proveedor:
				case ETipoAcreedor.Partner:
				case ETipoAcreedor.TransportistaDestino:
				case ETipoAcreedor.TransportistaOrigen:
					query += " AND \"TIPO\" = " + (long)providerType;
					break;
			}

			return query;
		}

		#endregion
	}

}

