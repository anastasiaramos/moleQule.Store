using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
	public class PaymentViewModel : ViewModelBase<Pago, PagoInfo>, IViewModel
	{
		#region Attributes

		protected PaymentBase _base = new PaymentBase();		
		
		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		
		[HiddenInput]
		public long OidAgente { get { return _base.Record.OidAgente; } set { _base.Record.OidAgente = value; } }
		
		[HiddenInput]
		public long OidUsuario { get { return _base.Record.OidUsuario; } set { _base.Record.OidUsuario = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_ID")]
		public long IdPago { get { return _base.Record.IdPago; } set { _base.Record.IdPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_TYPE")]
		public long Tipo { get { return _base.Record.Tipo; } set { _base.Record.Tipo = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get { return _base.Record.Fecha; } set { _base.Record.Fecha = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_AMOUNT")]
		public Decimal Importe { get { return _base.Record.Importe; } set { _base.Record.Importe = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
		public long MedioPago { get { return _base.Record.MedioPago; } set { _base.Record.MedioPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "EXPIRATION_DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Vencimiento { get { return _base.Record.Vencimiento; } set { _base.Record.Vencimiento = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "OBSERVACIONES")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }
		
		[HiddenInput]
		public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } set { _base.Record.OidCuentaBancaria = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "SERIAL")]
		public long Serial { get { return _base.Record.Serial; } set { _base.Record.Serial = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "CODE")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }
		
		[HiddenInput]
		public long OidTarjetaCredito { get { return _base.Record.OidTarjetaCredito; } set { _base.Record.OidTarjetaCredito = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "STATUS")]
		public long Estado { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_STATUS")]
		public long EstadoPago { get { return _base.Record.EstadoPago; } set { _base.Record.EstadoPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ACCOUNTANT_MOV_ID")]
		public string IdMovContable { get { return _base.Record.IdMovContable; } set { _base.Record.IdMovContable = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "BANK_CHARGES")]
		public Decimal GastosBancarios { get { return _base.Record.GastosBancarios; } set { _base.Record.GastosBancarios = value; } }		
		
		//UNLINKED PROPERTIES
        [HiddenInput]
        public long Status { get { return Estado; } set { Estado = value; } }

		public virtual EEstado EStatus { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }

		public virtual EEstado EEstadoPago { get { return _base.EPaymentStatus; } set { _base.Record.EstadoPago = (long)value; } }

		[Display(ResourceType = typeof(moleQule.Library.Common.Resources.Labels), Name = "STATUS")]
		public virtual string EstadoPagoLabel { get { return _base.PaymentStatusLabel; } set { } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CLIENT")]
		public virtual string Agente { get { return _base.Agente; } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } set { } }

		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(Pago source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(PagoInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(Pago dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public PaymentViewModel() { }

		public static PaymentViewModel New() 
		{
			PaymentViewModel obj = new PaymentViewModel();
            obj.CopyFrom(PagoInfo.New());
			return obj;
		}
        public static PaymentViewModel New(Pago source) { return New(source.GetInfo(false)); }
        public static PaymentViewModel New(PagoInfo source)
		{
			PaymentViewModel obj = new PaymentViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static PaymentViewModel Get(long oid)
		{
			PaymentViewModel obj = new PaymentViewModel();
            obj.CopyFrom(PagoInfo.Get(oid, false));
			return obj;
		}

		public static void Add(PaymentViewModel item)
		{
			Pago newItem = Pago.New((ETipoPago)item.Tipo);
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(PaymentViewModel source, HttpRequestBase request = null)
		{
            Pago item = Pago.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
            Pago.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class PaymentListViewModel : List<PaymentViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public PaymentListViewModel() { }

		public static PaymentListViewModel Get()
		{
			PaymentListViewModel list = new PaymentListViewModel();

			PaymentList sourceList = PaymentList.GetList(false);

			foreach (PagoInfo item in sourceList)
				list.Add(PaymentViewModel.New(item));

			return list;
		}
        public static PaymentListViewModel Get(PaymentList sourceList)
		{
			PaymentListViewModel list = new PaymentListViewModel();

            foreach (PagoInfo item in sourceList)
				list.Add(PaymentViewModel.New(item));

			return list;
		}

		#endregion
	}
}
