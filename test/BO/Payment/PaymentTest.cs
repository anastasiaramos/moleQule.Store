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
    public class PaymentTest
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
        public void PaymentNotIsNull()
        {
            PaymentList list = null;
            list = PaymentList.GetList(2015, false);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        #endregion

        #region List

        [TestMethod()]
        public void PaymentListNotIsNull()
        {
            PaymentList list = null;
            list = PaymentList.GetList(2015, false);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod()]
        public void PaymentListHasItems()
        {
            PaymentList list = null;
            list = PaymentList.GetList(2014, false);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void PaymentListHasItemsOnlyFromASupplier()
        {
            PaymentList list = null;
            QueryConditions conditions = new QueryConditions { Acreedor = ProviderBaseInfo.New(1, ETipoAcreedor.Proveedor) };
            list = PaymentList.GetList(conditions, false);
            Assert.IsNull(list.FirstOrDefault(x => x.OidAgente != conditions.Acreedor.Oid || x.ETipoAcreedor != ETipoAcreedor.Proveedor));
        }

        [TestMethod()]
        public void PaymentListHasItemsOnlyFromAEmployee()
        {
            PaymentList list = null;
            QueryConditions conditions = new QueryConditions { Acreedor = ProviderBaseInfo.New(1, ETipoAcreedor.Empleado) };
            list = PaymentList.GetList(conditions, false);
            Assert.IsNull(list.FirstOrDefault(x => x.OidAgente != conditions.Acreedor.Oid || x.ETipoAcreedor != ETipoAcreedor.Empleado));
        }
    }

#endregion

}
