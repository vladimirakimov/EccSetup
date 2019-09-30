using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ColoredIconClassMap : DomainClassMap<ColoredIcon>
    {
        public override void Map(BsonClassMap<ColoredIcon> cm)
        {
            cm.AutoMap();
        }
    }
}
