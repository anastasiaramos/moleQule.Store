using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Hipatia;

namespace moleQule.Face.Store
{
	public partial class ProviderMngForm : ProviderMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ProviderMngForm";
		public static Type Type { get { return typeof(ProviderMngForm); } }
		public override Type EntityType { get { return typeof(ProviderBase); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected ProviderBase _entity;
		protected ETipoAcreedor _tipo_acreedor;
		moleQule.Base.EEstado _estado = moleQule.Base.EEstado.Todos;

		#endregion

		#region Factory Methods

		public ProviderMngForm()
			: this(false) { }

		public ProviderMngForm(bool isModal)
			: this(isModal, null, ETipoAcreedor.Todos, moleQule.Base.EEstado.Todos) { }

		public ProviderMngForm(bool isModal, Form parent, ETipoAcreedor tipo, moleQule.Base.EEstado estado)
			: this(isModal, parent, tipo, estado, null) { }

		public ProviderMngForm(bool isModal, Form parent, ProveedorList lista)
			: this(isModal, parent, ETipoAcreedor.Todos, moleQule.Base.EEstado.Todos, lista) { }

		protected ProviderMngForm(bool isModal, Form parent, ETipoAcreedor tipo, moleQule.Base.EEstado estado, ProveedorList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource =  ProveedorList.NewList().GetSortedList(); 
			SortProperty = Nombre.DataPropertyName;

			this.Text = (_tipo_acreedor == ETipoAcreedor.Acreedor) ? Resources.Labels.ACREEDORES : Resources.Labels.PROVEEDORES;

			_tipo_acreedor = tipo;
			_estado = estado;
		}

		#endregion

		#region Layout
        
		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.6;
			Observaciones.Tag = 0.4;

			cols.Add(Nombre);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

        public override void FormatControls()
        {
            base.FormatControls();

            SetActionStyle(molAction.CustomAction1, Resources.Labels.PAGOS_TI, Properties.Resources.pago);
            MaximizeForm();
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			ProviderBaseInfo item = row.DataBoundItem as ProviderBaseInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			switch (view)
			{
				case molView.Select:

					ShowAction(molAction.Add);
					ShowAction(molAction.View);
					ShowAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.Copy);
					ShowAction(molAction.Select);
					ShowAction(molAction.SelectAll);
					ShowAction(molAction.FilterOn);
					ShowAction(molAction.FilterOff);
                    ShowAction(molAction.CustomAction1);

                    this.Top = 50;
                    this.StartPosition = FormStartPosition.CenterParent;

					break;

                case molView.Normal:

                    ShowAction(molAction.Add);
                    ShowAction(molAction.View);
                    ShowAction(molAction.Edit);
                    ShowAction(molAction.Delete);
                    HideAction(molAction.Print);
                    HideAction(molAction.PrintDetail);
                    HideAction(molAction.Copy);
                    HideAction(molAction.Select);
                    HideAction(molAction.SelectAll);
                    ShowAction(molAction.FilterOn);
                    ShowAction(molAction.FilterOff);
                    ShowAction(molAction.CustomAction1);
                    HideAction(molAction.Lock);
                    HideAction(molAction.Unlock);
                    HideAction(molAction.ChangeStateAnulado);
                    HideAction(molAction.ChangeStateContabilizado);
                    HideAction(molAction.ChangeStateEmitido);
                    HideAction(molAction.CustomAction2);
                    HideAction(molAction.CustomAction3);
                    HideAction(molAction.CustomAction4);
                    HideAction(molAction.EmailLink);
                    HideAction(molAction.EmailPDF);
                    HideAction(molAction.ExportPDF);
                    HideAction(molAction.PrintListQR);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Proveedor");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ProviderBaseList.GetList(_estado, false);
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Proveedores");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						ProviderBaseList listA = ProviderBaseList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						ProviderBaseList listD = ProviderBaseList.GetList(_filter_results);
						listD.RemoveItem(ActiveOID);
						_filter_results = listD.GetSortedList();
					}
					break;
			}

			_entity = null;
			RefreshSources();
        }

		#endregion

		#region Actions

		public override void OpenAddForm()
		{
			SelectEnumInputForm select_form = new SelectEnumInputForm(true);
            ETipoAcreedor[] tipos = new ETipoAcreedor[6] { ETipoAcreedor.Acreedor, ETipoAcreedor.Proveedor, ETipoAcreedor.Despachante, ETipoAcreedor.Naviera, ETipoAcreedor.TransportistaDestino, ETipoAcreedor.TransportistaOrigen};
			select_form.SetDataSource(moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetList(tipos, false));

			if (select_form.ShowDialog(this) != DialogResult.OK) return;

			ComboBoxSource estado = select_form.Selected as ComboBoxSource;

			switch ((ETipoAcreedor)(long)estado.Oid)
			{
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Proveedor:
					{
						ProveedorAddForm form = new ProveedorAddForm(this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;

				case ETipoAcreedor.Despachante:
					{
						DespachanteAddForm form = new DespachanteAddForm(this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;

				case ETipoAcreedor.Naviera:
					{
						NavieraAddForm form = new NavieraAddForm();
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;

				case ETipoAcreedor.TransportistaOrigen:
					{
						TransporterAddForm form = new TransporterAddForm(this, ETipoAcreedor.TransportistaOrigen);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;

				case ETipoAcreedor.TransportistaDestino:
					{
						TransporterAddForm form = new TransporterAddForm(this, ETipoAcreedor.TransportistaDestino);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;
			}
		}

		public override void OpenViewForm()
		{
			switch (ActiveItem.ETipoAcreedor)
			{
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Proveedor:
					{
						AddForm(new ProveedorViewForm(ActiveItem.OidAcreedor, ActiveItem.ETipoAcreedor, this));
					}
					break;

				case ETipoAcreedor.Despachante:
					{
						AddForm(new DespachanteViewForm(ActiveItem.OidAcreedor, this));
					}
					break;

				case ETipoAcreedor.Naviera:
					{
						AddForm(new NavieraViewForm(ActiveItem.OidAcreedor, this));
					}
					break;

				case ETipoAcreedor.TransportistaOrigen:
				case ETipoAcreedor.TransportistaDestino:
					{
						AddForm(new TransporterViewForm(ActiveItem.OidAcreedor, ActiveItem.ETipoAcreedor, this));
					}
					break;
			}
		}

		public override void OpenEditForm()
		{
			switch (ActiveItem.ETipoAcreedor)
			{
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Proveedor:
					{
						ProveedorEditForm form = new ProveedorEditForm(ActiveItem.OidAcreedor, ActiveItem.ETipoAcreedor, this);
						if (form.Entity != null)
						{
							AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
						}
					}
					break;

				case ETipoAcreedor.Despachante:
					{
						DespachanteEditForm form = new DespachanteEditForm(ActiveItem.OidAcreedor, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
                                _entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;

				case ETipoAcreedor.Naviera:
					{
						NavieraEditForm form = new NavieraEditForm(ActiveItem.OidAcreedor, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;

				case ETipoAcreedor.TransportistaOrigen:
				case ETipoAcreedor.TransportistaDestino:
					{
						TransporterEditForm form = new TransporterEditForm(ActiveItem.OidAcreedor, ActiveItem.ETipoAcreedor, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            if (form.ActionResult == DialogResult.OK)
                            {
								_entity = new ProviderBase();
								_entity.CopyValues(form.Entity.ProviderBase);
                                _entity.Record.CopyValues(form.Entity.Base.Record);
                            }
                        }
					}
					break;
			}
		}

		public override void DeleteAction()
        {
            switch (ActiveItem.ETipoAcreedor)
            {
                case ETipoAcreedor.Acreedor:
                case ETipoAcreedor.Proveedor:
                        Proveedor.Delete(ActiveItem.OidAcreedor, ActiveItem.ETipoAcreedor);
                    break;

                case ETipoAcreedor.Naviera:
                        Naviera.Delete(ActiveItem.OidAcreedor);
                    break;

                case ETipoAcreedor.Despachante:
                        Despachante.Delete(ActiveItem.OidAcreedor);
                    break;

                case ETipoAcreedor.TransportistaOrigen:
                case ETipoAcreedor.TransportistaDestino:
                        Transporter.Delete(ActiveItem.OidAcreedor);
                    break;
            }
			_action_result = DialogResult.OK;
		}

		public static void OpenEditFormAction(ETipoAcreedor providerType, long oid, Form parent)
		{
			switch (providerType)
			{
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Proveedor:
					{
						ProveedorEditForm form = new ProveedorEditForm(oid, providerType, parent);
						form.ShowDialog(parent);
					}
					break;

				case ETipoAcreedor.Naviera:
					{
						NavieraEditForm form = new NavieraEditForm(oid, parent);
						form.ShowDialog(parent);
					}
					break;

				case ETipoAcreedor.Despachante:
					{
						DespachanteEditForm form = new DespachanteEditForm(oid, parent);
						form.ShowDialog(parent);
					}
					break;

				case ETipoAcreedor.TransportistaOrigen:
				case ETipoAcreedor.TransportistaDestino:
					{
						TransporterEditForm form = new TransporterEditForm(oid, providerType, parent);
						form.ShowDialog(parent);
					}
					break;
			}

        }

        public override void CustomAction1() { GotoPagosAction(); }

        public void GotoPagosAction()
        {
            if (ActiveItem.EEstado == moleQule.Base.EEstado.Anulado) return;

            PaymentEditForm form = new PaymentEditForm(this, ActiveItem.OidAcreedor, PaymentSummary.Get(ActiveItem.ETipoAcreedor, ActiveItem.OidAcreedor));
            form.ShowDialog(this);
        }

        public override void ShowDocumentsAction()
        {            
            switch (ActiveItem.ETipoAcreedor)
            {
                case ETipoAcreedor.Acreedor:
                case ETipoAcreedor.Proveedor:
                    {
                        ProveedorInfo agente = ProveedorInfo.Get(ActiveItem.Oid, ActiveItem.ETipoAcreedor, false);

                        try
                        {
                            AgenteInfo agent = AgenteInfo.Get(typeof(Proveedor), agente);
                            AgenteEditForm form = new AgenteEditForm(typeof(Proveedor), agente, this);
                            AddForm(form);
                        }
                        catch (HipatiaException ex)
                        {
                            if (ex.Code == HipatiaCode.NO_AGENTE)
                            {
                                AgenteAddForm form = new AgenteAddForm(typeof(Proveedor), agente, this);
                                AddForm(form);
                            }
                        }
                    }
                    break;

                case ETipoAcreedor.Naviera:
                    {
                        NavieraInfo agente = NavieraInfo.Get(ActiveItem.Oid, false);

                        try
                        {
                            AgenteInfo agent = AgenteInfo.Get(typeof(Naviera), agente);
                            AgenteEditForm form = new AgenteEditForm(typeof(Naviera), agente, this);
                            AddForm(form);
                        }
                        catch (HipatiaException ex)
                        {
                            if (ex.Code == HipatiaCode.NO_AGENTE)
                            {
                                AgenteAddForm form = new AgenteAddForm(typeof(Naviera), agente, this);
                                AddForm(form);
                            }
                        }
                    }
                    break;

                case ETipoAcreedor.Despachante:
                    {
                        DespachanteInfo agente = DespachanteInfo.Get(ActiveItem.Oid, false);

                        try
                        {
                            AgenteInfo agent = AgenteInfo.Get(typeof(Despachante), agente);
                            AgenteEditForm form = new AgenteEditForm(typeof(Despachante), agente, this);
                            AddForm(form);
                        }
                        catch (HipatiaException ex)
                        {
                            if (ex.Code == HipatiaCode.NO_AGENTE)
                            {
                                AgenteAddForm form = new AgenteAddForm(typeof(Despachante), agente, this);
                                AddForm(form);
                            }
                        }
                    }
                    break;

                case ETipoAcreedor.TransportistaOrigen:
                case ETipoAcreedor.TransportistaDestino:
                    {
						TransporterInfo agente = TransporterInfo.Get(ActiveItem.Oid, ActiveItem.ETipoAcreedor, false);

                        try
                        {
                            AgenteInfo agent = AgenteInfo.Get(typeof(Transporter), agente);
                            AgenteEditForm form = new AgenteEditForm(typeof(Transporter), agente, this);
                            AddForm(form);
                        }
                        catch (HipatiaException ex)
                        {
                            if (ex.Code == HipatiaCode.NO_AGENTE)
                            {
                                AgenteAddForm form = new AgenteAddForm(typeof(Transporter), agente, this);
                                AddForm(form);
                            }
                        }
                    }
                    break;
            }
        }
		
		#endregion
	}

	public partial class ProviderMngBaseForm : Skin06.EntityMngSkinForm<ProviderBaseList, ProviderBaseInfo>
    {
        public ProviderMngBaseForm()
            : this(false, null, null) { }

        public ProviderMngBaseForm(bool isModal, Form parent, ProveedorList lista)
            : base(isModal, parent, lista) { }
    }
}
