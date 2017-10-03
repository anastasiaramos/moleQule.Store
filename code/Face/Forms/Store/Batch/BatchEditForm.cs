using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class BatchEditForm : Skin01.ActionSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public const string ID = "BatchEditForm";
		public static Type Type { get { return typeof(BatchEditForm); } }

		Batch _entity;
        Batch _source;
        private Expedient _expediente;

        #endregion

        #region Factory Methods

        public BatchEditForm(Batch batch, Expedient exp)
            : this(true, batch, exp) { }

        public BatchEditForm(bool isModal, Batch batch, Expedient exp)
			: base(isModal)
        {
            InitializeComponent();
            _source = batch;
            _entity = batch.Clone();
			_expediente = exp;
            SetFormData();
        }

        #endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
        }

        public override void RefreshSecondaryData()
        {
			PrecioProducto_NTB.Text = ProductInfo.Get(_entity.OidProducto).PrecioCompra.ToString("N5");
            PgMng.Grow();
        }

        #endregion

        #region Buttons

        protected override void SubmitAction()
        {
			_source.CopyFrom(_entity);

			_expediente.UpdateAyudaEstimada();
			_expediente.UpdateAyudas();

			_action_result = DialogResult.OK;
        }

        #endregion

        #region Events

        #endregion
    }
}

