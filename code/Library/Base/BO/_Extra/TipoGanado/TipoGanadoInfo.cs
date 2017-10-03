using System;
using System.Collections.Generic;
using System.Data;

using moleQule.CslaEx; 

using moleQule;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class TipoGanadoInfo : ReadOnlyBaseEx<TipoGanadoInfo>
	{
		#region Attributes

		protected TipoGanadoBase _base = new TipoGanadoBase();

		#endregion

		#region Properties

		public TipoGanadoBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Valor { get { return _base.Record.Valor; } }

		#endregion		 

		#region Factory Methods
		 
		protected TipoGanadoInfo() { /* require use of factory methods */ }

		private TipoGanadoInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
			
		internal TipoGanadoInfo(TipoGanado source)
		{
			_base.CopyValues(source);			
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static TipoGanadoInfo Get(IDataReader reader, bool childs)
		{
			return new TipoGanadoInfo(reader, childs);
		}

		public static TipoGanadoInfo New(long oid = 0) { return new TipoGanadoInfo() { Oid = oid }; }

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

