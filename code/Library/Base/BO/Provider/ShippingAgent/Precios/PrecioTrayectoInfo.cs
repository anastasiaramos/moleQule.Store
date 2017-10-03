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
	public class PrecioTrayectoInfo : ReadOnlyBaseEx<PrecioTrayectoInfo>
	{
        #region Business Methods

        public PrecioTrayectoBase _base = new PrecioTrayectoBase();

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public virtual string PuertoOrigen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PuertoOrigen;
			}
		}
		public virtual string PuertoDestino
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PuertoDestino;
			}
		}
		public virtual Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Precio;
			}
		}
		public virtual long OidNaviera
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidNaviera;
			}
		}
			
		#endregion		 

		#region Factory Methods
		 
		protected PrecioTrayectoInfo() { /* require use of factory methods */ }

		private PrecioTrayectoInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
			
		internal PrecioTrayectoInfo(PrecioTrayecto source)
		{
			Oid = source.Oid;
			_base.Record.PuertoOrigen = source.PuertoOrigen;
			_base.Record.PuertoDestino = source.PuertoDestino;
			_base.Record.Precio = source.Precio;
			_base.Record.OidNaviera = source.OidNaviera;			
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static PrecioTrayectoInfo Get(IDataReader reader, bool childs)
		{
			return new PrecioTrayectoInfo(reader, childs);
		}
		
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

