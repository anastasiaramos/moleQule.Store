using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule;
using moleQule.CslaEx;
using moleQule.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class ExpenseForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return 14; } }

        public virtual Expense Entity { get { return null; } set { } }
		public virtual ExpenseInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public ExpenseForm() 
			: this(-1, null) {}

		public ExpenseForm(long oid, Form parent)
			: this(oid, new object [2] { null, ECategoriaGasto.Otros }, true, parent) { }

		public ExpenseForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			MaximizeForm(new Size(600, 450));

			base.FormatControls();
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        /// <summary>
        /// Implementa Docs_BT_Click
        /// </summary>
        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Expense), EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Expense), EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

        protected virtual void SelectFamiliaAction() {}

        #endregion

        #region Buttons

        private void Familia_BT_Click(object sender, EventArgs e)
        {
            SelectFamiliaAction();
        }

        #endregion

        #region Events

        private void Pestanas_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage CurrentTab = Pestanas.TabPages[e.Index];
            Rectangle ItemRect = Pestanas.GetTabRect(e.Index);
            SolidBrush TextBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, (RectangleF)ItemRect, sf);
        }

        #endregion
    }
}