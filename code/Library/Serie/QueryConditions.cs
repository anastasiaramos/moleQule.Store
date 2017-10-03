using System;
using System.Collections.Generic;

using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Serie
{
    #region Querys

    public class QueryConditions : moleQule.Common.QueryConditions
    {
        public FamiliaInfo Family = null;
        public SerieInfo Serie = null;

        public EBankAccountLevel BankAccountLevel = EBankAccountLevel.Principal;
        public EMedioPago PaymentMethod = EMedioPago.Todos;
        public ITitular Holder = null;
        public ETipoFamilia FamilyType = ETipoFamilia.Todas;
        public ETipoSerie SerieType = ETipoSerie.Todas;

	}

    #endregion
}