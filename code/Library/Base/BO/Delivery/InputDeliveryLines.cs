using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Common.Structs;
using moleQule;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class InputDeliveryLines : BusinessListBaseEx<InputDeliveryLines, InputDeliveryLine>
    {
        #region Business Methods
	
        public InputDeliveryLine NewItem(InputDelivery parent)
        {
            this.NewItem(InputDeliveryLine.NewChild(parent));
            return this[Count - 1];
        }

		public InputDeliveryLine NewItem(InputDelivery parent, InputDeliveryLineInfo line)
        {
			this.NewItem(InputDeliveryLine.NewChild(parent, line));
            return this[Count - 1];
        }

		public InputDeliveryLine NewItem(InputDelivery parent, IDocumentLine line, ProductInfo product, decimal currencyRate)
		{
			this.NewItem(InputDeliveryLine.NewChild(parent, line, product, currencyRate));
			return this[Count - 1];
		}

        public void Remove(InputDeliveryLine item, bool cache)
        {
            base.Remove(item);

            /*if (cache)
            {
                PartidaList list = Cache.Instance.Get(typeof(PartidaList)) as PartidaList;
                if (list == null) return;

                PartidaInfo pExp = list.GetItem(item.OidPartida);
                if (pExp != null) 
                {
                    pExp.StockKilos -= item.CantidadKilos;
                    pExp.StockBultos -= item.CantidadBultos;
                }
            }*/
        }

        #endregion

        #region Factory Methods

        private InputDeliveryLines()
        {
            MarkAsChild();
        }
        private InputDeliveryLines(IList<InputDeliveryLine> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private InputDeliveryLines(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
        
        public static InputDeliveryLines NewChildList() { return new InputDeliveryLines(); }

        public static InputDeliveryLines GetChildList(IList<InputDeliveryLine> lista) { return new InputDeliveryLines(lista); }
		public static InputDeliveryLines GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static InputDeliveryLines GetChildList(int sessionCode, IDataReader reader, bool childs) { return new InputDeliveryLines(sessionCode, reader, childs); }

		internal void LoadLibroGanadero()
		{
			Hashtable oidExpedientes = new Hashtable();

			oidExpedientes.Add(0, 0);

			foreach (InputDeliveryLine item in this)
			{
                if (item.OidExpediente == 0) continue;
				if (oidExpedientes.ContainsKey(item.OidExpediente)) continue;

				oidExpedientes.Add(item.OidExpediente, item.OidExpediente);

				Expedient expediente = Store.Expedient.Get(item.OidExpediente, false, true, SessionCode);
				if (expediente == null) continue;
				if (expediente.ETipoExpediente != ETipoExpediente.Ganado) continue;

				LivestockBook libro = LivestockBook.Get(1, false, true, SessionCode);
				libro.LoadLineasByExpediente(item.OidExpediente, false);
			}

			oidExpedientes.Clear();
		}

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<InputDeliveryLine> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (InputDeliveryLine item in lista)
                this.AddItem(InputDeliveryLine.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(InputDeliveryLine.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
        		
        internal void Update(InputDelivery parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				LoadLibroGanadero();

				// update (thus deleting) any deleted child objects
				foreach (InputDeliveryLine obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();				

				// add/update any current child objects
				foreach (InputDeliveryLine obj in this)
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
					{
						obj.Update(parent);
					}
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }
       
		#endregion

		#region SQL

		public static string SELECT(InputDelivery delivery)
		{
			string query;

			QueryConditions conditions = new QueryConditions { InputDelivery = delivery.GetInfo(false) };
            query = InputDeliveryLineSQL.SELECT(conditions, true);

			return query;
		}

		#endregion
	}
}

