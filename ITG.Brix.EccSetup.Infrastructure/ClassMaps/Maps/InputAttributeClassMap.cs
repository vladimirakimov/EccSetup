using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class InputAttributeClassMap : DomainClassMap<InputAttribute>
    {
        public override void Map(BsonClassMap<InputAttribute> cm)
        {
            cm.AutoMap();
        }
    }
}
