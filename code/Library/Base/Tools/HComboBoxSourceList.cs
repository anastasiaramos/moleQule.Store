using System;
using System.Collections.Generic;
using System.Text;

using moleQule;

namespace moleQule.Library.Store
{
    public class HComboBoxSourceList : ComboBoxSourceList
    {
        public HComboBoxSourceList() { }

        public HComboBoxSourceList(ProveedorList lista)
        {           
            foreach (ProveedorInfo item in lista)
            {
                ComboBoxSource combo = new ComboBoxSource();
                combo.Texto = item.Nombre;
                combo.Oid = item.Oid;
                this.Add(combo);
            }

        }
        
    }
}
