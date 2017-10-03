using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Expenses : BusinessListBaseEx<Expenses, Expense>
    {
		#region Properties

		protected new Hashtable MaxSerial = new Hashtable();

		#endregion

        #region Business Methods

        public Expense NewItem(Expedient parent)
        {
			Expense item = Expense.NewChild(parent);
			SetNextCode(parent, item);
            this.AddItem(item);
            parent.UpdateGastosPartidas(true);
            return item;
        }
		public Expense NewItem(Expedient parent, InputInvoice fac) 
		{
			Expense item = Expense.NewChild(parent, fac);
			this.AddItem(item);
			return item;
		}
		public Expense NewItem(Expedient parent, InputInvoice fac, ECategoriaGasto tipo)
		{
			Expense item = Expense.NewChild(parent, fac, tipo);
			this.AddItem(item);
			return item;
		}
		public Expense NewItem(Expedient parent, InputInvoiceInfo fac) 
		{
			Expense item = Expense.NewChild(parent, fac);
			this.AddItem(item);
			return item;
		}
		public Expense NewItem(Expedient parent, InputInvoiceInfo fac, ECategoriaGasto tipo)
        {
			Expense item = Expense.NewChild(parent, fac, tipo);
			this.AddItem(item);
            return item;
        }
		public Expense NewItem(Expedient parent, InputInvoiceInfo fac, InputInvoiceLineInfo cf, InputDeliveryLineInfo ca)
		{
			Expense item = Expense.NewChild(parent, fac, cf, ca);
			SetNextCode(item);
			this.AddItem(item);
			//parent.UpdateGastos();
			return item;
		}
		public Expense NewItem(Expedient parent, Expense gasto, InputDeliveryLineInfo ca)
		{
			Expense item = Expense.NewChild(parent, gasto, ca);
			this.AddItem(item);
			return item;
		}

		public Expense NewItem(PayrollBatch parent, ECategoriaGasto tipo)
		{
			Expense item = Expense.NewChild(parent, tipo);
			SetNextCode(item);
			this.AddItem(item);
			return item;
		}
		public Expense NewItem(PayrollBatch parent, EmployeeInfo empleado)
		{
			Expense item = Expense.NewChild(parent, empleado);
			SetNextCode(item);
			this.AddItem(item);
			return item;
		}

		public void EditItem(Expedient parent, InputInvoiceInfo fac, bool throwStockException)
		{
			Expense gasto = GetItemByFactura(fac);
			EditItem(parent, fac, gasto, throwStockException);			
		}
		public void EditItem(Expedient parent, InputInvoiceInfo fac, Expense gasto, bool throwStockException)
		{
			if (gasto == null) return;
			gasto.CopyFrom(parent, fac, gasto.ECategoriaGasto);
            parent.UpdateGastosPartidas(throwStockException);
		}
		public void EditItem(Expedient parent, InputInvoiceLineInfo cf, Expense gasto)
		{
			if (gasto == null) return;
			gasto.CopyFrom(parent, cf);
		}

		public override void Remove(long oid)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }
        public new void Remove(Expense item)
        {
            throw new iQException(moleQule.Resources.Messages.REMOVE_NOT_ALLOWED);
        }
        public void Remove(Expedient parent, InputInvoiceInfo fac)
        {
			List<Expense> gastos = new List<Expense>();

            foreach(Expense item in this)
                if (item.OidFactura == fac.Oid)
					gastos.Add(item);

			foreach (Expense item in gastos)
					base.Remove(item);

            parent.UpdateGastosPartidas(true);
        }
        public void Remove(Expedient parent, InputInvoice fac)
        {
			List<Expense> gastos = new List<Expense>();

			foreach (Expense item in gastos)
				if (item.OidFactura == fac.Oid)
					gastos.Add(item);

			foreach (Expense item in Items)
				base.Remove(item);

            parent.UpdateGastosPartidas(true);
		}
		public void Remove(Expedient parent, Expense item)
		{
			base.Remove(item);
		}
		public void Remove(PayrollBatch parent, Expense item)
		{
			if (item.OidPago != 0) return;
			parent.CalculateTotal();
			base.Remove(item);
		}

		public void SetNextCode(Expense item) { SetNextCode(null, item); }
		public void SetNextCode(Expedient parent, Expense item)
		{
			switch (item.ECategoriaGasto)
			{
				case ECategoriaGasto.GeneralesExpediente:
					{
						item.Serial = 0;
						item.Codigo = string.Empty;
					}
					break;

				default:
					{
						if (item.OidFactura != 0)
						{
							item.Serial = 0;
							item.Codigo = string.Empty;
							return;
						}

						int index = this.IndexOf(item);

						if (index == 0)
						{
							item.GetNewCode(parent);

							if (MaxSerial[item.ECategoriaGasto] == null) MaxSerial.Add(item.ECategoriaGasto, item.Serial);
							MaxSerial[item.ECategoriaGasto] = item.Serial;
						}
						else
						{
							if (MaxSerial[item.ECategoriaGasto] == null)
							{
								item.GetNewCode(parent);
								MaxSerial.Add(item.ECategoriaGasto, item.Serial);
							}
							else
							{
								item.Serial = (long)MaxSerial[item.ECategoriaGasto] + 1;
								item.Codigo = item.Serial.ToString(Resources.Defaults.GASTO_CODE_FORMAT);
							}

							MaxSerial[item.ECategoriaGasto] = item.Serial;
						}
					}
					break;
			}
		}

		public Expense GetItem(Expense gasto)
		{
			foreach (Expense item in this)
				if ((item.Serial == gasto.Serial) && (item.ECategoriaGasto == gasto.ECategoriaGasto) && (item.OidExpediente == gasto.OidExpediente))
					return item;

			return null;
		}
		public Expense GetItem(Expense gasto, InputDeliveryLineInfo ca)
		{
			foreach (Expense item in this)
				if ((item.Serial == gasto.Serial) && (item.ECategoriaGasto == gasto.ECategoriaGasto) && (item.OidConceptoAlbaran == ca.Oid))
					return item;

			return null;
		}

		public Expense GetItemByConceptoFactura(InputInvoiceLineInfo cf)
		{
			return GetItemByConceptoFactura(cf.Oid);
		}
		public Expense GetItemByConceptoFactura(long oidConcepto)
		{
			foreach (Expense item in this)
				if (item.OidConceptoFactura == oidConcepto)
					return item;

			return null;
		}
		public Expense GetItemByConceptos(InputInvoiceLineInfo cf, InputDeliveryLineInfo ca)
		{
			foreach (Expense item in this)//Si la factura es nueva, item.OidConceptoFactura no va a ser igual a cf.Oid
				if ((item.OidConceptoFactura == cf.Oid) && (item.OidConceptoAlbaran == ca.Oid))
					return item;

			return null;
		}
		public Expense GetItemByFactura(long oid_factura)
		{
			foreach (Expense item in this)
				if (item.OidFactura == oid_factura)
					return item;

			return null;
		}
		public Expense GetItemByFactura(InputInvoiceInfo source)
		{
			return GetItemByFactura(source.Oid, source.ETipoAcreedor);
		}
		public Expense GetItemByFactura(InputInvoiceInfo source, Expedient expediente)
		{
			foreach (Expense item in this)
				if ((item.OidFactura == source.Oid) && (item.OidExpediente == expediente.Oid))
					return item;

			return null;
		}
		public Expense GetItemByFactura(long oid_factura, ETipoAcreedor tipo)
		{
			foreach (Expense item in this)
				if ((item.OidFactura == oid_factura) && (item.ETipoAcreedor == tipo))
					return item;

			return null;
		}
		public Expense GetItemByFactura(long oid_factura, ECategoriaGasto tipo)
		{
			foreach (Expense item in this)
			{
				if (tipo == ECategoriaGasto.NoStock)
				{
					if ((item.OidFactura == oid_factura) && (item.ECategoriaGasto != ECategoriaGasto.Stock))
						return item;
				}
				else
				{
					if ((item.OidFactura == oid_factura) && (item.ECategoriaGasto == tipo))
						return item;
				}
			}

			return null;
		}
		public Expense GetItemByFactura(long oid_factura, ETipoAcreedor tipo_acreedor, ECategoriaGasto tipo)
		{
			foreach (Expense item in this)
			{
				if (tipo == ECategoriaGasto.NoStock)
				{
					if ((item.OidFactura == oid_factura) && (item.ECategoriaGasto != ECategoriaGasto.Stock) && (item.ETipoAcreedor == tipo_acreedor))
						return item;
				}
				else
				{
					if ((item.OidFactura == oid_factura) && (item.ECategoriaGasto == tipo) && (item.ETipoAcreedor == tipo_acreedor))
						return item;
				}
			}

			return null;
		}
		public Expense GetItemByFactura(ETipoAcreedor tipo_acreedor, ECategoriaGasto tipo)
		{
			foreach (Expense item in this)
				if ((item.ECategoriaGasto == tipo) && (item.ETipoAcreedor == tipo_acreedor))
					return item;

			return null;
		}

		public Expense GetItemByAcreedor(long oid_acreedor, ETipoAcreedor tipo)
        {
            foreach (Expense item in this)
                if ((item.OidAcreedor == oid_acreedor) && (item.ETipoAcreedor == tipo))
                    return item;

            return null;
        }

        public Expense GetItemByTipoAcreedor(ETipoAcreedor tipo)
        {
            foreach (Expense item in this)
                if (item.ETipoAcreedor == tipo)
                    return item;

            return null;
        }

		public void UpdatePagoValues(Payment pago)
		{
			Expense item;
			decimal acumulado;

			for (int i = 0; i < Items.Count; i++)
			{
				item = Items[i];

				/*if (item.OidPago != pago.Oid)
					item.Asignado = 0;*/

				if (i == 0) acumulado = 0;
				else acumulado = Items[i - 1].Acumulado;

				item.Acumulado = acumulado + item.Pendiente;
				item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
			}
		}

		public static void DeleteItems(long oidRemesaNomina)
		{
			QueryConditions conditions = new QueryConditions { RemesaNomina = PayrollBatchInfo.New(oidRemesaNomina) };
			Expenses list = Expenses.GetList(conditions, false);
			list.Clear();
			list.Save();
			list.CloseSession();
		}
        
		#endregion

		#region Common Factory Methods

		private Expenses() { }

		public List<Expense> GetSubList(InputInvoiceInfo factura)
		{
			List<Expense> list = new List<Expense>();

			if (factura == null) return list;

			foreach (Expense item in this)
			{
				if (item.OidFactura == factura.Oid)
					list.Add(item);
			}

			return list;
		}
		public List<Expense> GetSubList(ECategoriaGasto tipo)
		{
			List<Expense> list = new List<Expense>();

			foreach (Expense item in this)
			{
				if (item.ECategoriaGasto == tipo)
					list.Add(item);
			}

			return list;
		}

		public List<ExpenseInfo> GetSubListInfoByTipo(ECategoriaGasto tipo)
		{
			List<ExpenseInfo> list = new List<ExpenseInfo>();

			foreach (Expense item in this)
			{
				if (item.ECategoriaGasto == tipo)
					list.Add(item.GetInfo());
			}

			return list;
		}

		public List<Expense> GetSubListFactura()
		{
			List<Expense> list = new List<Expense>();

			foreach (Expense item in this)
			{
				if (item.OidFactura != 0)
					list.Add(item);
			}

			return list;
		}
		public List<Expense> GetSubListOtrosGastos() 
		{
			List<Expense> list = new List<Expense>();

			foreach (Expense item in this)
			{
				if ((item.ECategoriaGasto == ECategoriaGasto.OtrosExpediente) && (item.OidFactura == 0))
					list.Add(item);
			}

			return list;
		}

		public Expenses GetListAgrupada()
		{
			Expenses gastos = Expenses.NewList();

			foreach (Expense item in this)
			{
				if (item.OidConceptoFactura != 0)
				{
					Expense gasto = gastos.GetItemByConceptoFactura(item.OidConceptoFactura);

					if (gasto == null)
						gastos.AddItem(item.Clone());
					else
						gasto.Total += item.Total;
				}
				else
				{
					Expense gasto = gastos.GetItem(item);

					if (gasto == null)
						gastos.AddItem(item.Clone());
					else
						gasto.Total += item.Total;
				}
			}

			return gastos;
		}
		public static Expenses GetListAgrupada(List<Expense> list)
		{
			Expenses gastos = Expenses.NewList();

			foreach (Expense item in list)
			{
				if (item.OidConceptoFactura != 0)
				{
					Expense gasto = gastos.GetItemByConceptoFactura(item.OidConceptoFactura);

					if (gasto == null)
						gastos.AddItem(item.Clone());
					else
						gasto.Total += item.Total;
				}
				else
				{
					Expense gasto = gastos.GetItem(item);

					if (gasto == null)
						gastos.AddItem(item.Clone());
					else
						gasto.Total += item.Total;
				}
			}

			return gastos;
		}

		#endregion

		#region Root Factory Methods

		public static Expenses NewList() { return new Expenses(); }

		public static Expenses GetList() { return GetList(new QueryConditions(), true); }
		public static Expenses GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SELECT(conditions), childs);
		}
		public static Expenses GetList(string query, bool childs)
		{
			CriteriaEx criteria = Expense.GetCriteria(Expense.OpenSession());
			criteria.Childs = childs;

			//No criteria. Retrieve all de List

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			Expense.BeginTransaction(criteria.Session);

			Expenses list = DataPortal.Fetch<Expenses>(criteria);
			return list;
		}

		public static Expenses GetPendientesList(ECategoriaGasto tipo, bool childs)
		{
			return GetPendientesList(tipo, null, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static Expenses GetPendientesList(ECategoriaGasto tipo, Payment pago, bool childs)
		{
			return GetPendientesList(tipo, pago, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static Expenses GetPendientesList(ECategoriaGasto categoria, Payment pago, DateTime f_ini, DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				CategoriaGasto = categoria,
				Payment = (pago != null) ? pago.GetInfo(false) : null,
				FechaIni = f_ini,
				FechaFin = f_fin,
			};

			return GetPendientesList(conditions, childs);
		}
		public static Expenses GetPendientesList(QueryConditions conditions, bool childs)
		{
			return GetList(Expenses.SELECT_PENDIENTES(conditions), childs);
		}

		public static Expenses GetByPagoAndPendientesList(ECategoriaGasto categoria, Payment pago, bool childs)
		{
			Expenses byPago = GetByPagoList(pago, childs);
			Expenses pendientes = GetPendientesList(categoria, childs);

			Expenses list = Expenses.NewList();

			foreach (Expense item in byPago)
				list.AddItem(item);

			foreach (Expense item in pendientes)
				if (list.GetItem(item.Oid) == null) list.AddItem(item);

			return list;
		}

		public static Expenses GetByPagoList(Payment pago, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Payment = pago.GetInfo(false) };

			return GetList(SELECT_BY_PAGO(conditions), childs);
		}

		public void Save(Payment parent)
		{
			foreach (Expense item in this)
			{
				if (item.Asignado == item.Total)
				{
					item.OidPago = parent.Oid;
					//item.EEstado = EEstado.Pagado;
				}
				else
				{
					item.OidPago = 0;
					item.EEstado = EEstado.Abierto;
				}
			}

			ApplyEdit();
			Save();
		}
		
		#endregion

		#region Child Factory Methods

		public Expenses(bool is_child)
        {
			if (is_child) MarkAsChild();
        }
        private Expenses(IList<Expense> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Expenses(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static Expenses NewChildList() { return new Expenses(true); }

        public static Expenses GetChildList(IList<Expense> lista) { return new Expenses(lista); }
		public static Expenses GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static Expenses GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Expenses(sessionCode, reader, childs); }

        public static Expenses GetChildList(Expedient parent, bool childs)
        {
            CriteriaEx criteria = Expense.GetCriteria(parent.SessionCode);
            criteria.Query = Expenses.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<Expenses>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    Expense.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        Expense item = Expense.GetChild(SessionCode, reader);
						this.AddItem(item);

						if (MaxSerial[item.ECategoriaGasto] == null)
							MaxSerial.Add(item.ECategoriaGasto, item.Serial);
						else if (item.Serial > (long)MaxSerial[item.ECategoriaGasto])
							MaxSerial[item.ECategoriaGasto] = item.Serial;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

		protected override void DataPortal_Update()
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (Expense obj in DeletedList)
				obj.DeleteSelf(this);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (Expense obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
						{
							if (this.IndexOf(obj) == 0) obj.GetNewCode();
							else SetNextCode(obj);

							obj.Insert(this);
						}
						else
							obj.Update(this);
					}
				}

				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				BeginTransaction();
				this.RaiseListChangedEvents = true;
			}
		}

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<Expense> lista)
        {
            this.RaiseListChangedEvents = false;

			foreach (Expense item in lista)
			{
				this.AddItem(Expense.GetChild(item));
			}

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

			while (reader.Read())
			{
				Expense item = Expense.GetChild(SessionCode, reader);
				this.AddItem(item);

				if (MaxSerial[item.ECategoriaGasto] == null)
					MaxSerial.Add(item.ECategoriaGasto, item.Serial);
				else if (item.Serial > (long)MaxSerial[item.ECategoriaGasto])
					MaxSerial[item.ECategoriaGasto] = item.Serial;
			}

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Expedient parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Expense obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Expense obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }

		internal void Update(PayrollBatch parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (Expense obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (Expense obj in this)
			{
				if (obj.IsNew)
					obj.Insert(parent);
				else
					obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Expense.SELECT(conditions, true); }
		public static string SELECT(Expedient source) { return Expense.SELECT(new QueryConditions { Expedient = source.GetInfo(false), CategoriaGasto = ECategoriaGasto.Expediente }, true); }
		public static string SELECT(PayrollBatch source) { return Expense.SELECT(new QueryConditions { RemesaNomina = source.GetInfo(false) }, true); }
		public static string SELECT(Payment source) { return Expense.SELECT(new QueryConditions { Payment = source.GetInfo(false) }, true); }
		public static string SELECT_BY_PAGO(QueryConditions conditions) { return Expense.SELECT(conditions, true); }
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return Expense.SELECT_PENDIENTES(conditions, true); }

        #endregion
    }
}