using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;

using moleQule.Common;
using moleQule.Common.Structs;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    class PaymentCommon
    {
        public static DialogResult ValidateAllocation(Payment payment, decimal deallocated, IEnumerable<ITransactionPayment> transactionPayments = null)
        {
            switch (payment.EMedioPago)
            {
                case EMedioPago.CompensacionFactura:
                    {
                        decimal importe = 0;

                        foreach (ITransactionPayment item in transactionPayments ?? new List<ITransactionPayment>())
                        {
                            if (item.Vinculado == Library.Store.Resources.Labels.RESET_PAGO)
                                importe += item.Asignado;
                        }

                        if (importe != 0)
                        {
                            ProgressInfoMng.ShowInfo(Resources.Messages.IMPORTE_PAGO_COMPENSACION);
                            return DialogResult.Ignore;
                        }
                    }
                    break;

                default:
                    {
                        if (payment.Pendiente == 0) return DialogResult.OK;

                        if (payment.Importe < 0)
                        {
                            if (deallocated < payment.Pendiente)
                            {
                                ProgressInfoMng.ShowInfo(string.Format(Resources.Messages.PAYMENT_LESS_ALLOCATION, deallocated, payment.Pendiente));
                                return DialogResult.Ignore;
                            }
                        }
                        else
                        {
                            if (deallocated > payment.Pendiente)
                            {
                                ProgressInfoMng.ShowInfo(string.Format(Resources.Messages.PAYMENT_BIGGER_ALLOCATION, deallocated, payment.Pendiente));
                                return DialogResult.Ignore;
                            }
                        }
                    }
                    break;
            }

            return DialogResult.OK;
        }

        public static DialogResult ValidateDueDate(Payment payment, CreditCardInfo creditCard, DateTime dueDate)
        {
            if (payment.EMedioPago != EMedioPago.Tarjeta) return DialogResult.OK;

            creditCard = creditCard ?? CreditCardInfo.Get(payment.OidCreditCard, false);

            if (creditCard.ETipoTarjeta != ETipoTarjeta.Credito) return DialogResult.OK;

            DateTime statementDueDate = StatementDatesFromOperationDueDate.GetStatementDueDate(creditCard, dueDate);

            if (!statementDueDate.Equals(dueDate))
            {
                string message = String.Format(moleQule.Common.Resources.Messages.STATEMENT_DATE_NOT_MISMATCH, statementDueDate.ToShortDateString());
                if (ProgressInfoMng.ShowQuestion(message) == System.Windows.Forms.DialogResult.Yes) 
                {
                    payment.Vencimiento = statementDueDate;
                    return DialogResult.OK;
                }
                else
                    return DialogResult.Ignore;
            }

            return DialogResult.OK;
        }
    }
}