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
	public class PrecioDestinoInfo : ReadOnlyBaseEx<PrecioDestinoInfo>
	{	
	    #region Attributes

        public PrecioDestinoBase _base = new PrecioDestinoBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidTransportista
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTransportista;
			}
		}
        public long OidCliente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidCliente;
            }
        }
		public string Puerto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Puerto;
			}
		}		
		public Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Precio;
			}
        }

		public string NumeroCliente { get { return _base.Record.NumeroCliente.ToString(); } }
		public string CodigoCliente	{ get { return _base.Record.CodigoCliente;	} }
		public string NombreCliente	{ get {	return _base.Record.NombreCliente;	} }		
        public string NClienteLabel { get { return _base.Record.NumeroCliente.ToString(); } } /*DEPRECATED*/

        #endregion

        #region Business Methods

        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _base.CopyValues(source);

			_base.Record.NumeroCliente = Format.DataReader.GetInt64(source, "ID_CLIENTE");
            _base.Record.CodigoCliente = Format.DataReader.GetString(source, "VAT_NUMBER_CLIENTE");
            _base.Record.NombreCliente = Format.DataReader.GetString(source, "NOMBRE_CLIENTE");            
        }

		#endregion		 

		#region Factory Methods
		 
		protected PrecioDestinoInfo() { /* require use of factory methods */ }
		private PrecioDestinoInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}			
		internal PrecioDestinoInfo(PrecioDestino source)
		{
            _base.CopyValues(source);			
		}

		public static PrecioDestinoInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new PrecioDestinoInfo(sessionCode, reader, childs);
		}
		
		#endregion		 
		 
		#region Data Access
		 
		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
			    CopyValues(source);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
					
		#endregion		
	}
}

