using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using NHibernate;
using moleQule;
using moleQule.CslaEx;

namespace moleQule.Store.Data
{
    [Serializable()]
    public class ExpedientRecord : RecordBase
    {
        #region Attributes

        private long _oid_proveedor;
        private long _oid_naviera;
        private long _oid_trans_origen;
        private long _oid_trans_destino;
        private long _oid_despachante;
        private long _oid_factura_pro;
        private long _oid_factura_nav;
        private long _oid_factura_des;
        private long _oid_factura_tor;
        private long _oid_factura_tde;
        private long _tipo_expediente;
        private long _serial;
        private string _codigo = string.Empty;
        private string _puerto_origen = string.Empty;
        private string _puerto_destino = string.Empty;
        private string _buque = string.Empty;
        private int _ano;
        private DateTime _fecha_pedido;
        private DateTime _fecha_fac_proveedor;
        private DateTime _fecha_embarque;
        private DateTime _fecha_llegada_muelle;
        private DateTime _fecha_despacho_destino;
        private DateTime _fecha_salida_muelle;
        private DateTime _fecha_regreso_muelle;
        private string _observaciones = string.Empty;
        private Decimal _flete_neto;
        private Decimal _baf;
        private bool _teus20 = false;
        private bool _teus40 = false;
        private Decimal _t3_origen;
        private Decimal _t3_destino;
        private Decimal _thc_origen;
        private Decimal _thc_destino;
        private Decimal _isps;
        private Decimal _total_impuestos;
        private bool _estimar_despachante = false;
        private bool _estimar_naviera = false;
        private bool _estimar_torigen = false;
        private bool _estimar_tdestino = false;
        private string _g_trans_fac = string.Empty;
        private Decimal _g_trans_total;
        private string _g_nav_fac = string.Empty;
        private Decimal _g_nav_total;
        private string _g_desp_fac = string.Empty;
        private Decimal _g_desp_total;
        private Decimal _g_desp_igic;
        private Decimal _g_desp_igic_serv;
        private string _g_trans_dest_fac = string.Empty;
        private Decimal _g_trans_dest_total;
        private Decimal _g_trans_dest_igic;
        private string _contenedor = string.Empty;
        private string _g_prov_fac = string.Empty;
        private Decimal _g_prov_total;
        private bool _ayuda = false;
        private string _tipo_mercancia = string.Empty;
        private string _nombre_cliente = string.Empty;
        private string _codigo_articulo = string.Empty;
        private string _nombre_trans_dest = string.Empty;
        private string _nombre_trans_orig = string.Empty;
        private Decimal _ayudas;
        private DateTime _fecha;

        #endregion

        #region Properties

        public virtual long OidProveedor { get { return _oid_proveedor; } set { _oid_proveedor = value; } }
        public virtual long OidNaviera { get { return _oid_naviera; } set { _oid_naviera = value; } }
        public virtual long OidTransOrigen { get { return _oid_trans_origen; } set { _oid_trans_origen = value; } }
        public virtual long OidTransDestino { get { return _oid_trans_destino; } set { _oid_trans_destino = value; } }
        public virtual long OidDespachante { get { return _oid_despachante; } set { _oid_despachante = value; } }
        public virtual long OidFacturaPro { get { return _oid_factura_pro; } set { _oid_factura_pro = value; } }
        public virtual long OidFacturaNav { get { return _oid_factura_nav; } set { _oid_factura_nav = value; } }
        public virtual long OidFacturaDes { get { return _oid_factura_des; } set { _oid_factura_des = value; } }
        public virtual long OidFacturaTor { get { return _oid_factura_tor; } set { _oid_factura_tor = value; } }
        public virtual long OidFacturaTde { get { return _oid_factura_tde; } set { _oid_factura_tde = value; } }
        public virtual long TipoExpediente { get { return _tipo_expediente; } set { _tipo_expediente = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string PuertoOrigen { get { return _puerto_origen; } set { _puerto_origen = value; } }
        public virtual string PuertoDestino { get { return _puerto_destino; } set { _puerto_destino = value; } }
        public virtual string Buque { get { return _buque; } set { _buque = value; } }
        public virtual int Ano { get { return _ano; } set { _ano = value; } }
        public virtual DateTime FechaPedido { get { return _fecha_pedido; } set { _fecha_pedido = value; } }
        public virtual DateTime FechaFacProveedor { get { return _fecha_fac_proveedor; } set { _fecha_fac_proveedor = value; } }
        public virtual DateTime FechaEmbarque { get { return _fecha_embarque; } set { _fecha_embarque = value; } }
        public virtual DateTime FechaLlegadaMuelle { get { return _fecha_llegada_muelle; } set { _fecha_llegada_muelle = value; } }
        public virtual DateTime FechaDespachoDestino { get { return _fecha_despacho_destino; } set { _fecha_despacho_destino = value; } }
        public virtual DateTime FechaSalidaMuelle { get { return _fecha_salida_muelle; } set { _fecha_salida_muelle = value; } }
        public virtual DateTime FechaRegresoMuelle { get { return _fecha_regreso_muelle; } set { _fecha_regreso_muelle = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual Decimal FleteNeto { get { return _flete_neto; } set { _flete_neto = value; } }
        public virtual Decimal Baf { get { return _baf; } set { _baf = value; } }
        public virtual bool Teus20 { get { return _teus20; } set { _teus20 = value; } }
        public virtual bool Teus40 { get { return _teus40; } set { _teus40 = value; } }
        public virtual Decimal T3Origen { get { return _t3_origen; } set { _t3_origen = value; } }
        public virtual Decimal T3Destino { get { return _t3_destino; } set { _t3_destino = value; } }
        public virtual Decimal ThcOrigen { get { return _thc_origen; } set { _thc_origen = value; } }
        public virtual Decimal ThcDestino { get { return _thc_destino; } set { _thc_destino = value; } }
        public virtual Decimal Isps { get { return _isps; } set { _isps = value; } }
        public virtual Decimal TotalImpuestos { get { return _total_impuestos; } set { _total_impuestos = value; } }
        public virtual bool EstimarDespachante { get { return _estimar_despachante; } set { _estimar_despachante = value; } }
        public virtual bool EstimarNaviera { get { return _estimar_naviera; } set { _estimar_naviera = value; } }
        public virtual bool EstimarTorigen { get { return _estimar_torigen; } set { _estimar_torigen = value; } }
        public virtual bool EstimarTdestino { get { return _estimar_tdestino; } set { _estimar_tdestino = value; } }
        public virtual string GTransFac { get { return _g_trans_fac; } set { _g_trans_fac = value; } }
        public virtual Decimal GTransTotal { get { return _g_trans_total; } set { _g_trans_total = value; } }
        public virtual string GNavFac { get { return _g_nav_fac; } set { _g_nav_fac = value; } }
        public virtual Decimal GNavTotal { get { return _g_nav_total; } set { _g_nav_total = value; } }
        public virtual string GDespFac { get { return _g_desp_fac; } set { _g_desp_fac = value; } }
        public virtual Decimal GDespTotal { get { return _g_desp_total; } set { _g_desp_total = value; } }
        public virtual Decimal GDespIgic { get { return _g_desp_igic; } set { _g_desp_igic = value; } }
        public virtual Decimal GDespIgicServ { get { return _g_desp_igic_serv; } set { _g_desp_igic_serv = value; } }
        public virtual string GTransDestFac { get { return _g_trans_dest_fac; } set { _g_trans_dest_fac = value; } }
        public virtual Decimal GTransDestTotal { get { return _g_trans_dest_total; } set { _g_trans_dest_total = value; } }
        public virtual Decimal GTransDestIgic { get { return _g_trans_dest_igic; } set { _g_trans_dest_igic = value; } }
        public virtual string Contenedor { get { return _contenedor; } set { _contenedor = value; } }
        public virtual string GProvFac { get { return _g_prov_fac; } set { _g_prov_fac = value; } }
        public virtual Decimal GProvTotal { get { return _g_prov_total; } set { _g_prov_total = value; } }
        public virtual bool Ayuda { get { return _ayuda; } set { _ayuda = value; } }
        public virtual string TipoMercancia { get { return _tipo_mercancia; } set { _tipo_mercancia = value; } }
        public virtual string NombreCliente { get { return _nombre_cliente; } set { _nombre_cliente = value; } }
        public virtual string CodigoArticulo { get { return _codigo_articulo; } set { _codigo_articulo = value; } }
        public virtual string NombreTransDest { get { return _nombre_trans_dest; } set { _nombre_trans_dest = value; } }
        public virtual string NombreTransOrig { get { return _nombre_trans_orig; } set { _nombre_trans_orig = value; } }
        public virtual Decimal Ayudas { get { return _ayudas; } set { _ayudas = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }

        #endregion

        #region Business Methods

        public ExpedientRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_proveedor = Format.DataReader.GetInt64(source, "OID_PROVEEDOR");
            _oid_naviera = Format.DataReader.GetInt64(source, "OID_NAVIERA");
            _oid_trans_origen = Format.DataReader.GetInt64(source, "OID_TRANS_ORIGEN");
            _oid_trans_destino = Format.DataReader.GetInt64(source, "OID_TRANS_DESTINO");
            _oid_despachante = Format.DataReader.GetInt64(source, "OID_DESPACHANTE");
            _oid_factura_pro = Format.DataReader.GetInt64(source, "OID_FACTURA_PRO");
            _oid_factura_nav = Format.DataReader.GetInt64(source, "OID_FACTURA_NAV");
            _oid_factura_des = Format.DataReader.GetInt64(source, "OID_FACTURA_DES");
            _oid_factura_tor = Format.DataReader.GetInt64(source, "OID_FACTURA_TOR");
            _oid_factura_tde = Format.DataReader.GetInt64(source, "OID_FACTURA_TDE");
            _tipo_expediente = Format.DataReader.GetInt64(source, "TIPO_EXPEDIENTE");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _puerto_origen = Format.DataReader.GetString(source, "PUERTO_ORIGEN");
            _puerto_destino = Format.DataReader.GetString(source, "PUERTO_DESTINO");
            _buque = Format.DataReader.GetString(source, "BUQUE");
            _ano = Format.DataReader.GetInt32(source, "ANO");
            _fecha_pedido = Format.DataReader.GetDateTime(source, "FECHA_PEDIDO");
            _fecha_fac_proveedor = Format.DataReader.GetDateTime(source, "FECHA_FAC_PROVEEDOR");
            _fecha_embarque = Format.DataReader.GetDateTime(source, "FECHA_EMBARQUE");
            _fecha_llegada_muelle = Format.DataReader.GetDateTime(source, "FECHA_LLEGADA_MUELLE");
            _fecha_despacho_destino = Format.DataReader.GetDateTime(source, "FECHA_DESPACHO_DESTINO");
            _fecha_salida_muelle = Format.DataReader.GetDateTime(source, "FECHA_SALIDA_MUELLE");
            _fecha_regreso_muelle = Format.DataReader.GetDateTime(source, "FECHA_REGRESO_MUELLE");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _flete_neto = Format.DataReader.GetDecimal(source, "FLETE_NETO");
            _baf = Format.DataReader.GetDecimal(source, "BAF");
            _teus20 = Format.DataReader.GetBool(source, "TEUS20");
            _teus40 = Format.DataReader.GetBool(source, "TEUS40");
            _t3_origen = Format.DataReader.GetDecimal(source, "T3_ORIGEN");
            _t3_destino = Format.DataReader.GetDecimal(source, "T3_DESTINO");
            _thc_origen = Format.DataReader.GetDecimal(source, "THC_ORIGEN");
            _thc_destino = Format.DataReader.GetDecimal(source, "THC_DESTINO");
            _isps = Format.DataReader.GetDecimal(source, "ISPS");
            _total_impuestos = Format.DataReader.GetDecimal(source, "TOTAL_IMPUESTOS");
            _estimar_despachante = Format.DataReader.GetBool(source, "ESTIMAR_DESPACHANTE");
            _estimar_naviera = Format.DataReader.GetBool(source, "ESTIMAR_NAVIERA");
            _estimar_torigen = Format.DataReader.GetBool(source, "ESTIMAR_TORIGEN");
            _estimar_tdestino = Format.DataReader.GetBool(source, "ESTIMAR_TDESTINO");
            _g_trans_fac = Format.DataReader.GetString(source, "G_TRANS_FAC");
            _g_trans_total = Format.DataReader.GetDecimal(source, "G_TRANS_TOTAL");
            _g_nav_fac = Format.DataReader.GetString(source, "G_NAV_FAC");
            _g_nav_total = Format.DataReader.GetDecimal(source, "G_NAV_TOTAL");
            _g_desp_fac = Format.DataReader.GetString(source, "G_DESP_FAC");
            _g_desp_total = Format.DataReader.GetDecimal(source, "G_DESP_TOTAL");
            _g_desp_igic = Format.DataReader.GetDecimal(source, "G_DESP_IGIC");
            _g_desp_igic_serv = Format.DataReader.GetDecimal(source, "G_DESP_IGIC_SERV");
            _g_trans_dest_fac = Format.DataReader.GetString(source, "G_TRANS_DEST_FAC");
            _g_trans_dest_total = Format.DataReader.GetDecimal(source, "G_TRANS_DEST_TOTAL");
            _g_trans_dest_igic = Format.DataReader.GetDecimal(source, "G_TRANS_DEST_IGIC");
            _contenedor = Format.DataReader.GetString(source, "CONTENEDOR");
            _g_prov_fac = Format.DataReader.GetString(source, "G_PROV_FAC");
            _g_prov_total = Format.DataReader.GetDecimal(source, "G_PROV_TOTAL");
            _ayuda = Format.DataReader.GetBool(source, "AYUDA");
            _tipo_mercancia = Format.DataReader.GetString(source, "TIPO_MERCANCIA");
            _nombre_cliente = Format.DataReader.GetString(source, "NOMBRE_CLIENTE");
            _codigo_articulo = Format.DataReader.GetString(source, "CODIGO_ARTICULO");
            _nombre_trans_dest = Format.DataReader.GetString(source, "NOMBRE_TRANS_DEST");
            _nombre_trans_orig = Format.DataReader.GetString(source, "NOMBRE_TRANS_ORIG");
            _ayudas = Format.DataReader.GetDecimal(source, "AYUDAS");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");

        }

        public virtual void CopyValues(ExpedientRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_proveedor = source.OidProveedor;
            _oid_naviera = source.OidNaviera;
            _oid_trans_origen = source.OidTransOrigen;
            _oid_trans_destino = source.OidTransDestino;
            _oid_despachante = source.OidDespachante;
            _oid_factura_pro = source.OidFacturaPro;
            _oid_factura_nav = source.OidFacturaNav;
            _oid_factura_des = source.OidFacturaDes;
            _oid_factura_tor = source.OidFacturaTor;
            _oid_factura_tde = source.OidFacturaTde;
            _tipo_expediente = source.TipoExpediente;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _puerto_origen = source.PuertoOrigen;
            _puerto_destino = source.PuertoDestino;
            _buque = source.Buque;
            _ano = source.Ano;
            _fecha_pedido = source.FechaPedido;
            _fecha_fac_proveedor = source.FechaFacProveedor;
            _fecha_embarque = source.FechaEmbarque;
            _fecha_llegada_muelle = source.FechaLlegadaMuelle;
            _fecha_despacho_destino = source.FechaDespachoDestino;
            _fecha_salida_muelle = source.FechaSalidaMuelle;
            _fecha_regreso_muelle = source.FechaRegresoMuelle;
            _observaciones = source.Observaciones;
            _flete_neto = source.FleteNeto;
            _baf = source.Baf;
            _teus20 = source.Teus20;
            _teus40 = source.Teus40;
            _t3_origen = source.T3Origen;
            _t3_destino = source.T3Destino;
            _thc_origen = source.ThcOrigen;
            _thc_destino = source.ThcDestino;
            _isps = source.Isps;
            _total_impuestos = source.TotalImpuestos;
            _estimar_despachante = source.EstimarDespachante;
            _estimar_naviera = source.EstimarNaviera;
            _estimar_torigen = source.EstimarTorigen;
            _estimar_tdestino = source.EstimarTdestino;
            _g_trans_fac = source.GTransFac;
            _g_trans_total = source.GTransTotal;
            _g_nav_fac = source.GNavFac;
            _g_nav_total = source.GNavTotal;
            _g_desp_fac = source.GDespFac;
            _g_desp_total = source.GDespTotal;
            _g_desp_igic = source.GDespIgic;
            _g_desp_igic_serv = source.GDespIgicServ;
            _g_trans_dest_fac = source.GTransDestFac;
            _g_trans_dest_total = source.GTransDestTotal;
            _g_trans_dest_igic = source.GTransDestIgic;
            _contenedor = source.Contenedor;
            _g_prov_fac = source.GProvFac;
            _g_prov_total = source.GProvTotal;
            _ayuda = source.Ayuda;
            _tipo_mercancia = source.TipoMercancia;
            _nombre_cliente = source.NombreCliente;
            _codigo_articulo = source.CodigoArticulo;
            _nombre_trans_dest = source.NombreTransDest;
            _nombre_trans_orig = source.NombreTransOrig;
            _ayudas = source.Ayudas;
            _fecha = source.Fecha;
        }

        #endregion
    }
}