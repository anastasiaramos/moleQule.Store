using System;
using System.Windows.Forms;
using System.ComponentModel;

using moleQule.Face;
using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class ContenedorAddForm : ContenedorUIForm
    {
        #region Factory Methods

        public ContenedorAddForm(moleQule.Store.Structs.ETipoExpediente tipo)
            : this(tipo, null) {}

		public ContenedorAddForm(moleQule.Store.Structs.ETipoExpediente tipo, Form parent)
            : base(-1, tipo, parent)
        {
            InitializeComponent();

            SetFormData();

            _mf_type = ManagerFormType.MFAdd;

            switch (tipo)
            {
                case moleQule.Store.Structs.ETipoExpediente.Alimentacion:
                    this.Text = String.Format(Resources.Labels.CONTAINER_ADD_TITLE, _entity.Codigo);
                    break;

                default:
                    this.Text = String.Format(Resources.Labels.EXPEDIENT_ADD_TITLE, _entity.Codigo);
                    break;
            }
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

