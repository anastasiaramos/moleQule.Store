using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LivestockBookLineForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "LivestockBookLineForm";
		public static Type Type { get { return typeof(LivestockBookLineForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public virtual LivestockBookLine Entity { get { return null; } set { } }
        public virtual LivestockBookLineInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public LivestockBookLineForm() 
			: this(-1) {}

        public LivestockBookLineForm(long oid) 
			: this(oid, ETipoLineaLibroGanadero.Todos, true, null) {}

		public LivestockBookLineForm(bool isModal) 
			: this(-1, ETipoLineaLibroGanadero.Todos, isModal, null) {}

        public LivestockBookLineForm(long oid, ETipoLineaLibroGanadero tipo, bool isModal, Form parent)
            : this(oid, new object[2] {null, tipo}, isModal, parent) {}

		public LivestockBookLineForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion			

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            ShowPair();
        }

        protected virtual void ShowPair() {}

        #endregion
    }
}