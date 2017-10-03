using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class AlbaranFacturaProveedorInfo : ReadOnlyBaseEx<AlbaranFacturaProveedorInfo>
	{	
		#region Attributes

		public InputDeliveryInvoiceBase _base = new InputDeliveryInvoiceBase();
        
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAlbaran { get { return _base.Record.OidAlbaran; } /*set { _oid_Albaran = value; }*/ }
		public long OidFactura { get { return _base.Record.OidFactura; } /*set { _oid_factura = value; }*/ }
        public DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } }
		
        //Campos no enlazados
        public Decimal Importe { get { return _base.Importe; } }
        public string CodigoFactura { get { return _base.CodigoFactura; } }
        public string CodigoAlbaran { get { return _base.CodigoAlbaran; } }

		#endregion
		
		#region Business Methods
					
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected AlbaranFacturaProveedorInfo() { /* require use of factory methods */ }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> origen de los datos</param>
        /// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		private AlbaranFacturaProveedorInfo(IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			Fetch(reader);
		}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="BusinessBaseEx"/> origen</param>
        /// <param name="copy_childs">Flag para copiar los hijos</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		internal AlbaranFacturaProveedorInfo(AlbaranFacturaProveedor item, bool copy_childs)
		{
			_base.CopyValues(item);
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
		public static AlbaranFacturaProveedorInfo GetChild(IDataReader reader)
        {
			return GetChild(reader, false);
		}
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		/// <remarks>La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista<remarks/>
		public static AlbaranFacturaProveedorInfo GetChild(IDataReader reader, bool retrieve_childs)
        {
			return new AlbaranFacturaProveedorInfo(reader, retrieve_childs);
		}

        public AlbaranFacturaProveedorPrint GetPrintObject(InputInvoiceInfo factura, IAcreedorInfo acreedor, InputDeliveryInfo albaran)
        {
            return AlbaranFacturaProveedorPrint.New(this, acreedor, factura, albaran);
        }

        public AlbaranFacturaProveedorPrint GetPrintObject(ETipoAcreedor tipo)
        {
            InputInvoiceInfo f = InputInvoiceInfo.Get(OidFactura, tipo);
            return AlbaranFacturaProveedorPrint.New(this, ProviderBaseInfo.Get(f.OidAcreedor, f.ETipoAcreedor), f, InputDeliveryInfo.Get(this.OidAlbaran, tipo));
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
		
	}
}
