using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using moleQule;
using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    /// <summary>
    /// Clase base para manejo (apertura y cierre) de formularios
    /// Es único en el sistema (singleton)
    /// </summary>
    /// <remarks>
    /// Para utilizar el FormMng es necesario indicar cual será el MainForm padre de los formularios
    /// Este MainForm deberá ser un formulario heredado de MainFormBase
    /// </remarks>
    public class FormMng : IFormMng
    {
        #region Factory Methods

        /// <summary>
        /// Única instancia de la clase MainBaseForm (Singleton)
        /// </summary>
        protected static FormMng _main;

        /// <summary>
        /// Unique FormMng Class Instance
        /// </summary>
        /// <remarks>
        /// Para utilizar el FormMng es necesario inicializar el MainForm padre de los formularios
        /// </remarks>
        public static FormMng Instance { get { return (_main != null) ? _main : new FormMng(); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormMng()
		{
			// Singleton
			_main = this;
		}

        #endregion

        #region Business Methods

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        public void OpenForm(string formID) { OpenForm(formID, null); }
        public void OpenForm(string formID, object param) { OpenForm(formID, new object[1] { param }, null); }
		public void OpenForm(string formID, object[] parameters) { OpenForm(formID, parameters, null); }

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        /// <param name="param">Parámetro para el formulario</param>
        public void OpenForm(string formID, object[] parameters, Form parent)
        {
            try
            {
                switch (formID)
                {
					case AlmacenEditForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(AlmacenEditForm.Type))
							{
								long oid_almacen = Library.Store.ModulePrincipal.GetDefaultAlmacenSetting();
								if (oid_almacen != 0)
								{
									AlmacenEditForm form = new AlmacenEditForm(oid_almacen, parent);
									FormMngBase.Instance.ShowFormulario(form);
								}
							}
						} break;

					case AlmacenMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(AlmacenMngForm.Type))
                            {
                                AlmacenMngForm em = new AlmacenMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BankLoanMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(BankLoanMngForm.Type))
                            {
                                BankLoanMngForm em = new BankLoanMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BatchMngForm.ID:
                        {
                            FormMngBase.Instance.CloseAllForms();
                            BatchMngForm em = new BatchMngForm(parent, (BatchList)parameters[0], (string)parameters[1]);
                            FormMngBase.Instance.ShowFormulario(em);

                        } break;

                    case CustomAgentMngForm.ID:
                        {
							if (FormMngBase.Instance.BuscarFormulario(CustomAgentMngForm.Type))
								((CustomAgentMngForm)GetFormulario(CustomAgentMngForm.Type)).Cerrar();

							CustomAgentMngForm em = new CustomAgentMngForm(false, parent, (moleQule.Base.EEstado)parameters[0]);
							FormMngBase.Instance.ShowFormulario(em);
                        } break;

					case EmployeeMngForm.ID:
						{
							if (FormMngBase.Instance.BuscarFormulario(EmployeeMngForm.Type))
								((EmployeeMngForm)GetFormulario(EmployeeMngForm.Type)).Cerrar();

							EmployeeMngForm em = new EmployeeMngForm(false, parent, (moleQule.Base.EEstado)parameters[0]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

					case EscandalloMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(EscandalloMngForm.Type))
							{
								EscandalloMngForm em = new EscandalloMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case ExpedienteAlimentacionMngForm.ID:
                        {
							FormMngBase.Instance.CloseAllForms();
                            ExpedienteAlimentacionMngForm em = new ExpedienteAlimentacionMngForm(parent, (ExpedienteList)parameters[0], (string)parameters[1]);                                
                            FormMngBase.Instance.ShowFormulario(em);
                        } break;

					case ExpedienteAlmacenMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(ExpedienteAlmacenMngForm.Type))
							{
								ExpedienteAlmacenMngForm em = new ExpedienteAlmacenMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case ExpedienteMaquinariaMngForm.ID:
                        {
							FormMngBase.Instance.CloseAllForms();
 							ExpedienteMaquinariaMngForm em = new ExpedienteMaquinariaMngForm(parent, (ExpedienteList)parameters[0], (string)parameters[1]);
                               FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case ExpedienteGanadoMngForm.ID:
                        {
							FormMngBase.Instance.CloseAllForms();
							ExpedienteGanadoMngForm em = new ExpedienteGanadoMngForm(parent, (ExpedienteList)parameters[0], (string)parameters[1]);
                            FormMngBase.Instance.ShowFormulario(em);
                        } break;

					case ExpedienteREAMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(ExpedienteREAMngForm.Type))
							{
								ExpedienteREAMngForm em = new ExpedienteREAMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case InputDeliveryAllMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InputDeliveryAllMngForm.Type))
                            {
                                InputDeliveryAllMngForm em = new InputDeliveryAllMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InputDeliveryBilledMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InputDeliveryBilledMngForm.Type))
                            {
                                InputDeliveryBilledMngForm em = new InputDeliveryBilledMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InputDeliveryNoBilledMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InputDeliveryNoBilledMngForm.Type))
                            {
                                InputDeliveryNoBilledMngForm em = new InputDeliveryNoBilledMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case LineaFomentoMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(LineaFomentoMngForm.Type))
							{
								LineaFomentoMngForm em = new LineaFomentoMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

					case LivestockBookLineMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(LivestockBookLineMngForm.Type))
							{
								LivestockBookLineMngForm em = new LivestockBookLineMngForm(parent, (int)parameters[0]);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case LoanMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(LoanMngForm.Type))
                            {
                                LoanMngForm em = new LoanMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case MerchantLoanMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(MerchantLoanMngForm.Type))
                            {
                                MerchantLoanMngForm em = new MerchantLoanMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InputInvoiceMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InputInvoiceMngForm.Type))
                            {
								int size = (parameters == null) ? 0 : parameters.GetLength(0);

  								switch (size)
								{
									case 0:
										{
											InputInvoiceMngForm em = new InputInvoiceMngForm(parent);
											FormMngBase.Instance.ShowFormulario(em);
										}
										break;

									case 1:
										{
											InputInvoiceMngForm em = new InputInvoiceMngForm(parent, (ETipoFacturas)parameters[0]);
											FormMngBase.Instance.ShowFormulario(em);
										}
										break;

									case 2:
										{
											InputInvoiceMngForm em = new InputInvoiceMngForm(parent, (ETipoFacturas)parameters[0], (InputInvoiceList)parameters[1]);
											FormMngBase.Instance.ShowFormulario(em);
										}
										break;
								}
                            }
                        } break;

                    case InputInvoiceAllMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(InputInvoicePayedMngForm.Type))
                            {
                                ((InputInvoicePayedMngForm)GetFormulario(InputInvoicePayedMngForm.Type)).Cerrar();
                            }
                            if (FormMngBase.Instance.BuscarFormulario(InputInvoiceDueMngForm.Type))
                            {
                                ((InputInvoiceDueMngForm)GetFormulario(InputInvoiceDueMngForm.Type)).Cerrar();
                            }
                            if (!FormMngBase.Instance.BuscarFormulario(InputInvoiceAllMngForm.Type))
                            {
                                InputInvoiceAllMngForm em = new InputInvoiceAllMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InputInvoicePayedMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(InputInvoiceAllMngForm.Type))
                            {
                                ((InputInvoiceAllMngForm)GetFormulario(InputInvoiceAllMngForm.Type)).Cerrar();
                            }
                            if (FormMngBase.Instance.BuscarFormulario(InputInvoiceDueMngForm.Type))
                            {
                                ((InputInvoiceDueMngForm)GetFormulario(InputInvoiceDueMngForm.Type)).Cerrar();
                            }
                            if (!FormMngBase.Instance.BuscarFormulario(InputInvoicePayedMngForm.Type))
                            {
                                InputInvoicePayedMngForm em = new InputInvoicePayedMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InputInvoiceDueMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(InputInvoiceAllMngForm.Type))
                            {
                                ((InputInvoiceAllMngForm)GetFormulario(InputInvoiceAllMngForm.Type)).Cerrar();
                            }
                            if (FormMngBase.Instance.BuscarFormulario(InputInvoicePayedMngForm.Type))
                            {
                                ((InputInvoicePayedMngForm)GetFormulario(InputInvoicePayedMngForm.Type)).Cerrar();
                            }
                            if (!FormMngBase.Instance.BuscarFormulario(InputInvoiceDueMngForm.Type))
                            {
                                InputInvoiceDueMngForm em = new InputInvoiceDueMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case FamilyMngForm.ID:
                        {
							if (!FormMngBase.Instance.BuscarFormulario(FamilyMngForm.Type))
                            {
								FamilyMngForm em = new FamilyMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case InformeGastosExpedienteActionForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(InformeGastosExpedienteActionForm.Type))
							{
								InformeGastosExpedienteActionForm em = new InformeGastosExpedienteActionForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case InventarioAlmacenMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InventarioAlmacenMngForm.Type))
                            {
                                InventarioAlmacenMngForm em = new InventarioAlmacenMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case ExpenseMngForm.ID:
						{
							if (FormMngBase.Instance.BuscarFormulario(ExpenseMngForm.Type))
								((ExpenseMngForm)GetFormulario(ExpenseMngForm.Type)).Cerrar();

							ExpenseMngForm em = new ExpenseMngForm(parent, (ECategoriaGasto)parameters[0]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

                    case InventarioValoradoActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InventarioValoradoActionForm.Type))
                            {
                                InventarioValoradoActionForm em = new InventarioValoradoActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case MovsStockActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(MovsStockActionForm.Type))
                            {
								MovsStockActionForm em = new MovsStockActionForm(parent);
								em.TipoExpediente_CB.SelectedValue = (long)(moleQule.Store.Structs.ETipoExpediente)parameters[0];
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case PayrollMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(PayrollMngForm.Type))
							{
								PayrollMngForm em = new PayrollMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case ProviderPaymentMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(ProviderPaymentMngForm.Type))
                            {
                                ProviderPaymentMngForm em = new ProviderPaymentMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case PaymentsControlActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(PaymentsControlActionForm.Type))
                            {
                                PaymentsControlActionForm em = new PaymentsControlActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case EmployeePaymentMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(EmployeePaymentMngForm.Type))
                            {
                                EmployeePaymentMngForm em = new EmployeePaymentMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case PaymentMngForm.ID:
                        {
							if (FormMngBase.Instance.BuscarFormulario(PaymentMngForm.Type))
								((PaymentMngForm)GetFormulario(PaymentMngForm.Type)).Cerrar();

							switch (parameters.GetLength(0))
							{
								case 1:
									{
										PaymentMngForm em = new PaymentMngForm(parent, (ETipoPago)parameters[0]);
										FormMngBase.Instance.ShowFormulario(em);
									}
									break;

								case 2:
									{
										PaymentMngForm em = new PaymentMngForm(parent, (ETipoPago)parameters[0], (PaymentList)parameters[1]);
										FormMngBase.Instance.ShowFormulario(em);
									}
									break;
							}
                        } break;

					case PartidaAlimentacionMngForm.ID:
						{
							FormMngBase.Instance.CloseAllForms();
							PartidaAlimentacionMngForm em = new PartidaAlimentacionMngForm(parent, (BatchList)parameters[0], (string)parameters[1]);
							FormMngBase.Instance.ShowFormulario(em);

						} break;

					case PartidaGanadoMngForm.ID:
						{
							FormMngBase.Instance.CloseAllForms();
							PartidaGanadoMngForm em = new PartidaGanadoMngForm(parent, (BatchList)parameters[0], (string)parameters[1]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

					case PartidaMaquinariaMngForm.ID:
						{
							FormMngBase.Instance.CloseAllForms();
							PartidaMaquinariaMngForm em = new PartidaMaquinariaMngForm(parent, (BatchList)parameters[0], (string)parameters[1]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

					case PayrollBatchMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(PayrollBatchMngForm.Type))
							{
								PayrollBatchMngForm em = new PayrollBatchMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case PedidoProveedorMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(PedidoProveedorMngForm.Type))
                            {
                                PedidoProveedorMngForm em = new PedidoProveedorMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case ProductAllMngForm.ID:
                        {
							if (!FormMngBase.Instance.BuscarFormulario(ProductAllMngForm.Type))
                            {
								ProductAllMngForm em = new ProductAllMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case ProductKitMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(ProductKitMngForm.Type))
							{
								ProductKitMngForm em = new ProductKitMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case ProjectMngForm.ID:
                        {
                            FormMngBase.Instance.CloseAllForms();
                            ProjectMngForm em = new ProjectMngForm(parent, (string)parameters[1]);
                            FormMngBase.Instance.ShowFormulario(em);
                        } break;

					case ProviderMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(ProviderMngForm.Type))
							{
								ProviderMngForm em = new ProviderMngForm(false, parent, (ETipoAcreedor)parameters[1], (moleQule.Base.EEstado)parameters[0]);
								FormMngBase.Instance.ShowFormulario(em);
							}
						}
						break;

                    case PurchasesActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(PurchasesActionForm.Type))
                            {
                                PurchasesActionForm em = new PurchasesActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case RazaAnimalUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(RazaAnimalUIForm.Type))
                            {
                                RazaAnimalUIForm em = new RazaAnimalUIForm();
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case SupplierMngForm.ID:
                        {
							if (FormMngBase.Instance.BuscarFormulario(SupplierMngForm.Type))
								((SupplierMngForm)GetFormulario(SupplierMngForm.Type)).Cerrar();

							SupplierMngForm em = new SupplierMngForm(false, parent, (ETipoAcreedor)parameters[1], (moleQule.Base.EEstado)parameters[0]);
                             FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case ShippingCompanyMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(ShippingCompanyMngForm.Type))
                                ((ShippingCompanyMngForm)GetFormulario(ShippingCompanyMngForm.Type)).Cerrar();

                            ShippingCompanyMngForm em = new ShippingCompanyMngForm(false, parent, (moleQule.Base.EEstado)parameters[0]);
                            FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case SerieMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(SerieMngForm.Type))
                            {
                                SerieMngForm em = new SerieMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case TipoAnimalUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(TipoAnimalUIForm.Type))
                            {
                                TipoAnimalUIForm em = new TipoAnimalUIForm();
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case TipoGanadoUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(TipoGanadoUIForm.Type))
                            {
                                TipoGanadoUIForm em = new TipoGanadoUIForm();
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case TipoGastoMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(TipoGastoMngForm.Type))
							{
								TipoGastoMngForm em = new TipoGastoMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case TransporterMngForm.ID:
                        {
							if (FormMngBase.Instance.BuscarFormulario(TransporterMngForm.Type))
								((TransporterMngForm)GetFormulario(TransporterMngForm.Type)).Cerrar();

							TransporterMngForm em = new TransporterMngForm(false, parent, (moleQule.Base.EEstado)parameters[0]);
							FormMngBase.Instance.ShowFormulario(em);
                        } break;

					case ToolMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(ToolMngForm.Type))
							{
								ToolMngForm em = new ToolMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

					case WorkMngForm.ID:
						{
							FormMngBase.Instance.CloseAllForms();
							WorkMngForm em = new WorkMngForm(parent, (string)parameters[1]);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

					case WorkReportMngForm.ID:
						{
							if (FormMngBase.Instance.BuscarFormulario(WorkReportMngForm.Type))
								((WorkReportMngForm)GetFormulario(WorkReportMngForm.Type)).Cerrar();

							WorkReportMngForm em = new WorkReportMngForm(false, parent);
							FormMngBase.Instance.ShowFormulario(em);
						} break;

                    case WorkReportCategoryUIForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(WorkReportCategoryUIForm.Type))
                            {
                                WorkReportCategoryUIForm em = new WorkReportCategoryUIForm();
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    default:
                        {
                            throw new iQImplementationException(string.Format(moleQule.Face.Resources.Messages.FORM_NOT_FOUND, formID), string.Empty);
                        } 
                }
            }
            catch (iQImplementationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
				if (Globals.Instance.ProgressInfoMng != null)
				{
					Globals.Instance.ProgressInfoMng.ShowErrorException(ex);
					Globals.Instance.ProgressInfoMng.FillUp();
				}
				else
					ProgressInfoMng.ShowException(ex);
            }
        }

        /// <summary>
        /// Devuelve un formulario hijo del tipo pasado como parámetro
        /// </summary>
        /// <param name="childType">Tipo de formulario</param>
        public object GetFormulario(Type childType) { return FormMngBase.Instance.GetFormulario(childType); }

        #endregion
    }
}
