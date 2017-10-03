using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Store.Data
{
	[Serializable()]
	public class ToolMap : ClassMapping<ToolRecord>
	{	
		public ToolMap()
		{
			Table("`STTool`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`STTool_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); });
			Property(x => x.ID, map => { map.Column("`CODE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Status, map => { map.Column("`STATUS`"); map.NotNullable(false); });
			Property(x => x.Name, map => { map.Column("`NAME`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Description, map => { map.Column("`DESCRIPTION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.From, map => { map.Column("`FROM`"); map.NotNullable(false); });
			Property(x => x.Till, map => { map.Column("`TILL`"); map.NotNullable(false); });
			Property(x => x.Cost, map => { map.Column("`COST`"); map.NotNullable(false); });
			Property(x => x.Location, map => { map.Column("`LOCATION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}