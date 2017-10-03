using System;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PedidoProveedorAddForm : PedidoProveedorUIForm
    {
        #region Attributes & Properties
		
        public const string ID = "PedidoProveedorAddForm";
		public static Type Type { get { return typeof(PedidoProveedorAddForm); } }

		#endregion
		
        #region Factory Methods

        public PedidoProveedorAddForm() 
			: this(ETipoAcreedor.Todos, null) {}

        public PedidoProveedorAddForm(ETipoAcreedor tipo, Form parent)
            : base(-1, tipo, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public PedidoProveedorAddForm(PedidoProveedor source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

		protected override void GetFormSourceData(object[] parameters)
		{
			ETipoAcreedor tipo = (ETipoAcreedor)parameters[0];
            
			_entity = PedidoProveedor.New();
			_entity.ETipoAcreedor = tipo;
            _entity.BeginEdit();
        }

        #endregion
    }
}
