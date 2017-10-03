using System;
using System.Windows.Forms;

using moleQule;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Store
{
	public partial class SerieAddForm : SerieUIForm
    {
        #region Factory Methods

		public SerieAddForm()
			: this(null) { }

		public SerieAddForm(Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
			_entity = Serie.Serie.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	

		private void SerieAddForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_entity != null) _entity.CloseSession();
		}
	}
}

