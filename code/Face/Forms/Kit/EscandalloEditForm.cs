using System;
using System.Windows.Forms;

using moleQule.CslaEx;

using moleQule.Library.Store;
using moleQule;
using moleQule.Face;

namespace moleQule.Face.Store
{
	public partial class EscandalloEditForm : EscandalloUIForm
    {
        #region Factory Methods

        public EscandalloEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (Entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid)
		{
			_entity = Product.Get(oid);
			_entity.BeginEdit();
		}

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            Serie_BT.Enabled = false;
            base.FormatControls();
        }
        
        #endregion

        #region Buttons

		#endregion 

    }
}

