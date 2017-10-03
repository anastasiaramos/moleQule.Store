using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ProductoPrint : ProductInfo
    {
        #region Attributes & Properties

        protected string _id;

        public string ID { get { return _id; } }

        #endregion

        #region Business Methods

        protected void CopyValues(ProductInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        protected void CopyValues(TrazabilidadInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.OidAyuda = source.OidAyuda;
            _base.Record.OidFamilia= source.OidSerie;
            _base._numero_familia = source.NumeroSerie;
            _base.Familia = source.NombreSerie;
            _base.Record.Bulto = source.Bulto;
            _base.Record.Observaciones = source.Observaciones;
            _base.Record.Nombre = source.Nombre;
            _base.Record.PrecioCompra = source.PrecioCompra;
            _base.Record.PrecioVenta = source.PrecioVenta;
            _base.Record.AyudaKilo = source.AyudaKilo;
            _base.Record.CodigoAduanero = source.CodigoAduanero;
            _base._oid_proveedor = source.OidProveedor;
            _base._oid_producto = source.OidProducto;
            _base._proveedor = source.Proveedor;
            _base._n_expediente = source.NExpediente;
            _base._fecha_compra = source.FechaCompra;
            _base._naviera = source.Naviera;
            _base._trans_ori = source.TransporteOri;
            _base._trans_dest = source.TrasporteDes;
            _base._cliente = source.Cliente;
            _base._fecha_venta = source.FechaVenta;
            _base._kilos_vendidos = source.KilosVendidos;
            _id = source.ID;
        }

        #endregion

        #region Factory Methods

        private ProductoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ProductoPrint New(ProductInfo source)
        {
            ProductoPrint item = new ProductoPrint();
            item.CopyValues(source);

            return item;
        }

        // called to load data from source
        public static ProductoPrint New(TrazabilidadInfo source)
        {
            ProductoPrint item = new ProductoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
