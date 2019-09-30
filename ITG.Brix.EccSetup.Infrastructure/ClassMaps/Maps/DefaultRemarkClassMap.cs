using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class DefaultRemarkClassMap : DomainClassMap<DefaultRemark>
    {
        public override void Map(BsonClassMap<DefaultRemark> cm)
        {
            cm.AutoMap();
        }
    }
}
