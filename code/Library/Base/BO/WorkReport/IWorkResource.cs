using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Common.Structs;
using moleQule;
using moleQule.Common; 

namespace moleQule.Library.Store
{
	public interface IWorkResource
	{
		string ID { get; }
		long Oid { get; }
		long EntityType { get; }
		ETipoEntidad EEntityType { get; }
		string Name { get; }
		decimal Cost { get; }
	}
}