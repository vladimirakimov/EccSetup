using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class BehaviorClassMap : DomainClassMap<Behavior>
    {
        public override void Map(BsonClassMap<Behavior> cm)
        {
            cm.MapCreator(x => new Behavior(x.Id, x.Name));
        }
    }
}
