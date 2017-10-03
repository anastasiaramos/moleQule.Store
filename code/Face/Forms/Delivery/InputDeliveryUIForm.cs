using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.CslaEx;
using moleQule.Face.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;

namespace moleQule.Face.Store
{
    public partial class InputDeliveryUIForm : InputDeliveryForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 7; } }

        /// <summary>
        /// Se trata de la InputDelivery actual y que se va a editar.
        /// </summary>
        protected InputDelivery _entity = null;

		protected PedidoProveedorList _orders = PedidoProveedorList.NewList();
		protected PedidoProveedorList _provider_orders = null;
		protected List<PedidoProveedorInfo> _results = new List<PedidoProveedorInfo>();

        public override InputDelivery Entity { get { return _entity; } set { _entity = value; } }
        public override InputDeliveryInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

		protected InputDeliveryLine Line { get { return Lines_BS.Current != null ? Lines_BS.Current as InputDeliveryLine : null; } }

        #endregion

        #region Factory Methods

        public InputDeliveryUIForm() 
			: this(-1, null, true, null) {}

		public InputDeliveryUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        protected override bool SaveObject()
        {
			this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
                PgMng.Reset(5, 1, Library.Store.Resources.Messages.ACTUALIZANDO_STOCKS, this);

                InputDelivery temp = _entity.Clone();
                temp.ApplyEdit();
                PgMng.Grow();

                _entity = temp.Save();
                _entity.ApplyEdit();
                PgMng.Grow();

                return true;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region Cache

		protected HashOidList _oidAlmacenes = new HashOidList();
		protected HashOidList _oidExpedientes = new HashOidList();
		protected HashOidList _oidProductos = new HashOidList();
		protected HashOidList _oidPartidas = new HashOidList();
		protected HashOidList _oidPedidos = new HashOidList();

		protected void AddCacheItem()
		{
			if (Lines_BS.Current == null) return;
			AddCacheItem((InputDeliveryLine)Lines_BS.Current);
		}
		protected void AddCacheItem(StoreInfo item)
		{
			if (item == null) return;
			_oidAlmacenes.Add(item.Oid);
		}
		protected void AddCacheItem(ExpedientInfo item)
		{
			if (item == null) return;
			_oidExpedientes.Add(item.Oid);
		}
		protected void AddCacheItem(InputDeliveryLine item)
		{
			if (item == null) return;

			_oidAlmacenes.Add(item.OidAlmacen);
			_oidExpedientes.Add(item.OidExpediente);
			_oidPartidas.Add(item.OidPartida);
			_oidProductos.Add(item.OidProducto);
			_oidPedidos.Add(item.OidPedido);
		}

		protected override void BuildCache()
		{
			foreach (InputDeliveryLine ca in _entity.Conceptos)
				AddCacheItem(ca);
		}

		protected override void CleanCache()
		{
			Cache.Instance.Remove(typeof(Stores));
			Cache.Instance.Remove(typeof(Expedients));
			Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));
		}

		#endregion

        #region Layout

		public override void FormatControls()
		{
			FechaRegistro_DTP.Enabled = AppContext.User.IsAdmin;

			Serie_BT.Enabled = (_entity.Conceptos.Count == 0);
			ActivateAction(molAction.CustomAction1, (_entity.EEstado == moleQule.Base.EEstado.Abierto));

			base.FormatControls();
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Lines_BS.DataSource = _entity.Conceptos;
			PgMng.Grow();

            DiasPago_TB.Text = _entity.DiasPago.ToString("00");
            Fecha_DTP.Value = _entity.Fecha;
			FechaRegistro_DTP.Value = _entity.FechaRegistro;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
        {
            if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
            PgMng.Grow();

            if (_entity.OidAcreedor != 0) SetProvider(ProviderBaseInfo.Get(_entity.OidAcreedor, _entity.ETipoAcreedor, false));
            PgMng.Grow();

			if (_entity.OidAlmacen != 0) SetStore(StoreInfo.Get(_entity.OidAlmacen, false));
			PgMng.Grow();

			if (_entity.OidExpediente != 0) SetExpedient(ExpedientInfo.Get(_entity.OidExpediente, false), false);
			PgMng.Grow();

			base.RefreshSecondaryData();
		}

        protected override void HideComponentes()
        {
            foreach (DataGridViewRow row in Lines_DGW.Rows)
                if ((row.DataBoundItem as InputDeliveryLine).IsKitComponent)
                    row.Visible = false;
        }

        #endregion

		#region Business Methods

		protected virtual void SetProvider(IAcreedorInfo source)
		{
			if (source == null) return;

			_provider = source;

			Providers_BS.DataSource = _provider;

            if (source.OidAcreedor != _entity.OidAcreedor || source.ETipoAcreedor != _entity.ETipoAcreedor)
            {
                _entity.CopyFrom(source);
                if (_entity.Conceptos.Count > 0)
                {
                    foreach (InputDeliveryLine item in _entity.Conceptos)
                        item.SetPrice(_provider);
                    _entity.CalculateTotal();
                }
            }

			DiasPago_TB.Text = _entity.DiasPago.ToString("00");

			//Cargamos los precios especiales del proveedor
			if (_provider.Productos == null)
				_provider.LoadChilds(typeof(ProductoProveedor), false);

			if (_provider.ETipoAcreedor != ETipoAcreedor.Proveedor)
			{
				SetStore(null);
				AddConceptoStock_TI.Enabled = false;
			}
			else
				AddConceptoStock_TI.Enabled = true;

			_provider_orders = PedidoProveedorList.GetByAcreedorList(_entity.OidAcreedor, _entity.ETipoAcreedor, false);

        }

		protected virtual void SetStore(StoreInfo source)
		{
			_store = source;

			if (_store != null)
			{
				_entity.Almacen = _store.Nombre;
				_entity.IDAlmacen = _store.Codigo;
				_entity.OidAlmacen = _store.Oid;
				Almacen_TB.Text = _entity.IDAlmacenAlmacen;

				AddCacheItem(_store);
			}
			else
			{
				_entity.OidAlmacen = 0;
				_entity.Almacen = string.Empty;
				_entity.IDAlmacen = string.Empty;
				Almacen_TB.Text = string.Empty;
			}
		}

		protected void SetExpedient(ExpedientInfo source, bool setToLines = true)
		{
			_expedient = source;

			if (_expedient != null)
			{
				_entity.Expediente = _expedient.Codigo;
				_entity.OidExpediente = _expedient.Oid;
				Expediente_TB.Text = _expedient.Codigo;

				AddCacheItem(_expedient);

				//setToLines es para evitar que pregunte al cargar el albarán
				if (_entity.Conceptos.Count > 0 && setToLines)
				{
					DialogResult result = ProgressInfoMng.ShowQuestion(string.Format("¿Asignar el expediente {0} a todos los conceptos del albarán?", _expedient.Codigo));
					
					foreach (InputDeliveryLine item in _entity.Conceptos)
					{
						if (result == DialogResult.No && item.OidExpediente != 0) continue;
						item.OidExpediente = source.Oid;
						item.Expediente = source.Codigo;
					}
				}
			}
		}

		protected void SetSerie(SerieInfo source, bool new_code)
		{
			if (source == null) return;

			_serie = source;

			_entity.OidSerie = source.Oid;
			_entity.NumeroSerie = source.Identificador;
			_entity.NombreSerie = source.Nombre;
			Serie_TB.Text = _entity.NSerieSerie;
			Observaciones_TB.Text = source.Cabecera;

			if (new_code) _entity.GetNewCode(_deliveryType);

			Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));

			ProductList.GetListBySerie(_serie.Oid, false, true);
		}

		protected void SetTransporter(TransporterInfo source)
		{
			if (source == null) return;

			/*_entity.OidTransportista = source.Oid;
			Transportista_TB.Text = source.Nombre;*/
		}

		private void DeleteKit(BatchInfo partida)
		{
			InputDeliveryLine concepto;

			foreach (BatchInfo item in partida.Componentes)
			{
				concepto = _entity.Conceptos.GetItem(new FCriteria<long>("OidPartida", item.Oid));
				_entity.Conceptos.Remove(concepto);
			}
		}

		protected void DoAddOrder(BackgroundWorker bk)
		{
			Datos.RaiseListChangedEvents = false;
			Lines_BS.RaiseListChangedEvents = false;

			try
			{
				PgMng.Reset(_results.Count + 1, 1, Resources.Messages.IMPORTANDO_ALBARANES, this);

				//Asignamos el ACREEDOR
				if (_entity.OidAcreedor == 0)
				{
					_entity.CopyFrom(_results[0]);
					SetProvider(ProviderBaseInfo.Get(_results[0].OidAcreedor, _results[0].ETipoAcreedor));
				}

				foreach (PedidoProveedorInfo item in _results)
				{
					_entity.Insert(item);
					_orders.RemoveItem(item.Oid);
				}

				PgMng.Grow(string.Empty, "Insertar el Pedido");

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
				Lines_BS.RaiseListChangedEvents = true;
#if TRACE
                PgMng.ShowCronos();
#endif
			}
		}

		#endregion

		#region Actions

		protected override void DefaultAction() { EditLineAction(); }

		protected override void SaveAction()
        {
            Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
            {
                InputDelivery = _entity.GetInfo(false),
                TipoAcreedor = new ETipoAcreedor[1] { _entity.ETipoAcreedor },
                Estado = moleQule.Base.EEstado.NoAnulado
            };

            conditions.InputDelivery.Oid = 0;

            if (InputDeliveryInfo.Exists(conditions, false).Oid != 0
                && InputDeliveryInfo.Exists(conditions,false).Oid != _entity.Oid)
                if (DialogResult.No == ProgressInfoMng.ShowQuestion("Existe un albarán de este proveedor con la misma fecha e importe. ¿Desea continuar?"))
                {
                    _action_result = DialogResult.Cancel;
                    return;
                }

            if (_entity.Rectificativo)
            {
                foreach (InputDeliveryLine item in _entity.Conceptos)
                {
                    if (item.FacturacionBulto && item.CantidadBultos > 0)
                        item.CantidadBultos = -item.CantidadBultos;
                    if (item.FacturacionPeso && item.CantidadKilos > 0)
                        item.CantidadKilos = -item.CantidadKilos;
                }

                _entity.CalculateTotal();
            }
            
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void CustomAction1() { CreateInvoiceAction(); }

		protected virtual void AddOrderAction(List<PedidoProveedorInfo> albaranes)
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_orders.Count == 0)
			{
				if (_entity.OidAcreedor != 0)
					_orders = PedidoProveedorList.GetPendientesList(_entity.OidAcreedor, _entity.ETipoAcreedor, _entity.OidSerie, false);
				else
					_orders = PedidoProveedorList.GetPendientesList(0, ETipoAcreedor.Todos, _entity.OidSerie, false);
			}

			if (albaranes == null)
			{
				PedidoProveedorSelectForm form = new PedidoProveedorSelectForm(this, PedidoProveedorList.GetList(_orders));
				form.ShowDialog(this);
				if (form.DialogResult == DialogResult.OK)
				{
					_results = form.Selected as List<PedidoProveedorInfo>;
				}
				else
					_results.Clear();
			}
			else
				_results = albaranes;

			if (_results.Count > 0)
			{
				foreach (PedidoProveedorInfo item in _results)
				{
					if (item.OidAcreedor != _results[0].OidAcreedor)
					{
						PgMng.ShowInfoException("No es posible asignar pedidos de clientes distintos a un mismo Albarán.");
						return;
					}
				}

				DoAddOrder(null);
			}

			if (Result == BGResult.OK)
			{
				Serie_BT.Enabled = false;
				Datos.ResetBindings(false);
			}

			if (Result == BGResult.OK)
				Lines_BS.ResetBindings(false);
		}

		protected virtual void CreateInvoiceAction()
		{
			if (_entity.EEstado != moleQule.Base.EEstado.Abierto) return;

			ExecuteAction(molAction.Save, true);

			if (_action_result != DialogResult.OK) return;

			InputInvoiceAddForm form = new InputInvoiceAddForm(_entity.GetInfo(), _provider, this);
            form.ShowDialog(this);
			
			if (form.ActionResult == DialogResult.OK)
			{
				_entity.EEstado = moleQule.Base.EEstado.Billed;
				_entity.NumeroFactura = form.Entity.Codigo;
				_entity.NumeroAcreedor = form.Entity.NumeroAcreedor;
			}
		}

		protected override void DeleteLineAction()
		{
			if (Lines_BS.Current == null) return;

			if (PgMng.ShowDeleteConfirmation() == DialogResult.Yes)
			{
				if (Line.OidPedido == 0)
				{
					PgMng.Reset(4, 1, Resources.Messages.UPDATING_STOCK, this);

					BatchInfo partida = BatchInfo.Get(Line.OidPartida, true);
					PgMng.Grow();

					if (partida.IsKit) DeleteKit(partida);
					PgMng.Grow();

					_entity.Conceptos.Remove(Line, true);
					_entity.CalculateTotal();
					PgMng.Grow();
				}
				else
				{
					long oidPedido = Line.OidPedido;

					_entity.Conceptos.Remove(Line, true);
					_entity.CalculateTotal();

					bool free_pedido = true;

					foreach (InputDeliveryLine item in _entity.Conceptos)
						if (item.OidPedido == Line.OidPedido) free_pedido = false;

					//Actualizamos la lista de pedidos disponibles
					if (free_pedido) _orders.AddItem(_provider_orders.GetItem(oidPedido));
				}


				ControlsMng.UpdateBinding(Lines_BS);
				ControlsMng.UpdateBinding(Datos);
				PgMng.FillUp();
			}

			Serie_BT.Enabled = (_entity.Conceptos.Count == 0);
		}

		protected override void EditLineAction()
		{
			if (Lines_BS.Current == null) return;

			InputDeliveryLine cf = (InputDeliveryLine)Lines_BS.Current;
			_provider = Providers_BS.Current as ProviderBaseInfo;

			InputDeliveryLineEditForm form = null;

			form = new InputDeliveryLineEditForm( _entity, _serie, _provider, cf, this);

			if (form.ShowDialog(this) == DialogResult.OK)
				_entity.CalculateTotal();
		}

		protected override void NewLineAction(bool stock)
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_entity.OidAcreedor == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.SELECT_HOLDER);
				return;
			}

			_provider = Providers_BS.Current as ProviderBaseInfo;

			InputDeliveryLineAddForm form = new InputDeliveryLineAddForm(_entity, _serie, _provider, this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculateTotal();
			}
		}

		protected virtual void ResetStoreAction() { SetStore(null); }

		protected virtual void SelectProviderAction()
		{
            //if (_entity.Conceptos.Count > 0)
            //{
            //    PgMng.ShowInfoException("No es posible cambiar el emisor a un albarán con conceptos asociados.");
            //    return;
            //}

            if (_entity.OidSerie == 0)
            {
                PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
                return;
            }

			ProviderSelectForm form = new ProviderSelectForm(this, moleQule.Base.EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_provider = form.Selected as IAcreedorInfo;
				SetProvider(_provider);
			}
		}

		protected virtual void SelectStoreAction()
		{
			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetStore((StoreInfo)form.Selected);
				//_entity.SetAlmacen(_almacen);
				RefreshLines();
			}
		}

		protected virtual void SelectExpedientAction()
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpedient((ExpedientInfo)form.Selected);
				RefreshLines();
			}
		}

        protected virtual void SetExpedienteManualAction()
        {
            _entity.OidExpediente = 0;
            _entity.Expediente = string.Empty;
        }

		protected virtual void SelectSerieAction()
		{
			SerieSelectForm form = new SerieSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSerie(form.Selected as SerieInfo, true);
			}
		}

		protected virtual void SelectTransporterAction()
		{
			TransporterSelectForm form = new TransporterSelectForm(this, moleQule.Base.EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetTransporter(form.Selected as TransporterInfo);
			}
		}

		protected virtual void SelectOwnerAction()
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

		protected override void SelectLineStoreAction()
		{
			if (Lines_BS.Current == null) return;

			InputDeliveryLine item = Lines_BS.Current as InputDeliveryLine;

			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				StoreInfo source = (StoreInfo)form.Selected;

				item.OidAlmacen = source.Oid;
				item.Almacen = source.Nombre;
				
				AddCacheItem(source);
			}
		}

		protected override void SelectLineExpedientAction()
		{
			if (Lines_BS.Current == null) return;

			InputDeliveryLine item = Lines_BS.Current as InputDeliveryLine;

			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ExpedientInfo source = (ExpedientInfo)form.Selected;

				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;

				AddCacheItem(source);
			}
		}

		protected override void SelectLineTaxAction()
		{
			if (Lines_BS.Current == null) return;

			InputDeliveryLine item = Lines_BS.Current as InputDeliveryLine;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				item.OidImpuesto = source.Oid;
				item.PImpuestos = source.Porcentaje;
			}
		}

		protected override void UpdateDeliveryAction()
		{
			InputDeliveryLine item = Lines_BS.Current as InputDeliveryLine;
			ProductInfo producto = ProductInfo.Get(item.OidProducto, false, true);

            _entity.CalculateTotal();

			ControlsMng.UpdateBinding(Lines_BS);
		}

        protected override void SetIRPFAction()
        {
            _entity.SetIRPF();
        }

        #endregion

        #region Print

        public override void PrintObject()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

            if (_action_result == DialogResult.OK)
            {
                FormMngBase.Instance.RefreshFormsData();

                /*AlbaranProveedorReportMng reportMng = new AlbaranProveedorReportMng(AppContext.ActiveSchema);
                FormatConfFacturaAlbaranProveedorReport conf = new FormatConfFacturaAlbaranProveedorReport();
                conf.nota = EntityInfo.Nota ? Nota_TB.Text : "";
                conf.cabecera = "ALBARAN";
                conf.copia = "";
                conf.cuenta_bancaria = "";
                conf.forma_pago = "";

                ReportViewer.SetReport(reportMng.GetAlbaranProveedorReport(EntityInfo, conf));
                ReportViewer.ShowDialog();*/
            }
        }

        #endregion

        #region Buttons

		private void Almacen_BT_Click(object sender, EventArgs e) { SelectStoreAction(); }
		private void Emisor_BT_Click(object sender, EventArgs e) { SelectProviderAction(); }
		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedientAction(); }
		private void ResetAlmacen_BT_Click(object sender, EventArgs e) { ResetStoreAction(); }
		private void Serie_BT_Click(object sender, EventArgs e) { SelectSerieAction(); }
		private void Transportista_BT_Click(object sender, EventArgs e) { SelectTransporterAction(); }
		private void Usuario_BT_Click(object sender, EventArgs e) { SelectOwnerAction(); }

        #endregion

        #region Events

        private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormaPago_CB.SelectedValue == null) return;
            EFormaPago forma_pago = (EFormaPago)(long)FormaPago_CB.SelectedValue;
            _entity.EFormaPago = forma_pago;

			_entity.Prevision = moleQule.Common.EnumFunctions.GetPrevisionPago(forma_pago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void DiasPago_TB_TextChanged(object sender, EventArgs e)
        {
            try { long a = Convert.ToInt64(DiasPago_TB.Text); }
			catch { return; }

			_entity.Prevision = moleQule.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.UpdateFecha(Fecha_DTP.Value);
	        Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void FechaRegistro_DTP_ValueChanged(object sender, EventArgs e)
        {
			_entity.FechaRegistro = FechaRegistro_DTP.Value;
        }

        private void Rectificativo_CKB_CheckedChanged(object sender, EventArgs e)
        {
            RectificativoAction();
        }

        private void PDescuento_NTB_TextChanged(object sender, EventArgs e)
        {
            _entity.PDescuento = PDescuento_NTB.DecimalValue;
            _entity.CalculateTotal();
            Lines_BS.ResetBindings(false);
            Datos.ResetBindings(false);
        }

        private void Descuento_NTB_TextChanged(object sender, EventArgs e)
        {
            if (_entity.PDescuento != 0)
            {
                Descuento_NTB.Text = Decimal.Round(_entity.Descuento,2).ToString();
                return;
            }

            _entity.Descuento = Descuento_NTB.DecimalValue;
            _entity.CalculateTotal();
            Lines_BS.ResetBindings(false);
            Datos.ResetBindings(false);
        }

		private void PIRPF_NTB_TextChanged(object sender, EventArgs e)
		{
            SetIRPFAction();
            _entity.CalculateTotal();
			Datos.ResetBindings(false);
		}

        private void ExpedienteManual_BT_Click(object sender, EventArgs e)
        {
            SetExpedienteManualAction();
        }

        #endregion
    }
}