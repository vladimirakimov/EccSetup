using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class SourceClassMap : DomainClassMap<Source>
    {
        public override void Map(BsonClassMap<Source> cm)
        {
            cm.AutoMap();
            cm.MapField("_sourceBusinessUnits").SetElementName("SourceBusinessUnits");
        }
    }
}
