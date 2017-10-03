using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule;
using moleQule.CslaEx;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face.Common;
using moleQule.Face.Invoice;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class WorkReportUIForm : WorkReportForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 7; } }

        /// <summary>
        /// Se trata de la WorkReport actual y que se va a editar.
        /// </summary>
        protected WorkReport _entity = null;

		protected PedidoProveedorList _pedidos = PedidoProveedorList.NewList();
		protected PedidoProveedorList _pedidos_acreedor = null;
		protected List<PedidoProveedorInfo> _results = new List<PedidoProveedorInfo>();

        public override WorkReport Entity { get { return _entity; } set { _entity = value; } }
        public override WorkReportInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        protected WorkReportResource Concepto { get { return Lines_BS.Current != null ? Lines_BS.Current as WorkReportResource : null; } }

        #endregion

        #region Factory Methods

        public WorkReportUIForm() 
			: this(-1, null, true, null) {}

		public WorkReportUIForm(long oid, object[] parameters, bool isModal, Form parent)
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

                WorkReport temp = _entity.Clone();
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

		protected override void BuildCache()
		{
			Cache.Instance.Save(typeof(WorkReport), _entity);
		}

		protected override void CleanCache()
		{
			Cache.Instance.Remove(typeof(WorkReports));
		}

		#endregion

        #region Layout

		public override void FormatControls()
		{
			Date_DTP.Enabled = AppContext.User.IsAdmin;

			base.FormatControls();

			DisableColumns();
		}

		protected virtual void DisableColumns()
		{
			foreach (DataGridViewRow row in Lines_DGW.Rows)
				switch ((row.DataBoundItem as WorkReportResource).EEntityType)
				{
					case ETipoEntidad.Tool:
						row.Cells["WRAmount"].ReadOnly = false;
						row.Cells["WRAmount"].Style.BackColor = row.Cells["WRCost"].InheritedStyle.BackColor;
						row.Cells["WRAmount"].Style.ForeColor = row.Cells["WRCost"].InheritedStyle.ForeColor;
						break;

					case ETipoEntidad.OutputDelivery:
						row.Cells["WRAmount"].ReadOnly = true;
						row.Cells["WRAmount"].Style.BackColor = row.Cells["WRTotal"].InheritedStyle.BackColor;
						row.Cells["WRAmount"].Style.ForeColor = row.Cells["WRTotal"].InheritedStyle.ForeColor;
						row.Cells["WRCost"].ReadOnly = true;
						row.Cells["WRCost"].Style.BackColor = row.Cells["WRTotal"].InheritedStyle.BackColor;
						row.Cells["WRCost"].Style.ForeColor = row.Cells["WRTotal"].InheritedStyle.ForeColor;
						break;

					case ETipoEntidad.Empleado:
						row.Cells["WRAmount"].ReadOnly = true;
						row.Cells["WRAmount"].Style.BackColor = row.Cells["WRTotal"].InheritedStyle.BackColor;
						row.Cells["WRAmount"].Style.ForeColor = row.Cells["WRTotal"].InheritedStyle.ForeColor;
						break;
				}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Lines_BS.DataSource = _entity.Lines;
			PgMng.Grow();

			Date_DTP.Value = _entity.Date;
			From_DTP.Value = _entity.From;
			Till_DTP.Value = _entity.Till;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
        {
			/*if (_entity.OidAlmacen != 0) SetAlmacen(StoreInfo.Get(_entity.OidAlmacen, false));
			PgMng.Grow();*/

			if (_entity.OidExpedient != 0) SetExpediente(ExpedientInfo.Get(_entity.OidExpedient, false));
			PgMng.Grow();

			base.RefreshSecondaryData();
		}

        #endregion

		#region Business Methods

		protected void SetExpediente(ExpedientInfo source)
		{
			_expedient = source;

			Expedient_BS.DataSource = _expedient;

			if (_expedient != null)
			{
				_entity.OidExpedient = _expedient.Oid;
				_entity.Expedient = _expedient.Codigo;
				Expediente_TB.Text = _expedient.Codigo;
			}
		}

		#endregion

		#region Actions

		protected override void DefaultAction() { EditLineAction(); }

		protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void DeleteLineAction()
		{
			if (Lines_BS.Current == null) return;

			WorkReportResource item = (WorkReportResource)Lines_BS.Current;

			_entity.Lines.Remove(item);

			UpdateWorkReportAction();
		}

		protected override void EditLineAction()
		{
			if (Lines_BS.Current == null) return;
			
			WorkReportResource cf = (WorkReportResource)Lines_BS.Current;

			switch (cf.EEntityType)
			{
                case ETipoEntidad.OutputDelivery:
                    {
                        DeliveryEditForm form = new DeliveryEditForm(cf.OidResource, ETipoEntidad.WorkReport, this);
                        form.ShowDialog();
                        cf.CopyFrom(form.Entity);
                    }
                    break;

				case ETipoEntidad.Empleado:
					{
						EmployeeEditForm form = new EmployeeEditForm(cf.OidResource, this);
						form.ShowDialog();
						cf.CopyFrom(form.Entity);
					}
					break;

				case ETipoEntidad.Tool:
					{
						ToolEditForm form = new ToolEditForm(cf.OidResource, this);
						form.ShowDialog();
						cf.CopyFrom(form.Entity);
					}
					break;
			}

			UpdateWorkReportAction();
		}

		protected void AddCategoryAction()
		{
			WorkReportCategoryUIForm form = new WorkReportCategoryUIForm();
			form.ShowDialog();
		}
		
		protected override void AddEmployeeLineAction()
		{
			if (_entity.OidExpedient == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.SELECT_HOLDER);
				return;
			}

			SelectEmployeeLineAction();
			UpdateWorkReportAction();
		}

		protected override void AddToolLineAction()
		{
			if (_entity.OidExpedient == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.SELECT_HOLDER);
				return;
			}

			SelectToolLineAction();
			UpdateWorkReportAction();
		}

		protected override void AddDeliveryLineAction()
		{
            if (_entity.OidExpedient == 0)
            {
                PgMng.ShowInfoException(Resources.Messages.SELECT_HOLDER);
                return;
            }

            DeliveryAddForm form = new DeliveryAddForm(ETipoEntidad.WorkReport, this);
            form.ShowDialog();

            if (form.ActionResult == DialogResult.OK)
            {
                OutputDelivery item = form.Entity;

                if (_entity.Lines.GetItem(item) == null)
                    _entity.Lines.NewItem(_entity, item);
            }

            UpdateWorkReportAction();
		}

		protected virtual void SelectCategoryAction()
		{
			WorkReportCategorySelectForm form = new WorkReportCategorySelectForm(this);			
	
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				WorkReportCategoryInfo item = (WorkReportCategoryInfo)form.Selected;
				_entity.Category = item.Oid;
				_entity.CategoryName = item.Name;
				Category_TB.Text = _entity.CategoryName;
			}
		}

		protected virtual void SelectEmployeeLineAction()
		{
			EmployeeList list = EmployeeList.GetList(moleQule.Base.EEstado.Active, false);

			EmployeeSelectForm form = new EmployeeSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				EmployeeInfo item = form.Selected as EmployeeInfo;

				if (_entity.Lines.GetItem(item) == null)
					_entity.Lines.NewItem(_entity, item);
			}
		}

		protected virtual void SelectStatusAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Active, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Status = estado.Oid;
			}
		}

		protected virtual void SelectExpedientAction()
		{
			ExpedienteList list = ExpedienteList.GetList(moleQule.Store.Structs.ETipoExpediente.Work, false);
			WorkSelectForm form = new WorkSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpediente((ExpedientInfo)form.Selected);
				RefreshLines();
			}
		}

		protected virtual void SelectToolLineAction()
		{
			ToolList list = ToolList.GetList(moleQule.Base.EEstado.Active, false);

			ToolSelectForm form = new ToolSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ToolInfo item = form.Selected as ToolInfo;
				_entity.Lines.NewItem(_entity, item);
			}
		}

		protected virtual void SelectUserAction()
		{
			UserList list = UserList.GetList(AppContext.ActiveSchema, false);

			UserSelectForm form = new UserSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				UserInfo user = form.Selected as UserInfo;
				_entity.OidOwner = user.Oid;
				_entity.Owner = user.Name;
				Usuario_TB.Text = _entity.Owner;
			}
		}

		protected override void UpdateWorkReportAction()
		{
			_entity.CalculateTotal();

			ControlsMng.UpdateBinding(Lines_BS);
			Lines_DGW.Refresh();

			DisableColumns();
		}

        #endregion

        #region Print

        public override void PrintObject()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

            if (_action_result == DialogResult.OK)
            {
                FormMngBase.Instance.RefreshFormsData();

                /*WorkReportReportMng reportMng = new WorkReportReportMng(AppContext.ActiveSchema);
                FormatConfFacturaWorkReportReport conf = new FormatConfFacturaWorkReportReport();
                conf.nota = EntityInfo.Nota ? Nota_TB.Text : "";
                conf.cabecera = "ALBARAN";
                conf.copia = "";
                conf.cuenta_bancaria = "";
                conf.forma_pago = "";

                ReportViewer.SetReport(reportMng.GetWorkReportReport(EntityInfo, conf));
                ReportViewer.ShowDialog();*/
            }
        }

        #endregion

        #region Buttons

		private void AddCategory_BT_Click(object sender, EventArgs e) { AddCategoryAction(); }
		private void Category_BT_Click(object sender, EventArgs e) { SelectCategoryAction(); }
		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedientAction(); }
		private void Usuario_BT_Click(object sender, EventArgs e) { SelectUserAction(); }
		private void Estado_BT_Click(object sender, EventArgs e) { SelectStatusAction(); }

        #endregion

        #region Events

        private void Date_DTP_ValueChanged(object sender, EventArgs e)
        {
			_entity.Date = Date_DTP.Value;
        }

		private void From_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.From = From_DTP.Value;
		}

		private void Till_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.Till = Till_DTP.Value;
		}

        #endregion
    }
}

