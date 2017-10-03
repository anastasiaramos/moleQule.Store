using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Serie;
using moleQule.Store.Data;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class InputDeliveryBase
    {
        #region Attributes

        private InputDeliveryRecord _record = new InputDeliveryRecord();

        //NO ENLAZADOS
        internal string _usuario = string.Empty;
        internal string _almacen = string.Empty;
        internal string _id_almacen = string.Empty;
        internal bool _n_albaran_manual = false;
        internal string _numero_serie = string.Empty;
        internal string _nombre_serie = string.Empty;
        internal string _numero_acreedor = string.Empty;
        internal string _nombre_acreedor = string.Empty;
        internal string _numero_factura = string.Empty;
        internal bool _facturado;
        internal long _oid_factura;
        internal string _expediente = string.Empty;

        #endregion

        #region Properties

        public InputDeliveryRecord Record { get { return _record; } set { _record = value; } }

        //NO ENLAZADOS
        public virtual EEstado EEstado { get { return (EEstado)_record.Estado; } }
        public virtual string EstadoLabel { get { return Base.EnumText<EEstado>.GetLabel(EEstado); } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return (ETipoAcreedor)_record.TipoAcreedor; } set { _record.TipoAcreedor = (long)value; } }
        public virtual string TipoAcreedorLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetLabel(ETipoAcreedor); } }
        public virtual EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } }
        public virtual string MedioPagoLabel { get { return moleQule.Common.Structs.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
        public virtual EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } }
        public virtual string FormaPagoLabel { get { return moleQule.Common.Structs.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
        public virtual string Expediente { get { return _expediente; } set { _expediente = value; } }

        public virtual Decimal Subtotal { get { return _record.BaseImponible + _record.Descuento; } }
        public virtual string NumeroAlbaran { get { return _numero_acreedor + "/" + _record.Codigo; } }
        public virtual string NSerieSerie { get { return _numero_serie + " - " + _nombre_serie; } }
        internal virtual string IDAlmacenAlmacen { get { return (_record.OidAlmacen != 0) ? _id_almacen + " - " + _almacen : string.Empty; } }
        public string FileName
        {
            get
            {
                return "AlbaranRecibido_" /* + FileNameAcreedor  "_" 
						+ Fecha.ToString("dd-MM-yyyy") + "_" 
						+ ((NumeroSerie != null) ? NumeroSerie : string.Empty) + "_" 
						+ ((Codigo != null) ? Codigo : string.Empty + ".pdf")*/;
            }
        }
        public string FileNameAcreedor
        {
            get
            {
                if ((_nombre_acreedor == null) || (_nombre_acreedor == string.Empty)) return string.Empty;
                return (_nombre_acreedor.Length > 15) ? _nombre_acreedor.Replace(".", "").Substring(0, 15) : _nombre_acreedor.Replace(".", "");
            }
        }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            if (_expediente == string.Empty && ETipoAcreedor == ETipoAcreedor.Acreedor)
                _expediente = "ACREEDOR";

            _usuario = Format.DataReader.GetString(source, "USUARIO");
            _almacen = Format.DataReader.GetString(source, "ALMACEN");
            _id_almacen = Format.DataReader.GetString(source, "ID_ALMACEN");
            _oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
            _facturado = Format.DataReader.GetDecimal(source, "FACTURAS") > 0;
            _numero_serie = Format.DataReader.GetString(source, "N_SERIE");
            _nombre_serie = Format.DataReader.GetString(source, "SERIE");
            _numero_acreedor = Format.DataReader.GetString(source, "CODIGO_ACREEDOR");
            _nombre_acreedor = Format.DataReader.GetString(source, "ACREEDOR");
            _numero_factura = Format.DataReader.GetString(source, "N_FACTURA");

            _record.Estado = (_record.Estado == (long)EEstado.Contabilizado) ? _record.Estado : (_facturado ? (long)EEstado.Billed : (long)EEstado.Abierto);
        }
        internal void CopyValues(InputDelivery source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _usuario = source.Usuario;
            _almacen = source.Almacen;
            _id_almacen = source.IDAlmacen;
            _numero_acreedor = source.NumeroAcreedor;
            _nombre_acreedor = source.NombreAcreedor;
            _numero_serie = source.NumeroSerie;
            _nombre_serie = source.NombreSerie;
            _numero_factura = source.NumeroFactura;
        }
        internal void CopyValues(InputDeliveryInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _usuario = source.Usuario;
            _almacen = source.Almacen;
            _id_almacen = source.IDAlmacen;
            _numero_acreedor = source.NumeroAcreedor;
            _nombre_acreedor = source.NombreAcreedor;
            _numero_serie = source.NumeroSerie;
            _nombre_serie = source.NombreSerie;
            _numero_factura = source.NumeroFactura;
        }

        #endregion
    }

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class InputDelivery : BusinessBaseEx<InputDelivery>, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return _base.Record.Fecha; } set { Fecha = value; } }
        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((InputDelivery)source); }
        public void DifferentYearChecks() { }
        public virtual void DifferentYearTasks(IEntityBase oldItem) { }
        public void SameYearTasks(IEntityBase newItem) { }
        public virtual void IEntityBaseSave(object parent) { Save(); }

        #endregion

        #region Attributes

        public InputDeliveryBase _base = new InputDeliveryBase();

        private InputDeliveryLines _conceptos = InputDeliveryLines.NewChildList();

        #endregion

        #region Properties

        public InputDeliveryBase Base { get { return _base; } }

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
                //CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
                    _base.Record.Oid = value;
                    //PropertyHasChanged();
                }
            }
        }
        public virtual long OidUsuario
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidUsuario;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.OidUsuario.Equals(value))
                {
                    _base.Record.OidUsuario = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidSerie
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidSerie;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.OidSerie.Equals(value))
                {
                    _base.Record.OidSerie = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidAlmacen
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidAlmacen;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidAlmacen.Equals(value))
                {
                    _base.Record.OidAlmacen = value;
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
        public virtual long Serial
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Serial;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.Serial.Equals(value))
                {
                    _base.Record.Serial = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Codigo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Codigo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;


                if (!_base.Record.Codigo.Equals(value))
                {
                    _base.Record.Codigo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long Estado
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Estado;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Estado.Equals(value))
                {
                    _base.Record.Estado = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidAcreedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidAcreedor;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.OidAcreedor.Equals(value))
                {
                    _base.Record.OidAcreedor = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long TipoAcreedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TipoAcreedor;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.TipoAcreedor.Equals(value))
                {
                    _base.Record.TipoAcreedor = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CuentaBancaria
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;


                if (!_base.Record.CuentaBancaria.Equals(value))
                {
                    _base.Record.CuentaBancaria = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Nota
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Nota;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Nota.Equals(value))
                {
                    _base.Record.Nota = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long Ano
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Ano;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.Ano.Equals(value))
                {
                    _base.Record.Ano = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Observaciones
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Observaciones;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;


                if (!_base.Record.Observaciones.Equals(value))
                {
                    _base.Record.Observaciones = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime Fecha
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Fecha;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.Fecha.Equals(value))
                {
                    _base.Record.Fecha = value;
                    Ano = _base.Record.Fecha.Year;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaRegistro
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaRegistro;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaRegistro.Equals(value))
                {
                    _base.Record.FechaRegistro = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal BaseImponible
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.BaseImponible;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.BaseImponible.Equals(value))
                {
                    _base.Record.BaseImponible = value;
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
                    _base.Record.PDescuento = Decimal.Round(value, 2);
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
                    _base.Record.PIrpf = Decimal.Round(value, 2);
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Descuento
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Descuento;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Descuento.Equals(value))
                {
                    _base.Record.Descuento = Decimal.Round(value, 2);
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Impuestos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Igic;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Igic.Equals(value))
                {
                    _base.Record.Igic = value;
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
        public virtual long FormaPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FormaPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.FormaPago.Equals(value))
                {
                    _base.Record.FormaPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long DiasPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.DiasPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.DiasPago.Equals(value))
                {
                    _base.Record.DiasPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long MedioPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.MedioPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.MedioPago.Equals(value))
                {
                    _base.Record.MedioPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime Prevision
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PrevisionPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.PrevisionPago.Equals(value))
                {
                    _base.Record.PrevisionPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Contado
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Contado;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Contado.Equals(value))
                {
                    _base.Record.Contado = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Rectificativo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Rectificativo;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Rectificativo.Equals(value))
                {
                    _base.Record.Rectificativo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal IRPF
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Irpf;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Irpf.Equals(value))
                {
                    _base.Record.Irpf = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual InputDeliveryLines Conceptos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _conceptos;
            }

            set
            {
                _conceptos = value;
            }
        }

        //NO ENLAZADOS
        public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
        public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
        public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
        public virtual string IDAlmacen { get { return _base._id_almacen; } set { _base._id_almacen = value; } }
        public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
        public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
        public virtual string NumeroAlbaran { get { return _base.NumeroAlbaran; } }
        public virtual string NumeroSerie { get { return _base._numero_serie; } set { _base._numero_serie = value; } }
        public virtual string NombreSerie { get { return _base._nombre_serie; } set { _base._nombre_serie = value; } }
        public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
        public virtual string NumeroAcreedor { get { return _base._numero_acreedor; } set { _base._numero_acreedor = value; } }
        public virtual string NombreAcreedor { get { return _base._nombre_acreedor; } set { _base._nombre_acreedor = value; } }
        public virtual bool NAlbaranManual { get { return _base._n_albaran_manual; } set { _base._n_albaran_manual = value; } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { TipoAcreedor = (long)value; } }
        public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
        public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
        public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
        public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } set { FormaPago = (long)value; } }
        public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
        public virtual Decimal Subtotal { get { return _base.Subtotal; } }
        public virtual string NumeroFactura { get { return _base._numero_factura; } set { _base._numero_factura = value; } }
        public virtual long OidFactura { get { return _base._oid_factura; } }
        public virtual bool Facturado { get { return _base._facturado; } set { _base._facturado = value; } }
        public virtual string Expediente { get { return _base.Expediente; } set { _base.Expediente = value; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _conceptos.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _conceptos.IsDirty;
            }
        }

        #endregion

        #region Business Methods

        public virtual InputDelivery CloneAsNew()
        {
            InputDelivery clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.GetNewCode(ETipoAlbaranes.Todos);
            clon.SessionCode = InputDelivery.OpenSession();
            InputDelivery.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.Conceptos.MarkAsNew();

            return clon;
        }
        public static InputDelivery CloneAsNew(InputDeliveryInfo source)
        {
            InputDelivery clon = InputDelivery.New();
            clon._base.CopyValues(source);

            clon.GetNewCode(clon.Contado ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos);
            clon.OidUsuario = AppContext.User.Oid;
            clon.Usuario = AppContext.User.Name;
            clon.EEstado = EEstado.Abierto;
            clon.FechaRegistro = DateTime.Now;

            clon.MarkNew();

            if (source.ConceptoAlbaranes == null) source.LoadChilds(typeof(InputDeliveryLines), false);

            foreach (InputDeliveryLineInfo item in source.ConceptoAlbaranes)
                clon.Conceptos.NewItem(clon, item);

            return clon;
        }

        public virtual void GetNewCode(ETipoAlbaranes tipo)
        {
            // Obtenemos el último serial de servicio
            Serial = InputDeliverySerialInfo.GetNext(tipo, OidSerie, Fecha.Year, Rectificativo);

            if (Rectificativo)
                Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-R");
            else
                Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
        }

        protected virtual void SetNewCode(ETipoAlbaranes tipo)
        {
            try
            {
                InputDeliveryList list = InputDeliveryList.GetList(false,
                                                                        ETipoAcreedor.Todos,
                                                                        0,
                                                                        OidSerie,
                                                                        tipo,
                                                                        Rectificativo ? ETipoFactura.Rectificativa : ETipoFactura.Ordinaria,
                                                                        Convert.ToInt32(Ano));

                if (list.GetItemByProperty("Codigo", Codigo) != null)
                    throw new iQException("Número de Albarán de Proveedor duplicado");
                Serial = Convert.ToInt64(Codigo);
            }
            catch
            {
                throw new iQException("Número de Albarán de Proveedor incorrecto." + System.Environment.NewLine +
                                        "Debe tener el formato " + Resources.Defaults.FACTURA_CODE_FORMAT);
            }
        }

        public virtual void CopyFrom(IAcreedorInfo holder)
        {
            if (holder == null) return;

            OidAcreedor = holder.OidAcreedor;
            TipoAcreedor = holder.TipoAcreedor;
            NombreAcreedor = holder.Nombre;
            NumeroAcreedor = holder.Codigo;
            MedioPago = holder.MedioPago;
            FormaPago = holder.FormaPago;
            DiasPago = holder.DiasPago;
            Prevision = Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
            PDescuento = 0;
            PIRPF = holder.PIRPF;
        }
        public virtual void CopyFrom(InputInvoice source)
        {
            if (source == null) return;

            OidSerie = source.OidSerie;
            OidAcreedor = source.OidAcreedor;
            TipoAcreedor = source.TipoAcreedor;
            Observaciones = source.Observaciones;
            Fecha = source.Fecha;
            BaseImponible = source.BaseImponible;
            Total = source.Total;
            Descuento = source.Descuento;
            Impuestos = source.Impuestos;
            PDescuento = source.PDescuento;
            CuentaBancaria = source.CuentaBancaria;
            FormaPago = source.FormaPago;
            DiasPago = source.DiasPago;
            MedioPago = source.MedioPago;
            Prevision = source.Prevision;
            IRPF = source.IRPF;

            NumeroFactura = source.Codigo;
        }
        public virtual void CopyFrom(PedidoProveedorInfo source)
        {
            if (source == null) return;

            Observaciones = source.Observaciones;
        }
        public virtual void CopyFrom(IDocument source, IAcreedorInfo holder)
        {
            if (source == null) return;

            CopyFrom(holder);

            OidSerie = ModulePrincipal.GetDefaultSerieSetting();
            Observaciones = source.Observaciones;
            BaseImponible = source.BaseImponible;
            Total = source.Total;
            Descuento = source.Descuento;
            Impuestos = source.Impuestos;
            PDescuento = source.PDescuento;
            FormaPago = source.FormaPago;
            DiasPago = source.DiasPago;
            MedioPago = source.MedioPago;
            Prevision = source.Prevision;
        }

        public virtual void AddProductosAcreedor(IAcreedorInfo acreedor, SerieInfo serie)
        {
            InputDeliveryLine cap;
            ProductInfo producto;

            if (Conceptos.Count > 0) return;
            if (serie.SerieFamilias == null) serie.LoadChilds(typeof(SerieFamilia), true);

            foreach (ProductoProveedorInfo item in acreedor.Productos)
            {
                if (!item.Automatico) continue;
                if (serie.SerieFamilias.GetItemByFamilia(item.OidFamilia) == null) continue;

                producto = ProductInfo.Get(item.OidProducto, false, true);

                cap = InputDeliveryLine.NewChild(this);
                cap.PImpuestos = serie.PImpuesto;
                cap.Purchase(this, serie, acreedor, producto);
                cap.CantidadKilos = 1;
                cap.CantidadBultos = 1;
                this.Conceptos.NewItem(cap);
            }

            CalculateTotal();
        }

        public virtual void CalculateTotal()
        {
            BaseImponible = 0;
            Descuento = PDescuento != 0 ? 0 : Descuento;
            Impuestos = 0;
            Total = 0;
            IRPF = 0;

            foreach (InputDeliveryLine item in Conceptos)
            {
                item.CheckQuantity(this);

                if (!item.IsKitComponent)
                {
                    item.CalculateTotal();

                    BaseImponible += item.BaseImponible;
                    Impuestos += item.Impuestos;
                    IRPF += item.IRPF;
                }
            }

            Descuento = PDescuento != 0 ? BaseImponible * PDescuento / 100 : Descuento;
            BaseImponible -= Descuento;
            Total = BaseImponible - IRPF + Impuestos;
        }

        public virtual InputDeliveryLine Purchase(ProductInfo product, InputDeliveryLine line)
        {
            if (product == null) return null;

            if (product.ETipoFacturacion == ETipoFacturacion.Unitaria)
            {
                int cantidad = line.FacturacionBulto ? (int)line.CantidadBultos : (int)line.CantidadKilos;

                for (int i = 0; i < cantidad; i++)
                {
                    InputDeliveryLine cp = Conceptos.NewItem(this, line.GetInfo());
                    cp.CantidadKilos = 1;
                    cp.CantidadBultos = 1;
                }
            }
            else
                return Conceptos.NewItem(this, line.GetInfo());

            return null;
        }

        public virtual void Insert(PedidoProveedorInfo source)
        {
            InputDeliveryLine newitem;

            if (source.Lineas == null) source.LoadPendiente();

            foreach (LineaPedidoProveedorInfo item in source.Lineas)
            {
                if (item.Pendiente == 0) continue;

                newitem = Conceptos.NewItem(this);
                newitem.CopyFrom(item);
            }

            CalculateTotal();
        }

        public virtual void SetAlmacen(StoreInfo source)
        {
            Almacen almacen = Store.Almacen.Get(source.Oid, false, true);
            SetAlmacen(almacen);
        }
        public virtual void SetAlmacen(Almacen source)
        {
            foreach (InputDeliveryLine item in Conceptos)
            {
                item.OidAlmacen = source.Oid;
                item.Almacen = source.Nombre;
            }

            source.UpdateStocks(false);
        }

        public virtual void SetExpediente(Expedient source, bool throwStockException)
        {
            foreach (InputDeliveryLine item in Conceptos)
            {
                item.OidExpediente = source.Oid;
                item.Expediente = source.Codigo;
            }

            source.UpdateStocks(throwStockException);
        }
        public virtual void SetExpediente(long oidExpedient, bool throwStockException, int sessionCode)
        {
            Expedient source = Store.Expedient.Get(OidExpediente, false, true, sessionCode);
            SetExpediente(source, throwStockException);
        }

        public virtual void UpdateFecha(DateTime fecha)
        {
            Fecha = fecha;
            Prevision = moleQule.Common.EnumFunctions.GetPrevisionPago(EFormaPago, fecha, DiasPago);

            foreach (InputDeliveryLine item in Conceptos)
            {
                //Forzamos que cambie una propiedad para que se marque a Dirty el Concepto
                //y se actualice la fecha de la partida al guardarlo
                item.OidAlbaran = 0;
                item.OidAlbaran = Oid;
            }
        }

        public virtual void SetIRPF()
        {
            foreach (InputDeliveryLine ca in Conceptos)
                if (ca.PIRPF == 0)
                    ca.PIRPF = PIRPF;
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //Codigo
            if (Codigo == string.Empty)
            {
                e.Description = Resources.Messages.NO_ID_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "Codigo");
            }

            //OidAcreedor
            if (OidAcreedor == 0)
            {
                e.Description = Resources.Messages.NO_ACREEDOR_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "IDAcreedor");
            }

            //OidSerie
            if (OidSerie == 0)
            {
                e.Description = Resources.Messages.NO_SERIE_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "NSerie");
            }

            return true;
        }

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_RECIBIDA);
        }

        public static void IsPosibleDelete(long oid, ETipoAcreedor tipo)
        {
            QueryConditions conditions = new QueryConditions
            {
                InputDelivery = InputDelivery.New().GetInfo(false),
                Estado = EEstado.NoAnulado
            };
            conditions.InputDelivery.Oid = oid;

            InputDeliveryInfo item = InputDeliveryInfo.Get(oid, tipo, false);

            if (item.EEstado != EEstado.Abierto)
                throw new iQException(Resources.Messages.ALBARAN_NO_ANULADO);
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        protected InputDelivery() { }

        public virtual InputDeliveryInfo GetInfo(bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new InputDeliveryInfo(this, childs);
        }

        #endregion

        #region Root Factory Methods

        public static InputDelivery New() { return New(ETipoAlbaranes.Todos); }
        public static InputDelivery New(ETipoAlbaranes tipo)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputDelivery obj = DataPortal.Create<InputDelivery>(new CriteriaCs(-1));
            return obj;
        }

        public static InputDelivery Get(long oid, ETipoAcreedor tipo) { return Get(oid, tipo, true); }
        public static InputDelivery Get(long oid, ETipoAcreedor tipo, bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = InputDelivery.GetCriteria(InputDelivery.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = InputDelivery.SELECT(oid, tipo);

            InputDelivery.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<InputDelivery>(criteria);
        }
        public static InputDelivery Get(long oid, ETipoAcreedor tipo, bool childs, int sessionCode)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = InputDelivery.GetCriteria(sessionCode);
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = InputDelivery.SELECT(oid, tipo);

            return DataPortal.Fetch<InputDelivery>(criteria);
        }

        public static InputDelivery Get(CriteriaEx criteria)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputDelivery.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<InputDelivery>(criteria);
        }

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// (La función debe ser "estática")
        /// </summary>
        /// <param name="oid"></param>
        public static void Delete(long oid, ETipoAcreedor tipo)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            IsPosibleDelete(oid, tipo);

            DataPortal.Delete(new CriteriaCs(oid));
        }

        /// <summary>
        /// Elimina todas los Albarans
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = InputDelivery.OpenSession();
            ISession sess = InputDelivery.Session(sessCode);
            ITransaction trans = InputDelivery.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from InputDeliveryRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                InputDelivery.CloseSession(sessCode);
            }
        }

        /// <summary>
        /// Elimina todas los Albarans
        /// </summary>
        public static void DeleteFromList(List<InputDeliveryInfo> list)
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = InputDelivery.OpenSession();
            ISession sess = InputDelivery.Session(sessCode);
            ITransaction trans = InputDelivery.BeginTransaction(sessCode);

            string oidAlbaranes = "0";

            try
            {
                foreach (InputDeliveryInfo item in list)
                {
                    oidAlbaranes += "," + item.Oid.ToString();
                    //El stock debe borrarlo el expediente porque algunos se cambian
                    //de AlbaranProveedor y no podemos eliminarlos
                    //Expediente.DeleteStock(item);
                    //sess.Delete("from StockRecord st where st.OidAlbaran = " + item.Oid.ToString());
                    sess.Delete("from InputDeliveryLineRecord ca where ca.OidAlbaran = " + item.Oid.ToString());
                }

                sess.Delete("from InputDeliveryRecord ab where ab.Oid in (" + oidAlbaranes + ")");

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                InputDelivery.CloseSession(sessCode);
            }
        }

        public override InputDelivery Save()
        {
            // Por interfaz Root/Child
            if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                bool save_libro = false;
#if TRACE
                ControlerBase.AppControler.Timer.Start();
#endif
                base.Save();

                if (OidExpediente > 0)
                    SetExpediente(OidExpediente, true, SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor.Save()");
#endif
                _conceptos.Update(this);

                Stores almacenes = Cache.Instance.Get(typeof(Stores)) as Stores;
                if (almacenes != null)
                {
                    //En caso de borrado
                    if (almacenes.SessionCode == -1) almacenes.SessionCode = SessionCode;
                    Expedients expedientes = Cache.Instance.Get(typeof(Expedients)) as Expedients;

                    if (expedientes != null)
                    {
                        //En caso de borrado
                        if (expedientes.SessionCode == -1) expedientes.SessionCode = SessionCode;
                        foreach (Expedient item in expedientes)
                        {

                            if (item.Facturas == null)
                                item.LoadChilds(typeof(Expense), true, true);

                            InputDeliveryLineList conceptos = InputDeliveryLineList.GetByExpedienteStockList(item.Oid, false, GetInfo(true));

                            foreach (InputInvoiceInfo fac in item.Facturas)
                            {
                                foreach (Almacen almacen in almacenes)
                                {
                                    almacen.LoadPartidasByExpediente(item.Oid, true);
                                    item.UpdateTotalesProductos(almacen.Partidas, true);
                                    item.UpdateGasto(fac, conceptos, true);
                                }
                            }

                            if (item.ETipoExpediente == ETipoExpediente.Ganado) save_libro = true;
                        }

                        expedientes.SaveAsChild();
                    }
#if TRACE
					ControlerBase.AppControler.Timer.Record("Regularización de Totales en Expedientes");
#endif
                    almacenes.SaveAsChild();
#if TRACE
				ControlerBase.AppControler.Timer.Record("almacenes.Save()");
#endif
                }

                if (save_libro)
                {
                    LivestockBook libro = LivestockBook.Get(1, true, true, SessionCode);
                    if (libro != null) libro.SaveAsChild();
                }
#if TRACE
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor.Conceptos.Update()");
#endif
                if (!SharedTransaction) Transaction().Commit();
#if TRACE
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor.Commit()");
#endif
                return this;
            }
            catch (Exception ex)
            {
                if (!SharedTransaction) if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                Cache.Instance.Remove(typeof(Stores));
                Cache.Instance.Remove(typeof(LivestockBooks));
                //Se utiliza para cargar la ayuda estimada del producto
                Cache.Instance.Remove(typeof(ProductList));
                Cache.Instance.Remove(typeof(ProveedorList));

                if (!SharedTransaction)
                {
                    Cache.Instance.Remove(typeof(Expedients));
                    if (CloseSessions && (this.IsNew || Transaction().WasCommitted)) CloseSession();
                    else BeginTransaction();
                }
            }
        }

        public override InputDelivery SaveAsChild()
        {
            // Por interfaz Root/Child
            if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();

                bool save_libro = false;
#if TRACE
                ControlerBase.AppControler.Timer.Start();
#endif
                base.SaveAsChild();
#if TRACE
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor.Save()");
#endif
                _conceptos.Update(this);

                Stores almacenes = Cache.Instance.Get(typeof(Stores)) as Stores;
                if (almacenes != null)
                {
                    Expedients expedientes = Cache.Instance.Get(typeof(Expedients)) as Expedients;

                    if (expedientes != null)
                    {
                        foreach (Expedient item in expedientes)
                        {

                            if (item.Facturas == null)
                                item.LoadChilds(typeof(Expense), true, true);

                            InputDeliveryLineList conceptos = InputDeliveryLineList.GetByExpedienteStockList(item.Oid, false, GetInfo(true));

                            foreach (InputInvoiceInfo fac in item.Facturas)
                            {
                                foreach (Almacen almacen in almacenes)
                                {
                                    almacen.LoadPartidasByExpediente(item.Oid, true);
                                    item.UpdateTotalesProductos(almacen.Partidas, true);
                                    item.UpdateGasto(fac, conceptos, true);
                                }
                            }

                            if (item.ETipoExpediente == ETipoExpediente.Ganado) save_libro = true;
                        }

                        expedientes.SaveAsChild();
                    }
#if TRACE
					ControlerBase.AppControler.Timer.Record("Regularización de Totales en Expedientes");
#endif
                    almacenes.SaveAsChild();
#if TRACE
				ControlerBase.AppControler.Timer.Record("almacenes.Save()");
#endif
                }

                if (save_libro)
                {
                    LivestockBook libro = LivestockBook.Get(1, true, true, SessionCode);
                    if (libro != null) libro.SaveAsChild();
                }
#if TRACE
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor.Conceptos.Update()");
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor.Commit()");
#endif
                return this;
            }
            catch (Exception ex)
            {
                //if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                Cache.Instance.Remove(typeof(Stores));
                Cache.Instance.Remove(typeof(LivestockBooks));
                //Se utiliza para cargar la ayuda estimada del producto
                Cache.Instance.Remove(typeof(ProductList));
                Cache.Instance.Remove(typeof(ProveedorList));
            }
        }

        #endregion

        #region Child Factory Methods

        private InputDelivery(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            SessionCode = sessionCode;
            Fetch(source);
        }

        public static InputDelivery NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputDelivery obj = DataPortal.Create<InputDelivery>(new CriteriaCs(-1));
            obj.MarkAsChild();

            return obj;
        }

        internal static InputDelivery GetChild(int sessionCode, IDataReader source) { return GetChild(sessionCode, source, false); }
        internal static InputDelivery GetChild(int sessionCode, IDataReader reader, bool childs) { return new InputDelivery(sessionCode, reader, childs); }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            Oid = (long)(new Random().Next());
            GetNewCode(ETipoAlbaranes.Todos);
            OidSerie = Library.Store.ModulePrincipal.GetDefaultSerieSetting();
            OidAlmacen = Library.Store.ModulePrincipal.GetDefaultAlmacenSetting();
            EEstado = EEstado.Abierto;
            OidUsuario = AppContext.User.Oid;
            Usuario = AppContext.User.Name;
            FechaRegistro = DateTime.Now;
            Fecha = DateTime.Now;
            Contado = false;
            EMedioPago = EMedioPago.Efectivo;
            EFormaPago = EFormaPago.Contado;
        }

        private void Fetch(InputDelivery source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    if (nHMng.UseDirectSQL)
                    {
                        IDataReader reader;
                        string query;

                        InputDeliveryLine.DoLOCK(Session());
                        query = InputDeliveryLines.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _conceptos = InputDeliveryLines.GetChildList(SessionCode, reader);
                    }
                }
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
                _base.CopyValues(source);

                if (Childs)
                {
                    IDataReader reader;
                    string query;

                    InputDeliveryLine.DoLOCK(Session());
                    query = InputDeliveryLines.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _conceptos = InputDeliveryLines.GetChildList(SessionCode, reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Insert(InputDeliveries parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                if (!NAlbaranManual)
                {
                    if (Contado)
                        GetNewCode(ETipoAlbaranes.Agrupados);
                    else
                        GetNewCode(ETipoAlbaranes.Todos);
                }
                else
                    if (Contado) SetNewCode(ETipoAlbaranes.Agrupados);
                    else SetNewCode(ETipoAlbaranes.Todos);

                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                parent.Session().Save(Base.Record);

                _conceptos.Update(this);

            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Update(InputDeliveries parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                if (NAlbaranManual)
                    if (Contado) SetNewCode(ETipoAlbaranes.Agrupados);
                    else SetNewCode(ETipoAlbaranes.Todos);

                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                InputDeliveryRecord obj = Session().Get<InputDeliveryRecord>(Oid);
                obj.CopyValues(Base.Record);
                Session().Update(obj);

                _conceptos.Update(this);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void DeleteSelf(InputDeliveries parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<InputDeliveryRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion

        #region Root Data Access

        // called to retrieve data from the database
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                MarkOld();

                if (nHMng.UseDirectSQL)
                {
                    InputDelivery.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        InputDeliveryLine.DoLOCK(Session());
                        query = InputDeliveryLines.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _conceptos = InputDeliveryLines.GetChildList(SessionCode, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                CloseSession();
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Insert()
        {
            if (!NAlbaranManual)
            {
                if (Contado)
                    GetNewCode(ETipoAlbaranes.Agrupados);
                else
                    GetNewCode(ETipoAlbaranes.Todos);
            }
            else
                if (Contado) SetNewCode(ETipoAlbaranes.Agrupados);
                else SetNewCode(ETipoAlbaranes.Todos);

            try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
                Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
            if (IsDirty)
            {
                if (NAlbaranManual)
                    if (Contado) SetNewCode(ETipoAlbaranes.Agrupados);
                    else SetNewCode(ETipoAlbaranes.Todos);
#if TRACE
				ControlerBase.AppControler.Timer.Record("AlbaranProveedor_SetNewCode");
#endif
                InputDelivery obj = null;
                try
                {
                    InputDeliveryRecord record = Session().Get<InputDeliveryRecord>(this.Oid);
                    obj = InputDelivery.Get(this.Oid, (ETipoAcreedor)record.TipoAcreedor, false, SessionCode);

                    if (Common.EntityBase.UpdateByYear(obj, this, null))
                    {
                        obj.Save();
                        Transaction().Commit();
                        CloseSession();
                        NewTransaction();
                    }
                    else
                    {
                        record.CopyValues(this.Base.Record);
                        Session().Update(record);
                        //obj.CloseSession();
                    }

                    MarkOld();

                }
                catch (Exception ex)
                {
                    //if (obj != null) obj.CloseSession();
                    iQExceptionHandler.TreatException(ex);
                }
            }
        }

        // deferred deletion
        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new CriteriaCs(Oid));
        }

        // inmediate deletion
        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criterio)
        {
            try
            {
                //Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Lo hacemos así para ajustar el stock
                InputDelivery obj = InputDelivery.Get(criterio.Oid, ETipoAcreedor);
                obj.BeginEdit();
                obj.Conceptos.Clear();
                obj.ApplyEdit();
                obj.Save();
                obj.CloseSession();

                //Si no hay integridad referencial, aqui se deben borrar las listas hijo

                // Obtenemos el objeto
                obj._base.Record = (InputDeliveryRecord)(Session().Get<InputDeliveryRecord>(criterio.Oid));
                Session().Delete(Session().Get<InputDeliveryRecord>(obj.Oid));

                Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                Expedients expedientes = (Expedients)Cache.Instance.Get(typeof(Expedients));
                if (expedientes != null) expedientes.CloseSession();
                CloseSession();
            }
        }

        #endregion

        #region SQL

        public static ProviderBaseInfo.SelectLocalCaller local_caller = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE);
        public static ProviderBaseInfo.SelectLocalCaller local_caller_EXISTS = new ProviderBaseInfo.SelectLocalCaller(SELECT_BASE_EXISTS);

        public new static string SELECT(long oid) { return SELECT(oid, ETipoAcreedor.Todos); }
        public static string SELECT(long oid, ETipoAcreedor tipo) { return SELECT(oid, tipo, true); }

        internal static string FIELDS()
        {
            string query;

            query = @"
			SELECT AP.*
					,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENTE""
					,COALESCE(A.""NOMBRE"", '') AS ""ACREEDOR""
					,COALESCE(A.""CODIGO"", '') AS ""CODIGO_ACREEDOR""
					,S.""IDENTIFICADOR"" AS ""N_SERIE""
					,S.""NOMBRE"" AS ""SERIE""
					,AFP.""FACTURAS"" AS ""FACTURAS""
					,AFP.""OID_FACTURA"" AS ""OID_FACTURA""
					,FP.""CODIGO"" AS ""N_FACTURA""
					,COALESCE(US.""NAME"", '') AS ""USUARIO""
					,COALESCE(AL.""NOMBRE"", '') AS ""ALMACEN""
					,COALESCE(AL.""CODIGO"", '') AS ""ID_ALMACEN""";

            return query;
        }

        internal static string JOIN_ACREEDOR(ETipoAcreedor tipo)
        {
            string query = string.Empty;

            if (tipo != ETipoAcreedor.Todos)
                query = @"
				INNER JOIN " + ProviderBaseInfo.TABLE(tipo) + @" AS A ON AP.""OID_ACREEDOR"" = A.""OID""";
            else
                query = @"
				LEFT JOIN " + ProviderBaseInfo.TABLE(ETipoAcreedor.Proveedor) + @" AS A ON AP.""OID_ACREEDOR"" = A.""OID""";

            return query;
        }

        internal static string WHERE(Library.Store.QueryConditions conditions)
        {
            if (conditions == null) return string.Empty;

            string query = string.Empty;

            query += @"
			WHERE (AP.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "AP");
            query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "AP");

            if (conditions.Usuario != null)
                query += @"
					AND AP.""OID_USUARIO"" = " + conditions.Usuario.Oid;

            if (conditions.InputDelivery != null)
            {
                if (conditions.InputDelivery.Oid != 0)
                    query += @"
						AND AP.""OID"" = " + conditions.InputDelivery.Oid;
                if (conditions.InputDelivery.Rectificativo)
                    query += @"
						AND AP.""RECTIFICATIVO"" = " + conditions.InputDelivery.Rectificativo.ToString().ToUpper();
            }

            if ((conditions.Acreedor != null) && (conditions.Acreedor.OidAcreedor != 0))
                query += @"
					AND AP.""OID_ACREEDOR"" = " + conditions.Acreedor.OidAcreedor;

            if ((conditions.TipoAcreedor[0] != ETipoAcreedor.Todos))
                query += @"
					AND AP.""TIPO_ACREEDOR"" = " + (long)conditions.TipoAcreedor[0];

            if (conditions.FacturaRecibida != null)
                query += @"
					AND AFP.""OID_FACTURA"" = " + conditions.FacturaRecibida.Oid;

            if (conditions.Serie != null)
                query += @"
					AND AP.""OID_SERIE"" = " + conditions.Serie.Oid;

            if (conditions.TipoAlbaranes != ETipoAlbaranes.Todos)
            {
                switch (conditions.TipoAlbaranes)
                {
                    case ETipoAlbaranes.NoFacturados:
                        string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
                        query += @"
							AND AP.""OID"" NOT IN (SELECT TAF1.""OID_ALBARAN"" FROM " + idi + " AS TAF1)";
                        break;
                }
            }

            return query + " " + conditions.ExtraWhere;
        }

        internal static string SELECT_BASE(Library.Store.QueryConditions conditions, ETipoAcreedor tipo)
        {
            string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
            string ts = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string idi = nHManager.Instance.GetSQLTable(typeof(InputDeliveryInvoiceRecord));
            string idl = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));
            string ii = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string ca = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.InputDeliveryLineRecord));

            string query =
            FIELDS() + @"
			FROM " + id + @" AS AP
			INNER JOIN " + ts + @" AS S ON S.""OID"" = AP.""OID_SERIE""
			LEFT JOIN " + us + @" AS US ON US.""OID"" = AP.""OID_USUARIO""
			LEFT JOIN " + al + @" AS AL ON AL.""OID"" = AP.""OID_ALMACEN""
			LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = AP.""OID_EXPEDIENTE""
			LEFT JOIN (SELECT ""OID_ALBARAN"", ""OID_FACTURA"", COUNT(""OID_FACTURA"") AS ""FACTURAS""
						FROM " + idi + @" GROUP BY ""OID_ALBARAN"", ""OID_FACTURA"") 
				AS AFP ON AFP.""OID_ALBARAN"" = AP.""OID""
			LEFT JOIN " + ii + @" AS FP ON FP.""OID"" = AFP.""OID_FACTURA""" +
            JOIN_ACREEDOR(tipo);

            if (conditions.Producto != null)
                query += @"
				INNER JOIN " + idl + @" AS CA ON CA.""OID_PRODUCTO"" = " + conditions.Producto.Oid;

            if (conditions.Partida != null)
                query += @"
				INNER JOIN " + idl + @" AS CA2 ON CA2.""OID_BATCH"" = " + conditions.Partida.Oid;

            if (conditions.TipoAlbaranes == ETipoAlbaranes.Facturados)
                query += @"
				INNER JOIN " + idi + @" AF1 ON A.""OID"" = AF1.""OID_ALBARAN""";

            if (tipo != ETipoAcreedor.Todos)
                conditions.ExtraWhere += @"
					AND AP.""TIPO_ACREEDOR"" = " + (long)tipo;

            query += WHERE(conditions);

            conditions.ExtraWhere = string.Empty;

            return query;
        }

        internal static string SELECT_BASE_EXISTS(Library.Store.QueryConditions conditions, ETipoAcreedor tipo)
        {
            string query = string.Empty;

            query = SELECT_BASE(conditions, tipo);

            if (conditions.InputDelivery != null)
            {
                if (conditions.InputDelivery.Fecha != DateTime.MinValue)
                    query += @"
						AND AP.""FECHA"" BETWEEN '" + QueryConditions.GetFechaMinLabel(conditions.InputDelivery.Fecha) + @"'
						AND '" + QueryConditions.GetFechaMaxLabel(conditions.InputDelivery.Fecha) + @"'";

                if (conditions.InputDelivery.Total != 0)
                    query += @"
						AND AP.""TOTAL"" = " + conditions.InputDelivery.Total;
            }

            return query;
        }

        internal static string SELECT(long oid, ETipoAcreedor tipo, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { InputDelivery = InputDeliveryInfo.New(oid) };
            conditions.TipoAcreedor[0] = tipo;

            query = SELECT_BASE(conditions, tipo);

            //query += EntityBase.LOCK("AP", lockTable);
            if (lockTable) query += " FOR UPDATE OF AP NOWAIT";

            return query;
        }

        internal static string SELECT(Library.Store.QueryConditions conditions)
        {
            string query = string.Empty;

            switch (conditions.TipoAlbaranes)
            {
                case ETipoAlbaranes.Todos:
                case ETipoAlbaranes.Facturados:
                case ETipoAlbaranes.NoFacturados:
                    {
                        if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                            query = ProviderBaseInfo.SELECT_BUILDER(local_caller, conditions);
                        else
                            query = SELECT_BASE(conditions, conditions.Acreedor != null ? conditions.Acreedor.ETipoAcreedor : conditions.TipoAcreedor[0]);
                    }
                    break;
            }

            query += @"
			ORDER BY ""FECHA"", ""SERIE"", ""CODIGO""";

            return query;
        }

        internal static string SELECT_EXISTS(Library.Store.QueryConditions conditions)
        {
            string query = string.Empty;

            if (conditions.TipoAcreedor[0] == ETipoAcreedor.Todos)
                query = ProviderBaseInfo.SELECT_BUILDER(local_caller_EXISTS, conditions);
            else
                query = SELECT_BASE_EXISTS(conditions, conditions.TipoAcreedor[0]);

            query += @"
			ORDER BY ""FECHA"", ""SERIE"", ""CODIGO""";

            return query;
        }

        public static string UPDATE_TIPO(QueryConditions conditions)
        {
            string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
            string query = string.Empty;

            query = @"UPDATE " + id + @" AS AP SET ""TIPO_ACREEDOR"" = " + conditions.Acreedor.TipoAcreedor;

            query += WHERE(conditions);

            return query;
        }

        #endregion
    }
}