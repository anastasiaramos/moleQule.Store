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
	public partial class AlmacenEditForm : AlmacenUIForm
	{
		#region Attributes & Properties

		public new const string ID = "AlmacenEditForm";
		public static Type Type { get { return typeof(AlmacenEditForm); } }

		#endregion

		#region Factory Methods

		public AlmacenEditForm(long oid, Form parent) 
			: base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFEdit;
		}

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid)
		{
			_entity = Almacen.Get(oid, false);
			_entity.BeginEdit();
		}

		#endregion
	}
}
