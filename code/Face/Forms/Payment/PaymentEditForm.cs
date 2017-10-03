using System;
using System.Windows.Forms;
using System.Reflection;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Common;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
	public partial class PaymentEditForm : PaymentUIForm
    {
        #region Factory Methods

        public PaymentEditForm() 
			: this(null, -1, null) { }

        public PaymentEditForm(Form parent, long oidAgent, PaymentSummary summary)
            : base(parent, oidAgent, summary)
		{
            InitializeComponent();
            
            if (_entity != null)
            {
                SetFormData();
                this.Text = Resources.Labels.PAGO_EDIT_TITLE + " " + _entity.Nombre.ToUpper();
            }

            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
        {
			_summary = (PaymentSummary)parameters[0];

            switch (_summary.ETipoAcreedor)
            {
				case ETipoAcreedor.Instructor:
					{
						Assembly assembly = Assembly.Load("moleQule.Library.Instruction");
						Type type = assembly.GetType("moleQule.Library.Instruction.Instructor");

						_entity = (IAcreedor)type.InvokeMember("Get", BindingFlags.InvokeMethod, null, null, new object[1] { _summary.OidAgente });
					}
					break;

                case ETipoAcreedor.Proveedor:
				case ETipoAcreedor.Acreedor:
                    _entity = Proveedor.Get(_summary.OidAgente, _summary.ETipoAcreedor);
                    break;

                case ETipoAcreedor.Naviera:
					_entity = Naviera.Get(_summary.OidAgente);
                    break;

                case ETipoAcreedor.TransportistaDestino:
					_entity = Transporter.Get(_summary.OidAgente, _summary.ETipoAcreedor);
                    _entity.ETipoAcreedor = ETipoAcreedor.TransportistaDestino;
                    break;

                case ETipoAcreedor.TransportistaOrigen:
					_entity = Transporter.Get(_summary.OidAgente, _summary.ETipoAcreedor);
                    _entity.ETipoAcreedor = ETipoAcreedor.TransportistaOrigen;
                    break;

                case ETipoAcreedor.Despachante:
					_entity = Despachante.Get(_summary.OidAgente);
                    break;

				case ETipoAcreedor.Partner:
					{
						Assembly assembly = Assembly.Load("moleQule.Library.Partner");
						Type type = assembly.GetType("moleQule.Library.Partner.Partner");

						_entity = (IAcreedor)type.InvokeMember("Get", BindingFlags.InvokeMethod, null, null, new object[1] { _summary.OidAgente });
					}
					break;
            }

			_entity.CloseSessions = false;
            _entity.BeginEdit();
        }

        #endregion
    }
}

