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
    public static class MainClass
    {
        public static int _instances = 0;

        public static void Init(TestContext testContext = null)
        {
            _instances++;

            if (_instances > 1) return;

            System.Diagnostics.Process.Start(@"xcopy", @"""P:\\dotNet\Cattle\code\\moleQule.Application\\Library\\Asm\Test"" "".\\Asm"" /Y /R /I");
        }

        public static void Login()
        {
            if (AppContext.Principal != null) return;

            try { AppController.Instance.Init("7.5.23.2"); }
            catch { }

            SettingsMng.Instance.SetLANServer("localhost");

            Principal.Login("Admin", "iQi_1998");

            //Carga del schema
            long oidSchema = SettingsMng.Instance.GetDefaultSchema();

            CompanyInfo company = CompanyInfo.Get(oidSchema);
            AppContext.Principal.ChangeUserSchema(company);
        }

        public static void Close()
        {
            _instances--;

            if (AppContext.Principal != null)
                AppContext.Principal.Logout();

            //if (_instances == 0)
            //{
            //    if (AppContext.Principal != null)
            //        AppContext.Principal.Logout();
            //}
        }        
    }  
}