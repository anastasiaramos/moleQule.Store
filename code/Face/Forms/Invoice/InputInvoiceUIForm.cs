using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class InputInvoiceUIForm : InputInvoiceForm, IBackGroundLauncher
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		protected InputInvoice _entity = null;
		protected ExpedientInfo _expediente = null;
		protected AlbaranFacturasProveedores _albaranes_factura = null;
		protected List<InputDeliveryInfo> _albaranes = new List<InputDeliveryInfo>();
		protected List<InputDeliveryInfo> _results = new List<InputDeliveryInfo>();

		public override InputInvoice Entity { get { return _entity; } set { _entity = value; } }
        public override InputInvoiceInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        public ProviderBaseInfo Acreedor { get { return (Datos_Emisor.Current != null) ? Datos_Emisor.Current as ProviderBaseInfo : null; } }

        #endregion

        #region Factory Methods

        public InputInvoiceUIForm()
            : this(-1, ETipoAcreedor.Todos, null) {}

		public InputInvoiceUIForm(Form parent)
            : this(-1, ETipoAcreedor.Todos, parent) {}

        public InputInvoiceUIForm(long oid, ETipoAcreedor tipo, Form parent)
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
        }

		public InputInvoiceUIForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
		}
    
		protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
				InputInvoice temp = _entity.Clone();
				temp.ApplyEdit();
				_entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout

		public override void FormatControls()
		{
			//IDE Compatibility
			if (AppContext.User == null) return;

			FechaRegistro_DTP.Enabled = AppContext.User.IsAdmin;

            base.FormatControls();

            Serie_BT.Enabled = (_entity.Conceptos.Count == 0);
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
			_albaranes_factura = _entity.AlbaranesFacturas.Clone();
			Datos_Lineas.DataSource = _entity.Conceptos;
            PgMng.Grow();

			DiasPago_NTB.Text = _entity.DiasPago.ToString();
			Fecha_DTP.Value = _entity.Fecha;
			FechaRegistro_DTP.Value = _entity.FechaRegistro;
			PgMng.Grow();

			base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			if (_entity.OidAcreedor != 0) SetEmisor(ProviderBaseInfo.Get(_entity.OidAcreedor, _entity.ETipoAcreedor, false));
			PgMng.Grow();

			if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

            base.RefreshSecondaryData();
		}

        #endregion

        #region Validation & Format

        #endregion

		#region Business Methods

		protected bool AddAlbaran()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return false;
			}

			InputDeliveryList list = null;

			list = InputDeliveryList.GetNoFacturados(false, _entity);

			//Quitamos de la lista los ya añadidos
			List<InputDeliveryInfo> lista = new List<InputDeliveryInfo>();
			foreach (InputDeliveryInfo item in list)
				if (_entity.AlbaranesFacturas.GetItemByAlbaran(item.Oid) == null)
					lista.Add(item);

			InputDeliveryList lista_completa = InputDeliveryList.GetListByAcreedor(false, _entity.ETipoAcreedor, _entity.OidAcreedor);
			//Añadimos a lista los eliminados
			foreach (AlbaranFacturaProveedor item in _albaranes_factura)
				if (_entity.AlbaranesFacturas.GetItemByAlbaran(item.OidAlbaran) == null)
					lista.Add(lista_completa.GetItem(item.OidAlbaran));

			InputDeliverySelectForm form = new InputDeliverySelectForm(this, InputDeliveryList.GetList(lista));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_results = form.Selected as List<InputDeliveryInfo>;

				if (_entity.Rectificativa && (_results.Count > 1))
				{
					PgMng.ShowInfoException("No es posible asignar varios albaranes a una factura rectificativa.");
					return false;
				}

				foreach (InputDeliveryInfo item in _results)
				{
					if (item.OidAcreedor != _results[0].OidAcreedor)
					{
						PgMng.ShowInfoException("No es posible asignar albaranes de acreedores distintos a una misma Factura.");
						return false;
					}
				}

                /*foreach (InputDeliveryInfo item in _results)
                {
                    if (item.PIRPF != _results[0].PIRPF)
                    {
                        PgMng.ShowInfoException(Resources.Messages.ALBARANES_CON_IRPF_DISTINTO);
                        return false;
                    }
                }*/

				_back_job = BackJob.AddAlbaran;
				//PgMng.StartBackJob(this);

				DoAddAlbaran(null);

				if (Result == BGResult.OK)
				{
					Serie_BT.Enabled = false;
					Datos.ResetBindings(false);
				}
			}

			return false;
		}

		protected void SetCuenta(IAcreedorInfo emisor)
		{
			if (emisor != null && _company != null)
			{
				if (_entity.EMedioPago == EMedioPago.Giro)
				{
					if (emisor.CuentaAsociada != string.Empty)
						_entity.CuentaBancaria = emisor.CuentaAsociada;
					else
						_entity.CuentaBancaria = _company.CuentaBancaria;
				}
				else
					_entity.CuentaBancaria = emisor.CuentaBancaria;
			}
		}

		protected void SetEmisor(IAcreedorInfo emisor)
        {
        //    if (_entity.AlbaranesFacturas.Count > 0)
        //    {
        //        InputDeliveryInfo albaran = InputDeliveryInfo.Get(_entity.AlbaranesFacturas[0].OidAlbaran, emisor.ETipoAcreedor, false);
        //        if (albaran == null || albaran.OidAcreedor != emisor.OidAcreedor)
        //        {
        //            PgMng.ShowInfoException(Resources.Messages.FACTURA_CON_ALBARANES);
        //            return;
        //        }
        //    }

            Datos_Emisor.DataSource = ProviderBase.New(emisor);

		    _entity.CopyFrom(emisor);

			SetCuenta(emisor);

			Datos_Emisor.ResetBindings(false);
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			_expediente = source;

			if (_expediente != null)
			{
				_entity.Expediente = _expediente.Codigo;
				_entity.OidExpediente = _expediente.Oid;
				Expediente_TB.Text = source.Codigo;

				//AddCacheItem(source);*/
			}
		}

		protected void SetSerie(SerieInfo serie, bool new_code)
		{
			if (serie == null) return;

			_entity.OidSerie = serie.Oid;
			_entity.NSerie = serie.Identificador;
			_entity.Serie = serie.Nombre;
			Serie_TB.Text = _entity.NSerieSerie;

			if (new_code) _entity.GetNewCode();
		}

		#endregion

		#region IBackGroundLauncher

		new enum BackJob { GetFormData, AddAlbaran }
		new BackJob _back_job = BackJob.GetFormData;

		/// <summary>
		/// La llama el backgroundworker para ejecutar codigo en segundo plano
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public new void BackGroundJob(BackgroundWorker bk)
		{
			switch (_back_job)
			{
				case BackJob.AddAlbaran:
					DoAddAlbaran(bk);
					break;

				default:
					base.BackGroundJob(bk);
					return;
			}

			if (Result == BGResult.OK)
			{
				Serie_BT.Enabled = false;
			}
		}

		protected void DoAddAlbaran(BackgroundWorker bk)
		{
			Datos.RaiseListChangedEvents = false;
			Datos_Lineas.RaiseListChangedEvents = false;

			try
            {
				PgMng.Reset(_results.Count + 1, 1, Resources.Messages.IMPORTANDO_ALBARANES, this);

				//Asignamos el titular
                if (_entity.OidAcreedor == 0 || _entity.AlbaranesFacturas.Count == 0)
                {
                    _entity.CopyFrom(_results[0]);
                    SetEmisor(ProviderBaseInfo.Get(_entity.OidAcreedor, _entity.ETipoAcreedor));
                }
                //else
                //{
                //    foreach (InputDeliveryInfo item in _results)
                //    {
                //        if (item.PIRPF != _entity.PIRPF)
                //        {
                //            PgMng.ShowInfoException(Resources.Messages.FACTURA_CON_IRPF_DISTINTO);
                //            return;
                //        }
                //    }
                //}

				foreach (InputDeliveryInfo item in _results)
				{
					item.LoadChilds(typeof(InputDeliveryLine), true);
					_entity.Insert(item);
					_albaranes.Add(item);
					PgMng.Grow(string.Empty, "Insertar el Albarán");
				}

				Result = BGResult.OK;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				PgMng.FillUp();

				Datos.RaiseListChangedEvents = true;
				Datos_Lineas.RaiseListChangedEvents = true;
#if TRACE
                PgMng.ShowCronos();
#endif
			}
		}

		#endregion

		#region Actions

        protected override void SaveAction()
        {
            InputInvoiceList list = (InputInvoiceList)Cache.Instance.Get(typeof(InputInvoiceList));

            if (list != null)
            {
                InputInvoiceInfo item = list.GetItemByNFactura(_entity.NFactura, _entity.Fecha.Year, _entity.OidAcreedor, _entity.ETipoAcreedor);
                if (item != null && item.Oid != _entity.Oid)
                {
                    if (ProgressInfoMng.ShowQuestion(String.Format(Library.Store.Resources.Messages.FACTURA_RECIBIDA_DUPLICADA, _entity.Acreedor)) == DialogResult.No)
                    {
                        _action_result = DialogResult.Ignore;
                        return;
                    }
                }
            } 
            
            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
            {
				FacturaRecibida = _entity.GetInfo(false),
                TipoAcreedor = new ETipoAcreedor[1] { _entity.ETipoAcreedor },
                Estado = moleQule.Base.EEstado.NoAnulado
            };

            conditions.FacturaRecibida.Oid = 0;

            InputInvoiceInfo prev_invoice = InputInvoiceInfo.Exists(conditions, false);

            if (prev_invoice.Oid != 0 && prev_invoice.Oid != _entity.Oid && prev_invoice.Total == _entity.Total)
                if (DialogResult.No == ProgressInfoMng.ShowQuestion("Existe una factura de este proveedor con la misma fecha, número e importe. ¿Desea continuar?"))
                {
                    _action_result = DialogResult.Cancel;
                    return;
                }

            if (_expediente != null)
                _entity.SetExpediente(_expediente);

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void EliminarAlbaranAction()
		{
			if (Entity.AlbaranesFacturas.Count == 0) return;

			InputDeliverySelectForm form = new InputDeliverySelectForm(this, InputDeliveryList.GetList(_albaranes));
	
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				List<InputDeliveryInfo> results = form.Selected as List<InputDeliveryInfo>;

				foreach (InputDeliveryInfo item in results)
				{
					_entity.Extract(item);
					_albaranes.Remove(item);
				}
			}

			if (_entity.AlbaranesFacturas.Count == 0)
			{
				Serie_BT.Enabled = true;
			}

			Datos.ResetBindings(false);
			RefreshLineas();
		}

		protected override void NuevoAlbaranAction()
		{
			bool res = false;

			res = AddAlbaran();

			if (Result == BGResult.OK)
			{
				Datos.ResetBindings(false);
				RefreshLineas();
			}
		}

		protected virtual void SelectCuentaAction()
		{
			BankAccountSelectForm form = new BankAccountSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				Cuenta_TB.Text = cuenta.Valor;
				_entity.CuentaBancaria = cuenta.Valor;
			}
		}

		protected virtual void SelectEmisorAction()
        {
			ProviderSelectForm form = new ProviderSelectForm(this, moleQule.Base.EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProviderBaseInfo proveedor = form.Selected as ProviderBaseInfo;

				SetEmisor(proveedor);

				CleanError(CodigoE_TB);
			}
		}

		protected virtual void SelectExpedienteAction()
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpediente((ExpedientInfo)form.Selected);
				_entity.SetExpediente(_expediente);
				RefreshLineas();
			}
		}

        protected virtual void SetExpedienteManualAction()
        {
            _entity.OidExpediente = 0;
            _entity.Expediente = string.Empty;
            _entity.ResetExpediente();
            RefreshLineas();
        }

		protected override void SelectExpedienteLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			InputInvoiceLine item = Datos_Lineas.Current as InputInvoiceLine;

			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ExpedientInfo source = (ExpedientInfo)form.Selected;

				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;

				RefreshLineas();

				//AddCacheItem(source);
			}
		}

		protected virtual void SelectSerieAction()
		{
			SerieSelectForm form = new SerieSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSerie(form.Selected as SerieInfo, true);
			}
		}

		protected virtual void SelectUsuarioAction()
		{
			UserList list = UserList.GetList(AppContext.ActiveSchema, false);

			UserSelectForm form = new UserSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				UserInfo user = form.Selected as UserInfo;
				_entity.OidUsuario = user.Oid;
				_entity.Usuario = user.Name;
				Usuario_TB.Text = _entity.Usuario;
			}
		}

        protected override void ShowAlbaranAction()
        {
            InputInvoiceLine concepto = Datos_Lineas.Current as InputInvoiceLine;

            if (concepto != null)
            {
                InputDeliveryLineInfo c_albaran = InputDeliveryLineInfo.Get(concepto.OidConceptoAlbaran, false);
                InputDeliveryViewForm form = new InputDeliveryViewForm(c_albaran.OidAlbaran, ETipoAcreedor.Todos, this);
                form.ShowDialog(this);
            }
        }
		
		protected override void UpdateFacturaAction()
		{
			InputInvoiceLine item = Datos_Lineas.Current as InputInvoiceLine;
			ProductInfo product = ProductInfo.Get(item.OidProducto, false, true);

			item.BalanceQuantity(_entity, product);
			_entity.CalculateTotal();

			ControlsMng.UpdateBinding(Datos_Lineas);
		}

        protected override void SetIRPFAction()
        {
            _entity.SetIRPF();
        }

        #endregion

        #region Buttons

		private void Cuenta_BT_Click(object sender, EventArgs e) { SelectCuentaAction(); }
		private void Emisor_BT_Click(object sender, EventArgs e) { SelectEmisorAction(); }
		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedienteAction(); }
		private void Serie_BT_Click(object sender, EventArgs e) { SelectSerieAction(); }
        private void Usuario_BT_Click(object sender, EventArgs e) { SelectUsuarioAction(); }
        private void ExpedienteManual_BT_Click(object sender, EventArgs e) { SetExpedienteManualAction(); }

		#endregion

		#region Events

		private void MedioPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MedioPago_CB.SelectedValue == null) return;
            SetCuenta(Acreedor);
        }
       
        private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormaPago_CB.SelectedValue == null) return;
            EFormaPago forma_pago = (EFormaPago)(long)FormaPago_CB.SelectedValue;
            _entity.EFormaPago = forma_pago;

            _entity.Prevision = moleQule.Common.EnumFunctions.GetPrevisionPago(forma_pago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void DiasPago_NTB_TextChanged(object sender, EventArgs e)
        {
            try { _entity.DiasPago = DiasPago_NTB.LongValue; }
            catch { _entity.DiasPago = DiasPago_NTB.LongValue; }

			_entity.Prevision = moleQule.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.Fecha = Fecha_DTP.Value;
			_entity.Prevision = moleQule.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, Fecha_DTP.Value, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

		private void Descuento_NTB_TextChanged(object sender, EventArgs e)
		{
			_entity.PDescuento = PDescuento_NTB.DecimalValue;
			_entity.CalculateTotal();
			Datos_Lineas.ResetBindings(true);
			Datos.ResetBindings(false);
		}

        private void Descuento_NTB_TextChanged_1(object sender, EventArgs e)
        {
            if (_entity.PDescuento != 0)
            {
                Descuento_NTB.Text = Decimal.Round(_entity.Descuento, 2).ToString();
                return;
            }

            _entity.Descuento = Descuento_NTB.DecimalValue;
            _entity.CalculateTotal();
            Datos_Lineas.ResetBindings(false);
            Datos.ResetBindings(false);
        }
		
		/*private void PIRPF_NTB_TextChanged(object sender, EventArgs e)
		{
			_entity.PIRPF = PIRPF_NTB.DecimalValue;
			_entity.CalculateTotal();
			Datos.ResetBindings(false);
		}*/

        #endregion
    }
}