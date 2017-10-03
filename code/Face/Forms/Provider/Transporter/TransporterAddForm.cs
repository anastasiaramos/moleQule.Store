using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class TransporterAddForm : TransporterUIForm
    {
        #region Factory Methods

		public TransporterAddForm()
			: this(null, ETipoAcreedor.TransportistaOrigen) { }

		public TransporterAddForm(Form parent)
			: this(parent, ETipoAcreedor.TransportistaOrigen) { }

		public TransporterAddForm(Form parent, ETipoAcreedor providerType)
            : base(-1, providerType, parent)
        {
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			ETipoAcreedor providerType = (ETipoAcreedor)parameters[0];

			_entity = Transporter.New(providerType);
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	
	}
}

