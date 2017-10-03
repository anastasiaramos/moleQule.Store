using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Base;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
//using moleQule.Library.Store.Reports.Empleado;

namespace moleQule.Face.Store
{
	public partial class EmployeeMngForm : EmployeeMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "EmpleadoMngForm";
		public static Type Type { get { return typeof(PedidoProveedorMngForm); } }
        public override Type EntityType { get { return typeof(Employee); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Employee _entity;
		protected moleQule.Base.EEstado _estado = moleQule.Base.EEstado.Todos;

		#endregion

		#region Factory Methods

		public EmployeeMngForm()
			: this(false) { }

		public EmployeeMngForm(bool isModal)
			: this(isModal, null, moleQule.Base.EEstado.Todos) { }

		public EmployeeMngForm(bool isModal, Form parent, moleQule.Base.EEstado status)
			: this(isModal, parent, status, null) { }

        public EmployeeMngForm(bool isModal, Form parent, moleQule.Base.EEstado status, EmployeeList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = EmployeeList.NewList().GetSortedList();
			SortProperty = Apellidos.DataPropertyName;

			_estado = status;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

        public override void FormatControls()
        {
            if (Tabla == null) return;

            base.FormatControls();

            SetActionStyle(molAction.CustomAction1, Resources.Labels.PAGOS_TI, Properties.Resources.pago);
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			EmployeeInfo item = row.DataBoundItem as EmployeeInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.ShowDocuments);
                    ShowAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.ShowDocuments);
                    ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Empleado");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = EmployeeList.GetList(_estado, false);
					break;
				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Empleados");
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
						EmployeeList listA = EmployeeList.GetList(_filter_results);
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
						EmployeeList listD = EmployeeList.GetList(_filter_results);
						listD.RemoveItem(ActiveOID);
						_filter_results = listD.GetSortedList();
					}
					break;
			}

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
        }

		#endregion

		#region Actions

		public override void OpenAddForm()
		{
			EmployeeAddForm form = new EmployeeAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new EmployeeViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			EmployeeEditForm form = new EmployeeEditForm(ActiveOID, this);
			if (form.EntityInfo != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
            Employee.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void ShowDocumentsAction()
		{
			try
			{
				AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
				AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
					AddForm(form);
				}
			}
        }

        public override void CustomAction1() { GotoPagosAction(); }

        public void GotoPagosAction()
        {
            if (ActiveItem.EEstado == moleQule.Base.EEstado.Anulado) return;

            EmployeePaymentEditForm form = new EmployeePaymentEditForm(this, ActiveOID, PaymentSummary.Get(ActiveItem.ETipoAcreedor, ActiveOID));
            form.ShowDialog(this);
        }

		#endregion

		#region Print

		public override void PrintList()
		{
			/*EmpleadoReportMng reportMng = new EmpleadoReportMng(AppContext.ActiveSchema);
			
			EmployeeListRpt report = reportMng.GetListReport(List);
			
			if (report != null)
			{
				ReportViewer.SetReport(report);
				ReportViewer.ShowDialog();
			}
			else
			{
				MessageBox.Show(Resources.Messages.NO_DATA_REPORTS,
								moleQule.Face.Resources.Labels.ADVISE_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}*/
		}

		#endregion
	}

    public partial class EmployeeMngBaseForm : Skin06.EntityMngSkinForm<EmployeeList, EmployeeInfo>
	{
		public EmployeeMngBaseForm()
			: this(false, null, null) { }

        public EmployeeMngBaseForm(bool isModal, Form parent, EmployeeList list)
			: base(isModal, parent, list) { }
	}
}
