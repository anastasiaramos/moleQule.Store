using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class LineasFomento : BusinessListBaseEx<LineasFomento, LineaFomento>, IEntidadRegistroList
	{
		#region IEntidadRegistroList

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (LineaFomento obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion
		
		#region Business Methods

		public decimal GetTotalAyudas()
		{
			decimal total = 0;

			foreach (LineaFomento item in this)
				if (item.EEstado != EEstado.Anulado)
					total += item.Subvencion;

			return total;
		}

		public void SetNextCode(Expedient parent, LineaFomento item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode();
			}
			else
			{
				item.Serial = this[index - 1].Serial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			}
		}

		public LineaFomento NewItem(Expedient parent, Batch source)
		{
			if (GetItemByPartida(source.GetInfo(false)) != null)
				throw new iQException(String.Format(Resources.Messages.LINEA_FOMENTO_DUPLICATED, source.CodigoAduanero));

			this.NewItem(LineaFomento.NewChild(parent, source));
			return this[Count - 1];
		}
		public LineaFomento NewItem(Expedient parent, BatchInfo source)
		{
			if (GetItemByPartida(source) != null)
				throw new iQException(String.Format(Resources.Messages.LINEA_FOMENTO_DUPLICATED, source.CodigoAduanero));

			this.NewItem(LineaFomento.NewChild(parent, source));
			return this[Count - 1];
		}

		public void UpdateItems(Expedient parent)
		{
			foreach (LineaFomento item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				item.CopyFrom(parent);
			}
		}

		public LineaFomento GetItemByPartida(BatchInfo source)
		{
			foreach (LineaFomento item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;

				if (item.CodigoAduanero == source.CodigoAduanero)
					return item;
			}
			return null;
		}

		public LineasFomento GetListByFactura(InputInvoiceInfo factura, LineaFomento linea)
		{
			LineasFomento lista = LineasFomento.NewChildList();

			foreach (LineaFomento item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;

				if ((item.Conocimiento == factura.NFactura) && (item.OidNaviera == factura.OidAcreedor) && (item.Oid != linea.Oid))
					lista.AddItem(item);
			}

			return lista;
		}

		public void RemoveItem(LineaFomento item)
		{
			if (item.EEstado != EEstado.Abierto)
				throw new iQException(Resources.Messages.DELETE_LINEA_FOMENTO_NOT_ALLOWED);

			Remove(item.Oid);
		}

		public void SetValues(Expedient parent, InputInvoiceInfo factura)
		{
			if (Items.Count != 1) return;

			LineaFomento item = Items[0];

			item.SetValues(factura);

			parent.UpdateTotalCostesCompensables();
		}

		public void SetValues(Expedient parent, InputInvoiceInfo factura, LineaFomento linea)
		{
			LineasFomento lista = GetListByFactura(factura, linea);

			if (lista.Count == 0)
			{
				linea.SetValues(factura);
			}
			else
			{
				decimal total_kilos = linea.Kilos;

				foreach (LineaFomento item in lista)
					total_kilos += item.Kilos;

				linea.SetValues(factura, (linea.Kilos / total_kilos));

				foreach (LineaFomento item in lista)
					item.SetValues(factura, (item.Kilos / total_kilos));
			}

			parent.UpdateTotalCostesCompensables();
		}

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineasFomento() { }

		#endregion		
		
		#region Root Factory Methods

		public static LineasFomento GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = LineaFomento.GetCriteria(LineaFomento.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(conditions);

			LineaFomento.BeginTransaction(criteria.SessionCode);

			LineasFomento list = DataPortal.Fetch<LineasFomento>(criteria);
			return list;
		}

		public static LineasFomento GetInformeFomentoList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = LineaFomento.GetCriteria(LineaFomento.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LineasFomento.SELECT_INFORME_FOMENTO(conditions);

			LineaFomento.BeginTransaction(criteria.SessionCode);

			LineasFomento list = DataPortal.Fetch<LineasFomento>(criteria);
			return list;
		}

		#endregion

		#region Child Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private LineasFomento(IList<LineaFomento> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private LineasFomento(IDataReader reader, bool retrieve_childs)
        {
            MarkAsChild();
            Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LineasFomento NewChildList() 
        { 
            LineasFomento list = new LineasFomento(); 
            list.MarkAsChild(); 
            return list; 
        }

		public static LineasFomento GetChildList(Expedient parent, bool childs)
		{
			CriteriaEx criteria = LineaFomento.GetCriteria(parent.SessionCode);
			criteria.Query = LineasFomento.SELECT(parent);
			criteria.Childs = childs;

			LineasFomento list = DataPortal.Fetch<LineasFomento>(criteria);

			parent.UpdateTotalCostesCompensables();

			return list;
		}
		public static LineasFomento GetChildList(IList<LineaFomento> lista) { return new LineasFomento(lista); }
        public static LineasFomento GetChildList(IDataReader reader) { return GetChildList(reader, true); }
        public static LineasFomento GetChildList(IDataReader reader, bool childs) { return new LineasFomento(reader, childs); }

		public static LineasFomento GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		internal static LineasFomento GetChildList(int sessionCode, string query, bool childs)
		{
			if (!LineaFomento.CanEditObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = LineaFomento.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<LineasFomento>(criteria);
		}

		#endregion

		#region Common Data Access

		private void Fetch(CriteriaEx criteria)
		{
			try
			{
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					LineaFomento.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(LineaFomento.GetChild(reader));
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

		#endregion

		#region Root Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			Fetch(criteria);
		}

		protected override void DataPortal_Update()
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (LineaFomento obj in DeletedList)
				obj.DeleteSelf(this);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (LineaFomento obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
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

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<LineaFomento> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (LineaFomento item in lista)
				this.AddItem(LineaFomento.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
		
        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen con los elementos a insertar</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(LineaFomento.GetChild(reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Expedient parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (LineaFomento obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (LineaFomento obj in this)
			{	
				if (!this.Contains(obj))
				{
					if (obj.IsNew)
					{
						SetNextCode(parent, obj);
						obj.Insert(parent);
					}
					else
						obj.Update(parent);
				}
			}

			this.RaiseListChangedEvents = true;
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return LineaFomento.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LineaFomento.SELECT(conditions, true); }
		public static string SELECT(Expedient parent) { return LineaFomento.SELECT(new QueryConditions{ Expedient = parent.GetInfo(false) }, true); }
		public static string SELECT_INFORME_FOMENTO(QueryConditions conditions) { return LineaFomento.SELECT_INFORME_FOMENTO(conditions, true); }
		
		#endregion
    }
}