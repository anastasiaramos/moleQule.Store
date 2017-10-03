using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Store.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class ProviderViewModel : ViewModelBase<ProviderBase, ProviderBaseInfo>, IViewModel
	{
		#region Attributes

		protected ProviderBase _base = new ProviderBase();
		protected Country _ocountry;

		#endregion

        #region Properties

		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long OidAcreedor { get { return _base.OidAcreedor; } set { _base.OidAcreedor = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "ID")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }

		[HiddenInput]
		public long Status { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }

		[HiddenInput]
		public virtual long TipoAcreedor { get { return _base.Record.TipoAcreedor; } set { _base.Record.TipoAcreedor = value; } }

		[Required]
		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "NAME")]
		public string Nombre { get { return _base.Record.Nombre; } set { _base.Record.Nombre = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "ALIAS")]
		public string Alias { get { return _base.Record.Alias; } set { _base.Record.Alias = value; } }

        [Required]
		[Display(ResourceType = typeof(Resources.Labels), Name = "VAT_NUMBER")]
		public string Identificador { get { return _base.Record.Identificador; } set { _base.Record.Identificador = value; } }

		//public string Titular { get { return _base._titular; } }
		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "ADDRESS")]
		public string Direccion { get { return _base.Record.Direccion; } set { _base.Record.Direccion = value; } }

		//public string Poblacion { get { return _base._poblacion; } }
		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "ZIP_CODE")]
		public string CodPostal { get { return _base.Record.CodPostal; } set { _base.Record.CodPostal = value; } }

		public string Provincia { get { return _base.Record.Provincia; } set { _base.Record.Provincia = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "CITY")]
		public string Municipio { get { return _base.Record.Municipio; } set { _base.Record.Municipio = value; } }

		[Required]
		[DataType(DataType.EmailAddress)]
        [Remote("ValidateEmail", "Account")]
		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "EMAIL")]
		public string Email { get { return _base.Record.Email; } set { _base.Record.Email = value; } }

		//[Display(ResourceType = typeof(Resources.Labels), Name = "COUNTRY")]
		//public string Country { get { return (_ocountry != null) ? _ocountry.Iso2 : _base.Record.Country; } set { _base.Record.Country = value; _ocountry = Library.Country.Find(value); } }

		//public string Prefix { get { return (_ocountry != null) ? _ocountry.Prefix : string.Empty; } set { _base.Record.Prefix = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "PHONE_NUMBER")]
		public string Telefono { get { return _base.Record.Telefono; } set { _base.Record.Telefono = value; } }

		[UIHint("MultilineText")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set {} }

		public virtual ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "TYPE")]
		public virtual string TipoAcreedorLabel { get { return _base.TipoAcreedorLabel; } set { } }

		public virtual ETipoEntidad EEntityType { get { return moleQule.Library.Store.EnumConvert.ToETipoEntidad(ETipoAcreedor); } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "COUNTRY")]
		public string Country { get { return "Spain";/*(_ocountry != null) ? _ocountry.Iso2 : _base.Record.Country*/ } set { /*_base.Record.Country = value;*/ } }

		public string Prefix { get { return "34"; /* (_ocountry != null) ? _ocountry.Prefix : string.Empty;*/ } set { /*_base.Record.Prefix = value;*/; } }

		//IUser
		[HiddenInput]
		public long OidUser { get { return _base.OidUser; } set { _base.OidUser = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "USERNAME")]
		public string Username { get { return _base.Username; } set { _base.Username = value; } }

		public virtual EEstadoItem EUserStatus { get { return _base.EUserStatus; } set { _base.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.UserStatusLabel; } set { } }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "CREATION_DATE")]
		public DateTime CreationDate { get { return _base.CreationDate; } set { _base.CreationDate = value; } }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "LAST_LOGIN_DATE")]
		public DateTime LastLoginDate { get { return _base.LastLoginDate; } set { _base.LastLoginDate = value; } }

		/*[Display(ResourceType = typeof(Resources.Labels), Name = "PHOTO")]
		public virtual string Photo { get { return _base.GetCrypFileName(Oid, EFile.Photo); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "PASSPORT")]
		public virtual string Passport { get { return _base.GetCrypFileName(Oid, EFile.Passport); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "DRIVING_LICENSE")]
		public virtual string DrivingLicense { get { return _base.GetCrypFileName(Oid, EFile.DrivingLicense); } set { ; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PHOTO")]
		public virtual string PhotoRelative { get { return Path.Combine(DataRelativePath, Photo).Replace("\\", "/"); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "PASSPORT")]
		public virtual string PassportRelative { get { return Path.Combine(DataRelativePath, Passport).Replace("\\", "/"); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "DRIVING_LICENSE")]
		public virtual string DrivingLicenseRelative { get { return Path.Combine(DataRelativePath, DrivingLicense).Replace("\\", "/"); } set { ; } }

		public virtual string PhotoAbsolute { get { return Path.Combine(DataAbsolutePath, _base.GetCrypFileName(Oid, EFile.Photo)); } set { ; } }
		public virtual string PassportAbsolute { get { return Path.Combine(DataAbsolutePath, _base.GetCrypFileName(Oid, EFile.Passport)); } set { ; } }
		public virtual string DrivingLicenseAbsolute { get { return Path.Combine(DataAbsolutePath, _base.GetCrypFileName(Oid, EFile.DrivingLicense)); } set { ; } }

		public virtual string DataRelativePath { get { return _base.GetRelativePath(Oid); } set { ; } }
		public virtual string DataAbsolutePath { get { return _base.GetAbsolutePath(Oid); } set { ; } }*/
		
		//public virtual EPrioridadPrecio EPrioridadPrecio { get { return _base.EPrioridadPrecio; } set { _base._prioridad_precio = (long)value; } }
		//public virtual string PrioridadPrecioLabel { get { return _base.PrioridadPrecioLabel; } }
		//public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		//public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		//public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		//public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		//public virtual Decimal TotalFacturado { get { return _base._total_facturado; } set { _base._total_facturado = value; } }
		//public virtual Decimal CreditoDispuesto { get { return _base._credito_dispuesto; } set { _base._credito_dispuesto = value; } }
		//public virtual string CuentaAsociada { get { return _base._cuenta_asociada; } set { _base._cuenta_asociada = value; } }
		//public virtual string Impuesto { get { return _base.Impuesto; } }
		//public virtual Decimal PImpuesto { get { return _base._p_impuesto; } }

		public ProviderBase BusinessObj { get; set; }
		public ProviderBaseInfo ReadOnlyObj { get; set; }

		#endregion

		#region Business Objects

		public new void CopyFrom(ProviderBaseInfo source)
		{
			if (source == null) return;

			_base.CopyValues(source);
			//_ocountry = Library.Country.Find(source.Country);
		}
		public void CopyTo(IAcreedor dest, HttpRequestBase request = null)
		{
			foreach (PropertyInfo item in this.GetType().GetProperties())
			{
				if (item == null) continue;
				if (item.Name == "Oid") continue;
				if (request != null  && request[item.Name] == null) continue;
				
				try
				{
					PropertyInfo propdest = typeof(IAcreedor).GetProperty(item.Name);
					if (propdest != null) propdest.SetValue(dest, item.GetValue(this, null), null);
				}
				catch { }
			}
		}

		#endregion

		#region Factory Methods

		public ProviderViewModel() {}

		public static ProviderViewModel New() { return New(ProviderBaseInfo.New()); }
		public static ProviderViewModel New(ProviderBase source) { return New(source.GetInfo(false)); }
		public static ProviderViewModel New(ProviderBaseInfo source)
		{
			ProviderViewModel obj = new ProviderViewModel();
			obj.CopyFrom(source);
			obj.ReadOnlyObj = source;
			return obj;
		}

		public static ProviderViewModel Get(long oid, ETipoAcreedor providerType)
		{
			ProviderViewModel obj = new ProviderViewModel();

			obj.ReadOnlyObj = ProviderBaseInfo.Get(oid, providerType, false);
			obj.CopyFrom(obj.ReadOnlyObj);			
			return obj;
		}

		public static void Add(ProviderViewModel item)
		{
			ProviderBase newItem = ProviderBase.New();
			item.CopyTo(newItem);
			newItem.ISave();
		}
		public static void Edit(ProviderViewModel source, HttpRequestBase request = null)
		{
			IAcreedor item = null;

			switch (source.ETipoAcreedor)
			{
				case ETipoAcreedor.Proveedor:
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Partner:
					item = Proveedor.Get(source.OidAcreedor, source.ETipoAcreedor);
					break;

				case ETipoAcreedor.Naviera:
					item = Naviera.Get(source.OidAcreedor);
					break;

				case ETipoAcreedor.Despachante:
					item = Despachante.Get(source.OidAcreedor);
					break;

				case ETipoAcreedor.TransportistaDestino:
				case ETipoAcreedor.TransportistaOrigen:
					item = Transporter.Get(source.OidAcreedor, source.ETipoAcreedor);
					break;
			}

			source.CopyTo(item, request);
			item.ISave();
		}
		public static void Remove(long oid, ETipoAcreedor providerType)
		{
			switch (providerType)
			{
 				case ETipoAcreedor.Proveedor:
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Partner:
					Proveedor.Delete(oid, providerType);
					break;

				case ETipoAcreedor.Naviera:
					Naviera.Delete(oid);
					break;

				case ETipoAcreedor.Despachante:
					Despachante.Delete(oid);
					break;

				case ETipoAcreedor.TransportistaDestino:
				case ETipoAcreedor.TransportistaOrigen:
					Transporter.Delete(oid);
					break;
			}		
		}

		#endregion
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class ProviderListViewModel : List<ProviderViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public ProviderListViewModel() { }

		public static ProviderListViewModel Get()
		{
			ProviderListViewModel list = new ProviderListViewModel();

			ProviderBaseList sourceList = ProviderBaseList.GetList();

			foreach (ProviderBaseInfo item in sourceList)
				list.Add(ProviderViewModel.New(item));

			return list;
		}

		public static ProviderListViewModel Get(ProviderBaseList sourceList)
		{
			ProviderListViewModel list = new ProviderListViewModel();

			foreach (ProviderBaseInfo item in sourceList)
				list.Add(ProviderViewModel.New(item));

			return list;
		}

		#endregion
	}
}
