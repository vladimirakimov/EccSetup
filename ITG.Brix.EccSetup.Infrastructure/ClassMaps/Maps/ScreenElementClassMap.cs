using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ScreenElementClassMap : DomainClassMap<ScreenElement>
    {
        public override void Map(BsonClassMap<ScreenElement> cm)
        {
            cm.AutoMap();
        }
    }
}
