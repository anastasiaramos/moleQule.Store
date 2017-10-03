using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ProductViewForm : ProductForm
	{
        #region Attributes & Properties 

        /// <summary>
        /// Se trata de la Producto actual y que se va a editar.
        /// </summary>
        private ProductInfo _entity;

        public override ProductInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public ProductViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = ProductInfo.Get(oid, true);

			if (_entity.IsKit) _entity.LoadChilds(typeof(Kit), false);
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            //this.Size = new System.Drawing.Size(833, 325);
            base.FormatControls();
        }

		#endregion

		#region Source

        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
			PgMng.Grow();

			Datos_Components.DataSource = _entity.Components;

			base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        protected override void ShowProductAction()
        {
            if (Datos_Components.Current == null) return;

            Kit item = Datos_Components.Current as Kit;

            ProductViewForm form = new ProductViewForm(item.OidProduct, this);

            form.ShowDialog();
        }

        #endregion
	}
}

