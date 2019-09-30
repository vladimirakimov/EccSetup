using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class IconClassMap : DomainClassMap<Icon>
    {
        public override void Map(BsonClassMap<Icon> cm)
        {
            cm.AutoMap();
        }
    }
}
