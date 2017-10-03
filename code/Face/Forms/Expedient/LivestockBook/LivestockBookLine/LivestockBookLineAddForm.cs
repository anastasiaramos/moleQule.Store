using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LivestockBookLineAddForm : LivestockBookLineUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "LineaLibroGanaderoAddForm";
		public new static Type Type { get { return typeof(LivestockBookLineAddForm); } }

		#endregion
		
        #region Factory Methods

		public LivestockBookLineAddForm(Form parent) 
			: base(parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
            _entity = LivestockBookLine.New(1);
			_entity.BeginEdit();
		}

        protected override void GetFormSourceData(object[] parameters)
        {
            _entity = LivestockBookLine.New(1);
			_entity.BeginEdit();

            _entity.EEstado = moleQule.Base.EEstado.Alta;
            _entity.ETipo = ETipoLineaLibroGanadero.TraspasoExplotacion;
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            Crotal_TB.BackColor = Observaciones_TB.BackColor;
            Crotal_TB.ForeColor = Observaciones_TB.ForeColor;
        }

        #endregion
    }
}
