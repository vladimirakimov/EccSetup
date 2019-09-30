using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class RemarkClassMap : DomainClassMap<Remark>
    {
        public override void Map(BsonClassMap<Remark> cm)
        {
            cm.AutoMap();
            cm.MapField("_defaultRemarks").SetElementName("DefaultRemarks");
            cm.MapField("_tags").SetElementName("Tags");
        }
    }
}
