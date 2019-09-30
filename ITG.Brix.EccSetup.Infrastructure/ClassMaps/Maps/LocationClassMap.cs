using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class LocationClassMap : DomainClassMap<Location>
    {
        public override void Map(BsonClassMap<Location> cm)
        {
            cm.AutoMap();
        }
    }
}
