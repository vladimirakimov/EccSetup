using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ScreenFilterClassMap : DomainClassMap<ScreenFilter>
    {
        public override void Map(BsonClassMap<ScreenFilter> cm)
        {
            cm.AutoMap();
        }
    }
}
