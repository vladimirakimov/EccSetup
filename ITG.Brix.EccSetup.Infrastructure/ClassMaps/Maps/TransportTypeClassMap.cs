using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class TransportTypeClassMap : DomainClassMap<TransportType>
    {
        public override void Map(BsonClassMap<TransportType> cm)
        {
            cm.AutoMap();
        }
    }
}
