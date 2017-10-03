using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx;

using moleQule;

using NHibernate;

namespace moleQule.Library.Store
{

    /// <summary>
    /// ReadOnly Business Object Root Collection
    /// ReadOnly Business Object Child Collection
    /// </summary>
    [Serializable()]
    public class LineaPedidoProveeedorList : ReadOnlyListBaseEx<LineaPedidoProveeedorList, LineaPedidoProveedorInfo>
    {
        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineaPedidoProveeedorList() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineaPedidoProveeedorList(IList<LineaPedidoProveedor> list, bool retrieve_childs)
        {
            Childs = retrieve_childs;
            Fetch(list);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineaPedidoProveeedorList(IDataReader reader, bool retrieve_childs)
        {
            Childs = retrieve_childs;
            Fetch(reader);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineaPedidoProveeedorList(IList<LineaPedidoProveedorInfo> list, bool retrieve_childs)
        {
            Childs = retrieve_childs;
            Fetch(list);
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Default call for GetList(bool retrieve_childs)
        /// </summary>
        /// <returns></returns>
        public static LineaPedidoProveeedorList GetList()
        {
            return LineaPedidoProveeedorList.GetList(true);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="retrieve_childs">Retrieving the childs</param>
        /// <returns></returns>
        public static LineaPedidoProveeedorList GetList(bool retrieve_childs)
        {
            CriteriaEx criteria = LineaPedidoProveedor.GetCriteria(LineaPedidoProveedor.OpenSession());
            criteria.Childs = retrieve_childs;

            //No criteria. Retrieve all de List

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            LineaPedidoProveeedorList list = DataPortal.Fetch<LineaPedidoProveeedorList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static LineaPedidoProveeedorList GetList(CriteriaEx criteria)
        {
            return LineaPedidoProveeedorList.RetrieveList(typeof(LineaPedidoProveedor), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static LineaPedidoProveeedorList GetList(IList<LineaPedidoProveedor> list) { return new LineaPedidoProveeedorList(list, false); }

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static LineaPedidoProveeedorList GetList(IList<LineaPedidoProveedorInfo> list) { return new LineaPedidoProveeedorList(list, false); }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<LineaPedidoProveedorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<LineaPedidoProveedorInfo> sortedList = new SortedBindingList<LineaPedidoProveedorInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos y sus hijos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <param name="childs">Traer hijos</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<LineaPedidoProveedorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<LineaPedidoProveedorInfo> sortedList = new SortedBindingList<LineaPedidoProveedorInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static LineaPedidoProveeedorList GetChildList(IList<LineaPedidoProveedor> list) { return new LineaPedidoProveeedorList(list, false); }
        public static LineaPedidoProveeedorList GetChildList(IList<LineaPedidoProveedor> list, bool retrieve_childs) { return new LineaPedidoProveeedorList(list, retrieve_childs); }
        public static LineaPedidoProveeedorList GetChildList(IDataReader reader) { return new LineaPedidoProveeedorList(reader, false); }
        public static LineaPedidoProveeedorList GetChildList(IDataReader reader, bool retrieve_childs) { return new LineaPedidoProveeedorList(reader, retrieve_childs); }
        public static LineaPedidoProveeedorList GetChildList(IList<LineaPedidoProveedorInfo> list) { return new LineaPedidoProveeedorList(list, false); }

        public static LineaPedidoProveeedorList GetPendientesChildList(PedidoProveedorInfo parent, bool childs)
        {
            CriteriaEx criteria = LineaPedidoProveedor.GetCriteria(LineaPedidoProveedor.OpenSession());
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions
            {
                PedidoProveedor = parent
            };
            criteria.Query = SELECT_PENDIENTES(conditions);

            LineaPedidoProveeedorList list = DataPortal.Fetch<LineaPedidoProveeedorList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Common Data Access

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
        /// </summary>
        /// <param name="lista">IList origen</param>
        private void Fetch(IList<LineaPedidoProveedor> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (LineaPedidoProveedor item in lista)
                this.AddItem(item.GetInfo(Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(LineaPedidoProveedorInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(LineaPedidoProveedorInfo.GetChild(SessionCode, reader, Childs));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return LineaPedidoProveedor.SELECT(conditions, false); }
        public static string SELECT(PedidoProveedorInfo pedido) { return SELECT(new QueryConditions { PedidoProveedor = pedido }); }
        public static string SELECT_PENDIENTES(QueryConditions conditions) { return LineaPedidoProveedor.SELECT_PENDIENTES(conditions, false); }

        #endregion
    }
}

