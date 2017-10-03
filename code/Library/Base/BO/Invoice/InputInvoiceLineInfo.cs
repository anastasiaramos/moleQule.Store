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
	public class InputInvoiceLineInfo : ReadOnlyBaseEx<InputInvoiceLineInfo>
	{
		#region Attributes

		public InputInvoiceLineBase _base = new InputInvoiceLineBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidFactura { get { return _base.Record.OidFactura; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } set { _base.Record.OidExpediente = value; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidConceptoAlbaran { get { return _base.Record.OidConceptoAlbaran; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public string CodigoProductoAcreedor { get { return _base.Record.CodigoProductoProveedor; } }
		public string Concepto { get { return _base.Record.Concepto; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
		public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
        public Decimal PIRPF { get { return _base.Record.PIrpf; } }

		//NO ENLAZADAS
		public virtual long OidAlmacen { get { return _base._oid_store; } set { _base._oid_store = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string IDAlmacen { get { return _base.StoreID; } set { _base.StoreID = value; } }
		public virtual string IDBatch { get { return _base.BatchID; } set { _base.BatchID = value; } }
		public virtual string Expediente { get { return _base._expedient; } set { _base._expedient = value; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { _base.Record.FacturacionBulto = !value; } }
		public virtual string CuentaContable { get { return _base._cuenta_contable; } }
		public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
		public virtual DateTime FechaFactura { get { return _base._fecha_factura; } set { _base._fecha_factura = value; } }
		public virtual string Acreedor { get { return _base._acreedor; } set { _base._acreedor = value; } }
        public virtual Decimal IRPF { get { return _base.IRPF; } }

        #endregion

        #region Business Methods
        
        public InputInvoiceLinePrint GetPrintObject() { return InputInvoiceLinePrint.New(this); }

		public virtual void CalculaTotal()
		{
			_base.Record.Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
			_base.Record.Total = BaseImponible + Impuestos - IRPF;
		}

		#endregion

		#region Factory Methods

		protected InputInvoiceLineInfo() { /* require use of factory methods */ }
		private InputInvoiceLineInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal InputInvoiceLineInfo(InputInvoiceLine item)
			: this(item, false) {}		
		internal InputInvoiceLineInfo(InputInvoiceLine source, bool childs)
		{
			_base.CopyValues(source);
			
			if (childs)
			{
			}
		}
		
		public static InputInvoiceLineInfo Get(long oid)
		{
			return Get(oid, false);
		}
		public static InputInvoiceLineInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = InputInvoiceLine.GetCriteria(InputInvoiceLine.OpenSession());
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = InputInvoiceLine.SELECT(oid, false);
				
			criteria.Childs = childs;
			InputInvoiceLineInfo obj = DataPortal.Fetch<InputInvoiceLineInfo>(criteria);
			InputInvoiceLine.CloseSession(criteria.SessionCode);
			return obj;
		}
		public static InputInvoiceLineInfo Get(IDataReader reader, bool childs)
		{
			return new InputInvoiceLineInfo(reader, childs);
		}

		public static InputInvoiceLineInfo New(long oid = 0) { return new InputInvoiceLineInfo() { Oid = oid }; }

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
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
		}

		#endregion
	}
}



