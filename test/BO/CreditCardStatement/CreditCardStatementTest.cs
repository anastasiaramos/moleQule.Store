using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System;

using moleQule.Library;
using moleQule.Library.Application;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Store.Tests
{    
    /// <summary>
    ///This is a test class for FacturaListTest and is intended
    ///to contain all FacturaListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CreditCardStatementTest
    {
        private TestContext testContextInstance;

        private CreditCard newCreditCard = null;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            MainClass.Init();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            MainClass.Login();
            Assert.IsNotNull(AppContext.Principal);

            newCreditCard = CreditCard.New();
            newCreditCard.ETipoTarjeta = ETipoTarjeta.Credito;
            newCreditCard.DiasPago = 5;
            newCreditCard.DiaExtracto = 17;
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            MainClass.Close();
        }

        #endregion

        #region Instance
        
        [TestMethod()]
        public void CreditCardStatementHasCorrectDueDateFromOperationDueDate()
        {
            CreditCardInfo creditCard = newCreditCard.GetInfo(false);
            DateTime operationDueDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 5);

            CreditCardStatement statement = CreditCardService.GetOrCreateStatementFromOperationDueDate(creditCard, operationDueDate);
            Assert.AreEqual(statement.DueDate, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 5));
        }

        [TestMethod()]
        public void CreditCardStatementHasCorrectDueDateFromOperationDifferentDueDate()
        {
            CreditCardInfo creditCard = newCreditCard.GetInfo(false);
            DateTime operationDueDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 7);

            CreditCardStatement statement = CreditCardService.GetOrCreateStatementFromOperationDueDate(creditCard, operationDueDate);
            Assert.AreEqual(statement.DueDate, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 5));
        }

        [TestMethod()]
        public void CreditCardStatementHasCorrectDueDateFromOperationDate()
        {
            CreditCardInfo creditCard = newCreditCard.GetInfo(false);
            DateTime operationDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 16);

            CreditCardStatement statement = CreditCardService.GetOrCreateStatementFromOperationDate(creditCard, operationDate);
            Assert.AreEqual(statement.DueDate, new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 5));
        }

        [TestMethod()]
        public void CreditCardStatementHasCorrectDueDateFromOperationDateLimitDay()
        {
            CreditCardInfo creditCard = newCreditCard.GetInfo(false);
            DateTime operationDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 17);

            CreditCardStatement statement = CreditCardService.GetOrCreateStatementFromOperationDate(creditCard, operationDate);
            Assert.AreEqual(statement.DueDate, new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 5));
        }

        [TestMethod()]
        public void CreditCardStatementHasCorrectDueDateFromOperationDateNextMonth()
        {
            CreditCardInfo creditCard = newCreditCard.GetInfo(false);
            DateTime operationDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 18);

            CreditCardStatement statement = CreditCardService.GetOrCreateStatementFromOperationDate(creditCard, operationDate);
            Assert.AreEqual(statement.DueDate, new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 5));
        }

        #endregion

        #region List

        [TestMethod()]
        public void CreditCardStatementListNotIsNull()
        {
            CreditCardStatementList list = null;
            list = CreditCardStatementList.GetList(2015, false);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod()]
        public void CreditCardStatementListHasItems()
        {
            CreditCardStatementList list = null;
            list = CreditCardStatementList.GetList(2014, false);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void CreditCardStatementListHasItemsOnlyFromAYear()
        {
            CreditCardStatementList list = null;
            list = CreditCardStatementList.GetList(2013, false);
            Assert.IsNull(list.FirstOrDefault(x => x.From.Year != 2013));
        }

        [TestMethod()]
        public void CreditCardStatementListLoadPayments()
        {
            CreditCardStatementList list = null;
            list = CreditCardStatementList.GetList(2014, false);

            Assert.IsTrue(list.Count > 0);

            PaymentList payments = PaymentList.GetByCreditCardStatement(list[0].Oid, false);

            Assert.IsTrue(payments.Count > 0);
            Assert.IsNull(payments.FirstOrDefault(x => x.OidLink != list[0].Oid));
            Assert.IsNull(payments.FirstOrDefault(x => x.OidTarjetaCredito != list[0].OidCreditCard));
            Assert.IsNull(payments.FirstOrDefault(x => x.Vencimiento < list[0].From));
            Assert.IsNull(payments.FirstOrDefault(x => x.Vencimiento > list[0].DueDate));
        }
        
        #endregion
    }    
}