using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class AlmacenAddForm : AlmacenUIForm
	{
		#region Attributes & Properties

		public new const string ID = "AlmacenAddForm";
		public static Type Type { get { return typeof(AlmacenAddForm); } }

		#endregion

        #region Factory Methods

        public AlmacenAddForm(Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData()
		{
			_entity = Almacen.New();
			_entity.BeginEdit();
		}

		#endregion
	}
}
