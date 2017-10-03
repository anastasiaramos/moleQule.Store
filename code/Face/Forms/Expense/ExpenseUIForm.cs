using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
	public partial class ExpenseUIForm : ExpenseForm
	{
		#region Business Methods

        protected Expense _entity;

        public override Expense Entity { get { return _entity; } set { _entity = value; } }
		public override ExpenseInfo EntityInfo	{ get { return (_entity != null) ? _entity.GetInfo() : null; } }

		public TipoGastoInfo _tipo = null;

        #endregion

        #region Factory Methods

        public ExpenseUIForm() 
			: this(-1, null, ECategoriaGasto.Otros) {}

		public ExpenseUIForm(long oid, Form parent)
			: this(oid, parent, ECategoriaGasto.Otros) { }

        public ExpenseUIForm(long oid, Form parent, ECategoriaGasto categoria)
            : this(oid, new object[2] { null, categoria }, true, parent) {}

        public ExpenseUIForm(Expense entity, Form parent)
            : this(-1, new object[1] { entity }, true, parent) {}

		public ExpenseUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Expense temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
			catch (Exception ex)
			{
				CleanCache();
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
				PgMng.FillUp();
			}
        }

        #endregion

        #region Layout & Source

		public override void FormatControls()
		{
			base.FormatControls();

			Categoria_BT.Enabled = (_tipo != null);
		}

        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
            PgMng.Grow();
            
            base.RefreshMainData();
		}

		public override void RefreshSecondaryData()
		{
			_tipo = TipoGastoInfo.Get(_entity.OidTipo, false);
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

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected void SetTipoAction()
		{
			TipoGastoSelectForm form = new TipoGastoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_tipo = form.Selected as TipoGastoInfo;
				_entity.OidTipo = _tipo.Oid;
				_entity.Tipo = _tipo.Nombre;
				if (_entity.Descripcion == string.Empty) _entity.Descripcion = _tipo.Nombre;
				_entity.CategoriaGasto = _tipo.Categoria;
				_entity.PrevisionPago = moleQule.Common.EnumFunctions.GetPrevisionPago(_tipo.EFormaPago, _entity.Fecha, _tipo.DiasPago);

				Categoria_BT.Enabled = false;
			}
		}

        protected void SetCategoriaAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			ECategoriaGasto[] list = { ECategoriaGasto.Administracion, ECategoriaGasto.Bancario, ECategoriaGasto.Impuesto, ECategoriaGasto.SeguroSocial, ECategoriaGasto.Otros };
			form.SetDataSource(moleQule.Store.Structs.EnumText<ECategoriaGasto>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource categoria = form.Selected as ComboBoxSource;
				_entity.CategoriaGasto = categoria.Oid;
			}
        }

		protected void SetEstadoAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Abierto, moleQule.Base.EEstado.Anulado, moleQule.Base.EEstado.Contabilizado, moleQule.Base.EEstado.Exportado };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
			}
		}

        #endregion

        #region Buttons

		private void Tipo_BT_Click(object sender, EventArgs e)
		{
			SetTipoAction();
		}

		private void Categoria_BT_Click(object sender, EventArgs e)
		{
			SetCategoriaAction();
		}

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SetEstadoAction();
		}

        #endregion
        
        #region Events

		private void Fecha_DTP_Validated(object sender, EventArgs e)
		{
			if ((_tipo != null) && (_tipo.Oid != 0))
				_entity.PrevisionPago = moleQule.Common.EnumFunctions.GetPrevisionPago(_tipo.EFormaPago, _entity.Fecha, _tipo.DiasPago);
		}

        #endregion
	}
}