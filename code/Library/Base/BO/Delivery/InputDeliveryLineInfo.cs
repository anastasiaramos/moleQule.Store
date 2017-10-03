using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Business Object with ReadOnly Childs
	/// </summary>
	[Serializable()]
    public class InputDeliveryLineInfo : ReadOnlyBaseEx<InputDeliveryLineInfo>
	{
		#region Attributes

		public InputDeliveryLineBase _base = new InputDeliveryLineBase();
			
		protected BatchList _partidas = null;
		protected StockList _stocks = null;

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidAlbaran { get { return _base.Record.OidAlbaran; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidLineaPedido { get { return _base.Record.OidLineaPedido; } }
        public long OidPartida { get { return _base.Record.OidBatch; } }
        public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidKit { get { return _base.Record.OidKit; } }
        public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public string CodigoProductoAcreedor { get { return _base.Record.CodigoProductoProveedor; } }
		public string Concepto { get { return _base.Record.Concepto; } }
        public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
        public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
        public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
        public Decimal PImpuestos { get { return _base.Record.PIgic; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
		public Decimal Gastos { get { return _base.Record.Gastos; } }
        public Decimal PIRPF { get { return _base.Record.PIrpf; } }

        public BatchList Partidas { get { return _partidas; } set { _partidas = value; } }
        public StockList Stocks { get { return _stocks; } set { _stocks = value; } }

        public virtual string Almacen { get { return _base.Almacen; } set { _base.Almacen = value; } }
        public virtual string IDAlmacen { get { return _base.IDAlmacen; } set { _base.IDAlmacen = value; } }
        public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string IDBatch { get { return _base.IDBatch; } set { _base.IDBatch = value; } }
		public virtual string CodigoExpediente { get { return _base._expediente; } } /*DEPRECATED*/
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual Decimal AyudaKilo { get { return _base.AyudaKilo; } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } }
        public virtual string SaleMethodLabel { get { return _base.SaleMethodLabel; } }
		public virtual string Ubicacion { get { return _base._ubicacion; } }
		public virtual long OidStock { get { return _base._oid_stock; } }
        public virtual long OidPedido { get { return _base._oid_pedido; } }
        public virtual Decimal IRPF { get { return _base.IRPF; } }

        #endregion

        #region Business Methods
        
        public InputDeliveryLinePrint GetPrintObject()
        {
            return InputDeliveryLinePrint.New(this);
        }

		#endregion

		#region Factory Methods

		protected InputDeliveryLineInfo() { /* require use of factory methods */ }
		private InputDeliveryLineInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal InputDeliveryLineInfo(InputDeliveryLine source, bool childs = false)
		{
            _base.CopyValues(source);		
			
			if (childs)
			{
                if (source.Partidas != null)
                    _partidas = BatchList.GetChildList(source.Partidas);
                
                if (source.Stocks != null)
                    _stocks = StockList.GetChildList(source.Stocks);				
			}
		}

		public static InputDeliveryLineInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = InputDeliveryLine.GetCriteria(InputDeliveryLine.OpenSession());
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = InputDeliveryLineSQL.SELECT(oid, false);
				
    		criteria.Childs = childs;
	
            InputDeliveryLineInfo obj = DataPortal.Fetch<InputDeliveryLineInfo>(criteria);
			InputDeliveryLine.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		public static InputDeliveryLineInfo Get(int sessionCode, IDataReader reader, bool childs)
		{
			return new InputDeliveryLineInfo(sessionCode, reader, childs);
		}

        public static InputDeliveryLineInfo New(long oid = 0) { return new InputDeliveryLineInfo() { Oid = oid }; }

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
						
                        query = BatchInfo.SELECT(OidPartida);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _partidas = BatchList.GetChildList(SessionCode, reader, false);

                        query = StockList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _stocks = StockList.GetChildList(reader);					
                    }
				}
			}
			catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
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

                    query = BatchInfo.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_partidas = BatchList.GetChildList(SessionCode, reader, false);
					
					query = StockList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _stocks = StockList.GetChildList(reader);					
				}
			}
			catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return InputDeliveryLineSQL.SELECT(oid, false); }

        #endregion
	}
}