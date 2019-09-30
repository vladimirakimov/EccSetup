using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class OperatorActivityClassClassMap : DomainClassMap<OperatorActivityClass>
    {
        public override void Map(BsonClassMap<OperatorActivityClass> cm)
        {
            cm.AutoMap();
        }
    }
}
