using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Store.Structs;
using moleQule.Serie;

namespace moleQule.Face.Store
{
    public partial class SerieForm : Skin01.ItemMngSkinForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 1; } }

		public virtual Serie.Serie Entity { get { return null; } set { } }
		public virtual SerieInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public SerieForm() 
			: this(-1, null) {}

		public SerieForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			MaximizeForm(new Size(625, 480));

            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Familia.Tag = 1;

            cols.Add(Familia);

            ControlsMng.MaximizeColumns(Familias_DG, cols);

            Familias_DG.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

		public override void RefreshSecondaryData()
		{
			Datos_Tipo.DataSource = moleQule.Store.Structs.EnumText<ETipoSerie>.GetList();
            PgMng.Grow();
		}
		
        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        public virtual void NuevaFamiliaAction() { }
        public virtual void BorrarFamiliaAction() { }

        #endregion

        #region Buttons

        private void Add_Button_Click(object sender, EventArgs e)
        {
            NuevaFamiliaAction();
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            BorrarFamiliaAction();
        }

        #endregion

        #region Events

        #endregion

    }
}

