using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class WorkOrderButtonClassMap : DomainClassMap<WorkOrderButton>
    {
        public override void Map(BsonClassMap<WorkOrderButton> cm)
        {
            cm.AutoMap();
        }
    }
}
