using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Base;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Library.Store;

namespace moleQule.Face.Store
{
    public partial class PayrollBatchUIForm : PayrollBatchForm
    {
        #region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected PayrollBatch _entity;
        public EmployeeList _employees = null;
        
		public override PayrollBatch Entity { get { return _entity; } set { _entity = value; } }
        public override PayrollBatchInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected PayrollBatchUIForm() 
			: this(null) { }

        public PayrollBatchUIForm(Form parent) 
			: this(-1, parent) { }

        public PayrollBatchUIForm(long oid) 
			: this(oid, null) { }

        public PayrollBatchUIForm(long oid, Form parent)
            : this(oid, 0, parent) { }

        public PayrollBatchUIForm(long oid_remesa, long oid_nomina, Form parent)
            : base(oid_remesa, oid_nomina, true, parent)
        {
            InitializeComponent();
        }

		protected override void CleanCache() 
		{
			Cache.Instance.Remove(typeof(EmployeeList));
			Cache.Instance.Remove(typeof(TipoGastoList));
		}

        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            PayrollBatch temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();					

                return true;
            }
            catch (Exception ex)
            {
				PgMng.ShowErrorException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout

		protected override void SetGridFormat()
		{
			foreach (DataGridViewRow row in Payrolls_DGW.Rows)
			{
				if (row.IsNewRow) return;

				Payroll item = (Payroll)row.DataBoundItem;

				if (item.OidPago != 0)
				{
					row.Cells[ImporteNomina.Index].ReadOnly = true;
					row.Cells[ImporteNomina.Index].Style.BackColor = Face.ControlTools.Instance.ReadOnlyStyle.BackColor;
				}

				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            if (_oid_nomina == 0)
                Payrolls_BS.DataSource = _entity.Nominas;
            else
            {
                FCriteria criteria = new FCriteria<long>("Oid", _oid_nomina, Operation.Equal);
                Payrolls_BS.DataSource = _entity.Nominas.GetSubList(criteria);
            }
			PgMng.Grow();

            base.RefreshMainData();
        }

        public override void  RefreshSecondaryData()
        {
            _employees = EmployeeList.GetAvailableList(false);
            PgMng.Grow();
        }

        #endregion

		#region Business Methods

		protected void ChangeState(Payroll item, moleQule.Base.EEstado estado)
		{
			switch (estado)
			{
				case moleQule.Base.EEstado.Abierto:

					item.EEstado = moleQule.Base.EEstado.Abierto;
					break;

				case moleQule.Base.EEstado.Anulado:

					if (item.OidPago != 0) return;
					item.EEstado = moleQule.Base.EEstado.Anulado;

					break;

				case moleQule.Base.EEstado.Contabilizado:

					if (item.OidPago == 0) return;
					item.EEstado = moleQule.Base.EEstado.Contabilizado;

					break;
			}

			_entity.CalculateTotal();
		}

        protected void ReCalculatePayrolls()
        {
            foreach (Payroll item in _entity.Nominas)
                item.CalculateValues(_employees.GetItem(item.OidEmpleado), _entity.Fecha);

            ControlsMng.UpdateBinding(Payrolls_BS);
        }

        protected override void UpdatePayroll() 
        {
            if (!ControlsMng.IsCurrentItemValid(Payrolls_DGW)) return;

            Payroll item = ControlsMng.GetCurrentItem(Payrolls_DGW) as Payroll;

            item.CalculateNeto(_employees.GetItem(item.OidEmpleado).EPayrollMethod);
            ControlsMng.UpdateBinding(Payrolls_BS);

            UpdateTotalRemesa();

            ControlsMng.UpdateBinding(Datos);
        }

		protected override void UpdateTotalRemesa() { _entity.CalculateTotal(); }

		#endregion

		#region Actions

		protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void CalculatePayrollAction()
        {
            if (!ControlsMng.IsCurrentItemValid(Payrolls_DGW)) return;

            Payroll item = ControlsMng.GetCurrentItem(Payrolls_DGW) as Payroll;

            item.CalculateValues(_employees.GetItem(item.OidEmpleado), _entity.Fecha);

            ControlsMng.UpdateBinding(Payrolls_BS);
        }

        protected override void DeletePayrollAction() 
		{
            if (!ControlsMng.IsCurrentItemValid(Payrolls_DGW)) return;

            Payroll item = ControlsMng.GetCurrentItem(Payrolls_DGW) as Payroll;

			if (item.OidPago != 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NOMINA_CON_PAGO);
				return;
			}

			_entity.Nominas.Remove(_entity, item);

			Payrolls_BS.DataSource = _entity.Nominas;
		}

        protected override void EditEmployeeAction()
        {
            if (!ControlsMng.IsCurrentItemValid(Payrolls_DGW)) return;

            Payroll item = ControlsMng.GetCurrentItem(Payrolls_DGW) as Payroll;

            EmployeeEditForm form = new EmployeeEditForm(item.OidEmpleado, this);
            form.ShowDialog();

            if (form.ActionResult == DialogResult.OK)
            {
                _employees.GetItem(item.OidEmpleado).CopyFrom(form.Entity);
                CalculatePayrollAction();
            }
        }

        protected override void NewPayrollAction()
        {
            _entity.Nominas.NewItem(_entity);
            Payrolls_BS.DataSource = _entity.Nominas;
        }

		protected override void SetEmployeeAction()
		{
            if (!ControlsMng.IsCurrentItemValid(Payrolls_DGW)) return;

            Payroll item = ControlsMng.GetCurrentItem(Payrolls_DGW) as Payroll;

			if (item.EEstado == moleQule.Base.EEstado.Anulado) return;

            EmployeeSelectForm form = new EmployeeSelectForm(this, _employees);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				EmployeeInfo empleado = form.Selected as EmployeeInfo;

				item.CopyFrom(_entity, empleado);
			}
		}

		protected override void SetStatusAction()
		{
            if (!ControlsMng.IsCurrentItemValid(Payrolls_DGW)) return;

            Payroll item = ControlsMng.GetCurrentItem(Payrolls_DGW) as Payroll;

			if (item.EEstado == moleQule.Base.EEstado.Anulado) return;
			if (item.EEstado == moleQule.Base.EEstado.Pagado) return;

			SelectEnumInputForm form = new SelectEnumInputForm(true);
			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Abierto, moleQule.Base.EEstado.Contabilizado, moleQule.Base.EEstado.Anulado };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				ChangeState(item, (moleQule.Base.EEstado)estado.Oid);
				
				Payrolls_BS.ResetBindings(false);

				SetGridFormat();
			}
		}

        #endregion

        #region Events
		
		private void Fecha_DTP_Validated(object sender, EventArgs e)
		{
			_entity.PrevisionPago = _entity.Fecha;
            _entity.UpdateDescription();
            ReCalculatePayrolls();
		}

        #endregion
    }
}
