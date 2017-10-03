using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ConceptoKitUIForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Batch _entity;
        protected Product _product;
        protected FamiliaInfo _familia;
        protected ExpedientInfo _expedient;
        protected BatchInfo _batch;

        protected BatchInfo ProExp { get { return _batch; } }
        protected ExpedientInfo Expedient { get { return _expedient; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        protected ConceptoKitUIForm() : base(false) { InitializeComponent(); }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConceptoKitUIForm(Product product, FamiliaInfo familia)
            : this(product, familia, null) { }

        /// Constructor
        /// </summary>
        public ConceptoKitUIForm(Product product, FamiliaInfo familia, ExpedientInfo exp)
            : base(true) 
        {
            InitializeComponent();

            _product = product;
            _familia = familia;
            _expedient = exp;
            SetFormData();
        }

        protected void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Proporcion":
                    if (_product.Partida == null) return;
                    if (ProExp == null) return;
                    _entity.KilosIniciales = _product.Partida.KilosIniciales * _entity.Proporcion / 100;
                    _entity.StockKilos = _entity.KilosIniciales;
                    _entity.BultosIniciales = _entity.KilosIniciales / ProExp.KilosPorBulto;
                    _entity.StockBultos = _entity.BultosIniciales;
                    break;
            }
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            if (Tabla_Productos == null) return;

            base.FormatControls();

            switch (_familia.ETipoFamilia)
            {
                case ETipoFamilia.Ganado:
                case ETipoFamilia.Maquinaria:

                    BultosIniciales.Visible = false;
                    StockBultos.Visible = false;
                    KiloPorBulto.Visible = false;

                    KilosIniciales.HeaderText = "Stock Inicial";
                    StockKilos.HeaderText = "Stock";

                    break;
            }
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData() {}

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (ProExp == null)
            {
                MessageBox.Show("Debe elegir un producto.",
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                
                _action_result = DialogResult.Ignore;
                return;
            }

            if (!(_entity.Proporcion > 0))
            {
                MessageBox.Show("Es necesario indicar la proporción de producto en la mezcla",
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (ProExp.StockKilos - _entity.KilosIniciales < 0)
            {
                MessageBox.Show(moleQule.Face.Store.Resources.Messages.STOCK_INSUFICIENTE + ProExp.StockKilos.ToString(),
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            DoSubmitAction();

            _action_result = DialogResult.OK;
        }

        protected virtual void DoSubmitAction() {}

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
            if (!IsModal) _entity.CancelEdit();

            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Business Methods

        protected virtual void ActualizaConcepto() {}

        #endregion

        #region Events
       
        private void Datos_Partida_CurrentChanged(object sender, EventArgs e)
        {
            ActualizaConcepto();
        }

        private void Proporcion_NTB_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                decimal value = Proporcion_NTB.DecimalValue;
                if (value < 0 || value > 100)
                    throw new Exception();
            }
            catch
            {
                _entity.Proporcion = 0;
                MessageBox.Show("El valor del campo Proporción debe estar entre 1 y 100",
                                    moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question);
            }
        }

        #endregion

    }
}

