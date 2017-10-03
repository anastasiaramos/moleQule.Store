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
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class EscandalloUIForm : EscandalloForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata de la Albaran actual y que se va a editar.
        /// </summary>
        protected Product _entity = null;

        public override Product Entity { get { return _entity; } set { _entity = value; } }
        public override ProductInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public EscandalloUIForm()
            : this(-1, null) {}

        public EscandalloUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        public EscandalloUIForm(Product product, Form parent)
            : base(-1, new object[1] { product }, true, parent)
        {
            InitializeComponent();
            _entity = product.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                Product temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();

                    //_entity.BeginEdit();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(iQExceptionHandler.GetAllMessages(ex),                                    
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

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Datos_PExpediente.DataSource = _entity.Partida;
			Datos_Componentes.DataSource = _entity.Partida.Componentes;
            Bar.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Validation & Format

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
			if (Entity.Partida.Componentes.Count == 0)
            {
                PgMng.ShowInfoException("No es posible guardar una mezcla sin la relación de componentes.");
                
                _action_result = DialogResult.Ignore;
                return;
            }

            if (_entity.Partida.KilosMezcla != _entity.Partida.KilosIniciales)
            {
                PgMng.ShowInfoException("El total de Kg de los productos no coincide con el total de Kg de la mezcla.");

                _action_result = DialogResult.Ignore;
                return;
            }

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        private void Serie_BT_Click(object sender, EventArgs e)
        {
            FamilySelectForm form = new FamilySelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                FamiliaInfo item = (FamiliaInfo)form.Selected;
                _entity.OidFamilia = item.Oid;
                _entity.Familia = item.Nombre;
                _entity.CodigoFamilia = item.Codigo.ToString(); 
            }
        }

        private void ImpuestoVenta_BT_Click(object sender, EventArgs e)
        {
            ImpuestoSelectForm form = new ImpuestoSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ImpuestoInfo item = form.Selected as ImpuestoInfo;
                _entity.SetImpuesto(item, ETipoSerie.Venta);
                ImpuestoVenta_TB.Text = _entity.ImpuestoVenta;
            }
        }

        private void DefectoVenta_BT_Click(object sender, EventArgs e)
        {
            _entity.SetImpuesto(null, ETipoSerie.Venta);
            ImpuestoVenta_TB.Text = _entity.ImpuestoVenta;
        }

        private void Expediente_BT_Click(object sender, EventArgs e)
        {
            if (_entity.OidFamilia == 0)
            {
                MessageBox.Show("Debe elegir una familia antes de poder asignar productos a esta Mezcla.",
                Face.Resources.Labels.ADVISE_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }

            if (_entity.Partida.KilosIniciales == 0)
            {
                MessageBox.Show("Debe indicar la cantidad total de kilos de la mezcla.",
                Face.Resources.Labels.ADVISE_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }

            if (_entity.Partida.KilosMezcla == _entity.Partida.KilosIniciales)
            {
                MessageBox.Show("Se ha alcanzado el total de kg de la mezcla.",
                Face.Resources.Labels.ADVISE_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                return;
            }

            ConceptoKitAddForm form = new ConceptoKitAddForm(_entity, FamiliaInfo.Get(_entity.OidFamilia));
            
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _entity.CalculaPrecioKit();
				if (_entity.Partida.Componentes.Count > 0)
                {
                    Serie_BT.Enabled = false;
                    Kilos_TB.Enabled = false;
                }
            }
        }

        private void Eliminar_Concepto_BT_Click(object sender, EventArgs e)
        {
            if (Datos_Componentes.Current == null) return;

            if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
            {
                Batch batch = (Batch)Datos_Componentes.Current;
				_entity.Partida.Componentes.Remove(batch);
                _entity.CalculaPrecioKit();
                Datos_Componentes.ResetBindings(false);
                Datos.ResetBindings(false);
            }

            _entity.CalculaPrecioKit();

			if (_entity.Partida.Componentes.Count > 0)
            {
                Serie_BT.Enabled = true;
                Kilos_TB.Enabled = true;                
            }
        }

        #endregion

        #region Events

        private void Kilos_TB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _entity.Partida.KilosIniciales = Convert.ToDecimal(Kilos_TB.Text);
            }
            catch { Kilos_TB.Text = "0,00"; }
        }

        #endregion

    }
}

