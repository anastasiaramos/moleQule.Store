using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Common.Structs;
using moleQule.CslaEx;
using moleQule.Common;
using moleQule.Serie;

namespace moleQule.Library.Store
{
    [Serializable()]
    public class InputDeliveryPrint : InputDeliveryInfo
    {
		#region Attributes

		protected string _direccion = string.Empty;
		protected string _poblacion = string.Empty;
		protected string _provincia = string.Empty;
		protected string _telefonos = string.Empty;
		protected string _fax = string.Empty;
		protected string _municipio = string.Empty;
		private string _nombre_transportista = string.Empty;

		#endregion

		#region Properties

		public decimal ImporteIgic { get { return _base.Record.Igic; } }
		public string IDAcreedor { get { return NumeroAcreedor; } }
		public string Direccion { get { return _direccion; } }
		public string Poblacion { get { return _poblacion; } }
		public string Provincia { get { return _provincia; } }
		public string Telefonos { get { return _telefonos; } }
		public string Fax { get { return _fax; } }
		public string Municipio { get { return _municipio; } }
		public string NombreTransportista { get { return _nombre_transportista; } }
		public string Serie { get { return NumeroSerie; } }
		public string ETipoAcreedorPrintLabel { get { return moleQule.Common.Structs.EnumText<ETipoAcreedor>.GetPrintLabel(ETipoAcreedor); } }

		#endregion

		#region Factory Methods

		protected InputDeliveryPrint() { /* require use of factory methods */ }

		// called to load data from source
		public static InputDeliveryPrint New(InputDeliveryInfo delivery, ProviderBaseInfo provider, SerieInfo serie)
		{
			InputDeliveryPrint item = new InputDeliveryPrint();
			item.CopyValues(delivery, provider, serie);

			return item;
		}

		#endregion

		#region Business Methods

        protected void CopyValues(InputDeliveryInfo source, ProviderBaseInfo acreedor, SerieInfo serie)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);

            if (acreedor != null)
            {
                _base._numero_acreedor = acreedor.Codigo;
				_direccion = acreedor.Direccion;
				_poblacion = acreedor.Localidad;
				_provincia = acreedor.Provincia;
				_telefonos = acreedor.Telefono;
				_municipio = acreedor.Municipio;
            }

            if (serie != null)
            {
				_base._numero_serie = serie.Identificador;
				_base._nombre_serie = serie.Nombre;
            }
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class AlbaranRecibidoPrint : InputDeliveryPrint
    {
        #region Factory Methods

        private AlbaranRecibidoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static AlbaranRecibidoPrint New(InputDeliveryInfo delivery, ProviderBaseInfo provider, SerieInfo serie)
        {
            AlbaranRecibidoPrint item = new AlbaranRecibidoPrint();
            item.CopyValues(delivery, provider, serie);

            return item;
        }

        #endregion
    }
}
