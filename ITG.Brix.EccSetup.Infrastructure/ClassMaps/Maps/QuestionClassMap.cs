using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class QuestionClassMap : DomainClassMap<Question>
    {
        public override void Map(BsonClassMap<Question> cm)
        {
            cm.AutoMap();
            cm.MapField("_answers").SetElementName("Answers");
        }
    }
}
