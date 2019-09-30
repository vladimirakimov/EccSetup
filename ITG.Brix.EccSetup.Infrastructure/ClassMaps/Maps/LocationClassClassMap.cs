using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class LocationClassClassMap : DomainClassMap<LocationClass>
    {
        public override void Map(BsonClassMap<LocationClass> cm)
        {
            cm.AutoMap();
        }
    }
}
