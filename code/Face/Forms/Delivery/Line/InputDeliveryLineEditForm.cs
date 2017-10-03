using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule;
using moleQule.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Serie;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class InputDeliveryLineEditForm : InputDeliveryLineUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoAlbaranProveedorEditForm";
        public static Type Type { get { return typeof(InputDeliveryLineEditForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        #endregion

        #region Factory Methods

		public InputDeliveryLineEditForm(InputDelivery delivery, SerieInfo serie, IAcreedorInfo provider, InputDeliveryLine line, Form parent)
            : base(line, delivery, serie, provider, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            Productos_BT.Enabled = false;
            base.FormatControls();

			SetStockControl(false);
        }

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;

            _product = ProductInfo.Get(_entity.OidProducto, true);

            Products_BS.DataSource = _product;
			PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Business Methods

        #endregion

        #region Actions

		protected override void SubmitAction()
		{
			if (_product.ETipoFacturacion == ETipoFacturacion.Unitaria)
			{
                if ((_entity.CantidadKilos > 1) || _entity.CantidadBultos > 1)
				{
					PgMng.ShowInfoException(Resources.Messages.MAX_CANTIDAD_EXPEDIENTE_UNO);

					_action_result = DialogResult.Ignore;
					return;
				}
			}

			base.SubmitAction();
		}

        protected override void CancelAction()
        {
            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}

