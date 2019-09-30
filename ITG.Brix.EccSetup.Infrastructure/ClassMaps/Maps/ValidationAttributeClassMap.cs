using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ValidationAttributeClassMap : DomainClassMap<ValidationAttribute>
    {
        public override void Map(BsonClassMap<ValidationAttribute> cm)
        {
            cm.AutoMap();
        }
    }
}
