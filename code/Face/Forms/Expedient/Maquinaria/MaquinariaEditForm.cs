using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class MaquinariaEditForm : Skin01.ActionSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "MaquinariaEditForm";
        public static Type Type { get { return typeof(MaquinariaEditForm); } }

        Maquinaria _entity;
		Maquinaria _source;
        Batch _batch;

        #endregion

        #region Factory Methods

        public MaquinariaEditForm(Maquinaria maquina, Batch batch)
            : this(true, maquina, batch) {}

        public MaquinariaEditForm(bool is_modal, Maquinaria maquina, Batch batch)
			: base(is_modal)
        {
            InitializeComponent();
			_source = maquina;
			_entity = maquina.Clone();
			_batch = batch;
            SetFormData();
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
        }

        #endregion

        #region Buttons

        protected override void SubmitAction()
        {
			_source.CopyFrom(_entity);
			if (_batch != null) _batch.TipoMercancia = _batch.Producto + " - ID: " + _source.Identificador;
            _action_result = DialogResult.OK;
        }

        #endregion

        #region Events

        #endregion

    }
}

