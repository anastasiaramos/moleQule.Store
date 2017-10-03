using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;
using Csla;
using moleQule.Common.Structs;
using moleQule.Library.CslaEx; 
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class FamiliaInfo : ReadOnlyBaseEx<FamiliaInfo>
	{	
		#region Attributes

        public FamilyBase _base = new FamilyBase();

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public string Codigo { get { return _base.Record.Codigo; } }
		public string Nombre { get { return _base.Record.Nombre; } }
        public string CuentaContableCompra { get { return _base.Record.CuentaContableCompra; } }
		public string CuentaContableVenta { get { return _base.Record.CuentaContableVenta; } }
        public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
        public bool AvisarBeneficioMinimo { get { return _base.Record.AvisarBeneficioMinimo; } }
        public decimal PBeneficioMinimo { get { return _base.Record.PBeneficioMinimo; } }

        public ETipoFamilia ETipoFamilia { get { return (ETipoFamilia)Oid; } }

        public string Impuesto { get { return (_base.Record.OidImpuesto != 0) ? _base.Impuesto : moleQule.Common.Structs.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } }
        public decimal PImpuesto { get { return _base.PImpuesto; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(Familia source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected FamiliaInfo() { /* require use of factory methods */ }
		private FamiliaInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal FamiliaInfo(Familia item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static FamiliaInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static FamiliaInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new FamiliaInfo(sessionCode, reader, childs); }

		public static FamiliaInfo New(long oid = 0) { return new FamiliaInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static FamiliaInfo Get(long oid, bool childs = true)
		{
			CriteriaEx criteria = Familia.GetCriteria(Familia.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = FamiliaInfo.SELECT(oid);
	
			FamiliaInfo obj = DataPortal.Fetch<FamiliaInfo>(criteria);
			Familia.CloseSession(criteria.SessionCode);
			return obj;
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
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion
		
		#region Child Data Access
		
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
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid) { return Familia.SELECT(oid, false); }

        #endregion		
	}
}
