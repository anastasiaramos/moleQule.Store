using System;
using System.Collections.Generic;

using moleQule.Hipatia;

namespace moleQule.Library.Store
{
    public partial class AgenteSelector : AgenteSelectorBase
    {
        #region Business Methods

        #endregion

        #region Source

        public new static IAgenteHipatiaList GetAgentes(EntidadInfo entidad)
        {
            IAgenteHipatiaList lista = new IAgenteHipatiaList(new List<IAgenteHipatia>());

			if (entidad.Tipo == typeof(Almacen).Name)
			{
				StoreList list = StoreList.GetList(false);

				foreach (StoreInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Despachante).Name)
			{
				DespachanteList list = DespachanteList.GetList(false);

				foreach (DespachanteInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Employee).Name)
			{
				EmployeeList list = EmployeeList.GetList(false);

				foreach (EmployeeInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Expedient).Name)
			{
				ExpedienteList list = ExpedienteList.GetList(false);

				foreach (ExpedientInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(InputInvoice).Name)
			{
				InputInvoiceList list = InputInvoiceList.GetList(false);

				foreach (InputInvoiceInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(LineaFomento).Name)
			{
				LineaFomentoList list = LineaFomentoList.GetList(false);

				foreach (LineaFomentoInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Naviera).Name)
			{
				NavieraList list = NavieraList.GetList(false);

				foreach (NavieraInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Payment).Name)
			{
				PaymentList list = PaymentList.GetList(false);

				foreach (PaymentInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Product).Name)
			{
				ProductList list = ProductList.GetList(false);

				foreach (ProductInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
			else if (entidad.Tipo == typeof(Proveedor).Name)
            {
				ProveedorList list = ProveedorList.GetList(false);

				foreach (ProveedorInfo obj in list)
                {
                    if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
                        lista.Add(obj);
                }
            }
			else if (entidad.Tipo == typeof(Transporter).Name)
			{
				TransporterList list = TransporterList.GetList(false);

				foreach (TransporterInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
            else
                throw new iQException("No se ha encontrado el tipo de entidad " + entidad.Tipo);

            return lista;
        }

        #endregion
    }
}