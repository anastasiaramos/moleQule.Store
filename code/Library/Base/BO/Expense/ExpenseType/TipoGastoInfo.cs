using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.CslaEx; 
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class TipoGastoInfo : ReadOnlyBaseEx<TipoGastoInfo>
	{	
		#region Attributes

		protected ExpenseTypeBase _base = new ExpenseTypeBase();

		#endregion
		
		#region Properties

		public ExpenseTypeBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Categoria { get { return _base.Record.Categoria; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public long OidCuentaBAsociada { get { return _base.Record.OidCuentaAsociada; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

		//NO ENLAZADAS
		public virtual ECategoriaGasto ECategoriaGasto { get { return _base.ECategoriaGasto; } }
		public virtual string CategoriaGastoLabel { get { return _base.CategoriaGastoLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual string CuentaAsociada { get { return _base.CuentaAsociada; } set { _base.CuentaAsociada = value; } }
				
		#endregion
		
		#region Business Methods
			
		public void CopyFrom(TipoGasto source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected TipoGastoInfo() { /* require use of factory methods */ }
		private TipoGastoInfo(IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			Fetch(reader);
		}
		internal TipoGastoInfo(TipoGasto item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}	

		public static TipoGastoInfo GetChild(IDataReader reader, bool retrieve_childs = false)
        {
			return new TipoGastoInfo(reader, retrieve_childs);
		}

		public static TipoGastoInfo New(long oid = 0) { return new TipoGastoInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static TipoGastoInfo Get(long oid)
        {
            return Get(oid, false);
        }
		
        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static TipoGastoInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = TipoGasto.GetCriteria(TipoGasto.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TipoGastoInfo.SELECT(oid);
	
			TipoGastoInfo obj = DataPortal.Fetch<TipoGastoInfo>(criteria);
			TipoGasto.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		#endregion
		
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return TipoGasto.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return TipoGasto.SELECT(conditions, false); }
		
        #endregion		
	}
}
