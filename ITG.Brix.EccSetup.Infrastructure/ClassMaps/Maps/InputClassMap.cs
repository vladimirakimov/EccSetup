using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class InputClassMap : DomainClassMap<Input>
    {
        public override void Map(BsonClassMap<Input> cm)
        {
            cm.AutoMap();
            cm.MapField("_tags").SetElementName("Tags");
            cm.MapField("_images").SetElementName("Images");
            cm.MapField("_videos").SetElementName("Videos");
            cm.MapField("_inputAttributes").SetElementName("InputAttributes");
        }
    }
}
