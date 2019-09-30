using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ScreenClassMap : DomainClassMap<Screen>
    {
        public override void Map(BsonClassMap<Screen> cm)
        {
            cm.AutoMap();
        }
    }
}
