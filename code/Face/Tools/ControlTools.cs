using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Face;
using moleQule.Common;

namespace moleQule.Face.Store
{
    public class ControlTools
	{
		#region Attributes & Properties

		public DataGridViewCellStyle LineaStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle LineaCerradaStyle = new DataGridViewCellStyle();
		public DataGridViewCellStyle LineaPendienteStyle = new DataGridViewCellStyle();

		#endregion

		#region Factory Methods

		/// <summary>
        /// Única instancia de la clase ControlerBase (Singleton)
        /// </summary>
        protected static ControlTools _main;

        /// <summary>
        /// Unique Controler Class Instance
        /// </summary>
		public static ControlTools Instance { get { return (_main != null) ? _main : new ControlTools(); } }
        
        /// <summary>
        /// Contructor 
        /// </summary>
		protected ControlTools()
        {
            // Singleton
            _main = this;

			InitStyles();
        }

		private void InitStyles()
		{
			LineaStyle = moleQule.Face.ControlTools.Instance.HeaderSelectedStyle;

			Face.ControlTools.Instance.CopyBasicStyle(LineaStyle);
			LineaStyle.WrapMode = DataGridViewTriState.True;

			LineaPendienteStyle = LineaStyle;
			LineaPendienteStyle.BackColor = Color.FromArgb(225, 205, 205);
			LineaPendienteStyle.ForeColor = Color.Black;
			LineaStyle.WrapMode = DataGridViewTriState.True;

			LineaCerradaStyle = Common.ControlTools.Instance.CerradoStyle;
			LineaStyle.WrapMode = DataGridViewTriState.True;
		}

		#endregion

		#region Business Methods

		public void SetRowColor(DataGridViewRow row, moleQule.Base.EEstado estado)
        {
			Common.ControlTools.Instance.SetRowColor(row, estado);
		}

		public void SetRowColorIM(DataGridViewRow row, moleQule.Base.EEstado estado)
		{
			Common.ControlTools.Instance.SetRowColorIM(row, estado);
		}

		public void ApplyStyle(DataGridViewCell cell, CellStyle style)
		{
			Face.ControlTools.Instance.ApplyStyle(cell, style);
		}

		#endregion
	}
}
