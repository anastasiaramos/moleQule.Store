using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LivestockBookLineEditForm : LivestockBookLineUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "LineaLibroGanaderoEditForm";
		public new static Type Type { get { return typeof(LivestockBookLineEditForm); } }

		#endregion
		
        #region Factory Methods

        public LivestockBookLineEditForm(long oid, ETipoLineaLibroGanadero tipo)
            : this(oid, tipo, null) { }

        public LivestockBookLineEditForm(long oid, ETipoLineaLibroGanadero tipo, Form parent)
            : base(oid, tipo, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text += ": " + Entity.Codigo;
            _mf_type = ManagerFormType.MFEdit;
        }

        public LivestockBookLineEditForm(LivestockBookLine source, Form parent)
			: base(source, true, parent)
		{
			InitializeComponent();
			SetFormData();
			this.Text += ": " + Entity.Codigo;
			_mf_type = ManagerFormType.MFEdit;
		}

		public override void DisposeForm()
		{
			if ((_entity != null) && !IsChild) _entity.CloseSession();

			base.DisposeForm();
		}

        protected override void GetFormSourceData(long oid)
		{
            _entity = LivestockBookLine.Get(Oid);
			_entity.BeginEdit();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
            LivestockBookLine source = parameters[0] as LivestockBookLine;

            if (source == null)
                _entity = LivestockBookLine.Get(oid, (ETipoLineaLibroGanadero)parameters[1]);
            else
            {
                IsChild = true;
                _source = source;
                _entity = _source.Clone();
            }

			_entity.BeginEdit();
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            if (_entity.OidConceptoAlbaran == 0)
            {
                Crotal_TB.ReadOnly = false;
                Crotal_TB.BackColor = Observaciones_TB.BackColor;
                Crotal_TB.ForeColor = Observaciones_TB.ForeColor;
            }
        }

        #endregion
    }
}
