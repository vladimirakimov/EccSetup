using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class CheckListClassMap : DomainClassMap<Checklist>
    {
        public override void Map(BsonClassMap<Checklist> cm)
        {
            cm.AutoMap();
            cm.MapField("_tags").SetElementName("Tags");
            cm.MapField("_questions").SetElementName("Questions");
        }
    }
}
