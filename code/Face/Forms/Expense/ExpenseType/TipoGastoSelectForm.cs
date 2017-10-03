using System;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class TipoGastoSelectForm : TipoGastoMngForm
    {

        #region Factory Methods

        public TipoGastoSelectForm()
            : this(null) {}

        public TipoGastoSelectForm(Form parent)
            : this(parent, null) {}

        public TipoGastoSelectForm(Form parent, TipoGastoList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            PgMng.Grow(string.Empty, "TipoGasto");

            long oid = ActiveOID;

            switch (DataType)
            {
                case EntityMngFormTypeData.Default:
                    List = TipoGastoList.GetSelectList(false);
                    break;

                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;
            }
            PgMng.Grow(string.Empty, "Lista de TipoGastos");
        }

        #endregion

        #region Actions

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
