using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Globalization;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 
using moleQule.Hipatia;
using moleQule.Serie;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class InputInvoiceInfo : ReadOnlyBaseEx<InputInvoiceInfo>, IAgenteHipatia, IEntidadRegistroInfo, ITransactionPayment
	{
        #region IAgenteHipatia

        public string IDHipatia { get { return Ano + "/" + Codigo; } }
        public string NombreHipatia { get { return Acreedor; } }
		public Type TipoEntidad { get { return typeof(InputInvoice); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region IEntidadRegistroInfo

		public moleQule.Common.Structs.ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.FacturaRecibida; } }
		public string DescripcionRegistro { get { return "FACTURA RECIBIDA Nº " + NFactura + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2") + " de " + Acreedor; } }

		#endregion

		#region ITransactionPayment

        public decimal Asignado { get { return _base.Allocated; } set { _base.Allocated = value; } }
        public decimal TotalPagado { get { return _base.Pagado; } set { _base.Pagado = value; } }
        public decimal Pendiente { get { return _base.Pendiente; } set { _base.Pendiente = value; } }
        public decimal PendienteAsignar { get { return _base.PendienteAsignar; } set { _base.PendienteAsignar = value; } }
        public string FechaAsignacion { get { return _base.AllocationDate; } set { _base.AllocationDate = value; } }
		public string Vinculado { get { return _base.Linked; } set { _base.Linked = value; } }
        public decimal Acumulado { get { return _base.Aggregate; } set { _base.Aggregate = value; } }

		#endregion

		#region Attributes

		public InputInvoiceBase _base = new InputInvoiceBase();

        protected InputInvoiceLineList _conceptos = null;
        protected AlbaranFacturaProveedorList _albaran_facturas = null;

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
        public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidAcreedor { get { return _base.Record.OidAcreedor; } }
		public long TipoAcreedor { get { return _base.Record.TipoAcreedor; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string NFactura { get { return _base.Record.NFactura; } }
		public string Acreedor { get { return _base.Record.Acreedor; } }
		public string VatNumber { get { return _base.Record.VatNumber; } }
		public string Direccion { get { return _base.Record.Direccion; } }
		public string CodigoPostal { get { return _base.Record.CodigoPostal; } }
		public string Provincia { get { return _base.Record.Provincia; } }
		public string Municipio { get { return _base.Record.Municipio; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public DateTime FechaRegistro { get { return _base.Record.FechaRegistro; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
		public Decimal Total { get { return _base.Record.Total; } set { } }
		public Decimal PIRPF { get { return _base.Record.PIrpf; } }
		public Decimal Impuestos { get { return _base.Record.Impuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public bool Nota { get { return _base.Record.Nota; } }
		public long Ano { get { return _base.Record.Ano; } }
		public bool AlbaranContado { get { return _base.Record.Albaran; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
        public Decimal Descuento { get { return _base.Record.Descuento; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public DateTime Prevision { get { return _base.Record.Prevision; } }
		public bool Rectificativa { get { return _base.Record.Rectificativa; } }
		public string Albaranes { get { return _base.Record.Albaranes; } }
        public Decimal IRPF { get { return _base.Record.Irpf; } }

        public InputInvoiceLineList Conceptos { get { return _conceptos; } }
        public AlbaranFacturaProveedorList AlbaranesFacturas { get { return _albaran_facturas; } }

        //CAMPOS NO ENLAZADOS
		public string Usuario { get { return _base._usuario; } }
        public string NSerie { get { return _base._n_serie; } }
		public string Serie { get { return _base._serie; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
        public string CodigoExpediente { get { return _base._expediente; } } /* DEPRECATED */
		public string Expediente { get { return _base._expediente; } }
		public string IDAcreedor { get { return _base._n_acreedor; } }
        public string NumeroAcreedor { get { return IDAcreedor; } } /* DEPRECATED */
        public bool Pagada { get { return _base.Pagada; } }
		public decimal Pagado { get { return _base.Pagado; } set { _base.Pagado = value; } }
		public decimal PendienteVencimiento { get { return _base._efectos_negociados; } set { _base._efectos_negociados = value; } }
		public decimal Vencido { get { return _base._efectos_devueltos; } set { _base._efectos_devueltos = value; } }
		public decimal EfectosPendientesVto { get { return _base._efectos_pendientes_vto; } set { _base._efectos_devueltos = value; } }
		public long DiasTranscurridos { get { return _base.DiasTranscurridos; } }
        public Decimal Subtotal { get { return _base.Subtotal; } }
        public ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } }
		public string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public EEstado EEstado { get { return _base.EEstado; } }
		public string EstadoLabel { get { return _base.EstadoLabel; } }
		public DateTime FechaPagoFactura { get { return _base._fecha_pago; } }
		public string IDPago { get { return _base._id_pago; } }
		public string FileName { get { return _base.FileName; } }
		public string FechaPago { get { return _base.FechaPago; } }		
		public string IDMovimientoContable { get { return _base._id_mov_contable; } }
		public string FechaRegistroLb { get { return _base.FechaRegistroLb; } }
		public string Activo { get { return _base.Linked; } set { _base.Linked = value; } }
		public virtual decimal TotalExpediente { get { return _base._total_expediente; } set { _base._total_expediente = value; } }
        public virtual bool NecesitaExpediente { get { return _base.NecesitaExpediente; } }
		public DateTime StepDate { get { return _base.StepDate; } }

		#endregion
		
		#region Business Methods

		protected void CopyValues(InputInvoice source)
		{
			if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
		}

        public void CopyFrom(InputInvoice source) { CopyValues(source); }

        public List<CuentaResumen> GetCuentas()
        {
			try
			{
				List<CuentaResumen> list = new List<CuentaResumen>();
				ProductList productos = ProductList.GetList(false, true);
				FamiliaList familias = FamiliaList.GetList(false, true);
				bool nuevo;
				ProductInfo producto;
				FamiliaInfo familia;
				string cuenta;

				foreach (InputInvoiceLineInfo item in _conceptos)
				{
					nuevo = true;
					producto = productos.GetItem(item.OidProducto);
                    if (producto == null) producto = productos.GetItem(InputDeliveryLineInfo.Get(item.OidConceptoAlbaran, false).OidProducto);
					familia = familias.GetItem(producto.OidFamilia);

					cuenta = (producto.CuentaContableCompra == string.Empty) ? familia.CuentaContableCompra : producto.CuentaContableCompra;

					//Agrupamos los conceptos por cuentas contables
					for (int i = 0; i < list.Count; i++)
					{
						CuentaResumen cr = list[i];

						//Tiene prioridad la cuenta contable del producto
						if (producto.CuentaContableCompra != string.Empty)
						{
							if (cr.CuentaContable == producto.CuentaContableCompra)
							{
								cr.Importe += item.BaseImponible;
								list[i] = cr;
								nuevo = false;
								break;
							}
						}
						//Luego la de la familia
						else if (cr.CuentaContable == familia.CuentaContableCompra)
						{
							cr.Importe += item.BaseImponible;
							list[i] = cr;
							nuevo = false;
							break;
						}
					}

					if (nuevo)
						list.Add(new CuentaResumen { 
										OidFamilia = producto.OidFamilia, 
										Importe = item.BaseImponible, 
										CuentaContable = cuenta
									});
				}

				return list;
			}
			catch 
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_CUENTA, NFactura, Acreedor));
			}
        }
		public List<CuentaResumen> GetCuentasAndImpuestos(bool get_indirectos)
		{
			try
			{
				List<CuentaResumen> list = new List<CuentaResumen>();
				ProductList productos = ProductList.GetList(false, true);
				FamiliaList familias = FamiliaList.GetList(false, true);
                ImpuestoList impuestos = ImpuestoList.GetList(false);
				bool nuevo;
				ProductInfo producto;
				FamiliaInfo familia;
				string cuenta;
                string nombre;

				foreach (InputInvoiceLineInfo item in _conceptos)
				{
					ImpuestoResumen impuesto = new ImpuestoResumen();

					nuevo = true;
					producto = productos.GetItem(item.OidProducto);
					familia = familias.GetItem(producto.OidFamilia);


					cuenta = (producto.CuentaContableCompra == string.Empty) ? familia.CuentaContableCompra : producto.CuentaContableCompra;
                    nombre = (producto.CuentaContableCompra == string.Empty)
                        ? familia.Codigo + " " + familia.Nombre
                        : producto.Codigo + " " + producto.Nombre;

					//Agrupamos los conceptos por cuentas contables
					for (int i = 0; i < list.Count; i++)
					{
						CuentaResumen cr = list[i];

						//Tiene prioridad la cuenta contable del producto
						if (producto.CuentaContableCompra != string.Empty)
						{
                            if ((cr.CuentaContable == producto.CuentaContableCompra) && (cr.Impuesto != null && cr.Impuesto.OidImpuesto == item.OidImpuesto))
							{
								cr.Importe += item.BaseImponible;
								cr.Impuesto.Importe += item.Impuestos;
								cr.Impuesto.BaseImponible += item.BaseImponible;
								list[i] = cr;
								nuevo = false;
								break;
							}
						}
						//Luego la de la familia
						else if ((cr.CuentaContable == familia.CuentaContableCompra) && (cr.Impuesto != null && cr.Impuesto.OidImpuesto == item.OidImpuesto))
                        {

							cr.Importe += item.BaseImponible;
							cr.Impuesto.Importe += item.Impuestos;
                            cr.Impuesto.BaseImponible += item.BaseImponible;
							list[i] = cr;
							nuevo = false;
							break;
						}
					}

					if (nuevo)
					{
						CuentaResumen new_cr = new CuentaResumen
						{
							OidFamilia = producto.OidFamilia,
							Importe = item.BaseImponible,
							CuentaContable = cuenta,
                            Nombre = nombre,
						};

						if (item.Impuestos != 0)
						{
                            ImpuestoInfo imp = null;

                            if (item.OidImpuesto != 0)
                                imp = impuestos.GetItem(item.OidImpuesto);
                            else
                                imp = impuestos.GetItemByProperty("Porcentaje",item.PImpuestos);

							new_cr.Impuesto = new ImpuestoResumen
							{
								OidImpuesto = item.OidImpuesto,
								BaseImponible = item.BaseImponible,
								Importe = item.Impuestos,                                
                                SubtipoFacturaRecibida = imp.CodigoImpuestoA3Recibida,
                                Porcentaje = imp.Porcentaje,
							};	
						}
						else if ((get_indirectos) && (item.CuentaContable == "4727000001"))
						{
							new_cr.Impuesto = new ImpuestoResumen
							{
								OidImpuesto = 4,
								BaseImponible = 0,
								Importe = item.Total,
                                SubtipoFacturaRecibida = impuestos.GetItem(4).CodigoImpuestoA3Recibida,
							};
						}
						
						list.Add(new_cr);
					}
				}

				return list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_CUENTA, NFactura, Acreedor));
			}
		}

		public List<ImpuestoResumen> GetImpuestos() { return GetImpuestos(false); }
        public List<ImpuestoResumen> GetImpuestos(bool get_indirectos)
        {
			try
			{
				List<ImpuestoResumen> list = new List<ImpuestoResumen>();
				bool nuevo;

				foreach (InputInvoiceLineInfo item in _conceptos)
				{
					if (item.Impuestos != 0)
					{
						nuevo = true;

						//Agrupamos los conceptos por tipo de impuesto devengado
						for (int i = 0; i < list.Count; i++)
						{
							ImpuestoResumen cr = list[i];

							if (cr.OidImpuesto == item.OidImpuesto)
							{
								cr.Importe += item.Impuestos;
								cr.BaseImponible += item.BaseImponible;
								list[i] = cr;
								nuevo = false;
								break;
							}
						}

						if (nuevo)
							list.Add(new ImpuestoResumen { OidImpuesto = item.OidImpuesto, Importe = item.Impuestos, BaseImponible = item.BaseImponible });
					}
					else if ((get_indirectos) && (item.CuentaContable == "4727000001"))
						list.Add(new ImpuestoResumen { OidImpuesto = 4, Importe = item.Total, BaseImponible = 0});
				}

				return list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, NFactura, Acreedor));
			}

        }

        public ControlPagosPrint GetControlPagosPrintObject(ExpedientInfo expediente, PaymentSummary resumen)
        {
            return ControlPagosPrint.New(this, expediente, resumen);
        }

		public bool ContainsPartida(long oid_expediente)
		{
			foreach (InputInvoiceLineInfo item in Conceptos)
			{
				if (item.OidExpediente == oid_expediente) return true;
			}

			return false;
		}

		public void CalculateTotal()
		{
			_base.Record.BaseImponible = 0;
			_base.Record.Impuestos = 0;
			_base.Record.Total = 0;
            _base.Record.Irpf = 0;

			foreach (InputInvoiceLineInfo item in Conceptos)
			{
				if (!item.IsKitComponent)
				{
					item.CalculaTotal();

					_base.Record.BaseImponible += item.BaseImponible;
				}
			}

			_base.Record.Descuento = PDescuento != 0 ? BaseImponible * PDescuento / 100 : Descuento;

			//Esta funcion actualiza la propiedad Impuestos 
			_base.GetImpuestos(Conceptos);
            _base.GetIRPF(Conceptos);

			_base.Record.BaseImponible -= Descuento;
			_base.Record.Total = BaseImponible - IRPF + Impuestos;
		}

        public Hashtable GetIRPF() { return _base.GetIRPF(_conceptos); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected InputInvoiceInfo() { /* require use of factory methods */ }
		private InputInvoiceInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal InputInvoiceInfo(InputInvoice source, bool childs)
		{
			CopyValues(source);
			
			if (childs)
			{
				_conceptos = (source.Conceptos != null) ? InputInvoiceLineList.GetChildList(source.Conceptos) : null;
				_albaran_facturas = (source.AlbaranesFacturas != null) ? AlbaranFacturaProveedorList.GetChildList(source.AlbaranesFacturas) : null;
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
		public static InputInvoiceInfo GetChild(int sessionCode, IDataReader reader)
        {
			return GetChild(sessionCode, reader, false);
		}
		public static InputInvoiceInfo GetChild(int sessionCode, IDataReader reader, bool retrieve_childs)
        {
			return new InputInvoiceInfo(sessionCode, reader, retrieve_childs);
		}

		public virtual void LoadChilds(Type type, bool get_childs)
		{
			if (type.Equals(typeof(InputInvoiceLine)))
			{
				_conceptos = InputInvoiceLineList.GetChildList(this, get_childs);
			}
		}

		public static InputInvoiceInfo New(long oid = 0) { return new InputInvoiceInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
        public static InputInvoiceInfo Get(long oid, ETipoAcreedor tipo) { return Get(oid, tipo, false); }
		public static InputInvoiceInfo Get(long oid, ETipoAcreedor tipo, bool retrieve_childs)
		{
			CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = InputInvoiceInfo.SELECT(oid, tipo);
			else
				criteria.AddOidSearch(oid);
	
			InputInvoiceInfo obj = DataPortal.Fetch<InputInvoiceInfo>(criteria);
			InputInvoice.CloseSession(criteria.SessionCode);
			return obj;
		}

        internal static InputInvoiceInfo Get(string query, bool childs)
        {
            CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            InputInvoiceInfo obj = DataPortal.Fetch<InputInvoiceInfo>(criteria);
            InputInvoice.CloseSession(criteria.SessionCode);
            return obj;
        }

        public static InputInvoiceInfo Exists(QueryConditions conditions, bool childs)
        {
            return Get(SELECT_EXISTS(conditions), childs);
        }

        public InputInvoicePrint GetPrintObject()
        {
            return InputInvoicePrint.New(this, null);
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
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;

                        query = InputInvoiceLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos = InputInvoiceLineList.GetChildList(SessionCode, reader);

						query = AlbaranFacturaProveedorList.SELECT_BY_FACTURA(this.Oid);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturaProveedorList.GetChildList(reader);
                    }
				}
			}
            catch (Exception ex){
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
            string query = string.Empty;

			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{					
					IDataReader reader;

                    query = InputInvoiceLineList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _conceptos = InputInvoiceLineList.GetChildList(SessionCode, reader);

					query = AlbaranFacturaProveedorList.SELECT_BY_FACTURA(this.Oid);
					reader = nHMng.SQLNativeSelect(query, Session());
					_albaran_facturas = AlbaranFacturaProveedorList.GetChildList(reader);
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { query });
            }
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid, ETipoAcreedor tipo) { return InputInvoiceSQL.SELECT(oid, tipo, false); }
        public static string SELECT_EXISTS(QueryConditions conditions) { return InputInvoiceSQL.SELECT_EXISTS(conditions); }

        #endregion
    }

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialFacturaProveedorInfo : SerialInfo
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
        protected SerialFacturaProveedorInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static SerialFacturaProveedorInfo Get(int year)
        {
            CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(year);

            SerialFacturaProveedorInfo obj = DataPortal.Fetch<SerialFacturaProveedorInfo>(criteria);
            InputInvoice.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(int year)
        {
            return Get(year).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(int year)
        {
            string f = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
            string query;

			QueryConditions conditions = new QueryConditions 
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
					" FROM " + f + " AS F" +
					" WHERE TRUE";

			if (year != DateTime.MinValue.Year)//FECHA_REGISTRO
				query += " AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            return query;
        }

        #endregion
    }
}
