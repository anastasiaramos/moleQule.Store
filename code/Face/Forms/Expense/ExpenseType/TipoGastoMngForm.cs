using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.CslaEx;

using moleQule;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports;
using moleQule.Face;

namespace moleQule.Face.Store
{
	public partial class TipoGastoMngForm : TipoGastoMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "TipoGastoMngForm";
		public static Type Type { get { return typeof(TipoGastoMngForm); } }
		public override Type EntityType { get { return typeof(TipoGasto); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public TipoGasto _entity;

		#endregion
		
		#region Factory Methods

		public TipoGastoMngForm()
            : this(null) {}
			
		public TipoGastoMngForm(Form parent)
			: this(false, parent) {}
		
		public TipoGastoMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}
		
		public TipoGastoMngForm(bool is_modal, Form parent, TipoGastoList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();
			SetView();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
            Datos.DataSource = TipoGastoList.NewList().GetSortedList();			
			SortProperty = Nombre.DataPropertyName;
        }
		
		#endregion
	
		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.3;
			Observaciones.Tag = 0.7;

			cols.Add(Nombre);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);   
		}

		public override void FormatControls()
		{
            if (Tabla == null) return;
			
			base.FormatControls();
		}
		
		protected override void SetRowFormat(DataGridViewRow row)
        {
            /*if (row.IsNewRow) return;
            
			TipoGastoInfo item = (TipoGastoInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);*/
        }
		
		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "TipoGasto");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = TipoGastoList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de TipoGastos");
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
                        TipoGastoList listA = TipoGastoList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Unlock:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        TipoGastoList listD = TipoGastoList.GetList(_filter_results);
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
			TipoGastoAddForm form = new TipoGastoAddForm();
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{			
			AddForm(new TipoGastoViewForm(ActiveOID));
		}

        public override void OpenEditForm() 
        {             
			TipoGastoEditForm form = new TipoGastoEditForm(ActiveOID);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteObject(long oid)
		{
			TipoGasto.Delete(oid);
			_action_result = DialogResult.OK;
		}

		/// <summary>Duplica un objeto y abre el formulario para editar item
		/// <returns>void</returns>
		/// </summary>
		/*public override void DuplicateObject(long oid) 
		{
			TipoGasto old = TipoGasto.Get(oid);
			TipoGasto dup = old.CloneAsNew();
			old.CloseSession();
			
			AddForm(new TipoGastoAddForm(dup));

		}*/

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public override void PrintList() 
		{
			/*TipoGastoReportMng reportMng = new TipoGastoReportMng(AppContext.ActiveSchema);
			
			TipoExpenseListRpt report = reportMng.GetListReport(List);
			
			if (report != null)
			{
				ReportViewer.SetReport(report);
				ReportViewer.ShowDialog();
			}
			else
			{
				MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
								moleQule.Face.Resources.Labels.ADVISE_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}*/
		}

		#endregion
    }

    public partial class TipoGastoMngBaseForm : Skin06.EntityMngSkinForm<TipoGastoList, TipoGastoInfo>
	{
		public TipoGastoMngBaseForm()
			: this(false, null, null) { }

        public TipoGastoMngBaseForm(bool isModal, Form parent, TipoGastoList lista)
			: base(isModal, parent, lista) { }
	}
}
