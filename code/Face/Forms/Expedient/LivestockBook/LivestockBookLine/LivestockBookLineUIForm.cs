using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using moleQule.Common.Structs;
using moleQule.Face;
using moleQule.Base;
using moleQule.Face.Common;
using moleQule;
using moleQule.Common;
using moleQule.Library.Store;
using moleQule.Store.Structs;

namespace moleQule.Face.Store
{
    public partial class LivestockBookLineUIForm : LivestockBookLineForm
    {
        #region Attributes & Properties

        public new const string ID = "LivestockBookLineUIForm";
		public new static Type Type { get { return typeof(LivestockBookLineUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected LivestockBookLine _entity;
        protected LivestockBookLine _source;

        public override LivestockBookLine Entity { get { return _entity; } set { _entity = value; } }
        public override LivestockBookLineInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected LivestockBookLineUIForm() 
			: this(null) {}

        public LivestockBookLineUIForm(Form parent) 
			: this(-1, ETipoLineaLibroGanadero.Todos, true, parent) { }

        public LivestockBookLineUIForm(long oid) 
			: this(oid, ETipoLineaLibroGanadero.Todos, true, null) { }

        public LivestockBookLineUIForm(long oid, ETipoLineaLibroGanadero tipo, bool isModal, Form parent)
            : base(oid, tipo, isModal, parent)
        {
            InitializeComponent();
        }

        public LivestockBookLineUIForm(LivestockBookLine source, bool isModal, Form parent)
			: base(source.Oid, new object[2] { source, source.Tipo }, isModal, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
			//Se está editando un objeto hijo de una lista. Ya se encargará la lista de guardar si procede
			if (IsChild)
			{
				_source.CopyFrom(_entity);				
				return true;
			}

			this.Datos.RaiseListChangedEvents = false;

            LivestockBookLine temp = _entity.Clone();
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
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            Status_BT.Enabled = _entity.ETipo == ETipoLineaLibroGanadero.TraspasoExplotacion;
            Pair_BT.Enabled = !new ETipoLineaLibroGanadero [] { 
                                    ETipoLineaLibroGanadero.Importacion, 
                                    ETipoLineaLibroGanadero.Venta 
                                }.Contains(_entity.ETipo);
        }

        protected override void ShowPair()
        {
            if (_entity == null) return;

            switch (_entity.EEstado)
            {
                case moleQule.Base.EEstado.Baja:

                    Pair_BT.Enabled = true;
                    Pair_BT.Visible = true;
                    PairID_LB.Visible = true;
                    PairID_TB.Visible = true;
                    Sexo_BT.Enabled = false;
                    Crotal_TB.Enabled = false;
                    Raza_BT.Enabled = false;
                    Edad_TB.Enabled = false;

                    break;

                case moleQule.Base.EEstado.Alta:

                    Pair_BT.Enabled = false;
                    Pair_BT.Visible = false;
                    PairID_LB.Visible = false;
                    PairID_TB.Visible = false;
                    Crotal_TB.Enabled = true;
                    Sexo_BT.Enabled = true;
                    Raza_BT.Enabled = true;
                    Edad_TB.Enabled = true;

                    break;
            }
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
 
            base.RefreshMainData();
        }

        protected ETipoLineaLibroGanadero[] GetTypes()
        {
            ETipoLineaLibroGanadero[] list = null;

            switch (_entity.EEstado)
            {
                case moleQule.Base.EEstado.Alta:
                    list = new ETipoLineaLibroGanadero[]
                        { 
                            ETipoLineaLibroGanadero.Nacimiento, 
                            ETipoLineaLibroGanadero.TraspasoExplotacion
                        };
                    break;

                case moleQule.Base.EEstado.Baja:
                    list = new ETipoLineaLibroGanadero[]
                        { 
                            ETipoLineaLibroGanadero.Muerte,
                            ETipoLineaLibroGanadero.TraspasoExplotacion                            
                        };
                    break;
            }

            return list;
        }
        
        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {	
        }
		
        #endregion

        #region Business Methods

        protected LivestockBookLineInfo SelectEntryPair()
        {
            LivestockBookLineList list = LivestockBookLineList.GetAvailableList(_entity.OidLibro, false);

            LivestockBookLineSelectForm form = new LivestockBookLineSelectForm(this, list);
            form.ShowDialog();

            if (form.ActionResult == System.Windows.Forms.DialogResult.OK)
                return form.Selected as LivestockBookLineInfo;

            return null;
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }
		
		protected void ChangeStateAction(moleQule.Base.EEstado estado)
		{
			if (_entity.EEstado == moleQule.Base.EEstado.Anulado)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
				return;
			}

			switch (estado)
			{
				case moleQule.Base.EEstado.Anulado:
					{
						if (_entity.EEstado == moleQule.Base.EEstado.Contabilizado)
						{
							PgMng.ShowInfoException(moleQule.Common.Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
							return;
						}

						if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
						{
							return;
						}
					}
					break;

                case moleQule.Base.EEstado.Baja:
                    {
                        if (_entity.OidPair == 0)
                        {
                            LivestockBookLineInfo entry = SelectEntryPair();

                            if (entry != null)
                                _entity.CopyFromPair(entry);
                            else
                                return;
                        }
                    }
                    break;

                case moleQule.Base.EEstado.Alta:
                    {
                        _entity.OidPair = 0;
                    }
                    break;
			}

			_entity.EEstado = estado;

            ShowPair();
		}

		#endregion

		#region Buttons

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			moleQule.Base.EEstado[] list = { moleQule.Base.EEstado.Anulado, moleQule.Base.EEstado.Alta, moleQule.Base.EEstado.Baja };
			form.SetDataSource(moleQule.Base.EnumText<moleQule.Base.EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
    			ChangeStateAction((moleQule.Base.EEstado)estado.Oid);
			}
		}

        private void Pair_BT_Click(object sender, EventArgs e)
        {
            LivestockBookLineInfo entry = SelectEntryPair();

            if (entry != null)
                _entity.CopyFromPair(entry);
            else
                return;
        }

		private void Raza_BT_Click(object sender, EventArgs e)
		{
			SelectInputForm form = new SelectInputForm(RazaAnimalList.GetList(false));			
	
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				RazaAnimalInfo raza = (RazaAnimalInfo)form.Selected;
				_entity.Raza = raza.Valor;
				Raza_TB.Text = _entity.Raza;
			}
		}

		private void Sexo_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(moleQule.Common.Structs.EnumText<ESexo>.GetList(false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource sexo = form.Selected as ComboBoxSource;
				_entity.Sexo = sexo.Oid;
				Sexo_TB.Text = sexo.Texto;
			}
		}

		private void Tipo_BT_Click(object sender, EventArgs e)
		{
            if (!_entity.CanChangeType()) return;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

            form.SetDataSource(moleQule.Store.Structs.EnumText<ETipoLineaLibroGanadero>.GetList(GetTypes()));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Tipo = estado.Oid;
				Tipo_TB.Text = _entity.TipoLabel;

                switch (_entity.ETipo)
                {
                    case ETipoLineaLibroGanadero.Nacimiento:
                        ChangeStateAction(moleQule.Base.EEstado.Alta);
                        break;

                    case ETipoLineaLibroGanadero.Muerte:
                        ChangeStateAction(moleQule.Base.EEstado.Baja);
                        break;
                }
			}

            Status_BT.Enabled = _entity.ETipo == ETipoLineaLibroGanadero.TraspasoExplotacion;
		}

		#endregion

		#region Events

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.Fecha = Fecha_DTP.Value;
		}

        #endregion
    }
}
