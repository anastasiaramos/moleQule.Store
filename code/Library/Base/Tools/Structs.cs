using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using moleQule;
using moleQule.Common.Structs;
using moleQule.Invoice.Structs;

namespace moleQule.Store.Structs
{
    #region Enums

    public enum EEstadoConceptoPedido { Pedido = 1, Pendiente = 2 }

    public enum EPagos { Todos, Pagados, Pendientes }
    public enum EPrioridadPrecio { Cero = 0, Producto = 1, Titular = 2, UltimoPrecio = 3 }
    public enum ESerie { GENERICA = 0, ALIMENTACION = 1, GANADO = 2, MAQUINARIA = 3, FORMACION = 4 }

    public enum ECategoriaGasto
    {
        Todos = 0, GeneralesExpediente = 1, Stock = 2, OtrosExpediente = 3, Bancario = 4, Nomina = 5, SeguroSocial = 6, Impuesto = 7, Administracion = 8, Otros = 9,
        Seleccione = -1, NoStock = -2, Expediente = -3, Generales = -4, NoExpediente = -5, OtrosBalance = -6, IRPFNominas = -7, SeguroSocialNominas = -8
    }
    public enum EPayrollMethod { All = 0, Month = 1, ByHour = 2, MonthPlusHours = 3, MonthPlusExtras = 4 }

    public enum ETipoAlbaranes { Lista, Todos, Facturados, NoFacturados, Agrupados, NoTicket, Ticket, Work }
    public enum ETipoAyuda { REA, Fomento }
    public enum ETipoExpediente { Todos = 0, Almacen = 1, Alimentacion = 2, Maquinaria = 3, Ganado = 4, Work = 5, Project = 6 }
    public enum ETipoFacturas { Todas, Cobradas, Pendientes, Pagadas, DudosoCobro }
    public enum ETipoFactura
    {
        Todas = 0, Ordinaria = 1, Rectificativa = 2,
        NoAgrupadas = -1
    }
    public enum ETipoFamilia { Todas = 0, Alimentacion = 1, Ganado = 2, Maquinaria = 5, Transporte = 4, Servicios = 7 }
    public enum ETipoInforme { Todos = 0, Proveedor = 1, Naviera = 2, TransportistaOrigen = 3, TransportistaDestino = 4, Despachante = 5 }
    public enum ETipoLineaLibroGanadero { Todos = 0, Importacion = 1, Venta = 2, Muerte = 3, TraspasoExplotacion = 4, Nacimiento = 5 }
    public enum ETipoPago { Todos = 0, Factura = 1, Gasto = 2, Nomina = 3, Prestamo = 4, Fraccionado = 5, ExtractoTarjeta = 6, FraccionadoTarjeta = 7 }
    public enum ETipoProducto { Todos = 0, Expediente = 1, Libres = 2, Almacen = 3, Kit = 4 }
    public enum ETipoPartida { Todos = 0, Generico = 1, Kit = 2, Componente = 3 }
    public enum ETipoSerie { Todas = 0, Compra = 1, Venta = 2, CompraVenta = 3, Work = 4 }
    public enum ETipoStock { Todos = 0, Compra = 1, Venta = 2, MovimientoEntrada = 3, Merma = 4, MovimientoSalida = 5, AltaKit = 6, BajaKit = 7, Reserva = 8, Consumo = 9 }
    public enum ETipoTransportista { Todos = 0, Origen = 1, Destino = 2 }
    
    public enum Perfil
    {
        Examinador = 1, // 0000000000000001
        Instructor = 2, // 0000000000000010
        RExamenes = 4, // 0000000000000100
        InstPracticas = 8, // 0000000000001000
        RespInstruccion = 16, // 0000000000010000
        RespCalidad = 32, // 0000000000100000
        EvalPracticas = 64, // 0000000001000000
        Auditor = 128, // 0000000100000000
        Gerente = 256, // 0000001000000000
        Administrador = 512, // 0000010000000000
        Empleado = 1024, // 0000001000000000
    }

    public static class EnumConvert
    {
        public static ECategoriaGasto ToECategoriaGasto(ETipoPago source)
        {
            switch (source)
            {
                case ETipoPago.Gasto: return ECategoriaGasto.Generales;
                case ETipoPago.Nomina: return ECategoriaGasto.Nomina;
            }

            return ECategoriaGasto.Todos;
        }
        public static ETipoAcreedor ToETipoAcreedor(ETipoEntidad source)
        {
            switch (source)
            {
                case moleQule.Common.Structs.ETipoEntidad.Acreedor: return ETipoAcreedor.Acreedor;
                case moleQule.Common.Structs.ETipoEntidad.Partner: return ETipoAcreedor.Partner;
                case moleQule.Common.Structs.ETipoEntidad.Proveedor: return ETipoAcreedor.Proveedor;
                case moleQule.Common.Structs.ETipoEntidad.Naviera: return ETipoAcreedor.Naviera;
                case moleQule.Common.Structs.ETipoEntidad.TransportistaOrigen: return ETipoAcreedor.TransportistaOrigen;
                case moleQule.Common.Structs.ETipoEntidad.TransportistaDestino: return ETipoAcreedor.TransportistaDestino;
                case moleQule.Common.Structs.ETipoEntidad.Despachante: return ETipoAcreedor.Despachante;
                case moleQule.Common.Structs.ETipoEntidad.Instructor: return ETipoAcreedor.Instructor;
            }

            return ETipoAcreedor.Todos;
        }
        public static ETipoAcreedor ToETipoAcreedor(ETipoTitular source)
        {
            switch (source)
            {
                case ETipoTitular.Acreedor: return ETipoAcreedor.Acreedor;
                case ETipoTitular.Partner: return ETipoAcreedor.Partner;
                case ETipoTitular.Proveedor: return ETipoAcreedor.Proveedor;
                case ETipoTitular.Naviera: return ETipoAcreedor.Naviera;
                case ETipoTitular.TransportistaOrigen: return ETipoAcreedor.TransportistaOrigen;
                case ETipoTitular.TransportistaDestino: return ETipoAcreedor.TransportistaDestino;
                case ETipoTitular.Despachante: return ETipoAcreedor.Despachante;
                case ETipoTitular.Instructor: return ETipoAcreedor.Instructor;
            }

            return ETipoAcreedor.Todos;
        }
        public static ETipoAcreedor ToETipoAcreedor(ETipoTransportista source)
        {
            switch (source)
            {
                case ETipoTransportista.Destino: return ETipoAcreedor.TransportistaDestino;
                case ETipoTransportista.Origen: return ETipoAcreedor.TransportistaOrigen;
            }

            return ETipoAcreedor.Todos;
        }
        public static ETipoEntidad ToETipoEntidad(ETipoAcreedor source)
        {
            switch (source)
            {
                case ETipoAcreedor.Acreedor: return moleQule.Common.Structs.ETipoEntidad.Acreedor;
                case ETipoAcreedor.Partner: return moleQule.Common.Structs.ETipoEntidad.Partner;
                case ETipoAcreedor.Proveedor: return moleQule.Common.Structs.ETipoEntidad.Proveedor;
                case ETipoAcreedor.Naviera: return moleQule.Common.Structs.ETipoEntidad.Naviera;
                case ETipoAcreedor.TransportistaOrigen: return moleQule.Common.Structs.ETipoEntidad.TransportistaOrigen;
                case ETipoAcreedor.TransportistaDestino: return moleQule.Common.Structs.ETipoEntidad.TransportistaDestino;
                case ETipoAcreedor.Despachante: return moleQule.Common.Structs.ETipoEntidad.Despachante;
                case ETipoAcreedor.Instructor: return moleQule.Common.Structs.ETipoEntidad.Instructor;
            }

            return moleQule.Common.Structs.ETipoEntidad.Todos;
        }
        public static EBankLineType ToETipoMovimientoBanco(ETipoPago source)
        {
            switch (source)
            {
                case ETipoPago.Factura: return EBankLineType.PagoFactura;
                case ETipoPago.Gasto: return EBankLineType.PagoGasto;
                case ETipoPago.Nomina: return EBankLineType.PagoNomina;
                case ETipoPago.Prestamo: return EBankLineType.PagoPrestamo;
                case ETipoPago.ExtractoTarjeta: return EBankLineType.ExtractoTarjeta;
            }

            return EBankLineType.Todos;
        }
        public static ETipoPago ToETipoPago(EBankLineType source)
        {
            switch (source)
            {
                case EBankLineType.PagoFactura: return ETipoPago.Factura;
                case EBankLineType.PagoNomina: return ETipoPago.Nomina;
                case EBankLineType.PagoGasto: return ETipoPago.Gasto;
                case EBankLineType.Prestamo: return ETipoPago.Prestamo;
                case EBankLineType.PagoPrestamo: return ETipoPago.Prestamo;
            }

            return ETipoPago.Todos;
        }
        public static ETipoPago ToETipoPago(ECategoriaGasto source)
        {
            switch (source)
            {
                case ECategoriaGasto.Todos: return ETipoPago.Todos;
                case ECategoriaGasto.Nomina: return ETipoPago.Nomina;
                case ECategoriaGasto.GeneralesExpediente:
                case ECategoriaGasto.Stock:
                case ECategoriaGasto.Expediente:
                case ECategoriaGasto.OtrosExpediente:
                    return ETipoPago.Factura;

                default: return ETipoPago.Gasto;
            }
        }
        public static ETipoTitular ToETipoTitular(ETipoAcreedor source)
        {
            switch (source)
            {
                case ETipoAcreedor.Acreedor: return ETipoTitular.Acreedor;
                case ETipoAcreedor.Proveedor: return ETipoTitular.Proveedor;
                case ETipoAcreedor.Naviera: return ETipoTitular.Naviera;
                case ETipoAcreedor.TransportistaOrigen: return ETipoTitular.TransportistaOrigen;
                case ETipoAcreedor.TransportistaDestino: return ETipoTitular.TransportistaDestino;
                case ETipoAcreedor.Despachante: return ETipoTitular.Despachante;
                case ETipoAcreedor.Instructor: return ETipoTitular.Instructor;
            }

            return ETipoTitular.Todos;
        }

        public static ETipoTransportista ToETipoTransportista(ETipoAcreedor source)
        {
            switch (source)
            {
                case ETipoAcreedor.TransportistaOrigen: return ETipoTransportista.Origen;
                case ETipoAcreedor.TransportistaDestino: return ETipoTransportista.Destino;
            }

            return ETipoTransportista.Todos;
        }
    }

    #endregion

    #region EnumText

    public class EnumText<T> : EnumTextBase<T>
    {
        public static ComboBoxList<T> GetList()
        {
            return GetList(Resources.Enums.ResourceManager);
        }

        public static ComboBoxList<T> GetList(bool empty_value)
        {
            return GetList(Resources.Enums.ResourceManager, empty_value);
        }

        public static ComboBoxList<T> GetList(bool empty_value, bool special_values)
        {
            return GetList(Resources.Enums.ResourceManager, empty_value, special_values);
        }

        public static ComboBoxList<T> GetList(bool empty_value, bool special_values, bool all_value)
        {
            return GetList(Resources.Enums.ResourceManager, empty_value, special_values, all_value);
        }

        public static ComboBoxList<T> GetList(T[] list)
        {
            return GetList(list, false);
        }

        public static ComboBoxList<T> GetList(T[] list, bool empty_value)
        {
            return GetList(Resources.Enums.ResourceManager, list, empty_value);
        }

        public static string GetLabel(object value)
        {
            return GetLabel(Resources.Enums.ResourceManager, value);
        }

        public static string GetPrintLabel(object value)
        {
            return GetPrintLabel(Resources.Enums.ResourceManager, value);
        }
    }

    #endregion
}
