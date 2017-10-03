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
    public class ExpenseTest
    {
        private TestContext testContextInstance;

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
            Assert.IsNotNull(AppContext.Principal);
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            MainClass.Close();
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        #endregion

        [TestMethod()]
        public void ExpenseListNotIsNull()
        {
            ExpenseList list = null;
            list = ExpenseList.GetList(ECategoriaGasto.Todos, 2000, false);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod()]
        public void ExpenseListHasItems()
        {
            ExpenseList list = null;
            list = ExpenseList.GetList(false);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void ExpenseListHasItemsOnlyFromAYear()
        {
            ExpenseList list = null;
            list = ExpenseList.GetList(ECategoriaGasto.Todos, 2013, false);
            Assert.IsTrue(list.Count > 0);
            Assert.IsNull(list.FirstOrDefault(x => x.Fecha.Year != 2013));
        }

        [TestMethod()]
        public void ExpenseListHasItemsOnlyFromBankCategory()
        {
            ExpenseList list = null;
            list = ExpenseList.GetList(ECategoriaGasto.Bancario, 2013, false);
            Assert.IsTrue(list.Count > 0);
            Assert.IsNull(list.FirstOrDefault(x => x.Fecha.Year != 2013 || x.ECategoriaGasto != ECategoriaGasto.Bancario));
        }

        [TestMethod()]
        public void ExpenseListHasItemsOnlyFromTaxCategory()
        {
            ExpenseList list = null;
            list = ExpenseList.GetList(ECategoriaGasto.Impuesto, 2013, false);
            Assert.IsTrue(list.Count > 0);
            Assert.IsNull(list.FirstOrDefault(x => x.Fecha.Year != 2013 || x.ECategoriaGasto != ECategoriaGasto.Impuesto));
        }
    }
}
