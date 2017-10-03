using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
    /// <summary>
    /// ReadOnly Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ExpedienteList : ReadOnlyListBaseEx<ExpedienteList, ExpedientInfo>
    {
        #region Business Methods

        public decimal GetTotalExpenses()
        {
            return Items.Sum(x => x.GastoTotal);
        }

        #endregion

        #region Common Factory Methods

        private ExpedienteList() { }
        private ExpedienteList(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static ExpedienteList GetChildList(int sessionCode, IDataReader reader)
        {
            return ExpedienteList.GetChildList(sessionCode, reader, true);
        }
		public static ExpedienteList GetChildList(int sessionCode, IDataReader reader, bool childs)
        {
            return new ExpedienteList(sessionCode, reader, childs);
        }
		public static ExpedienteList GetChildList(IList<ExpedientInfo> list)
		{
			ExpedienteList flist = new ExpedienteList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (ExpedientInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

		#endregion

		#region Root Factory Methods

		public static ExpedienteList NewList() { return new ExpedienteList(); }

        public static ExpedienteList GetList() { return ExpedienteList.GetList(false); }
        public static ExpedienteList GetList(bool childs)
        {
			return GetList(ExpedienteList.SELECT(), childs);
        }
		public static ExpedienteList GetList(ETipoExpediente t) { return ExpedienteList.GetList(t, true); }
		public static ExpedienteList GetList(ETipoExpediente t, bool childs)
		{
			return GetList(ExpedienteList.SELECT_BY_TYPE(t), childs);
		}
		public static ExpedienteList GetList(ETipoExpediente t, bool childs, bool cache)
		{
			ExpedienteList list;

			if (!Cache.Instance.Contains(typeof(ExpedienteList)))
			{
				list = ExpedienteList.GetList(t, childs);
				Cache.Instance.Save(typeof(ExpedienteList), list);
			}
			else
				list = Cache.Instance.Get(typeof(ExpedienteList)) as ExpedienteList;

			return list;
		}

		public static ExpedienteList GetList(List<long> oidList, bool childs)
		{
			QueryConditions conditions = new QueryConditions { OidList = oidList };		
			return GetList(ExpedienteList.SELECT(conditions), childs);
		}
        public static ExpedienteList GetList(Relations list, bool childs)
        {
            QueryConditions conditions = new QueryConditions { OidList = list.ToChildsOidList() };
            return GetList(ExpedienteList.SELECT(conditions), childs);
        }

		public static ExpedienteList GetList(QueryConditions conditions, ExpedientInfo fromExp, ExpedientInfo tillExp)
		{
			CriteriaEx criteria = Stock.GetCriteria(Expedient.OpenSession());
			criteria.Childs = false;

			criteria.Query = ExpedienteList.SELECT(conditions, fromExp, tillExp);

			ExpedienteList list = DataPortal.Fetch<ExpedienteList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static ExpedienteList GetListByYear(ETipoExpediente t, int ano, bool childs)
		{
			return GetList(ExpedienteList.SELECT_BY_TYPE_AND_YEAR(t, ano), childs);
		}
		public static ExpedienteList GetListByAcreedor(ETipoAcreedor tipo, long oid)
		{
			return GetList(ExpedienteList.SELECT_BY_ACREEDOR(tipo, oid), false);
		}
		public static ExpedienteList GetListFomento(string cod_aduanero,
														NavieraInfo naviera,
														PuertoInfo p_origen,
														PuertoInfo p_destino,
														DateTime from,
														DateTime till)
		{
			CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			criteria.Childs = false;

			//No criteria. Retrieve all de List

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ExpedienteList.SELECT_FOMENTO(cod_aduanero,
																naviera,
																p_origen,
																p_destino,
																from,
																till);

			ExpedienteList list = DataPortal.Fetch<ExpedienteList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static ExpedienteList GetListSinFomento(ETipoExpediente t, bool childs) 
		{
			return GetList(ExpedienteList.SELECT_SIN_FOMENTO(t), childs);
		}

		public static ExpedienteList GetListByRango(ExpedientInfo ini, ExpedientInfo fin, bool childs)
		{
			return GetList(ExpedienteList.SELECT_BY_RANGO(ini, fin), childs);
		}

		public static ExpedienteList GetListByStockProducto(IStockable line)
        {
            QueryConditions conditions = new QueryConditions
            {
                IStockable = line
            };

            return GetList(SELECT_BY_STOCK_PRODUCTO(conditions), false);
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
		private static ExpedienteList GetList(string query, bool childs)
		{
			if (!Expedient.CanGetObject())
				throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;

			ExpedienteList list = DataPortal.Fetch<ExpedienteList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
        public static ExpedienteList GetList(IList<ExpedientInfo> list)
        {
            ExpedienteList flist = new ExpedienteList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ExpedientInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static ExpedienteList GetList(IList<Expedient> list)
        {
            ExpedienteList flist = new ExpedienteList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Expedient item in list)
                    flist.AddItem(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ExpedientInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, ETipoExpediente t)
		{
			SortedBindingList<ExpedientInfo> sortedList = new SortedBindingList<ExpedientInfo>(GetList(t));

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		public static SortedBindingList<ExpedientInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs, ETipoExpediente t)
		{
			SortedBindingList<ExpedientInfo> sortedList = new SortedBindingList<ExpedientInfo>(GetList(t, childs));

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		public static SortedBindingList<ExpedientInfo> GetSortedListByYear(bool childs, ETipoExpediente t, int ano, string sortProperty, ListSortDirection sortDirection)
		{
			CriteriaEx criteria = Expedient.GetCriteria(Expedient.OpenSession());
			criteria.Childs = childs;

			criteria.Query = ExpedienteList.SELECT_BY_TYPE_AND_YEAR(t, ano);

			ExpedienteList list = DataPortal.Fetch<ExpedienteList>(criteria);

			CloseSession(criteria.SessionCode);

			SortedBindingList<ExpedientInfo> sortedList = new SortedBindingList<ExpedientInfo>(list);

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}

        #endregion

		#region Common Data Access

		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;

			while (reader.Read())
				this.AddItem(ExpedientInfo.Get(SessionCode, reader, Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(ExpedientInfo.Get(SessionCode, reader, Childs));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions)
		{
			return SELECT(conditions, null, null);
		}
		public static string SELECT(QueryConditions conditions, ExpedientInfo from, ExpedientInfo till)
		{
			string query = string.Empty;

			query = Expedient.FIELDS(conditions) +
                Expedient.JOIN(conditions) +
                Expedient.WHERE(conditions);

			if (from != null)
				query += " AND E.\"CODIGO\" >='" + from.Codigo + "'";

			if (till != null)
				query += " AND E.\"CODIGO\" <='" + till.Codigo + "'";

			query += " ORDER BY E.\"CODIGO\"";

			return query;
		}

		public static string SELECT_BY_RANGO(ExpedientInfo from, ExpedientInfo till)
		{
            QueryConditions conditions = new QueryConditions();

            return 
            Expedient.FIELDS(conditions) +
            Expedient.JOIN(conditions) +
            Expedient.WHERE(conditions) + @"
			    AND E.""CODIGO"" BETWEEN '" + from.Codigo + "' AND '" + till.Codigo + "'" + @"
			ORDER BY E.""CODIGO""";
		}

        public static string SELECT_BY_TYPE(ETipoExpediente expedientType)
        {
            QueryConditions conditions = new QueryConditions();

            string query = 
            Expedient.FIELDS(conditions) +
            Expedient.JOIN(conditions) +
            Expedient.WHERE(conditions);
            
            if (expedientType != ETipoExpediente.Todos)
                query += " AND E.\"TIPO_EXPEDIENTE\" = " + (long)expedientType;

            query += " ORDER BY E.\"CODIGO\"";

            return query;
        }

        public static string SELECT_BY_TYPE_AND_YEAR(ETipoExpediente expedientType, int year)
        {
            QueryConditions conditions = new QueryConditions();

            string query = 
            Expedient.FIELDS(conditions) +
            Expedient.JOIN(conditions) +
            Expedient.WHERE(conditions);
            
			if (expedientType != ETipoExpediente.Todos)
				query += " AND E.\"TIPO_EXPEDIENTE\" = " + (long)expedientType;

            query += " AND \"ANO\" = " + year.ToString();

			query += " ORDER BY E.\"CODIGO\"";

            return query;
        }

        public static string SELECT_BY_ACREEDOR(ETipoAcreedor expedientType, long oid)
        {
            string tabla = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));

            QueryConditions conditions = new QueryConditions();

            string query = 
            Expedient.FIELDS(conditions) +
            Expedient.JOIN(conditions) +
            Expedient.WHERE(conditions);

            switch (expedientType)
            {
				case ETipoAcreedor.Despachante:
					query = " AND \"OID_DESPACHANTE\" = " + oid;
					break;

				case ETipoAcreedor.Naviera:
					query = " AND \"OID_NAVIERA\" = " + oid;
					break;

                case ETipoAcreedor.Proveedor:
                    query = " AND \"OID_PROVEEDOR\" = " + oid;
                    break;

                case ETipoAcreedor.TransportistaOrigen:
                    query = " AND \"OID_TRANS_ORIGEN\" = " + oid;
                    break;

                case ETipoAcreedor.TransportistaDestino:
                    query = " AND \"OID_TRANS_DESTINO\" = " + oid;
                    break;
            }

            return query;
        }

        public static string SELECT_BY_STOCK_PRODUCTO(QueryConditions conditions)
        {
            string e = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ExpedientRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.SupplierRecord));
            string d = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.CustomAgentRecord));
            string t = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.TransporterRecord));
            string n = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.ShippingCompanyRecord));
            string pe = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.BatchRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.StockRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.REAChargeRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(moleQule.Invoice.Data.ChargeRecord));
            string cbn = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            
            string query = 
            Expedient.FIELDS() +
            " FROM " + e + " AS E" +
            " LEFT JOIN " + su + " AS P ON P.\"OID\" = E.\"OID_PROVEEDOR\"" +
            " LEFT JOIN " + d + " AS D ON D.\"OID\" = E.\"OID_DESPACHANTE\"" +
            " LEFT JOIN " + t + " AS T_ORIG ON T_ORIG.\"OID\" = E.\"OID_TRANS_ORIGEN\"" +
            " LEFT JOIN " + t + " AS T_DEST ON T_DEST.\"OID\" = E.\"OID_TRANS_DESTINO\"" +
            " LEFT JOIN " + n + " AS NAV ON NAV.\"OID\" = E.\"OID_NAVIERA\"" +
            " LEFT JOIN (SELECT \"OID_EXPEDIENTE\"," +
            "               SUM(\"KILOS_INICIALES\") AS \"STOCK_K_INICIAL\"," +
            "               SUM(\"BULTOS_INICIALES\") AS \"STOCK_B_INICIAL\"," +
            "               SUM(\"AYUDA_RECIBIDA_KILO\" * \"KILOS_INICIALES\") AS \"TOTAL_REA_ESTIMADA\"," +
            "               \"OID_PRODUCTO\"" +
            "               FROM " + pe + " GROUP BY \"OID_EXPEDIENTE\", \"OID_PRODUCTO\")" +
            "       AS PE ON PE.\"OID_EXPEDIENTE\" = E.\"OID\"" +
            " LEFT JOIN (SELECT \"OID_EXPEDIENTE\"," +
            "                   SUM(\"KILOS\") AS \"STOCK_K\"," +
            "                   SUM(\"BULTOS\") AS \"STOCK_B\"" +
            "               FROM " + s + " GROUP BY \"OID_EXPEDIENTE\")" +
            "       AS S ON S.\"OID_EXPEDIENTE\" = E.\"OID\"" +
            " LEFT JOIN (SELECT \"OID_EXPEDIENTE\", MAX(\"FECHA\") AS \"FECHA_COBRO_REA\"," +
            "                   MAX(CB.\"VALOR\") AS \"CUENTA_COBRO_REA\"," +
            "                   SUM(\"CANTIDAD\") AS \"TOTAL_REA_COBRADA\"" +
            "               FROM " + cr + " AS CR1" +
            "               INNER JOIN " + cb + " AS C ON C.\"OID\" = CR1.\"OID_COBRO\"" +
            "               LEFT JOIN " + cbn + " AS CB ON CB.\"OID\" = C.\"OID_CUENTA_BANCARIA\"" +
            "               GROUP BY \"OID_EXPEDIENTE\")" +
            "      AS CR ON E.\"OID\" = CR.\"OID_EXPEDIENTE\"" +
            " WHERE PE.\"OID_PRODUCTO\" = " + conditions.IStockable.OidProduct + " AND S.\"STOCK_K\" >= " + conditions.IStockable.Kilos;

            return query;
        }

        public static string SELECT_FOMENTO(    string cod_aduanero,
                                                NavieraInfo naviera,
                                                PuertoInfo p_origen,
                                                PuertoInfo p_destino,
                                                DateTime from,
                                                DateTime till)
        {
            QueryConditions conditions = new QueryConditions();

            string query = Expedient.FIELDS(conditions) +
                Expedient.JOIN(conditions) +
                Expedient.WHERE(conditions) +
                    " AND E.\"FECHA_CONOCIMIENTO\" >= '" + from.ToString("MM/dd/yyyy") + "'" +
                    " AND E.\"FECHA_CONOCIMIENTO\" <= '" + till.ToString("MM/dd/yyyy") + "'";

			if (cod_aduanero != string.Empty) query += " AND E.\"CODIGO_ARTICULO\" = '" + cod_aduanero + "'";
            if (naviera != null) query += " AND E.\"OID_NAVIERA\" = " + naviera.Oid;
            if (p_origen != null) query += " AND E.\"PUERTO_ORIGEN\" = '" + p_origen.Valor + "'";
            if (p_destino != null) query += " AND E.\"PUERTO_DESTINO\" = '" + p_destino.Valor + "'";

            query += " ORDER BY E.\"CODIGO\"";

            return query;
        }

        public static string SELECT_SIN_FOMENTO(ETipoExpediente expedientType)
		{
            string lf = nHManager.Instance.GetSQLTable(typeof(moleQule.Store.Data.LineaFomentoRecord));

            QueryConditions conditions = new QueryConditions();

            string query = Expedient.FIELDS(conditions) +
                Expedient.JOIN(conditions) +
                Expedient.WHERE(conditions) +
					" AND E.\"OID\" NOT IN (SELECT LF.\"OID_EXPEDIENTE\"" + 
					"							FROM " + lf + " AS LF " +
					"							GROUP BY \"OID_EXPEDIENTE\")" +
                    " AND \"TIPO_EXPEDIENTE\" = " + ((long)expedientType).ToString();

			query += " ORDER BY E.\"CODIGO\"";

			return query;
		}

        #endregion
    }
}