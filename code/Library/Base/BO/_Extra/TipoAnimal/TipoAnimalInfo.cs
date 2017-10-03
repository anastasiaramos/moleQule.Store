using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class TipoAnimalInfo : ReadOnlyBaseEx<TipoAnimalInfo>
	{
		#region Attributes

		protected TipoAnimalBase _base = new TipoAnimalBase();

		#endregion

		#region Properties

		public TipoAnimalBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Valor { get { return _base.Record.Valor; } }

		#endregion	 

		#region Factory Methods
		 
		protected TipoAnimalInfo() { /* require use of factory methods */ }

		private TipoAnimalInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
			
		internal TipoAnimalInfo(TipoAnimal source)
		{
			_base.CopyValues(source);			
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static TipoAnimalInfo Get(IDataReader reader, bool childs)
		{
			return new TipoAnimalInfo(reader, childs);
		}

		public static TipoAnimalInfo New(long oid = 0) { return new TipoAnimalInfo() { Oid = oid }; }

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

