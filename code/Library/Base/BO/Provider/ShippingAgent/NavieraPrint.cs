using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using moleQule;

using Csla;
using moleQule.CslaEx; 

namespace moleQule.Library.Store
{
    [Serializable()]
    public class NavieraPrint : NavieraInfo
    {

        #region Attributes & Properties
		
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(NavieraInfo source)
        {
            if (source == null) return;

            _base.CopyValues(source);
            Oid = source.Oid;			
        }

        #endregion

        #region Factory Methods

        private NavieraPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static NavieraPrint New(NavieraInfo source)
        {
            NavieraPrint item = new NavieraPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
