using System;
using System.Collections.Generic;

using moleQule.Base;
using moleQule.Common.Structs;
using moleQule.Invoice.Structs;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Library.Loan
{
    #region Querys

    public class QueryConditions : moleQule.Library.Common.QueryConditions
    {
        public LoanInfo Loan = null;
        public PaymentInfo Payment = null;

        public EBankAccountLevel BankAccountLevel = EBankAccountLevel.Principal;
        public EBankLineType BankLineType = EBankLineType.Todos;
        ////public IBankLineInfo IBankLine = null;
        public ELoanType LoanType = ELoanType.All;
        public EMedioPago PaymentMethod = EMedioPago.Todos;
        public ETipoPago PaymentType = ETipoPago.Todos;
        public ITitular Holder = null;
	}

    #endregion
}
