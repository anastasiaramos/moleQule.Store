using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Serie;

namespace moleQule.Face.Store
{
    public partial class PedidoProveedorUIForm : PedidoProveedorForm
    {
        #region Attributes & Properties
		
		protected override int BarSteps { get { return base.BarSteps + 2; } }

        protected PedidoProveedor _entity;
		protected SerieInfo _serie = null;
		protected IAcreedorInfo _acreedor = null;
		protected StoreInfo _almacen = null;
		protected ExpedientInfo _expediente = null;

        public override PedidoProveedor Entity { get { return _entity; } set { _entity = value; } }
        public override PedidoProveedorInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected PedidoProveedorUIForm() 
			: this(-1, ETipoAcreedor.Todos) { }
		
        public PedidoProveedorUIForm(long oid, ETipoAcreedor tipo) 
			: this(oid, tipo, true) {}

		public PedidoProveedorUIForm(long oid, ETipoAcreedor tipo, bool isModal) 
			: this(oid, tipo, isModal, null) {}

        public PedidoProveedorUIForm(long oid, ETipoAcreedor tipo, bool isModal, Form parent)
            : base(oid, tipo, isModal, parent)
        {
            InitializeComponent();
        }

		public override void DisposeForm()
		{
			Cache.Instance.Remove(typeof(ProductList));
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            try
            {
				PedidoProveedor temp = _entity.Clone();
				temp.ApplyEdit();

				_entity = temp.Save();
                _entity.ApplyEdit();
					
                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout
		
		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.DataGridView == Lineas_DGW)
			{
				LineaPedidoProveedor item = row.DataBoundItem as LineaPedidoProveedor;
				row.DefaultCellStyle = (item.Pendiente != 0) ? ControlTools.Instance.LineaPendienteStyle : ControlTools.Instance.LineaCerradaStyle;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
			
			Datos_Lineas.DataSource = _entity.Lineas;
			PgMng.Grow();
						
            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			if (_entity.OidAcreedor != 0) SetAcreedor(ProviderBaseInfo.Get(_entity.OidAcreedor, _entity.ETipoAcreedor, false));
			PgMng.Grow();

			if (_entity.OidAlmacen != 0) SetAlmacen(StoreInfo.Get(_entity.OidAlmacen, false));
			PgMng.Grow();

			if (_entity.OidExpediente != 0) SetExpediente(ExpedientInfo.Get(_entity.OidExpediente, false));
			PgMng.Grow();

			if (_acreedor != null) ProductList.GetList(_acreedor, _serie, false, true);
		}

        #endregion

		#region Business Methods

		protected void SetAcreedor(IAcreedorInfo source)
		{
			if (source == null) return;

			_acreedor = source;

			Datos_Acreedor.DataSource = _acreedor;

			_entity.CopyFrom(_acreedor);

			//Cargamos los precios especiales del proveedor
			if (source.Productos == null) source.LoadChilds(typeof(ProductoProveedor), false);

			Cache.Instance.Remove(typeof(ProductList));
		}

		protected virtual ProductInfo SelectProducto()
		{
			ProductList lista = ProductList.GetList(_acreedor, _serie, false, true);

			ProductSelectForm form = new ProductSelectForm(this, lista);
			form.ShowCodigoProveedor(true);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProductInfo producto = (form.Selected as ProductInfo);
				return producto;
			}

			return null;
		}

		protected void SetAlmacen(StoreInfo source)
		{
			_almacen = source;

			if (_almacen != null)
			{
				_entity.Almacen = _almacen.Nombre;
				_entity.OidAlmacen = _almacen.Oid;
				Almacen_TB.Text = _almacen.Nombre;
			}
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			_expediente = source;

			switch (_expediente.ETipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Ganado:
				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:

					/*						

											if (_producto.ETipoVenta != ETipoVenta.Unitaria)
											{
												PgMng.ShowInfoException("No es posible asignar productos no unitarios a este tipo de expediente");
												return;
											}*/

					break;
			}

			if (_expediente != null)
			{
				_entity.Expediente = _expediente.Codigo;
				_entity.OidExpediente = _expediente.Oid;
				Expediente_TB.Text = _expediente.Codigo;
			}
		}

		protected void SetSerie(SerieInfo source, bool new_code)
		{
			if (source == null) return;

			_serie = source;

			_entity.OidSerie = source.Oid;
			_entity.NSerie = source.Identificador;
			_entity.Serie = source.Nombre;
			Serie_TB.Text = _entity.NSerieSerie;

			if (new_code) _entity.GetNewCode();

			Cache.Instance.Remove(typeof(ProductList));
		}

		#endregion

		#region Actions

		protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void DefaultAction() { EditLineaAction(); }

		protected override void CustomAction1() { CrearAlbaranAction(); }

		protected virtual void CrearAlbaranAction()
		{
			if (_entity.EEstado != moleQule.Base.EEstado.Abierto) return;

			ExecuteAction(molAction.Save);

			if (_action_result == DialogResult.OK)
			{
				_acreedor = Datos_Acreedor.DataSource as IAcreedorInfo;

				InputDeliveryAddForm form = new InputDeliveryAddForm(_acreedor, _entity.GetInfo(), this);
				form.ShowDialog();
			}
		}

		protected virtual void SelectAcreedorAction()
		{
			ProviderSelectForm form = new ProviderSelectForm(this, moleQule.Base.EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetAcreedor((IAcreedorInfo)form.Selected);
			}
		}

		protected virtual void SelectEstadoAction()
		{
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Anulado, moleQule.Base.EEstado.Abierto, moleQule.Base.EEstado.Closed, moleQule.Base.EEstado.Cancelado };

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				_entity.Estado = estado.Oid;
				Estado_TB.Text = estado.Texto;
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

		protected virtual void SelectAlmacenAction()
		{
			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetAlmacen((StoreInfo)form.Selected);
				//_entity.SetAlmacen(_almacen);
				RefreshLineas();
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

		protected override void AddLineaAction()
		{
			if (_acreedor == null)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_PROV_SELECTED);
				return;
			}

			ProductInfo producto = SelectProducto();
			
			if (producto == null) return;

			_entity.Lineas.NewItem(_entity, _acreedor, producto);

			RefreshLineas();
		}

		protected override void EditLineaAction()
		{
			if (Lineas_DGW.CurrentRow.DataBoundItem == null) return;

			LineaPedidoProveedor item = (LineaPedidoProveedor)Lineas_DGW.CurrentRow.DataBoundItem;
			if (item == null) return;

			ProductInfo producto = SelectProducto();

			if (producto == null) return;

			item.CopyFrom(_entity, _acreedor, producto);
			_entity.CalculaTotal();

			RefreshLineas();
		}

		protected override void DeleteLineaAction()
		{
			if (Lineas_DGW.CurrentRow.DataBoundItem == null) return;
			LineaPedidoProveedor item = (LineaPedidoProveedor)Lineas_DGW.CurrentRow.DataBoundItem;
			if (item == null) return;

			if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				_entity.Lineas.RemoveItem(_entity, item);

				RefreshLineas();
			}
		}

		protected override void SelectAlmacenLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedidoProveedor item = Datos_Lineas.Current as LineaPedidoProveedor;

			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				StoreInfo source = (StoreInfo)form.Selected;

				item.OidAlmacen = source.Oid;
				item.Almacen = source.Nombre;

				RefreshLineas();
			}
		}

		protected override void SelectExpedienteLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedidoProveedor item = Datos_Lineas.Current as LineaPedidoProveedor;

			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ExpedientInfo source = (ExpedientInfo)form.Selected;

				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;

				RefreshLineas();
			}
		}

		protected override void SelectImpuestoLineaAction() 
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedidoProveedor item = Datos_Lineas.Current as LineaPedidoProveedor;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				item.OidImpuesto = source.Oid;
				item.PImpuestos = source.Porcentaje;

				RefreshLineas();
			}
		}

		protected override void UpdatePedidoAction()
		{
			LineaPedidoProveedor item = Datos_Lineas.Current as LineaPedidoProveedor;
			ProductInfo producto = ProductInfo.Get(item.OidProducto, false, true);
			
			item.AjustaCantidad(producto);
			_entity.CalculaTotal();

			RefreshLineas();
		}

        #endregion

		#region Buttons

		private void Emisor_BT_Click(object sender, EventArgs e) { SelectAcreedorAction(); }
		private void Estado_BT_Click(object sender, EventArgs e) { SelectEstadoAction(); }
		private void Usuario_BT_Click(object sender, EventArgs e) { SelectUsuarioAction(); }
		private void Almacen_BT_Click(object sender, EventArgs e) { SelectAlmacenAction(); }
		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedienteAction(); }

		#endregion
	}
}
