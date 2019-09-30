using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ScreenTypeClassMap : DomainClassMap<ScreenType>
    {
        public override void Map(BsonClassMap<ScreenType> cm)
        {
            cm.AutoMap();
        }
    }
}
