using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class TagClassMap : DomainClassMap<Tag>
    {
        public override void Map(BsonClassMap<Tag> cm)
        {
            cm.AutoMap();
        }
    }
}
