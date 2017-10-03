using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LivestockBookLineViewForm : LivestockBookLineForm
    {
        #region Attributes & Properties

        public new const string ID = "LivestockBookLineViewForm";
		public new static Type Type { get { return typeof(LivestockBookLineViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private LivestockBookLineInfo _entity;

        public override LivestockBookLineInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public LivestockBookLineViewForm(long oid) 
            : this(oid, null) {}

        public LivestockBookLineViewForm(long oid, Form parent)
            : base(oid, ETipoLineaLibroGanadero.Todos, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
		{
            _entity = LivestockBookLineInfo.Get(oid);
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

        protected override void ShowPair()
        {
            if (_entity == null) return;

            Pair_BT.Enabled = _entity.EEstado == moleQule.Base.EEstado.Baja;
            PairID_LB.Visible = _entity.EEstado == moleQule.Base.EEstado.Baja;
            PairID_TB.Visible = _entity.EEstado == moleQule.Base.EEstado.Baja;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;

            base.RefreshMainData();
        }
		
        #endregion

        #region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion
    }
}