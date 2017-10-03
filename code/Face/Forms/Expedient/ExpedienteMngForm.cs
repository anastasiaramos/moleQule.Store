using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;

namespace moleQule.Face.Store
{
	public partial class ExpedienteMngForm : ExpedienteBaseMngForm
	{
		#region Attributes & Properties

		public const string ID = "ExpedienteMngForm";
		public static Type Type { get { return null; } }
		public override Type EntityType { get { return typeof(Expedient); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Expedient _entity;

		protected moleQule.Store.Structs.ETipoExpediente _tipo_expediente;

		public moleQule.Store.Structs.ETipoExpediente ETipoExpediente { get { return _tipo_expediente; } }

		public DataGridViewCellStyle CompleteStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle MarkedStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle UnmarkedStyle = new DataGridViewCellStyle();

		#endregion

		#region Factory Methods

		public ExpedienteMngForm()
			: this(false) { }

		//Por defecto se abre Alimentacion.
		public ExpedienteMngForm(bool isModal)
			: this(isModal, null) { }

		public ExpedienteMngForm(bool isModal, Form parent)
			: this(isModal, parent, moleQule.Store.Structs.ETipoExpediente.Alimentacion) { }

		public ExpedienteMngForm(moleQule.Store.Structs.ETipoExpediente t)
			: this(null, t) { }

		public ExpedienteMngForm(moleQule.Store.Structs.ETipoExpediente t, ExpedienteList list)
			: this(false, null, list, t) { }

		public ExpedienteMngForm(Form parent, moleQule.Store.Structs.ETipoExpediente t)
			: this(false, parent, t) { }

		public ExpedienteMngForm(bool isModal, Form parent, moleQule.Store.Structs.ETipoExpediente t)
			: this(isModal, parent, null, t) { }

		public ExpedienteMngForm(Form parent, moleQule.Store.Structs.ETipoExpediente t, ExpedienteList list)
			: this(false, parent, list, t) { }
		
		public ExpedienteMngForm(bool isModal, Form parent, ExpedienteList list, moleQule.Store.Structs.ETipoExpediente t)
			: base(isModal, parent, list)
		{
			InitializeComponent();
			SetView();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ExpedienteList.NewList().GetSortedList();
			SortProperty = Codigo.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_tipo_expediente = t;			
		}

		#endregion

		#region Layout & Format

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			switch (_tipo_expediente)
			{
				case moleQule.Store.Structs.ETipoExpediente.Almacen:

					this.Icon = Properties.Resources.store;

					Kilos.DefaultCellStyle.Format = "N0";
					StockKilos.DefaultCellStyle.Format = "N0";
					StockBultos.DefaultCellStyle.Format = "N2";
					
					PuertoOrigen.Visible = false;
					PuertoDestino.Visible = false;
					Proveedor.Visible = false;
					NombreCliente.Visible = false;
					Naviera.Visible = false;
					NombreTransOrigen.Visible = false;
					NombreTransDest.Visible = false;
					Contenedor.Visible = false;
					FechaDespachoDestino.Visible = false;
					FechaEmbarque.Visible = false;
					FechaLlegadaMuelle.Visible = false;
					FechaSalidaMuelle.Visible = false;
					FechaRegresoMuelle.Visible = false;

					Observaciones.Tag = 0.6;
					TipoMercancia.Tag = 0.4;

					cols.Add(Observaciones);
					cols.Add(TipoMercancia);

					break;

				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:

					this.Icon = Properties.Resources.alimentacion;

					Kilos.DefaultCellStyle.Format = "N0";
					StockKilos.DefaultCellStyle.Format = "N0";
					StockBultos.DefaultCellStyle.Format = "N2";

					Observaciones.Tag = 0.3;
					Proveedor.Tag = 0.2;
					NombreCliente.Tag = 0.15;
					TipoMercancia.Tag = 0.35;

					cols.Add(Observaciones);
					cols.Add(Proveedor);
					cols.Add(NombreCliente);
					cols.Add(TipoMercancia);

					break;

				case moleQule.Store.Structs.ETipoExpediente.Ganado:
					try
					{
						this.Icon = Properties.Resources.ganado;

						Tabla.Columns.Remove(TipoMercancia.Name);
						Tabla.Columns.Remove(StockKilos.Name);
						Tabla.Columns.Remove(StockBultos.Name);

						Kilos.HeaderText = "Cabezas";
						Kilos.Width = TextRenderer.MeasureText("Cabezas ", this.Font).Width;
						Kilos.DataPropertyName = "NCabezas";

						Observaciones.Tag = 0.6;
						Proveedor.Tag = 0.2;
						NombreCliente.Tag = 0.2;

						cols.Add(Observaciones);
						cols.Add(Proveedor);
						cols.Add(NombreCliente);
					}
					catch { }
					break;

				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
					try
					{
						this.Icon = Properties.Resources.maquinaria;

						Tabla.Columns.Remove(TipoMercancia.Name);
						Tabla.Columns.Remove(StockKilos.Name);
						Tabla.Columns.Remove(StockBultos.Name);

						Kilos.HeaderText = "Máquinas";
						Kilos.Width = TextRenderer.MeasureText("Máquinas ", this.Font).Width;
						Kilos.DataPropertyName = "NMaquinas";

						Observaciones.Tag = 0.6;
						Proveedor.Tag = 0.2;
						NombreCliente.Tag = 0.2;

						cols.Add(Observaciones);
						cols.Add(Proveedor);
						cols.Add(NombreCliente);
					}
					catch { }
					break;

                case moleQule.Store.Structs.ETipoExpediente.Project:
				case moleQule.Store.Structs.ETipoExpediente.Work:

					this.Icon = (_tipo_expediente == moleQule.Store.Structs.ETipoExpediente.Work)
                                    ? Properties.Resources.work
                                    : Properties.Resources.project;

					TipoMercancia.HeaderText = "Descripción";

					Kilos.DefaultCellStyle.Format = "N0";
					StockKilos.DefaultCellStyle.Format = "N0";
					StockBultos.DefaultCellStyle.Format = "N2";
					Kilos.Visible = false;
					StockKilos.Visible = false;
					StockBultos.Visible = false;

					PuertoOrigen.Visible = false;
					PuertoDestino.Visible = false;
					Proveedor.Visible = false;
					Naviera.Visible = false;
					NombreTransOrigen.Visible = false;
					NombreTransDest.Visible = false;
					Contenedor.Visible = false;
					FechaDespachoDestino.Visible = false;
					FechaLlegadaMuelle.Visible = false;
					FechaSalidaMuelle.Visible = false;

					FechaEmbarque.Visible = true;
					FechaEmbarque.HeaderText = "Fecha Inicio";
					FechaRegresoMuelle.Visible = true;
					FechaRegresoMuelle.HeaderText = "Fecha Fin";

					NombreCliente.Tag = 0.2;
					Observaciones.Tag = 0.4;
					TipoMercancia.Tag = 0.4;

					cols.Add(NombreCliente);
					cols.Add(Observaciones);
					cols.Add(TipoMercancia);

					break;

				case moleQule.Store.Structs.ETipoExpediente.Todos:
					try
					{
						TipoMercancia.Tag = 0.3;
						Observaciones.Tag = 0.3;
						Proveedor.Tag = 0.2;
						NombreCliente.Tag = 0.2;

						cols.Add(TipoMercancia);
						cols.Add(Observaciones);
						cols.Add(Proveedor);
						cols.Add(NombreCliente);
					}
					catch { }
					break;
			}

			ControlsMng.MaximizeColumns(Tabla, cols);

			Face.ControlTools.Instance.CopyBasicStyle(CompleteStyle);
			CompleteStyle.BackColor = Color.Gainsboro;

			Face.ControlTools.Instance.CopyBasicStyle(MarkedStyle);
			MarkedStyle.BackColor = Color.LightGreen;

			Face.ControlTools.Instance.CopyBasicStyle(UnmarkedStyle);
			UnmarkedStyle.ForeColor = Color.Transparent;
			UnmarkedStyle.SelectionForeColor = Color.Transparent;
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

            ExpedientInfo item = (ExpedientInfo)row.DataBoundItem;

            if ((item.ETipo == moleQule.Store.Structs.ETipoExpediente.Alimentacion) ||
                (item.ETipo == moleQule.Store.Structs.ETipoExpediente.Ganado) ||
                (item.ETipo == moleQule.Store.Structs.ETipoExpediente.Maquinaria))
            {
                if ((item.FechaRegresoMuelle == DateTime.MinValue) ||
                    (item.FechaSalidaMuelle == DateTime.MinValue) ||
                    (item.FechaDespachoDestino == DateTime.MinValue) ||
                    (item.FechaLlegadaMuelle == DateTime.MinValue) ||
                    (item.FechaEmbarque == DateTime.MinValue))
                {
                    row.DefaultCellStyle = BasicStyle;

                    row.Cells[FechaEmbarque.Index].Style = (item.FechaEmbarque != DateTime.MinValue) ? MarkedStyle : UnmarkedStyle;
                    row.Cells[FechaLlegadaMuelle.Index].Style = (item.FechaLlegadaMuelle != DateTime.MinValue) ? MarkedStyle : UnmarkedStyle;
                    row.Cells[FechaDespachoDestino.Name].Style = (item.FechaDespachoDestino != DateTime.MinValue) ? MarkedStyle : UnmarkedStyle;
                    row.Cells[FechaSalidaMuelle.Name].Style = (item.FechaSalidaMuelle != DateTime.MaxValue) ? MarkedStyle : UnmarkedStyle;
                    row.Cells[FechaRegresoMuelle.Name].Style = (item.FechaRegresoMuelle != DateTime.MinValue) ? MarkedStyle : UnmarkedStyle;

                }
                else
                    row.DefaultCellStyle = CompleteStyle;

                //switch (item.PuertoDestino)
                //{
                //    case "TENERIFE": row.DefaultCellStyle.ForeColor = Color.Red; break;
                //    case "LA PALMA": row.DefaultCellStyle.ForeColor = Color.Green; break;
                //    default: row.DefaultCellStyle.ForeColor = Face.Common.ControlTools.Instance.AbiertoStyle.ForeColor; break;
                //}
            }
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					ShowAction(molAction.ShowDocuments);
					
					Naviera.Visible = false;
					NombreTransDest.Visible = false;
					NombreTransOrigen.Visible = false;
					PuertoDestino.Visible = false;
					FechaDespachoDestino.Visible = false;
					FechaEmbarque.Visible = false;
					FechaLlegadaMuelle.Visible = false;
					FechaSalidaMuelle.Visible = false;
					FechaRegresoMuelle.Visible = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Add);
					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Expedient");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ExpedienteList.GetList(_tipo_expediente, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Expedients");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						ExpedienteList listA = ExpedienteList.GetList(_filter_results);
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
						ExpedienteList listD = ExpedienteList.GetList(_filter_results);
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
            moleQule.Store.Structs.ETipoExpediente tipo = ActiveItem == null ? _tipo_expediente : ActiveItem.ETipoExpediente;

			switch (tipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Almacen:
					{
						ExpedienteAlmacenAddForm form = new ExpedienteAlmacenAddForm(this);
						AddForm(form);
						if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
					}
					break;

				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
				case moleQule.Store.Structs.ETipoExpediente.Ganado:
				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
					{
						ContenedorAddForm form = new ContenedorAddForm(tipo, this);
						AddForm(form);
						if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
					}
					break;

                case moleQule.Store.Structs.ETipoExpediente.Project:
                    {
                        ProjectAddForm form = new ProjectAddForm(this);
                        AddForm(form);
                        if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
                    }
                    break;

				case moleQule.Store.Structs.ETipoExpediente.Work:
					{
						WorkAddForm form = new WorkAddForm(this);
						AddForm(form);
						if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
					}
					break;

			}
		}

		public override void OpenViewForm()
        {
            moleQule.Store.Structs.ETipoExpediente tipo = ActiveItem == null ? _tipo_expediente : ActiveItem.ETipoExpediente;

			switch (tipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Almacen:
					{
						ExpedienteAlmacenViewForm form = new ExpedienteAlmacenViewForm(ActiveOID, this);
						AddForm(form);
						_entity = form.Entity;
					}
					break;

				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
				case moleQule.Store.Structs.ETipoExpediente.Ganado:
				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
					{
						ContenedorViewForm form = new ContenedorViewForm(ActiveOID, tipo, this);
						AddForm(form);
						_entity = form.Entity;
					}
					break;

                case moleQule.Store.Structs.ETipoExpediente.Project:
                    {
                        ProjectViewForm form = new ProjectViewForm(ActiveOID, this);
                        AddForm(form);
                        _entity = form.Entity;
                    }
                    break;

				case moleQule.Store.Structs.ETipoExpediente.Work:
					{
						WorkViewForm form = new WorkViewForm(ActiveOID, this);
						AddForm(form);
						_entity = form.Entity;
					}
					break;
			}
		}

		public override void OpenEditForm()
        {
            moleQule.Store.Structs.ETipoExpediente tipo = ActiveItem == null ? _tipo_expediente : ActiveItem.ETipoExpediente;

			switch (tipo)
			{
				case moleQule.Store.Structs.ETipoExpediente.Almacen:
					{
						ExpedienteAlmacenEditForm form = new ExpedienteAlmacenEditForm(ActiveOID, this);
						if (form.Entity != null)
						{
							AddForm(form);
							_entity = form.Entity;
						}
					}
					break;

				case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
				case moleQule.Store.Structs.ETipoExpediente.Ganado:
				case moleQule.Store.Structs.ETipoExpediente.Maquinaria:
					{
						ContenedorEditForm form = new ContenedorEditForm(ActiveOID, tipo, this);
						AddForm(form);
						_entity = form.Entity;
					}
					break;

                case moleQule.Store.Structs.ETipoExpediente.Project:
                    {
                        ProjectEditForm form = new ProjectEditForm(ActiveOID, this);
                        if (form.Entity != null)
                        {
                            AddForm(form);
                            _entity = form.Entity;
                        }
                    }
                    break;

				case moleQule.Store.Structs.ETipoExpediente.Work:
					{
						WorkEditForm form = new WorkEditForm(ActiveOID, this);
						if (form.Entity != null)
						{
							AddForm(form);
							_entity = form.Entity;
						}
					}
					break;
			}
		}

		public override void DeleteObject(long oid)
		{
			ExpedientInfo exp = ExpedientInfo.Get(oid, false);

            exp.LoadChilds(typeof(Batch), false, true);
			if (exp.Partidas.Count > 0)
			{
				MessageBox.Show("El expediente tiene productos asociados",
						moleQule.Face.Resources.Labels.ADVISE_TITLE,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);

				_action_result = DialogResult.Ignore;
				return;
			}

            exp.LoadChilds(typeof(Maquinaria), false, true);
			if (exp.Maquinarias != null && exp.Maquinarias.Count > 0)
			{
				MessageBox.Show("El expediente tiene maquinas asociadas",
						moleQule.Face.Resources.Labels.ADVISE_TITLE,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);

				_action_result = DialogResult.Ignore;
				return;
			}

			Expedient.Delete(oid);
			_action_result = DialogResult.OK;

			ExpedienteList cache = Cache.Instance.Get(typeof(ExpedienteList)) as ExpedienteList;
			if (cache != null) cache.RemoveItem(oid);

			//Se eliminan todos los formularios de ese objeto
			foreach (EntityDriverForm form in _list_active_form)
			{
				if (form is ItemMngBaseForm)
				{
					if (((ItemMngBaseForm)form).Oid == oid)
					{
						form.Dispose();
						break;
					}
				}
			}
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

		#endregion

		#region Events
		
		private void ExpedienteMngForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Cache.Instance.Remove(typeof(ExpedienteList));
		}

		#endregion
	}

	public partial class ExpedienteBaseMngForm : Skin06.EntityMngSkinForm<ExpedienteList, ExpedientInfo>
	{
		public ExpedienteBaseMngForm()
			: this(false, null, null) { }

		public ExpedienteBaseMngForm(bool isModal, Form parent, ExpedienteList lista)
			: base(isModal, parent, lista) { }
	}

	public class ExpedienteAlmacenMngForm : ExpedienteMngForm
	{
		#region Attributes & Properties

		public new const string ID = "ExpedienteAlmacenMngForm";
		public new static Type Type { get { return typeof(ExpedienteAlmacenMngForm); } }

		#endregion

		#region Factory Methods

		public ExpedienteAlmacenMngForm(Form parent)
			: base(parent, moleQule.Store.Structs.ETipoExpediente.Almacen)
		{
			this.Text = Resources.Labels.ALMACEN;
		}

		#endregion

		#region Actions

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ExpedienteAlListRpt report = reportMng.GetAlListReport(ExpedienteList.GetList((IList<ExpedientInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public class ExpedienteAlimentacionMngForm : ExpedienteMngForm
	{
		#region Attributes & Properties

		public new const string ID = "ExpedienteAlimentacionMngForm";
		public new static Type Type { get { return typeof(ExpedienteAlimentacionMngForm); } }

		#endregion

		#region Factory Methods

		public ExpedienteAlimentacionMngForm(Form parent, ExpedienteList list, string title)
			: base(parent, moleQule.Store.Structs.ETipoExpediente.Alimentacion, list)
		{
			this.Text = title;
		}

		#endregion

		#region Actions

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ExpedienteAlListRpt report = reportMng.GetAlListReport(ExpedienteList.GetList((IList<ExpedientInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public class ExpedienteGanadoMngForm : ExpedienteMngForm
	{
		#region Attributes & Properties

		public new const string ID = "ExpedienteGanadoMngForm";
		public new static Type Type { get { return typeof(ExpedienteGanadoMngForm); } }

		#endregion

		#region Factory Methods

		public ExpedienteGanadoMngForm(Form parent, ExpedienteList list, string title)
			: base(parent, moleQule.Store.Structs.ETipoExpediente.Ganado, list)
		{
			this.Text = title;
		}

		#endregion

		#region Actions

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ExpedienteGaListRpt report = reportMng.GetGaListReport(ExpedienteList.GetList((IList<ExpedientInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public class ExpedienteMaquinariaMngForm : ExpedienteMngForm
	{
		#region Attributes & Properties

		public new const string ID = "ExpedienteMaquinariaMngForm";
		public new static Type Type { get { return typeof(ExpedienteMaquinariaMngForm); } }

		#endregion

		#region Factory Methods

		public ExpedienteMaquinariaMngForm(Form parent, ExpedienteList list, string title)
			: base(parent, moleQule.Store.Structs.ETipoExpediente.Maquinaria, list)
		{
			this.Text = title;
		}

		#endregion

		#region Actions

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ExpedienteMaListRpt report = reportMng.GetMaListReport(ExpedienteList.GetList((IList<ExpedientInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

    public class ProjectMngForm : ExpedienteMngForm
    {
        #region Attributes & Properties

        public new const string ID = "ProjectMngForm";
        public new static Type Type { get { return typeof(ProjectMngForm); } }

        #endregion

        #region Factory Methods

        public ProjectMngForm(Form parent, string title)
            : base(parent, moleQule.Store.Structs.ETipoExpediente.Project)
        {
            this.Text = title;
        }

        #endregion

        #region Actions

        public override void PrintList()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            ExpedienteAlListRpt report = reportMng.GetAlListReport(ExpedienteList.GetList((IList<ExpedientInfo>)Datos.List));

            PgMng.FillUp();
            ShowReport(report);
        }

        #endregion
    }

	public class WorkMngForm : ExpedienteMngForm
	{
		#region Attributes & Properties

		public new const string ID = "WorkMngForm";
		public new static Type Type { get { return typeof(WorkMngForm); } }

		#endregion

		#region Factory Methods

		public WorkMngForm(Form parent, string title)
			: base(parent, moleQule.Store.Structs.ETipoExpediente.Work)
		{
			this.Text = title;
		}

		#endregion

		#region Actions

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ExpedienteAlListRpt report = reportMng.GetAlListReport(ExpedienteList.GetList((IList<ExpedientInfo>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}
}
