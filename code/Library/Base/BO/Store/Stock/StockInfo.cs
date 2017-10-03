using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class StockInfo : ReadOnlyBaseEx<StockInfo>
	{
		#region Attributes

		protected StockBase _base = new StockBase();

		#endregion

		#region Properties

		public StockBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } set { _base.Record.OidProducto = value; } }
		public long OidCliente { get { return _base.OidCliente; } set { _base.OidCliente = value; } }
		public long OidAlbaran { get { return _base.Record.OidAlbaran; } }
		public long OidConceptoAlbaran { get { return _base.Record.OidConceptoAlbaran; } }
		public long OidStock { get { return _base.Record.OidStock; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public string Concepto { get { return _base.Record.Concepto; } }
		public Decimal Bultos { get { return _base.Record.Bultos; } }
		public Decimal Kilos { get { return _base.Record.Kilos; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } set { _base.Record.Fecha = value; } }
		public bool Entrada { get { return _base.Entrada; } }
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }
		public long Tipo { get { return _base.Record.Tipo; } }
		public bool Inicial { get { return _base.Record.Inicial; } }
		public long OidLineaPedido { get { return _base.Record.OidLineaPedido; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public int OidUsuario { get { return _base.Record.OidUsuario; } }
        public long OidEnlace { get { return _base.Record.OidEnlace; } }

        //CAMPOS NO ENLAZADOS
		public virtual ETipoStock ETipoStock { get { return _base.ETipoStock; } set { _base.ETipoStock = value; } }
		public virtual string TipoStockLabel { get { return _base.TipoStockLabel; } }
		public virtual long OidSerie { get { return _base._oid_serie; } }
        public virtual long OidFamilia { get { return _base._oid_familia; } }
		public virtual Decimal KilosActuales { get { return _base._stock_kgs; } set { _base._stock_kgs = Decimal.Round(value, 2); } }
        public virtual Decimal BultosActuales { get { return _base._stock_bultos; } set { _base._stock_bultos = Decimal.Round(value, 4); } }
        public virtual bool IsKitComponent { get { return OidKit > 0; } }
		public virtual string IDPartida { get { return _base._id_partida; } set { _base._id_partida = value; } }
		public virtual string Producto { get { return _base._producto; } set { _base._producto = value; } }
        public virtual bool Bulto { get { return _base._facturacion_bulto; } set { _base._facturacion_bulto = value; } }
        public virtual string NAlbaran { get { return _base._n_albaran; } set { _base._n_albaran = value; } }
        public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
        public virtual string NCliente { get { return _base._n_titular; } set { _base._n_titular = value; } }
        public virtual string Cliente { get { return _base._titular; } set { _base._titular = value; } }
		public virtual string NTitular { get { return _base._n_titular; } set { _base._n_titular = value; } }
        public virtual string Proveedor { get { return _base._proveedor; } set { _base._proveedor = value; } }
        public virtual string Store { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string StoreID { get { return _base.StoreID; } set { _base.StoreID = value; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual Decimal KilosAcumulados { get { return _base._stock_acumulado_kgs; } set { _base._stock_acumulado_kgs = value; } }
		public virtual Decimal BultosAcumulados { get { return _base._stock_acumulado_bultos; } set { _base._stock_acumulado_bultos = value; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { Bulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
        public virtual string Usuario { get { return _base._usuario; } }
        public decimal PurchasePrice { get { return _base.PurchasePrice; } set { _base.PurchasePrice = value; } }
        public decimal SalePrice { get { return _base.SalePrice; } set { _base.SalePrice = value; } }
        public decimal AvgPurchasePrice { get { return _base.AvgPurchasePrice; } set { _base.AvgPurchasePrice = value; } }
        public decimal LastPurchasePrice { get { return _base.LastPurchasePrice; } set { _base.LastPurchasePrice = value; } }

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
		protected StockInfo() { /* require use of factory methods */ }
		private StockInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal StockInfo(Stock item, bool childs)
		{
			_base.CopyValues(item);
        }

        public StockPrint GetPrintObject()
        {
            return StockPrint.New(this);
        }

		public static StockInfo New(long oid = 0) { return new StockInfo() { Oid = oid }; }

        #endregion

        #region Child Factory Methods
        
		public static StockInfo GetChild(IDataReader reader, bool childs = false)
        {
			return new StockInfo(reader, childs);
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
				_base.CopyValues(source);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid) { return Stock.SELECT(oid, false); }

        #endregion
	}
}
