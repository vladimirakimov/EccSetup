using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class OperatorActivityClassMap : DomainClassMap<OperatorActivity>
    {
        public override void Map(BsonClassMap<OperatorActivity> cm)
        {
            cm.AutoMap();
        }
    }
}
