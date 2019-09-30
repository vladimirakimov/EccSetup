using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ChecklistAnswerClassMap : DomainClassMap<ChecklistAnswer>
    {
        public override void Map(BsonClassMap<ChecklistAnswer> cm)
        {
            cm.AutoMap();
        }
    }
}
