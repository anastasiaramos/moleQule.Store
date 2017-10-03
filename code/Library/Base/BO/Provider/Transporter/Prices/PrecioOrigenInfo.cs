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
	public class PrecioOrigenInfo : ReadOnlyBaseEx<PrecioOrigenInfo>
	{
	
	    #region Attributes & Properties

        public PrecioOrigenBase _base = new PrecioOrigenBase();
        public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		 
		public virtual long OidTransportista
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTransportista;
			}
		}
        public virtual long OidProveedor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidProveedor;
            }
        }
		public virtual string Proveedor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Proveedor;
			}
		}
		public virtual string Puerto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Puerto;
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

        #endregion

        #region Business Methods
        
        #endregion			 

		#region Factory Methods
		 
		protected PrecioOrigenInfo() { /* require use of factory methods */ }
		private PrecioOrigenInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}			
		internal PrecioOrigenInfo(PrecioOrigen source)
		{
            _base.CopyValues(source);
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static PrecioOrigenInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new PrecioOrigenInfo(sessionCode, reader, childs);
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

