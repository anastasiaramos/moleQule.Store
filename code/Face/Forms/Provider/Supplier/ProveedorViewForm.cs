using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class ProveedorViewForm : SupplierForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private ProveedorInfo _entity;

        public override ProveedorInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar
        /// </summary>
        private ProveedorViewForm() 
			: this(-1, ETipoAcreedor.Proveedor, null) { }

        public ProveedorViewForm(long oid, ETipoAcreedor providerType,  Form parent)
            : base(oid, providerType, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = ProveedorInfo.Get(oid, (ETipoAcreedor)parameters[0], true);
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            SetReadOnlyControls(this.Controls);
			base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Bar.Grow();

			Datos_Productos.DataSource = _entity.Productos;

            base.RefreshMainData();
        }

        #endregion

        #region Validation & Format


        #endregion

        #region Print

        //public override void PrintData(long entidad, PrintSource source, PrintType type)
        //{
        //    switch (entidad)
        //    {
        //        case Entidad.Historia:
        //            {
        //                ClienteReportMng rptMng = new ClienteReportMng(AppContext.ActiveSchema);
        //                List<HistoriaInfo> list = new List<HistoriaInfo>();

        //                switch (source)
        //                {
        //                    case PrintSource.All:
        //                        {
        //                            foreach (DataGridViewRow row in Historias_Grid.Rows)
        //                                list.Add((HistoriaInfo)(row.DataBoundItem));

        //                        } break;

        //                    case PrintSource.Selection:
        //                        {
        //                            foreach (DataGridViewRow row in Historias_Grid.SelectedRows)
        //                                list.Add((HistoriaInfo)(row.DataBoundItem));

        //                        } break;
        //                }

        //                if (list.Count == 0) return;

        //                ReportViewer.SetReport(rptMng.GetHistoriaListReport(EntityInfo,
        //                                                                HistoriaList.GetChildList(list)));

        //            } break;
        //    }

        //    ReportViewer.ShowDialog();
        //}


        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
    }

}
