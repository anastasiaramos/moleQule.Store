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
using moleQule.CslaEx; 
using moleQule;
using moleQule.Common;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class InputInvoice : BusinessBaseEx<InputInvoice, InputInvoiceSQL>, IEntidadRegistro, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return _base.Record.Fecha; } set { Fecha = value; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((InputInvoice)source); }
        public void DifferentYearChecks() { }
        public virtual void DifferentYearTasks(IEntityBase oldItem) { }
        public void SameYearTasks(IEntityBase newItem)
        {
            /*Expediente expediente = null;

            if (OidExpediente != 0)
            {
                expediente = Store.Expediente.Get(OidExpediente, false, true, newItem.SessionCode);
                expediente.LoadConceptosAlbaranes(false);
                if (expediente.Partidas.Count == 0) expediente.LoadChilds(typeof(Partida), true, true);

                if (OidExpedienteOld == OidExpediente)
                    expediente.EditaGasto((FacturaRecibida)newItem, expediente.Conceptos, true);
                else
                    expediente.NuevoGasto((FacturaRecibida)newItem, expediente.Conceptos, true);
            }

            if ((OidExpedienteOld != 0) && (OidExpediente != OidExpedienteOld))
            {
                expediente = Store.Expediente.Get(OidExpedienteOld, false, true);
                expediente.LoadConceptosAlbaranes(false);
                if (expediente.Partidas.Count == 0) expediente.LoadChilds(typeof(Partida), true, true);
                expediente.RemoveGasto((FacturaRecibida)newItem, expediente.Conceptos, true);
            }*/
        }

        public virtual void IEntityBaseSave(object parent)
        {
            if (parent != null)
            {
                if (parent.GetType() == typeof(InputInvoices))
                    Insert((InputInvoices)parent);
                else
                    Save();
            }
            else
                Save();
        }

        #endregion

		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.FacturaRecibida; } }
		public virtual string DescripcionRegistro { get { return "FACTURA RECIBIDA Nº " + NFactura + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2") + " de " + Acreedor; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
            InputInvoiceRecord obj = Session().Get<InputInvoiceRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

		#region Attributes

		public InputInvoiceBase _base = new InputInvoiceBase();

        private InputInvoiceLines _concepto_facturas = InputInvoiceLines.NewChildList();
        private AlbaranFacturasProveedores _albaran_facturas = AlbaranFacturasProveedores.NewChildList();

		private long OidExpedienteOld;

        #endregion

        #region Properties

        public InputInvoiceBase Base { get { return _base; } }

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
		public virtual string NFactura
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.NFactura;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.NFactura.Equals(value))
				{
					_base.Record.NFactura = value;
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
		public virtual string Acreedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Acreedor;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Acreedor.Equals(value))
                {
                    _base.Record.Acreedor = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string VatNumber
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.VatNumber;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

				if (!_base.Record.VatNumber.Equals(value))
                {
                    _base.Record.VatNumber = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Direccion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Direccion;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Direccion.Equals(value))
                {
                    _base.Record.Direccion = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CodigoPostal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CodigoPostal;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.CodigoPostal.Equals(value))
                {
                    _base.Record.CodigoPostal = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Provincia
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Provincia;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Provincia.Equals(value))
                {
                    _base.Record.Provincia = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Municipio
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Municipio;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Municipio.Equals(value))
                {
                    _base.Record.Municipio = value;
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
        public virtual bool AlbaranContado
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Albaran;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.Albaran.Equals(value))
                {
                    _base.Record.Albaran = value;
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
                    _base.Record.PDescuento = value;
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
                    _base.Record.Descuento= value;
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
        public virtual Decimal Impuestos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Impuestos;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Impuestos.Equals(value))
                {
                    _base.Record.Impuestos = value;
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
                return _base.Record.Prevision;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Prevision.Equals(value))
                {
                    _base.Record.Prevision = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Rectificativa
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Rectificativa;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Rectificativa.Equals(value))
                {
                    _base.Record.Rectificativa = value;
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
		public virtual string Albaranes
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Albaranes;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Albaranes.Equals(value))
				{
					_base.Record.Albaranes = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string Expediente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base._expediente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base._expediente.Equals(value))
                {
                    _base._expediente = value;
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

        public virtual InputInvoiceLines Conceptos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _concepto_facturas;
            }

            set
            {
                _concepto_facturas = value;
            }

        }
        public virtual AlbaranFacturasProveedores AlbaranesFacturas
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _albaran_facturas;
            }

            set
            {
                _albaran_facturas = value;
            }

        }

        //NO ENLAZADOS	
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public virtual string Serie { get { return _base._serie; } set { _base._serie = value; } }
		public virtual string NSerie { get { return _base._n_serie; } set { _base._n_serie = value; PropertyHasChanged(); } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string IDAcreedor { get { return _base._n_acreedor; } set { _base._n_acreedor = value; PropertyHasChanged(); } }
		public virtual string NumeroAcreedor { get { return IDAcreedor; } set { IDAcreedor = value; } } /*DEPRECATED*/
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual decimal Pagado { get { return _base.Pagado; } set { _base.Pagado = value; } }
		public virtual decimal Pendiente { get { return _base.Pendiente; } set { _base.Pendiente = value; } }
        public virtual decimal PendienteAsignar { get { return _base.PendienteAsignar; } set { _base.PendienteAsignar = value; } }
		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { TipoAcreedor = (long)value; } }
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } set { FormaPago = (long)value; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual string IDMovimientoContable { get { return _base._id_mov_contable; } }
		public virtual decimal TotalExpediente { get { return _base._total_expediente; } set { _base._total_expediente = value; } }
        public virtual bool NecesitaExpediente { get { return _base.NecesitaExpediente; } }

		public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _concepto_facturas.IsValid && _albaran_facturas.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _concepto_facturas.IsDirty || _albaran_facturas.IsDirty;
            }
        }

		public virtual void SetAlbaranes()
		{
			Albaranes = string.Empty;

			foreach (AlbaranFacturaProveedor item in AlbaranesFacturas)
				Albaranes += item.CodigoAlbaran + "; ";
		}

        #endregion

        #region Business Methods

        public virtual InputInvoice CloneAsNew()
        {
            InputInvoice clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.Codigo = (0).ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
            clon.SessionCode = InputInvoice.OpenSession();
            InputInvoice.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.Conceptos.MarkAsNew();
            clon.AlbaranesFacturas.MarkAsNew();

            return clon;
        }

		public virtual void GetNewCode()
		{
            Serial = SerialFacturaProveedorInfo.GetNext(Fecha.Year);
			Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
		}

		public virtual void CopyFrom(IAcreedorInfo source)
		{
			if (source == null) return;

			OidAcreedor = source.OidAcreedor;
			TipoAcreedor = source.TipoAcreedor;
			Acreedor = source.Nombre;
			IDAcreedor = source.Codigo;

			VatNumber = source.ID;
			MedioPago = source.MedioPago;
			FormaPago = source.FormaPago;
			DiasPago = source.DiasPago;
			Prevision = Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
			PDescuento = 0;
			//PIRPF = source.PIRPF;
		}
		public virtual void CopyFrom(InputDeliveryInfo source)
		{
			if (source == null) return;

			OidSerie = source.OidSerie;
			OidAcreedor = source.OidAcreedor;
			Acreedor = source.NombreAcreedor;
			TipoAcreedor = source.TipoAcreedor;
			Observaciones = source.Observaciones;
			BaseImponible = source.BaseImponible;
			Total = source.Total;
			PIRPF = source.PIRPF;
			Descuento = source.Descuento;
			Impuestos = source.Impuestos;
			PDescuento = source.PDescuento;
			Nota = source.Nota;
			Ano = source.Fecha.Year;
			CuentaBancaria = source.CuentaBancaria;
			FormaPago = source.FormaPago;
			DiasPago = source.DiasPago;
			MedioPago = source.MedioPago;
			Prevision = source.Prevision;
            IRPF = source.IRPF;

			NumeroAcreedor = source.NumeroAcreedor;
			NSerie = source.NumeroSerie;
		}
		public virtual void CopyFrom(InputInvoiceInfo source)
		{
			if (source == null) return;

			_base.CopyValues(source);

			OidExpedienteOld = OidExpediente;

			_concepto_facturas = InputInvoiceLines.GetChildList(source.Conceptos);
		}

		public virtual void CalculateTotal()
		{
            BaseImponible = 0;
            Descuento = PDescuento != 0 ? 0 : Descuento;
			Impuestos = 0;
			Total = 0;
            IRPF = 0;

			foreach (InputInvoiceLine item in Conceptos)
			{
				if (!item.IsKitComponent)
				{
					item.CalculateTotal();

					BaseImponible += item.BaseImponible;
				}
			}

			Descuento = PDescuento != 0 ? BaseImponible * PDescuento / 100 : Descuento;
			
			//Esta funcion actualiza la propiedad Impuestos 
			_base.GetImpuestos(Conceptos);
            _base.GetIRPF(Conceptos);

			//Para marcar la propiedad como Dirty
            //Impuestos = _base._impuestos_list.TotalizeImpuestos();
            _base._impuestos_list.TotalizeImpuestos();
            Impuestos = Impuestos;

			BaseImponible -= Descuento;
			Total = BaseImponible - IRPF + Impuestos;
		}

		public static InputInvoice ChangeEstado(long oid, ETipoAcreedor tipo, EEstado estado)
		{
			if (!CanChangeState())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			InputInvoice item = null;

			try
			{
				item = InputInvoice.Get(oid, tipo, true);

				if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) && (!AutorizationRulesControler.CanEditObject(moleQule.Invoice.Structs.Resources.SecureItems.CUENTA_CONTABLE)))
					throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

				Common.EntityBase.CheckChangeState(item.EEstado, estado);

				item.BeginEdit();
				item.EEstado = estado;
				item.ApplyEdit();
				item.Save();
			}
			finally
			{
				if (item != null) item.CloseSession();
			}

			return item;
		}

		/// <summary>
		/// Elimina los conceptos de factura asociados a un albarán
		/// </summary>
		/// <param name="source"></param>
		public virtual void Extract(InputDeliveryInfo source)
		{
			AlbaranesFacturas.Remove(this, source);

			foreach (InputDeliveryLineInfo item in source.ConceptoAlbaranes)
				this.Conceptos.Remove(item);

			CalculateTotal();
		}

		/// <summary>
		/// Crea los conceptos de factura asociados a un albarán
		/// </summary>
		/// <param name="source"></param>
		public virtual void Insert(InputDeliveryInfo source)
		{
			InputInvoiceLine newitem;

			AlbaranesFacturas.NewItem(this, source);

			foreach (InputDeliveryLineInfo item in source.ConceptoAlbaranes)
			{
				newitem = Conceptos.NewItem(this);
				newitem.CopyFrom(item);
			}

			CalculateTotal();
		}

		public virtual void SetExpediente(ExpedientInfo source)
        {
            foreach (InputInvoiceLine item in Conceptos)
            {
                if (item.OidExpediente != 0)
                    continue;

                item.OidExpediente = source.Oid;
                item.Expediente = source.Codigo;
            }
		}
		public virtual void SetExpediente(Expedient source)
		{
            SetExpediente(source.GetInfo());
		}

        public virtual void ResetExpediente()
        {
            foreach (InputInvoiceLine item in Conceptos)
            {
                item.OidExpediente = 0;
                item.Expediente = string.Empty;
            }
        }

        public virtual void SetIRPF()
        {
            foreach (InputInvoiceLine cf in Conceptos)
                if (cf.PIRPF == 0)
                    cf.PIRPF = PIRPF;
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

			//NFactura
			if (NFactura == string.Empty)
			{
				e.Description = Resources.Messages.NO_NUMEROFACTURA_SELECTED;
				throw new iQValidationException(e.Description, string.Empty, "NFactura");
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

		public static bool CanChangeState()
		{
			return AutorizationRulesControler.CanGetObject(moleQule.Common.Resources.SecureItems.ESTADO);
		}
		
		public static void IsPosibleDelete(long oid, ETipoAcreedor tipo)
		{
			QueryConditions conditions = new QueryConditions
			{
				FacturaRecibida = InputInvoice.New().GetInfo(false),
				Estado = EEstado.NoAnulado,
				PaymentType = ETipoPago.Factura
			};
			conditions.FacturaRecibida.Oid = oid;

			InputInvoiceInfo item = InputInvoiceInfo.Get(oid, tipo, false);

			if (item.EEstado != EEstado.Abierto)
				throw new iQException(Resources.Messages.FACTURA_NO_ANULADA);

			ExpenseList gastos = ExpenseList.GetAsociadoList(conditions, false);

			if (gastos.Count > 0)
				throw new iQException(Resources.Messages.EXPEDIENTES_ASOCIADOS);

			TransactionPaymentList pagos = TransactionPaymentList.GetList(conditions, false);

			if (pagos.Count > 0)
				throw new iQException(Resources.Messages.PAGOS_ASOCIADOS);
		}

        #endregion

		#region Child Factory Methods

		private InputInvoice(int sessionCode, IDataReader source, bool childs)
		{
			MarkAsChild();
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(source);
		}

		public static InputInvoice NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			InputInvoice obj = DataPortal.Create<InputInvoice>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}

		internal static InputInvoice GetChild(int sessionCode, IDataReader source, bool retrieve_childs)
		{
			return new InputInvoice(sessionCode, source, retrieve_childs);
		}

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        protected InputInvoice() {}

        public virtual InputInvoiceInfo GetInfo(bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new InputInvoiceInfo(this, childs);
        }

		public virtual void LoadChilds(Type type, bool get_childs)
		{
			if (type.Equals(typeof(AlbaranFacturaProveedor)))
			{
				_albaran_facturas = AlbaranFacturasProveedores.GetChildList(this, get_childs);
			}
            else if (type.Equals(typeof(InputInvoiceLine)))
            {
                _concepto_facturas = InputInvoiceLines.GetChildList(this, get_childs);
            }
		}

        #endregion

        #region Root Factory Methods

        public static InputInvoice New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputInvoice obj = DataPortal.Create<InputInvoice>(new CriteriaCs(-1));
            return obj;
        }

		public static InputInvoice New(InputInvoiceInfo source)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			InputInvoice obj = InputInvoice.New();
			obj.CopyFrom(source);
			return obj;
		}

		public static InputInvoice Get(long oid, bool childs) { return Get(oid, ETipoAcreedor.Todos, childs); }
        public static InputInvoice Get(long oid, ETipoAcreedor tipo, bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = InputInvoice.GetCriteria(InputInvoice.OpenSession());
			criteria.Childs = childs;
            
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = InputInvoiceSQL.SELECT(oid, tipo);

            InputInvoice.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<InputInvoice>(criteria);
        }

        public static InputInvoice Get(CriteriaEx criteria)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            InputInvoice.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<InputInvoice>(criteria);
        }

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// (La funci�n debe ser "est�tica")
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
        /// Elimina todas los FacturaProveedors
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = InputInvoice.OpenSession();
            ISession sess = InputInvoice.Session(sessCode);
            ITransaction trans = InputInvoice.BeginTransaction(sessCode);

            try
            {
				sess.Delete("from InputInvoiceRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                InputInvoice.CloseSession(sessCode);
            }
        }

        public override InputInvoice Save()
        {
			// Por la posible doble interfaz Root/Child
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
				base.Save();

                _concepto_facturas.Update(this);
                _albaran_facturas.Update(this);

                UpdateAlbaranes();
                UpdateExpedientes(true);

				Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				Cache.Instance.Remove(typeof(Expedients));

				if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
        }

        private void UpdateExpedientes(bool throwStockException)
        {
            Expedients expedientes = Cache.Instance.Get(typeof(Expedients)) as Expedients;

            if (expedientes != null)
            {
                foreach (Expedient expediente in expedientes)
                {
                    if (expediente.Conceptos == null)
                    {
                        expediente.LoadConceptosStockAlbaranes(false);
                        if (expediente.Partidas.Count == 0) expediente.LoadChilds(typeof(Batch), true, throwStockException);
                    }

                    expediente.UpdateGasto(this, expediente.Conceptos, throwStockException);
                }

                expedientes.SaveAsChild();
            }
        }

        private void UpdateAlbaranes()
        {
            foreach (AlbaranFacturaProveedor item in AlbaranesFacturas)
            {
                InputDelivery albaran = InputDelivery.Get(item.OidAlbaran, false, SessionCode);
                albaran.OidAcreedor = OidAcreedor;
                albaran.TipoAcreedor = TipoAcreedor;
                albaran.Save();
            }
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            Oid = (long)(new Random().Next());
            FechaRegistro = DateTime.Now;
            Fecha = DateTime.Now;
			OidSerie = Library.Store.ModulePrincipal.GetDefaultSerieSetting();
			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;
            EMedioPago = EMedioPago.Efectivo;
            EFormaPago = EFormaPago.Contado;
			EEstado = EEstado.Abierto;
			GetNewCode();
        }

		private void Fetch(InputInvoice source)
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

						InputInvoiceLine.DoLOCK(Session());
						query = InputInvoiceLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_concepto_facturas = InputInvoiceLines.GetChildList(SessionCode, reader);

						AlbaranFacturaProveedor.DoLOCK(Session());
						query = AlbaranFacturasProveedores.SELECT_BY_FACTURA(this.Oid);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturasProveedores.GetChildList(reader);
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
					if (nHMng.UseDirectSQL)
					{
						IDataReader reader;
						string query;

						InputInvoiceLine.DoLOCK(Session());
						query = InputInvoiceLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_concepto_facturas = InputInvoiceLines.GetChildList(SessionCode, reader);

						AlbaranFacturaProveedor.DoLOCK(Session());
						query = AlbaranFacturasProveedores.SELECT_BY_FACTURA(this.Oid);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturasProveedores.GetChildList(reader);
					}
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Insert(InputInvoices parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            SessionCode = parent.SessionCode;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            _concepto_facturas.Update(this);
            _albaran_facturas.Update(this);

            UpdateExpedientes(true);

			MarkOld();
		}

		internal void Update(InputInvoices parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
            InputInvoiceRecord obj = Session().Get<InputInvoiceRecord>(Oid);
            obj.CopyValues(this.Base.Record);
            //Common.EntityBase.UpdateByYear(obj, this, parent);
			Session().Update(obj);

            InputInvoice obj_aux = InputInvoice.Get(Oid, true, SessionCode);

            obj_aux.Conceptos = this.Conceptos.Clone();

            obj_aux.Conceptos.Update(this);
            obj_aux.AlbaranesFacturas.Update(this);
            
			MarkOld();
		}

		internal void DeleteSelf(InputInvoices parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<InputInvoiceRecord>(Oid));
            UpdateExpedientes(true);

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
                    InputInvoice.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        InputInvoiceLine.DoLOCK(Session());
                        query = InputInvoiceLines.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _concepto_facturas = InputInvoiceLines.GetChildList(SessionCode, reader);

                        AlbaranFacturaProveedor.DoLOCK(Session());
                        query = AlbaranFacturasProveedores.SELECT_BY_FACTURA(this.Oid);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _albaran_facturas = AlbaranFacturasProveedores.GetChildList(reader);

						SetAlbaranes();
                    }
                }
                else
                {
                    throw new iQImplementationException("FacturaRecibida::DataPortal_Fetch()");                       
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Insert()
        {               
            try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }

                GetNewCode();
                Session().Save(Base.Record);

				if (OidExpediente != 0)
				{
					Expedient expediente = Store.Expedient.Get(OidExpediente, false, true, SessionCode);
					if (expediente.Partidas.Count == 0) expediente.LoadChilds(typeof(Batch), true, true);
                    if (expediente.Conceptos == null || expediente.Conceptos.Count == 0)
                        expediente.LoadChilds(typeof(InputDeliveryLine), false, true);
					//expediente.NuevoGasto(this, expediente.Conceptos, true);
				}
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
            if (!IsDirty) return;

			try
			{
				InputInvoiceRecord record = Session().Get<InputInvoiceRecord>(Oid);
                InputInvoice obj = InputInvoice.Get(Oid, true, SessionCode);

                if (Common.EntityBase.UpdateByYear(obj, this, null))
                {
                    //La factura no se elimina, sólo se marca como anulada para que quede constancia
                    //pero se eliminan los registros de Albaran_Factura porque si no aparecerían duplicados
                    if (obj.EEstado == EEstado.Anulado)
                    {
                        obj.AlbaranesFacturas = AlbaranesFacturas;
                        obj.AlbaranesFacturas.RemoveAll();
                    }
                    obj.Save();

                    AlbaranesFacturas.Update(this);
                    Conceptos.Update(this);

                    Transaction().Commit();
                    CloseSession();
                    NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    Session().Update(record);
                    AlbaranesFacturas.Update(this);
                    Conceptos.Update(this);
                }

				MarkOld();
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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

                //Si no hay integridad referencial, aqui se deben borrar las listas hijo
                CriteriaEx criteria = GetCriteria();
                criteria.AddOidSearch(criterio.Oid);

                // Obtenemos el objeto
                InputInvoiceRecord obj = (InputInvoiceRecord)(criteria.UniqueResult<InputInvoiceRecord>());
                Session().Delete(Session().Get<InputInvoiceRecord>(obj.Oid));

                InputInvoiceLineList conceptos = InputInvoiceLineList.GetByFacturaList(obj.Oid, false);
                Dictionary<long, long> expedientes = new Dictionary<long, long>();

				foreach(InputInvoiceLineInfo cf in conceptos)
				{
                    if (cf.OidExpediente == 0) continue;

                    if (expedientes.ContainsKey(cf.OidExpediente)) continue;

                    expedientes.Add(cf.OidExpediente, cf.OidExpediente);

                    InputInvoice obj_aux = InputInvoice.Get(obj.Oid, true, SessionCode);
					Expedient expediente = Store.Expedient.Get(cf.OidExpediente, false, true, SessionCode);
                    expediente.LoadChilds(typeof(InputDeliveryLine), false, true);
                    expediente.RemoveGasto(obj_aux, expediente.Conceptos, true);
                    if (expediente.Partidas.Count == 0) expediente.LoadChilds(typeof(Batch), true, true);
                    expediente.SaveAsChild();
                }
                                
                Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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

        public new static string SELECT(CriteriaEx criteria, bool lockTable)
        {
            return InputInvoiceSQL.SELECT(criteria, lockTable);
        }

        #endregion
    }
}