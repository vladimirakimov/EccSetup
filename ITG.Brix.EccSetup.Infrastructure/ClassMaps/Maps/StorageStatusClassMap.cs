using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class StorageStatusClassMap : DomainClassMap<StorageStatus>
    {
        public override void Map(BsonClassMap<StorageStatus> cm)
        {
            cm.AutoMap();
        }
    }
}
