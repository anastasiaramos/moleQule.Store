using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class SelectInputForm : moleQule.Face.Common.SelectInputForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps; } }

        private enum ETipoLista { Puertos };

        private SortedBindingList<PuertoInfo> _puertos;
		private SortedBindingList<RazaAnimalInfo> _razas;
		private SortedBindingList<WorkReportCategoryInfo> _work_report_categories;

        private Type _tipo;

        #endregion

        #region Factory Methods

		public SelectInputForm(PuertoList list)
            : base(true)
        {
            InitializeComponent();

			_puertos = PuertoList.SortList(list, "Valor", ListSortDirection.Ascending);
            _tipo = typeof(PuertoList);

			this.Text = Resources.Labels.SELECT_PUERTO_TITLE;

			SetFormData();
        }

		public SelectInputForm(RazaAnimalList list)
			: base(true)
		{
			InitializeComponent();

			_razas = RazaAnimalList.SortList(list, "Valor", ListSortDirection.Ascending);
			_tipo = typeof(RazaAnimalList);

			this.Text = Resources.Labels.RAZAANIMAL_TITLE;

			SetFormData();
		}

		public SelectInputForm(WorkReportCategoryList list)
			: base(true)
		{
			InitializeComponent();

			_work_report_categories = WorkReportCategoryList.SortList(list, "Name", ListSortDirection.Ascending);
			_tipo = typeof(WorkReportCategoryList);

			this.Text = Resources.Labels.SELECT_WORK_REPORT_CATEGORY;

			SetFormData();
		}

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            if (_tipo == typeof(PuertoList))
            {
                Datos.DataSource = _puertos;
                Nombre.DataPropertyName = "Valor";
                Numero.DataPropertyName = "Codigo";
                Numero.Visible = true;
            }
			else if (_tipo == typeof(RazaAnimalList))
			{
				Datos.DataSource = _razas;
				Nombre.DataPropertyName = "Valor";
				Numero.Visible = false;
			}
			else if (_tipo == typeof(WorkReportCategoryList))
			{
				Datos.DataSource = _work_report_categories;
				Nombre.DataPropertyName = "Name";
				Numero.Visible = false;
			}

            base.RefreshMainData();
        }

        #endregion
    }
}