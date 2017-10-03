using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx;
using NHibernate;

using moleQule;

namespace moleQule.Library.Store
{
    /// <summary>
    /// Editable Business Object Root Collection
    /// Editable Business Object Child Collection
    /// </summary>
    [Serializable()]
    public class LineaPedidoProveedores : BusinessListBaseEx<LineaPedidoProveedores, LineaPedidoProveedor>
    {
        #region Root Business Methods

        public LineaPedidoProveedor NewItem(PedidoProveedor parent, IAcreedorInfo acreedor, ProductInfo producto)
        {
            LineaPedidoProveedor item = LineaPedidoProveedor.NewChild(parent, acreedor, producto);
            this.AddItem(item);

            parent.CalculaTotal();

            return item;
        }

        public void RemoveItem(PedidoProveedor parent, LineaPedidoProveedor item)
        {
            base.Remove(item);
            parent.CalculaTotal();
        }

        public new void Remove(LineaPedidoProveedor item)
        {
            throw new iQNotAllowedCodeException("Remove");
        }

        #endregion

        #region Child Business Methods

        /// <summary>
        /// Crea un nuevo elemento y lo añade a la lista
        /// </summary>
        /// <returns>Nuevo item</returns>
        public LineaPedidoProveedor NewItem(PedidoProveedor parent)
        {
            this.NewItem(LineaPedidoProveedor.NewChild(parent));
            return this[Count - 1];
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineaPedidoProveedores() { }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LineaPedidoProveedores NewList()
        {
            if (!LineaPedidoProveedor.CanAddObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            return new LineaPedidoProveedores();
        }

        /// <summary>
        /// Obtiene de la base de datos todos los elementos y construye la lista
        /// </summary>
        /// <returns>Lista de los elementos de la tabla en la base de datos</returns>
        /// <remarks>No obtiene los hijos de los elementos de la lista</remarks>
        public static LineaPedidoProveedores GetList() { return GetList(false); }

        /// <summary>
        /// Obtiene de la base de datos todos los elementos y construye la lista
        /// </summary>
        /// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de los elementos de la tabla en la base de datos</returns>
        public static LineaPedidoProveedores GetList(bool retrieve_childs)
        {
            if (!LineaPedidoProveedor.CanEditObject())
                throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = LineaPedidoProveedor.GetCriteria(LineaPedidoProveedor.OpenSession());
            criteria.Childs = retrieve_childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            LineaPedidoProveedor.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<LineaPedidoProveedores>(criteria);
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private LineaPedidoProveedores(IList<LineaPedidoProveedor> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private LineaPedidoProveedores(IDataReader reader, bool retrieve_childs)
        {
            MarkAsChild();
            Childs = retrieve_childs;
            Fetch(reader);
        }

        /// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LineaPedidoProveedores NewChildList()
        {
            LineaPedidoProveedores list = new LineaPedidoProveedores();
            list.MarkAsChild();
            return list;
        }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
        public static LineaPedidoProveedores GetChildList(IList<LineaPedidoProveedor> lista) { return new LineaPedidoProveedores(lista); }
        public static LineaPedidoProveedores GetChildList(IDataReader reader) { return GetChildList(reader, true); }
        public static LineaPedidoProveedores GetChildList(IDataReader reader, bool retrieve_childs) { return new LineaPedidoProveedores(reader, retrieve_childs); }

        public static LineaPedidoProveedores GetPendientesChildList(PedidoProveedor parent, bool childs)
        {
            CriteriaEx criteria = Stock.GetCriteria(parent.SessionCode);
            criteria.Childs = childs;

            QueryConditions conditions = new QueryConditions
            {
                PedidoProveedor = parent.GetInfo()
            };
            criteria.Query = SELECT_PENDIENTES(conditions);

            return DataPortal.Fetch<LineaPedidoProveedores>(criteria);
        }

        #endregion

        #region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    LineaPedidoProveedor.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(LineaPedidoProveedor.GetChild(SessionCode, reader, Childs));
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

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
        /// de los flags de cada objeto de la lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (LineaPedidoProveedor obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (LineaPedidoProveedor obj in this)
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
        private void Fetch(IList<LineaPedidoProveedor> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (LineaPedidoProveedor item in lista)
                this.AddItem(LineaPedidoProveedor.GetChild(item, Childs));

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
                this.AddItem(LineaPedidoProveedor.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }


        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
        /// de los flags de cada objeto de la lista
        /// </summary>
        /// <param name="parent">BusinessBaseEx padre de la lista</param>
        internal void Update(PedidoProveedor parent)
        {
            try
            {
                this.RaiseListChangedEvents = false;

                SessionCode = parent.SessionCode;

                // update (thus deleting) any deleted child objects
                foreach (LineaPedidoProveedor obj in DeletedList)
                    obj.DeleteSelf(parent);

                // now that they are deleted, remove them from memory too
                DeletedList.Clear();

                // add/update any current child objects
                foreach (LineaPedidoProveedor obj in this)
                {
                    if (!this.Contains(obj))
                    {
                        if (obj.IsNew)
                            obj.Insert(parent);
                        else
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

		public static string SELECT() { return LineaPedidoProveedor.SELECT(new QueryConditions(), true); }
        public static string SELECT(QueryConditions conditions) { return LineaPedidoProveedor.SELECT(conditions, true); }
        public static string SELECT(PedidoProveedor pedido) { return SELECT(new QueryConditions { PedidoProveedor = pedido.GetInfo(false) }); }
        public static string SELECT_PENDIENTES(QueryConditions conditions) { return LineaPedidoProveedor.SELECT_PENDIENTES(conditions, true); }

        #endregion
    }
}

