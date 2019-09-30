using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class FlowClassMap : DomainClassMap<Flow>
    {
        public override void Map(BsonClassMap<Flow> cm)
        {
            cm.AutoMap();            
        }
    }
}
