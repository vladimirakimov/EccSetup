using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class RemarkIconClassMap : DomainClassMap<RemarkIcon>
    {
        public override void Map(BsonClassMap<RemarkIcon> cm)
        {
            cm.AutoMap();
        }
    }
}
