using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class TypePlanningClassMap : DomainClassMap<TypePlanning>
    {
        public override void Map(BsonClassMap<TypePlanning> cm)
        {
            cm.AutoMap();
        }
    }
}
