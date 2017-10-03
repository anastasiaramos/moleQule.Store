using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.CslaEx;

using moleQule;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
    public partial class EscandalloForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps; } }

		public virtual Product Entity { get { return null; } set { } }
        public virtual ProductInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public EscandalloForm()
			: this(-1) { }

        public EscandalloForm(long oid) 
			: this(oid, true, null) {}

        public EscandalloForm(long oid, bool isModal, Form parent)
            : this(oid, null, isModal, parent) {}

		public EscandalloForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            if (Componentes_DGW == null) return;

            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            TipoMercancia.Tag = 1;

            cols.Add(TipoMercancia);

            ControlsMng.MaximizeColumns(Componentes_DGW, cols);
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Print

        public override void PrintObject()
        {
/*            AlbaranReportMng reportMng = new AlbaranReportMng(AppContext.ActiveSchema);
            FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();
            ReportViewer.SetReport(reportMng.GetAlbaran(EntityInfo, conf));
            ReportViewer.ShowDialog();*/
        }

        #endregion

        #region Buttons
        
        protected override void PrintAction()
        {
            PrintObject();
        }

        #endregion

        #region Events

		private void Datos_DataSourceChanged(object sender, EventArgs e)
		{
			//SetDependentControlSource(ID_GB.Name);
		}

        #endregion

		private void Componentes_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
    }
}

