using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.CslaEx;
using moleQule;
using moleQule.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Serie;

namespace moleQule.Face.Store
{
	public partial class SerieUIForm : SerieForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata de la Serie actual y que se va a editar.
        /// </summary>
        protected moleQule.Serie.Serie _entity;

        public override moleQule.Serie.Serie Entity { get { return _entity; } set { _entity = value; } }
		public override SerieInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public SerieUIForm() 
			: this(-1, null) {}

		public SerieUIForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

        public SerieUIForm(moleQule.Serie.Serie Serie)
            : base()
        {
            InitializeComponent();
            _entity = Serie.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = moleQule.Serie.Serie.Get(oid);
            _entity.BeginEdit();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                moleQule.Serie.Serie temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();

                    return true;
                }
				catch (iQValidationException ex)
				{
					MessageBox.Show(iQExceptionHandler.GetAllMessages(ex) +
									Environment.NewLine + ex.SysMessage,
									Application.ProductName,
									MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation);
					return false;

				}
				catch (Exception ex)
				{
					MessageBox.Show(moleQule.Face.Resources.Messages.OPERATION_ERROR + Environment.NewLine +
									ex.Message,
                                    Application.ProductName,
									MessageBoxButtons.OK,
									MessageBoxIcon.Exclamation);
					return false;
				}
                finally
                {
                    this.Datos.RaiseListChangedEvents = true;
                }
            }

        }

        #endregion

        #region Layout & Source

		/// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
		public override void FormatControls()
		{
			base.FormatControls();
		}

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
            Bar.Grow();

            Datos_Familias.DataSource = _entity.SerieFamilias;
            Bar.Grow();

			base.RefreshMainData();
        }

        #endregion

		#region Validation & Format

		/// <summary>
		/// Valida datos de entrada
		/// </summary>
		protected override void ValidateInput()
		{
			
		}

		#endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            if (_entity.SerieFamilias.Count == 0)
            {
                MessageBox.Show(Resources.Messages.NO_FAMILIAS_ASOCIADAS);
                _action_result = DialogResult.Ignore;
                return;
            }
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        public override void NuevaFamiliaAction() 
        {
            FamilySelectForm form = new FamilySelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                FamiliaInfo item = form.Selected as FamiliaInfo;
               
                SerieFamilia newItem = _entity.SerieFamilias.NewItem(_entity, item);

                Datos_Familias.ResetBindings(false);
            }
        }

        public override void BorrarFamiliaAction()
        {
            if (Datos.Current == null) return;

            SerieFamilia item = Datos_Familias.Current as SerieFamilia;

            _entity.SerieFamilias.Remove(item);
            Datos_Familias.ResetBindings(false);
        }

        #endregion

        #region Buttons

        private void Impuesto_BT_Click(object sender, EventArgs e)
        {
            ImpuestoSelectForm form = new ImpuestoSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ImpuestoInfo item = form.Selected as ImpuestoInfo;
                _entity.SetImpuesto(item);
                Impuesto_TB.Text = _entity.Impuesto;
            }
        }

        private void Defecto_BT_Click(object sender, EventArgs e)
        {
            _entity.SetImpuesto(null);
            Impuesto_TB.Text = _entity.Impuesto;
        }

        #endregion
	}
}