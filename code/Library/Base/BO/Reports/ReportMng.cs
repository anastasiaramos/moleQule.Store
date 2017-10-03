using System;
using System.Reflection;
using System.Runtime.Remoting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Reports;

namespace moleQule.Library.Store.Reports
{
    [Serializable()]
    public abstract class ReportMng : BaseReportMng
	{
		#region Attributes & Properties
		
		#endregion

		#region Factory Methods

		public ReportMng() : this (null) {}

		public ReportMng(ISchemaInfo schema)
            : this(schema, string.Empty) {}

        public ReportMng(ISchemaInfo schema, string title)
            : this(schema, title, string.Empty) {}

		public ReportMng(ISchemaInfo schema, string title, string filter)
			: base(schema, title, filter) {}

		#endregion

		#region Business Methods

        protected override ReportClass GetReportFromName(string folder, string className)
        {
            Assembly assembly = null;
            string pattern = string.Empty;

            try
            {
                assembly = Assembly.Load("moleQule.Library.App");
                pattern = "moleQule.Library.App.Modules.Store.Reports.{0}.{1}.{2}";
            }
            catch
            {
                assembly = Assembly.Load("moleQule.Library.Application");
                pattern = "moleQule.Library.Application.Modules.Store.Reports.{0}.{1}.{2}";
            }

            try
            {
                Assembly invoiceassembly = Assembly.Load("moleQule.Library.Invoice");
                Type type = invoiceassembly.GetType("moleQule.Library.Invoice.ModulePrincipal");

                string template = (string)type.InvokeMember("GetInvoiceTemplateSetting"
                                                            , BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public
                                                            , null, null, null );

                string subfolder = (template != ((long)EValue.Default).ToString())
                                        ? template
                                        : "s" + AppContext.ActiveSchema.SchemaCode;

                //string subfolder = (Library.Invoice.ModulePrincipal.GetInvoiceTemplateSetting() != ((long)EValue.Default).ToString())
                //                        ? Library.Invoice.ModulePrincipal.GetInvoiceTemplateSetting()
                //                        : "s" + AppContext.ActiveSchema.SchemaCode;

                ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(
                                                    assembly.FullName,
                                                    String.Format(pattern, folder, subfolder, className));

                return (ReportClass)object_handle.Unwrap();
            }
            catch
            {
                assembly = Assembly.Load("moleQule.Library.Store");
                pattern = "moleQule.Library.Store.Reports.{0}.{1}";

                ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(
                                                        assembly.FullName,
                                                        String.Format(pattern, folder, className));

                return (ReportClass)object_handle.Unwrap();
            }
        }

        protected ReportClass GetReportFromNameDeprectated(string folder, string className)
        {
            Assembly assembly = Assembly.Load("moleQule.Library.Application");

            if (AppContext.ActiveSchema.UseDefaultReports)
            {
                ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, "moleQule.Library.Application.Modules.Store.Reports." + folder + "." + className);
                return (ReportClass)object_handle.Unwrap();
            }
            else
            {
                try
                {
                    ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, "moleQule.Library.Application.Modules.Store.Reports." + folder + ".s" + AppContext.ActiveSchema.SchemaCode + "." + className);
                    return (ReportClass)object_handle.Unwrap();
                }
                catch
                {
                    ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, "moleQule.Library.Application.Modules.Store.Reports." + folder + "." + className);
                    return (ReportClass)object_handle.Unwrap();
                }
            }
        }

		#endregion
    }
}