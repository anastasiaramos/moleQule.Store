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
    public class InputInvoiceTest
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
        public void InputInvoiceListNotIsNull()
        {
            InputInvoiceList list = null;
            list = InputInvoiceList.GetList(ETipoAcreedor.Todos, 2014, false);
            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void InputInvoiceListHasItems()
        {
            InputInvoiceList list = null;
            list = InputInvoiceList.GetList(ETipoAcreedor.Todos, 2014, false);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void InputInvoiceListHasItemsOnlyFromAClient()
        {
            InputInvoiceList list = null;
            QueryConditions conditions = new QueryConditions { Acreedor = ProviderBaseInfo.New(1, ETipoAcreedor.Proveedor) };
            list = InputInvoiceList.GetList(conditions, false);
            Assert.IsNull(list.FirstOrDefault(x => x.OidAcreedor != conditions.Acreedor.Oid));
        }

        [TestMethod()]
        public void InputInvoiceListHasItemsOnlyFromASerie()
        {
            InputInvoiceList list = null;
            QueryConditions conditions = new QueryConditions { Serie = SerieInfo.New(1) };
            list = InputInvoiceList.GetList(conditions, false);
            Assert.IsNull(list.FirstOrDefault(x => x.OidSerie != conditions.Serie.Oid));
        }
    }
}
