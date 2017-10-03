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
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Child Object
    /// </summary>
    [Serializable()]
    public class LineaPedidoProveedorInfo : ReadOnlyBaseEx<LineaPedidoProveedorInfo>
    {
        #region Attributes

		public InputOrderLineBase _base = new InputOrderLineBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidPedido { get { return _base.Record.OidPedido; } }
        public long OidProducto { get { return _base.Record.OidProducto; } }
        public long OidKit { get { return _base.Record.OidKit; } }
        public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
        public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
        public long OidExpediente { get { return _base.Record.OidExpediente; } }
        public string CodigoProductoAcreedor { get { return _base.Record.CodigoProductoProveedor; } }
        public string Concepto { get { return _base.Record.Concepto; } }
        public bool FacturacionBulto { get { return _base.Record.FacturacionBultos; } }
        public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
        public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
        public Decimal Precio { get { return _base.Record.Precio; } }
        public Decimal PDescuento { get { return _base.Record.PDescuento; } }
        public Decimal Subtotal { get { return _base.Record.Subtotal; } }
        public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
        public Decimal Total { get { return _base.Record.Total; } }
        public Decimal Gastos { get { return _base.Record.Gastos; } }
        public long Estado { get { return _base.Record.Estado; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }

        //NO ENLAZADAS
        public virtual EEstado EEstado { get { return _base.EEstado; } }
        public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
        public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
        public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
        public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } }
        public virtual long OidStock { get { return _base._oid_stock; } }
        public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
        public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
        public virtual Decimal Descuento { get { return _base.Descuento; } }
        public virtual Decimal Impuestos { get { return _base.Impuestos; } }
        public virtual Decimal Pendiente { get { return _base._pendiente; } }
        public virtual Decimal PendienteBultos { get { return _base._pendiente_bultos; } }
        public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
        public virtual bool IsComplete { get { return _base.IsComplete; } }

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
        protected LineaPedidoProveedorInfo() { /* require use of factory methods */ }
        private LineaPedidoProveedorInfo(int sessionCode, IDataReader reader, bool childs)
        {
            SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }
        internal LineaPedidoProveedorInfo(LineaPedidoProveedor item, bool copy_childs)
        {
            _base.CopyValues(item);

            if (copy_childs)
            {

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
        public static LineaPedidoProveedorInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
        public static LineaPedidoProveedorInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new LineaPedidoProveedorInfo(sessionCode, reader, childs); }

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
    }
}
