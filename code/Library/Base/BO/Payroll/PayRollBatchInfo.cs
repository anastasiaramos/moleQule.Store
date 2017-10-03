using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Base;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common;
using moleQule.CslaEx;
using moleQule.Store.Structs;

namespace moleQule.Library.Store
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class PayrollBatchInfo : ReadOnlyBaseEx<PayrollBatchInfo, PayrollBatch>, IEntidadRegistroInfo
	{
		#region IEntidadRegistroInfo

		public moleQule.Common.Structs.ETipoEntidad ETipoEntidad { get { return moleQule.Common.Structs.ETipoEntidad.Nomina; } }
		public string DescripcionRegistro { get { return "REMESA DE NOMINAS Nº " + Codigo + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2"); } }

		#endregion

		#region Attributes

		protected PayrollBatchBase _base = new PayrollBatchBase();

		protected PayrollList _nominas = null;
		protected ExpenseList _gastos = null;

		#endregion
		
		#region Properties

		public PayrollBatchBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal IRPF { get { return _base.Record.Irpf; } }
		public Decimal SeguroEmpresa { get { return _base.Record.SeguroEmpresa; } }
		public Decimal SeguroPersonal { get { return _base.Record.SeguroPersonal; } }
		public DateTime PrevisionPago { get { return _base.Record.PrevisionPago; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public Decimal BaseIRPF { get { return _base.Record.BaseIrpf; } }
		public Decimal Descuentos { get { return _base.Record.Descuentos; } }

		public PayrollList Nominas { get { return _nominas; } }
		public ExpenseList Gastos { get { return _gastos; } }

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual decimal Neto { get { return _base.Neto; } set { _base.Neto = value; } }
		public virtual decimal Seguro { get { return _base.Seguro; } }

		#endregion
		
		#region Business Methods
				
		public void CopyFrom(PayrollBatch source) { _base.CopyValues(source); }

		public virtual void CalculaTotal()
		{
			if (_gastos == null) return;

			Neto = 0;
			_base.Record.Total = 0;

			foreach (ExpenseInfo item in _gastos)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				if (item.ECategoriaGasto != ECategoriaGasto.Nomina) continue;

				Neto += item.Total;
			}

			_base.Record.Total = Neto + SeguroEmpresa + SeguroPersonal + IRPF;
		}
		
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected PayrollBatchInfo() { /* require use of factory methods */ }
		private PayrollBatchInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal PayrollBatchInfo(PayrollBatch item, bool childs)
		{
			_base.CopyValues(item);

			if (childs)
			{
				_nominas = (item.Nominas != null) ? PayrollList.GetChildList(item.Nominas) : null;
				_gastos = (item.Gastos != null) ? ExpenseList.GetChildList(item.Gastos) : null;				
			}
		}
	
		public static PayrollBatchInfo GetChild(IDataReader reader, bool childs = false)
        {
			return new PayrollBatchInfo(reader, childs);
		}

		public static PayrollBatchInfo New(long oid = 0) { return new PayrollBatchInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		public static PayrollBatchInfo Get(long oid, bool childs = false)
		{
			if (!PayrollBatch.CanGetObject()) throw new System.Security.SecurityException(moleQule.Resources.Messages.USER_NOT_ALLOWED);
			return Get(PayrollBatch.SELECT(oid, false), childs);
		}
		
		#endregion
		
		#region Common Data Access
								
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
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					query = PayrollList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_nominas = PayrollList.GetChildList(reader);	

					query = ExpenseList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _gastos = ExpenseList.GetChildList(reader);					
				}
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;

						query = PayrollList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_nominas = PayrollList.GetChildList(reader);	

						query = ExpenseList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _gastos = ExpenseList.GetChildList(reader);						
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

		public static string SELECT(QueryConditions conditions) { return PayrollBatch.SELECT(conditions, false); }
		
        #endregion		
	}
}
