using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.CslaEx;
using moleQule.Common;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class InputInvoiceLineBase
	{
		#region Attributes

        private InputInvoiceLineRecord _record = new InputInvoiceLineRecord();

		//NO ENLAZADOS
		internal string _cuenta_contable = string.Empty;
		internal long _oid_store;
        public string _store = string.Empty;
        public string _store_id = string.Empty;
		internal string _expedient = string.Empty;
		internal string _n_factura = string.Empty;
		internal DateTime _fecha_factura;
		internal string _acreedor = string.Empty;
		internal string _id_batch = string.Empty;

		#endregion

		#region Properties

        public InputInvoiceLineRecord Record { get { return _record; } set { _record = value; } }

		//Campos no enlazados
		public virtual bool IsKitComponent { get { return _record.OidKit > 0; } }
		public virtual Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
		public virtual Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 4); } }
		public virtual Decimal Impuestos { get { return Decimal.Round((BaseImponible * _record.PImpuestos) / 100, 4); } }
		public virtual bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		public virtual Decimal IRPF { get { return Decimal.Round((BaseImponible * _record.PIrpf) / 100, 4); } }
        public virtual string StoreID { get { return _store_id; } set { _store_id = value; } }
        public virtual string Store { get { return _store; } set { _store = value; } }
        public virtual string BatchID { get { return _id_batch; } set { _id_batch = value; } }

		#endregion

		#region Business Methods

		public void CopyValues(IDataReader source)
		{
			if (source == null) return;

            _record.CopyValues(source);

			_oid_store = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_store = Format.DataReader.GetString(source, "STORE");
            _store_id = Format.DataReader.GetString(source, "STORE_ID");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_expedient = Format.DataReader.GetString(source, "EXPEDIENTE");
			_n_factura = Format.DataReader.GetString(source, "N_FACTURA");
			_fecha_factura = Format.DataReader.GetDateTime(source, "FECHA_FACTURA");
			_acreedor = Format.DataReader.GetString(source, "ACREEDOR");
			_id_batch = Format.DataReader.GetString(source, "ID_BATCH");

			//Si no tiene expediente comprobamos si tiene el del gasto asociado
			_record.OidExpediente = (_record.OidExpediente == 0) ? Format.DataReader.GetInt64(source, "OID_EXPEDIENTE_GASTO") : _record.OidExpediente;
			_expedient = (_expedient == string.Empty) ? Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE_GASTO") : _expedient;
		}
		public void CopyValues(InputInvoiceLine source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_oid_store = source.OidAlmacen;
			_store = source.Almacen;
            _store_id = source.IDAlmacen;
			_cuenta_contable = source.CuentaContable;
			_n_factura = source.NFactura;
			_fecha_factura = source.FechaFactura;
			_acreedor = source.Acreedor;
			_id_batch = source.IDBatch;
		}
		public void CopyValues(InputInvoiceLineInfo source)
		{
            if (source == null) return;

            _record.CopyValues(source._base.Record);

			_oid_store = source.OidAlmacen;
            _store = source.Almacen;
            _store_id = source.IDAlmacen;
			_cuenta_contable = source.CuentaContable;
			_n_factura = source.NFactura;
			_fecha_factura = source.FechaFactura;
			_acreedor = source.Acreedor;
			_id_batch = source.IDBatch;
		}

		#endregion
	}

    /// <summary>
    /// Editable Child Business Object
    /// </summary>
    [Serializable()]
    public class InputInvoiceLine : BusinessBaseEx<InputInvoiceLine>
    {
        #region Attributes

		public InputInvoiceLineBase _base = new InputInvoiceLineBase();

        #endregion

        #region Properties

        public override long Oid
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Oid;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                // CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
                    _base.Record.Oid = value;
                    //PropertyHasChanged();
                }
            }
        }

        public virtual long OidFactura
        {

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidFactura;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidFactura.Equals(value))
                {
                    _base.Record.OidFactura = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidExpediente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidExpediente;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set 
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidExpediente.Equals(value))
                {
                    _base.Record.OidExpediente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidProducto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidProducto;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidProducto.Equals(value))
                {
                    _base.Record.OidProducto = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidPartida
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidPartida;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidPartida.Equals(value))
                {
                    _base.Record.OidPartida = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidConceptoAlbaran
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidConceptoAlbaran;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidConceptoAlbaran.Equals(value))
                {
                    _base.Record.OidConceptoAlbaran = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidKit
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidKit;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidKit.Equals(value))
                {
                    _base.Record.OidKit = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidImpuesto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidImpuesto;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidImpuesto.Equals(value))
                {
                    _base.Record.OidImpuesto = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string CodigoProductoAcreedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoProductoProveedor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoProductoProveedor.Equals(value))
				{
					_base.Record.CodigoProductoProveedor = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string Concepto
        {

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Concepto;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.Record.Concepto.Equals(value))
                {
                    _base.Record.Concepto = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal CantidadBultos
        {

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CantidadBultos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.CantidadBultos.Equals(value))
                {
                    _base.Record.CantidadBultos = value;
                    if (_base.Record.FacturacionBulto) CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal CantidadKilos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CantidadKilos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.CantidadKilos.Equals(value))
                {
                    _base.Record.CantidadKilos = value;
                    if (!_base.Record.FacturacionBulto) CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Precio
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Precio;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Precio.Equals(value))
                {
                    _base.Record.Precio = value;
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PImpuestos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PImpuestos;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PImpuestos.Equals(value))
                {
                    _base.Record.PImpuestos = value;
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PDescuento
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PDescuento;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PDescuento.Equals(value))
                {
                    _base.Record.PDescuento = value;
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Subtotal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Subtotal;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Subtotal.Equals(value))
                {
                    _base.Record.Subtotal = value;                   
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Total
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Total;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Total.Equals(value))
                {
                    _base.Record.Total = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool FacturacionBulto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FacturacionBulto;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.FacturacionBulto.Equals(value))
                {
                    _base.Record.FacturacionBulto = value;
                    Subtotal = (_base.Record.FacturacionBulto) ? _base.Record.CantidadBultos * _base.Record.Precio : _base.Record.CantidadKilos * _base.Record.Precio;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal PIRPF
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PIrpf;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.PIrpf.Equals(value))
                {
                    _base.Record.PIrpf = value;
                    CalculateTotal();
                    PropertyHasChanged();
                }
            }
        }

        //Campos no enlazados
		public virtual long OidAlmacen { get { return _base._oid_store; } set { _base._oid_store = value; } }
		public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string IDAlmacen { get { return _base.StoreID; } set { _base.StoreID = value; } }
		public virtual string IDBatch { get { return _base.BatchID; } set { _base.BatchID = value; } }
		public virtual string Expediente { get { return _base._expedient; } set { _base._expedient = value; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual string CuentaContable { get { return _base._cuenta_contable; } }
		public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
		public virtual DateTime FechaFactura { get { return _base._fecha_factura; } set { _base._fecha_factura = value; } }
		public virtual string Acreedor { get { return _base._acreedor; } set { _base._acreedor = value; } }
        public virtual Decimal IRPF { get { return _base.IRPF; } set { PropertyHasChanged(); } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty;
            }
        }
       
        #endregion

        #region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_base.CopyValues(source);

			//Esto es necesario pq si el OidExpediente viene a cero y se le asigna el del gasto
			//el objeto de sesion no se entera y luego en el Update no lo tenenmos
            InputInvoiceLineRecord obj = Session().Get<InputInvoiceLineRecord>(Oid);
			obj.OidExpediente = OidExpediente;
			//obj.Expediente = Expediente;
		}	
      
		public virtual void CopyFrom(InputDeliveryLineInfo source)
        {
            if (source == null) return;

            OidConceptoAlbaran = source.Oid;
            OidExpediente = source.OidExpediente;
			OidAlmacen = source.OidAlmacen;
            OidPartida = source.OidPartida;
            OidProducto = source.OidProducto;
            OidKit = source.OidKit;
            OidImpuesto = source.OidImpuesto;
            Concepto = source.Concepto;
            CantidadBultos = source.CantidadBultos;
            CantidadKilos = source.CantidadKilos;
            PImpuestos = source.PImpuestos;
            PDescuento = source.PDescuento;
            Total = source.Total;
            Precio = source.Precio;
            FacturacionBulto = source.FacturacionBulto;
            Subtotal = source.Subtotal;
            Expediente = source.CodigoExpediente;
            IDAlmacen = source.IDAlmacen;
            Almacen = source.Almacen;
            CodigoProductoAcreedor = source.CodigoProductoAcreedor;
            PIRPF = source.PIRPF;

			IDBatch = source.IDBatch;
        }

		/// <summary>
		/// Ajusta la cantidad de los bultos para que elimine decimales espureos
		/// </summary>
		/// <param name="partida"></param>
		public void BalanceQuantity(InputInvoice invoice, ProductInfo product)
		{
			if (product == null)
			{
				if (FacturacionPeso)
					CantidadBultos = CantidadKilos;
				else
					CantidadKilos = CantidadBultos;
			}
			else
			{
				if (FacturacionPeso)
					CantidadBultos = (product.KilosBulto == 0) ? CantidadBultos : CantidadKilos / product.KilosBulto;
				else
					CantidadKilos = (product.KilosBulto == 0) ? CantidadKilos : CantidadBultos * product.KilosBulto;
			}

            if (invoice.Rectificativa)
            {
                CantidadKilos = (CantidadKilos > 0) ? -CantidadKilos : CantidadKilos;
                CantidadBultos = (CantidadBultos > 0) ? -CantidadBultos : CantidadBultos;
            }
            else
            {
                CantidadKilos = (CantidadKilos < 0) ? -CantidadKilos : CantidadKilos;
                CantidadBultos = (CantidadBultos < 0) ? -CantidadBultos : CantidadBultos;
            }
		}
		private void BalancePieces(BatchInfo batch)
		{
			if (batch.StockKilos == 0) return;

			if (CantidadKilos == batch.StockKilos)
				CantidadBultos = batch.StockBultos;
			else
				CantidadBultos = CantidadKilos / batch.KilosPorBulto;
		}
		private void BalanceKilos(BatchInfo batch)
		{
			if (batch.StockBultos == 0) return;

			if (CantidadBultos == batch.StockBultos)
				CantidadKilos = batch.StockKilos;
			else
				CantidadKilos = CantidadBultos * batch.KilosPorBulto;
		}

        public virtual void CalculateTotal()
        {
			Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
			Total = BaseImponible + Impuestos - IRPF;

			//Forzamos el refresco del form
			//Impuestos = Impuestos;
			//Descuento = Descuento;
        }

        #endregion

        #region Validation Rules

        //región a rellenar si hay campos requeridos o claves ajenas

        //Descomentar en caso de existir reglas de validación
        /*protected override void AddBusinessRules()
        {	
            //Agregar reglas de validación
        }*/

        #endregion

        #region Authorization Rules

        public static bool CanAddObject()
        {
			return InputInvoice.CanAddObject();
        }

        public static bool CanGetObject()
        {
			return InputInvoice.CanGetObject();
        }

        public static bool CanDeleteObject()
        {
			return InputInvoice.CanDeleteObject();
        }

        public static bool CanEditObject()
        {
			return InputInvoice.CanEditObject();
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public InputInvoiceLine()
        {
            MarkAsChild();
            Oid = (long)(new Random()).Next();
        }
        private InputInvoiceLine(InputInvoiceLine source)
        {
            MarkAsChild();
            Fetch(source);
        }
		private InputInvoiceLine(InputInvoiceLineInfo source)
		{
			MarkAsChild();
			Fetch(source);
		}
        private InputInvoiceLine(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            MarkAsChild();
            Fetch(reader);
        }

        private InputInvoiceLine(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}

        //Por cada padre que tenga la clase
        public static InputInvoiceLine NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new InputInvoiceLine();
        }
        public static InputInvoiceLine NewChild(InputInvoice parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputInvoiceLine obj = new InputInvoiceLine();
            obj.OidFactura = parent.Oid;

            return obj;
        }

        internal static InputInvoiceLine GetChild(InputInvoiceLine source)
        {
            return new InputInvoiceLine(source);
        }
		internal static InputInvoiceLine GetChild(InputInvoiceLineInfo source)
		{
			return new InputInvoiceLine(source);
		}
        internal static InputInvoiceLine GetChild(int sessionCode, IDataReader reader)
        {
            return new InputInvoiceLine(sessionCode, reader);
        }

        public virtual InputInvoiceLineInfo GetInfo(bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
                  moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new InputInvoiceLineInfo(this, childs);
        }

        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        /// <summary>
        /// No se debe utilizar esta función para guardar. Hace falta el padre.
        /// Utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override InputInvoiceLine Save()
        {
            throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Child Data Access

        private void Fetch(InputInvoiceLine source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
            MarkOld();
        }
		private void Fetch(InputInvoiceLineInfo source)
		{
			try
			{
				SessionCode = source.SessionCode;

                _base.CopyValues(source);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			MarkOld();
		}
        private void Fetch(IDataReader source)
        {
            try
            {
                CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();

        }

        internal void Insert(InputInvoice parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidFactura = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(_base.Record);

			if ((OidExpediente != 0) && (parent.OidExpediente != OidExpediente))
				Store.Expedient.Get(OidExpediente, false, true, parent.SessionCode);

            MarkOld();
        }

        internal void Update(InputInvoice parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            this.OidFactura = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
            InputInvoiceLineRecord obj = Session().Get<InputInvoiceLineRecord>(Oid);
				
			long oid_exp_old = obj.OidExpediente;

            obj.CopyValues(_base.Record);
            Session().Update(obj);

			if ((OidExpediente != 0) && (parent.OidExpediente != OidExpediente))
				Store.Expedient.Get(OidExpediente, false, true, parent.SessionCode);

            if ((oid_exp_old != 0) && (OidExpediente != oid_exp_old))
            {
                Store.Expedient.Get(oid_exp_old, false, true, parent.SessionCode);

                InputDeliveryLineInfo ca = InputDeliveryLineInfo.Get(OidConceptoAlbaran, false);

                if (ca.OidExpediente != OidExpediente)
                {
                    InputDelivery albaran = InputDelivery.Get(ca.OidAlbaran, ETipoAcreedor.Todos, true, SessionCode);

                    albaran.Conceptos.GetItem(ca.Oid).OidExpediente = OidExpediente;

                    albaran.SaveAsChild();
                }
            }

            MarkOld();
        }

        internal void DeleteSelf(InputInvoice parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<InputInvoiceLineRecord>(Oid));

			if ((OidExpediente != 0) && (parent.OidExpediente != OidExpediente))
				Store.Expedient.Get(OidExpediente, false, true);

            MarkNew();
        }

        #endregion

		#region SQL

		internal enum ETipoQuery { GENERAL = 0, COSTES = 1 }

		internal static string SELECT_FIELDS(ETipoQuery tipo, QueryConditions conditions)
		{
			string query;

			query = @"
            SELECT CF.*
                    ,COALESCE(AL.""OID"", 0) AS ""OID_ALMACEN""
                    ,COALESCE(AL.""CODIGO"", '') AS ""STORE_ID""
                    ,COALESCE(AL.""NOMBRE"", '') AS ""STORE""
                    ,COALESCE(BA.""CODIGO"", '') AS ""ID_BATCH""
                    ,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENTE""
                    ,COALESCE(PR.""CUENTA_CONTABLE_COMPRA"", '') AS ""CUENTA_CONTABLE""
                    ,COALESCE(GT.""OID_EXPEDIENTE"", 0) AS ""OID_EXPEDIENTE_GASTO""
                    ,COALESCE(GT.""CODIGO"", '') AS ""CODIGO_EXPEDIENTE_GASTO""";

			switch (tipo)
			{
				case ETipoQuery.GENERAL:
					query += @"
                        ,'' AS ""N_FACTURA""
                        ,NULL AS ""FECHA_FACTURA""
                        ,'' AS ""ACREEDOR""";
					break;

				case ETipoQuery.COSTES:
					query += @"
                        ,FC.""N_FACTURA"" AS ""N_FACTURA""
                        ,FC.""FECHA"" AS ""FECHA_FACTURA""
                        ,A.""NOMBRE"" AS ""ACREEDOR""";
					break;
			}

			return query;
		}

		internal static string WHERE(Library.Store.QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = string.Empty;

			query += @"
            WHERE TRUE";

			if (conditions.ConceptoFacturaRecibida != null) 
                query += @"
                    AND CF.""OID"" = " + conditions.ConceptoFacturaRecibida.Oid;
			
            if (conditions.FacturaRecibida != null) 
                query += @"
                    AND CF.""OID_FACTURA"" = " + conditions.FacturaRecibida.Oid;
			
            if (conditions.Expedient != null) 
                query += @"
                    AND CF.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT_BASE(ETipoQuery tipo, Library.Store.QueryConditions conditions)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceLineRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ProductRecord));
            string ca = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string gt = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpenseRecord));
			string ba = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

			string query;

			query = 
            SELECT_FIELDS(tipo, conditions) + @"
            FROM " + cf + @" AS CF
            LEFT JOIN " + pr + @" AS PR ON PR.""OID"" = CF.""OID_PRODUCTO""
            LEFT JOIN " + ca + @" AS CA ON CA.""OID"" = CF.""OID_CONCEPTO_ALBARAN""
            LEFT JOIN " + al + @" AS AL ON AL.""OID"" = CA.""OID_ALMACEN""
            LEFT JOIN " + ba + @" AS BA ON BA.""OID"" = CA.""OID_BATCH""
            LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = CA.""OID_EXPEDIENTE""
            LEFT JOIN (SELECT GT.""OID_CONCEPTO_FACTURA"", GT.""OID_EXPEDIENTE"", EX.""CODIGO""
		                FROM " + gt + @" AS GT 
		                INNER JOIN " + ex + @" AS EX ON EX.""OID"" = GT.""OID_EXPEDIENTE""
		                GROUP BY ""OID_CONCEPTO_FACTURA"", ""OID_EXPEDIENTE"", EX.""CODIGO"")
                AS GT ON GT.""OID_CONCEPTO_FACTURA"" = CF.""OID""";// AND EX.""OID"" = GT.""OID_EXPEDIENTE"""; 

			return query;
		}

		internal static string SELECT_BASE_COSTES(Library.Store.QueryConditions conditions)
		{
			string query;

			query = SELECT_BASE(ETipoQuery.COSTES, conditions);

			if (conditions.Expedient != null)
			{
                string fc = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputInvoiceRecord));
                string se = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SerieRecord));
                string pv = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
                string pa = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));

				query += @"
                INNER JOIN " + pa + @" AS PA ON PA.""OID"" = CF.""OID_BATCH"" AND PA.""OID_EXPEDIENTE"" = " + conditions.Expedient.Oid + @"
                INNER JOIN " + fc + @" AS FC ON FC.""OID"" = CF.""OID_FACTURA""
                INNER JOIN " + se + @" AS SE ON SE.""OID"" = FC.""OID_SERIE""
                INNER JOIN " + pv + @" AS A ON A.""OID"" = FC.""OID_ACREEDOR"" AND FC.""TIPO_ACREEDOR"" = " + (long)ETipoAcreedor.Proveedor;
			}

			return query;
		}

		internal static string SELECT(Library.Store.QueryConditions conditions, bool lockTable)
		{
			string query =
            SELECT_BASE(ETipoQuery.GENERAL, conditions) +
            WHERE(conditions) +
            Common.EntityBase.LOCK("CF", lockTable);
			
			return query;
		}

		internal static string SELECT_COSTES(Library.Store.QueryConditions conditions, bool lockTable)
		{
			string query =
            SELECT_BASE_COSTES(conditions) +
			WHERE(conditions) +
            Common.EntityBase.LOCK("CF", lockTable);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions { ConceptoFacturaRecibida = InputInvoiceLineInfo.New(oid) };
			return SELECT(conditions, lockTable);
		}

		#endregion
    }
}