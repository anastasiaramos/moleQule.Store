using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Reflection;

using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store.Properties;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class ModulePrincipal
    {
		#region Application Settings
		
		public static void SaveSettings() { Settings.Default.Save(); }

		public static void UpgradeSettings()
		{
			Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
			Version ver = ensamblado.GetName().Version;

			/*if (Properties.Settings.Default.MODULE_VERSION != ver.ToString())
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.MODULE_VERSION = ver.ToString();
			}*/
		}

		public static string GetDBVersion() { return Settings.Default.DB_VERSION; }

		#endregion

		#region Schema Settings

		//AYUDAS
		public static long GetAyudaREASetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_AYUDA_REA)); }
			catch { return 0; }
		}
		public static void SetAyudaREASetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_AYUDA_REA, value.ToString());
		}

		public static long GetAyudaPOSEISetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_AYUDA_POSEI)); }
			catch { return 0; }
		}
		public static void SetAyudaPOSEISetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_AYUDA_POSEI, value.ToString());
		}

		public static long GetAyudaFomentoSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_AYUDA_FOMENTO)); }
			catch { return 0; }
		}
		public static void SetAyudaFomentoSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_AYUDA_FOMENTO, value.ToString());
		}

		//FOMENTO
		public static long GetDefaultFleteSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_FLETE)); }
			catch { return 0; }
		}
		public static void SetDefaultFleteSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_FLETE, value.ToString());
		}

		public static long GetDefaultT3OrigenSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_T3ORIGEN)); }
			catch { return 0;}
		}
		public static void SetDefaultT3OrigenSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_T3ORIGEN, value.ToString());
		}

		public static long GetDefaultT3DestinoSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_T3DESTINO)); }
			catch { return 0; }
		}
		public static void SetDefaultT3DestinoSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_T3DESTINO, value.ToString());
		}

		public static long GetDefaultTHCOrigenSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_THCORIGEN)); }
			catch { return 0; }
		}
		public static void SetDefaultTHCOrigenSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_THCORIGEN, value.ToString());
		}

		public static long GetDefaultTHCDestinoSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_THCDESTINO)); }
			catch { return 0; }
		}
		public static void SetDefaultTHCDestinoSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_THCDESTINO, value.ToString());
		}

		public static long GetDefaultISPSSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_ISPS)); }
			catch { return 0; }
		}
		public static void SetDefaultISPSSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_ISPS, value.ToString());
		}

		public static long GetDefaultBAFSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_BAF)); }
			catch { return 0; }
		}
		public static void SetDefaultBAFSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_BAF, value.ToString());
		}

		//NOMINAS
		public static long GetDefaultNominasSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_NOMINAS)); }
			catch { return 0; }
		}
		public static void SetDefaultNominasSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_NOMINAS, value.ToString());
		}

		public static long GetDefaultSegurosSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_SEGUROS)); }
			catch { return 0; }
		}
		public static void SetDefaultSegurosSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_SEGUROS, value.ToString());
		}

		public static long GetDefaultIRPFSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_IRPF)); }
			catch { return 0; }
		}
		public static void SetDefaultIRPFSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_IRPF, value.ToString());
		}

		public static ETipoDescuento GetDefaultTipoDescuentoSetting()
		{
			try { return (ETipoDescuento)Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_TIPO_DESCUENTO)); }
			catch { return ETipoDescuento.Porcentaje; }
		}
		public static void SetDefaultTipoDescuentoSetting(ETipoDescuento value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_TIPO_DESCUENTO, ((long)value).ToString());
		}

		public static ETipoFacturacion GetDefaultTipoFacturacionSetting()
		{
			try { return (ETipoFacturacion)Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_TIPO_FACTURACION)); }
			catch { return ETipoFacturacion.Unidad; }
		}
		public static void SetDefaultTipoFacturacionSetting(ETipoFacturacion value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_TIPO_FACTURACION, ((long)value).ToString());
		}
		
		public static bool GetControlStockNegativo()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CONTROL_STOCK_NEGATIVO)); }
			catch { return false; }
		}
		public static void SetControlStockNegativo(bool value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CONTROL_STOCK_NEGATIVO, value.ToString());
		}

		#endregion

        #region User Settings

		//FACTURACION
		public static long GetDefaultSerieSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_SERIE_COMPRA)); }
			catch { return 0; }
		}
		public static void SetDefaultSerieSetting(long value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_SERIE_COMPRA, value.ToString());
		}
		
		public static long GetDefaultAlmacenSetting()
		{
			try { return Convert.ToInt64(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_ALMACEN)); }
			catch { return 1; }
		}
		public static void SetDefaultAlmacenSetting(long value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_ALMACEN, value.ToString());
		}

		//NOTIFICACIONES
		public static void SetNotifyPagos(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_PAGOS, value.ToString());
		}
		public static bool GetNotifyPagos()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_PAGOS)); }
			catch { return false; }
		}
		
		public static void SetNotifyFacturasRecibidas(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_FACTURAS_RECIBIDAS, value.ToString());
		}
		public static bool GetNotifyFacturasRecibidas()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_FACTURAS_RECIBIDAS)); }
			catch { return false; }
		}

		public static void SetNotifyGastos(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_GASTOS, value.ToString());
		}
		public static bool GetNotifyGastos()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_GASTOS)); }
			catch { return false; }
		}

		public static void SetNotifyPlazoPagos(decimal value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_PAGOS, value.ToString());
		}
		public static int GetNotifyPlazoPagos()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_PAGOS)); }
			catch { return 0; }
		}

		public static void SetNotifyPlazoFacturasRecibidas(decimal value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_FACTURAS_RECIBIDAS, value.ToString());
		}
		public static int GetNotifyPlazoFacturasRecibidas()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_FACTURAS_RECIBIDAS)); }
			catch { return 0; }
		}

		public static void SetNotifyPlazoGastos(decimal value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_GASTOS, value.ToString());
		}
		public static int GetNotifyPlazoGastos()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_GASTOS)); }
			catch { return 0; }
		}

        //PRODUCTOS
        public static void SetCodigoProductoAutomaticoSetting(bool value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CODIGO_PRODUCTO_AUTOMATICO, value.ToString());
        }

        public static bool GetCodigoProductoAutomaticoSetting()
        { 
            try { return Convert.ToBoolean(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CODIGO_PRODUCTO_AUTOMATICO)); }
            catch { return true; }
        }

        #endregion
    }
}