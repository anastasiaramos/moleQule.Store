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
	public class RazaAnimalInfo : ReadOnlyBaseEx<RazaAnimalInfo>
	{
		#region Attributes

		protected RazaAnimalBase _base = new RazaAnimalBase();

		#endregion

		#region Properties

		public RazaAnimalBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Valor { get { return _base.Record.Valor; } }

		#endregion	 

		#region Factory Methods
		 
		protected RazaAnimalInfo() { /* require use of factory methods */ }

		private RazaAnimalInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
			
		internal RazaAnimalInfo(RazaAnimal source)
		{
			_base.CopyValues(source);			
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static RazaAnimalInfo Get(IDataReader reader, bool childs)
		{
			return new RazaAnimalInfo(reader, childs);
		}

		public static RazaAnimalInfo New(long oid = 0) { return new RazaAnimalInfo() { Oid = oid }; }

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

