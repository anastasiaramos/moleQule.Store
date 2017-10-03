using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;
using moleQule.Store.Data;

namespace moleQule.Library.Store
{
	[Serializable()]
	public class PayrollBatchBase
	{
		#region Attributes

		private PayrollBatchRecord _record = new PayrollBatchRecord();

		private decimal _neto;

		#endregion

		#region Properties

		public PayrollBatchRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Base.EnumText<EEstado>.GetLabel(EStatus); } }
		public decimal Neto { get { return _neto; } set { _neto = value; } }
		public decimal Seguro { get { return _record.SeguroEmpresa + _record.SeguroPersonal; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_neto = Format.DataReader.GetDecimal(source, "NETO");
		}
		internal void CopyValues(PayrollBatch source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
			
			_neto = source.Neto;
		}
		internal void CopyValues(PayrollBatchInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
			
			_neto = source.Neto;
		}

		#endregion
	}
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class PayrollBatch : BusinessBaseEx<PayrollBatch>, IEntidadRegistro
	{
		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.Pago; } }
		public string DescripcionRegistro { get { return "REMESA DE NOMINA Nº " + Codigo + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2"); } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			PayrollBatchRecord obj = Session().Get<PayrollBatchRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

		#region Attributes

		protected PayrollBatchBase _base = new PayrollBatchBase();

		private Payrolls _nominas = Payrolls.NewChildList();
		private Expenses _gastos = Expenses.NewChildList();

		#endregion
		
		#region Properties

		public PayrollBatchBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Descripcion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descripcion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Descripcion.Equals(value))
				{
					_base.Record.Descripcion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Total
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Total;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Total.Equals(value))
				{
					_base.Record.Total = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal IRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Irpf;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Irpf.Equals(value))
				{
					_base.Record.Irpf = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal SeguroEmpresa
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.SeguroEmpresa;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.SeguroEmpresa.Equals(value))
				{
					_base.Record.SeguroEmpresa = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal SeguroPersonal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.SeguroPersonal;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.SeguroPersonal.Equals(value))
				{
					_base.Record.SeguroPersonal = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime PrevisionPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PrevisionPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PrevisionPago.Equals(value))
				{
					_base.Record.PrevisionPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Estado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal BaseIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BaseIrpf;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.BaseIrpf.Equals(value))
				{
					_base.Record.BaseIrpf = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Descuentos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descuentos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Descuentos.Equals(value))
				{
					_base.Record.Descuentos = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual Payrolls Nominas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _nominas;
			}
		}
		public virtual Expenses Gastos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _gastos;
			}
		}

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual decimal Neto { get { return _base.Neto; } set { _base.Neto = value; PropertyHasChanged(); } }
		public virtual decimal Seguro { get { return _base.Seguro; } set { PropertyHasChanged(); } }

		public override bool IsValid
		{
			get { return base.IsValid
							&& _nominas.IsValid
							&& _gastos.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
							|| _nominas.IsDirty
							|| _gastos.IsDirty; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual PayrollBatch CloneAsNew()
		{
			PayrollBatch clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = PayrollBatch.OpenSession();
			PayrollBatch.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Nominas.MarkAsNew();
			clon.Gastos.MarkAsNew();
			
			return clon;
		}
	
		protected virtual void CopyFrom(PayrollBatchInfo source)
		{
			if (source == null) return;
			
			_base.CopyValues(source);
			Oid = source.Oid;
		}
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNextByYear(typeof(PayrollBatch), Fecha.Year);
            Codigo = Serial.ToString(Resources.Defaults.REMESANOMINA_CODE_FORMAT) + "/" + Fecha.ToString("yy");
        }

		public static PayrollBatch ChangeStatus(long oid, EEstado status)
		{
			PayrollBatch item = PayrollBatch.Get(oid, false);
			item.BeginEdit();
			item.EEstado = status;
			item.ApplyEdit();
			item.Save();
			item.CloseSession();

			return item;
		}

		public virtual void CalculateTotal()
		{
			Neto = 0;
			Total = 0;
			IRPF = 0;
			SeguroPersonal = 0;
			Descuentos = 0;
            BaseIRPF = 0;

			foreach (Payroll item in _nominas)
			{
				if (item.EEstado == EEstado.Anulado) continue;

				Neto += item.Neto;
				IRPF += item.IRPF;
				SeguroPersonal += item.Seguro;
				Descuentos += item.Descuentos;
                BaseIRPF += item.BaseIRPF;
			}

			Seguro = SeguroEmpresa + SeguroPersonal;
			Total = Neto + Seguro + IRPF + Descuentos;
		}

		protected void CreateAssociatedExpenses()
		{
			TipoGastoList tipos = TipoGastoList.GetList(false, true);

			Expense gasto = Gastos.NewItem(this, ECategoriaGasto.SeguroSocial);
			TipoGastoInfo tipo = tipos.GetItem(ModulePrincipal.GetDefaultSegurosSetting());

			if (tipo.Oid == 0) throw new iQException(Resources.Messages.NO_TIPOGASTO_SEGUROS, iQExceptionCode.INVALID_OBJECT);

			gasto.OidTipo = tipo.Oid;
			gasto.PrevisionPago = Common.EnumFunctions.GetPrevisionPago(tipo.EFormaPago, Fecha, tipo.DiasPago);

			gasto = Gastos.NewItem(this, ECategoriaGasto.Impuesto);
			tipo = tipos.GetItem(ModulePrincipal.GetDefaultIRPFSetting());

			if (tipo.Oid == 0) throw new iQException(Resources.Messages.NO_TIPOGASTO_IRPF, iQExceptionCode.INVALID_OBJECT);
			
			gasto.OidTipo = tipo.Oid;
			gasto.PrevisionPago = Common.EnumFunctions.GetPrevisionPago(tipo.EFormaPago, Fecha, tipo.DiasPago);

			InsertStaff();

			CalculateTotal();
		}

		protected string GetPeriod(DateTime fecha)
		{
			if (fecha.Month < 4) return "1T";
			if ((4 >= fecha.Month) && (fecha.Month < 7)) return "2T";
			if ((7 >= fecha.Month) && (fecha.Month < 10)) return "3T";
			if (10 >= fecha.Month) return "4T";

			return string.Empty;
		}

		public virtual void InsertStaff()
		{
			EmployeeList employees = EmployeeList.GetList(false, true);

			foreach (EmployeeInfo item in employees)
			{
				if (item.EEstado == EEstado.Baja) continue;
				Payroll payroll = Nominas.NewItem(this, item);
			}
		}

		protected void UpdateAssociatedExpenses()
		{
			if (Gastos.Count == 0) return;

			EmployeeList empleados = EmployeeList.GetList(false, true);

			TipoGastoList tipos = TipoGastoList.GetList(false, true);

			foreach (Payroll item in Nominas)
			{
                if (empleados.GetItem(item.OidEmpleado) == null)
                {
                    Cache.Instance.Remove(typeof(EmployeeList));
                    empleados = EmployeeList.GetList(false, true);
                }

                item.Fecha = Fecha;
				item.PrevisionPago = PrevisionPago;
				item.Descripcion = String.Format(Resources.Messages.GASTO_NOMINA, empleados.GetItem(item.OidEmpleado).NombreCompleto.ToUpper());
				item.Observaciones = Descripcion;
			}

			foreach (Expense item in Gastos)
			{
				if (item.EEstado == EEstado.Baja) return;

				item.Observaciones = String.Format(Resources.Messages.GASTO_ASOCIADO, Descripcion);

				switch (item.ECategoriaGasto)
				{
					case ECategoriaGasto.SeguroSocial:
						{
							TipoGastoInfo tipo = tipos.GetItem(ModulePrincipal.GetDefaultSegurosSetting());
							item.Fecha = Fecha;
							item.PrevisionPago = Common.EnumFunctions.GetPrevisionPago(tipo.EFormaPago, Fecha, tipo.DiasPago);
							item.Total = Seguro;
							item.Descripcion = tipo.Nombre + " " + item.Fecha.ToString("MMMM") + " " + item.Fecha.ToString("yyyy");
						}
						break;

					case ECategoriaGasto.Impuesto:
						{
							TipoGastoInfo tipo = tipos.GetItem(ModulePrincipal.GetDefaultIRPFSetting());
							item.Fecha = Fecha;
							item.PrevisionPago = Common.EnumFunctions.GetPrevisionPago(tipo.EFormaPago, Fecha, tipo.DiasPago);
							item.Total = IRPF;
							item.Descripcion = tipo.Nombre + " " + GetPeriod(item.Fecha) + " " + item.Fecha.ToString("yyyy");
						}
						break;
				}
			}
		}

        public void UpdateDescription()
        {
            Descripcion = string.Format(Resources.Defaults.REMESANOMINA_DEFAULT_DESCRIPTION, Fecha.ToString("MMMM - yyyy").ToUpper());
        }

		#endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.NOMINA);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.NOMINA);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.NOMINA);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.NOMINA);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				RemesaNomina = PayrollBatch.New().GetInfo(false),
				PaymentType = ETipoPago.Nomina,
				Estado = EEstado.NoAnulado,
			};
			conditions.RemesaNomina.Oid = oid;

			TransactionPaymentList pagos = TransactionPaymentList.GetList(conditions, false);

			if (pagos.Count > 0)
				throw new iQException(Resources.Messages.PAGOS_ASOCIADOS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected PayrollBatch () {}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private PayrollBatch(PayrollBatch source, bool retrieve_childs)
        {
			MarkAsChild();
			Childs = retrieve_childs;
            Fetch(source);
        }
        private PayrollBatch(int sessionCode, IDataReader source, bool retrieve_childs)
        {
            MarkAsChild();
			SessionCode = sessionCode;
			Childs = retrieve_childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static PayrollBatch NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			PayrollBatch obj = DataPortal.Create<PayrollBatch>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Nomina con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Nomina source, bool retrieve_childs)
		/// <remarks/>
		internal static PayrollBatch GetChild(PayrollBatch source)
		{
			return new PayrollBatch(source, false);
		}
		internal static PayrollBatch GetChild(PayrollBatch source, bool childs)
		{
			return new PayrollBatch(source, childs);
		}
        internal static PayrollBatch GetChild(int sessionCode, IDataReader source)
        {
            return new PayrollBatch(sessionCode, source, false);
        }
		internal static PayrollBatch GetChild(int sessionCode, IDataReader source, bool childs)
        {
			return new PayrollBatch(sessionCode, source, childs);
        }
		
		public virtual PayrollBatchInfo GetInfo() { return GetInfo(true); }
		public virtual PayrollBatchInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PayrollBatchInfo(this, get_childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static PayrollBatch New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<PayrollBatch>(new CriteriaCs(-1));
		}
		
		public static PayrollBatch Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = PayrollBatch.GetCriteria(PayrollBatch.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = PayrollBatch.SELECT(oid);
				
			PayrollBatch.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<PayrollBatch>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			IsPosibleDelete(oid);

			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Nomina. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = PayrollBatch.OpenSession();
			ISession sess = PayrollBatch.Session(sessCode);
			ITransaction trans = PayrollBatch.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from PayrollBatchRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				PayrollBatch.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override PayrollBatch Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(moleQule.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                CalculateTotal();

				base.Save();

				_nominas.Update(this);
				_gastos.Update(this);				
				
				Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				Cache.Instance.Remove(typeof(TipoGastoList));
				Cache.Instance.Remove(typeof(EmployeeList));

				if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
		}
				
		#endregion				
		
		#region Common Data Access
		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			Fecha = DateTime.Now;
			PrevisionPago = DateTime.Now;
			GetNewCode();
			EEstado = EEstado.Abierto;
            UpdateDescription();

			CreateAssociatedExpenses();
		}
		
		private void Fetch(PayrollBatch source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					string query;
					IDataReader reader;

					Payroll.DoLOCK(Session());
					query = Payrolls.SELECT(this);
					reader = nHMng.SQLNativeSelect(query);
					_nominas = Payrolls.GetChildList(SessionCode, reader, false);

					Expense.DoLOCK(Session());
					query = Expenses.SELECT(this);
					reader = nHMng.SQLNativeSelect(query);
					_gastos = Expenses.GetChildList(SessionCode, reader, false);					
				}
			} 

			MarkOld();
		}
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					string query;
					IDataReader reader;

					Payroll.DoLOCK(Session());
					query = Payrolls.SELECT(this);
					reader = nHMng.SQLNativeSelect(query);
					_nominas = Payrolls.GetChildList(SessionCode, reader, false);		

					Expense.DoLOCK(Session());
					query = Expenses.SELECT(this);
					reader = nHMng.SQLNativeSelect(query);
					_gastos = Expenses.GetChildList(SessionCode, reader, false);					
				}
			}   

            MarkOld();
        }

		internal void Insert(PayrollBatchs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			UpdateAssociatedExpenses();

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		internal void Update(PayrollBatchs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			PayrollBatchRecord obj = Session().Get<PayrollBatchRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			UpdateAssociatedExpenses();

			MarkOld();
		}

		internal void DeleteSelf(PayrollBatchs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			Gastos.Clear();

			SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<PayrollBatchRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
            string query = string.Empty;

			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
                query = criteria.Query;

				if (nHMng.UseDirectSQL)
				{
					PayrollBatch.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{	
						Payroll.DoLOCK(Session());
						query = Payrolls.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_nominas = Payrolls.GetChildList(SessionCode, reader);		
												
						Expense.DoLOCK(Session());
						query = Expenses.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_gastos = Expenses.GetChildList(SessionCode, reader);						
 					} 
				}

				CalculateTotal();

				MarkOld();
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { query });
            }
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
        {
            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			
			GetNewCode();
			UpdateAssociatedExpenses();

			Session().Save(Base.Record);
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

			PayrollBatchRecord obj = Session().Get<PayrollBatchRecord>(Oid);
			obj.CopyValues(this.Base.Record);
			Session().Update(obj);

			UpdateAssociatedExpenses();

			MarkOld();			
		}
		
		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				PayrollBatchRecord obj = (PayrollBatchRecord)(criterio.UniqueResult());
				Session().Delete(obj);
				
				Transaction().Commit();
				
				Expenses.DeleteItems(obj.Oid); 
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion			
		
		#region Child Data Access

		internal void Insert(IAcreedor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			GetNewCode();
			UpdateAssociatedExpenses();

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);

			MarkOld();
		}

		internal void Update(IAcreedor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(moleQule.Resources.Messages.GENERIC_VALIDATION_ERROR);

			PayrollBatchRecord obj = parent.Session().Get<PayrollBatchRecord>(Oid);

			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);

			UpdateAssociatedExpenses();

			MarkOld();
		}

		internal void DeleteSelf(IAcreedor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			PayrollBatchRecord obj = parent.Session().Get<PayrollBatchRecord>(Oid);
			obj.CopyValues(Base.Record);

			parent.Session().Update(obj);

			Expenses.DeleteItems(obj.Oid); 

			MarkNew();
		}

		#endregion

        #region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
			{
				/*{ 
					"Gross", 
					new ForeignField() {                        
						Property = "GROSS", 
						TableAlias = String.Empty, 
						Column = null
					}
				}*/
			};
		}

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = 
			"	SELECT RN.*" +
			"		,NM.\"NETO\" AS \"NETO\"" +
			"		,NM.\"IRPF\" AS \"IRPF\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;

			string query = @"
            WHERE (RN.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += Common.EntityBase.ESTADO_CONDITION(conditions.Estado, "RN");
            query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "RN");

            if (conditions.RemesaNomina != null)
		       if (conditions.RemesaNomina.Oid != 0) 
                   query += @"
                    AND RN.""OID"" = " + conditions.RemesaNomina.Oid;				

			return query;
		}
		
        internal static string SELECT(long oid, bool lockTable)
        {			
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { RemesaNomina = PayrollBatch.New().GetInfo(false) };
			conditions.RemesaNomina.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string rn = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollBatchRecord));
			string nm = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.PayrollRecord));
            
			string query;

			query = 
				SELECT_FIELDS() + @"
					FROM " + rn + @" AS RN
					LEFT JOIN (SELECT ""OID_REMESA""
										,SUM(""NETO"") AS ""NETO""
										,SUM(""BASE_IRPF"" * ""P_IRPF"" / 100) AS ""IRPF""
								FROM " + nm + @"
					WHERE ""ESTADO"" != " + (long)EEstado.Anulado + @"
					GROUP BY ""OID_REMESA"")
					AS NM ON NM.""OID_REMESA"" = RN.""OID""" +
				WHERE(conditions);

			if (conditions.Orders == null)
			{
				conditions.Orders = new OrderList();
				conditions.Orders.Add(FilterMng.BuildOrderItem("Fecha", ListSortDirection.Descending, typeof(PayrollBatch)));
				conditions.Orders.Add(FilterMng.BuildOrderItem("Codigo", ListSortDirection.Descending, typeof(PayrollBatch)));
			}

			query += ORDER(conditions.Orders, string.Empty, ForeignFields());
			query += LIMIT(conditions.PagingInfo);

			query += Common.EntityBase.LOCK("RN", lockTable);

            return query;
        }
		
		#endregion
	}
}