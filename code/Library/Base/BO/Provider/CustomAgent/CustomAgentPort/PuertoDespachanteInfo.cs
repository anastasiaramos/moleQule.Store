using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.CslaEx; 
using moleQule;
using NHibernate;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class PuertoDespachanteInfo : ReadOnlyBaseEx<PuertoDespachanteInfo>
	{
		#region Attributes

		protected PuertoDespachanteBase _base = new PuertoDespachanteBase();

		#endregion

		#region Properties

		public PuertoDespachanteBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidPuerto { get { return _base.Record.OidPuerto; } }
		public long OidDespachante { get { return _base.Record.OidDespachante; } }

		#endregion

		#region Business Methods

		public void CopyFrom(PuertoDespachante source) { _base.CopyValues(source); }

		#endregion		

		#region Factory Methods
		 
		protected PuertoDespachanteInfo() { /* require use of factory methods */ }
		private PuertoDespachanteInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}			
		internal PuertoDespachanteInfo(PuertoDespachante source)
		{
			_base.CopyValues(source);			
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static PuertoDespachanteInfo Get(IDataReader reader, bool childs)
		{
			return new PuertoDespachanteInfo(reader, childs);
		}

		public static PuertoDespachanteInfo New(long oid = 0) { return new PuertoDespachanteInfo() { Oid = oid }; }

		#endregion		 
		 
		#region Data Access
		 
		//called to copy data from IDataReader
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
		
	}
}

