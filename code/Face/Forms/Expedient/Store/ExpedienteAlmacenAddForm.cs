using System;
using System.Windows.Forms;
using System.ComponentModel;

using moleQule;
using moleQule.Face;

using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ExpedienteAlmacenAddForm : ExpedienteAlmacenUIForm
    {

        #region Factory Methods

        public ExpedienteAlmacenAddForm()
            : this(null) {}

		public ExpedienteAlmacenAddForm(Form parent)
            : base(-1, parent)
        {
            InitializeComponent();

            SetFormData();

            _mf_type = ManagerFormType.MFAdd;
            this.Text = Resources.Labels.EXPEDIENT_ADD_TITLE;
        }

		protected override void GetFormSourceData(object[] parameters)
		{
			moleQule.Store.Structs.ETipoExpediente tipo = (moleQule.Store.Structs.ETipoExpediente)parameters[0];

            _entity = Expedient.New(tipo);
            _entity.BeginEdit();
		}

		#endregion

		#region Business Methods

		protected override void LoadStock() { }

		#endregion

	}
}

