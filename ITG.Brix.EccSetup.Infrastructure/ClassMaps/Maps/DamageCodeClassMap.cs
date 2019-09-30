using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class DamageCodeClassMap : DomainClassMap<DamageCode>
    {
        public override void Map(BsonClassMap<DamageCode> cm)
        {
            cm.AutoMap();
        }
    }
}
