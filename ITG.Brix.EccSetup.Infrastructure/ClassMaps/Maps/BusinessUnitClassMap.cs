using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class BusinessUnitClassMap : DomainClassMap<BusinessUnit>
    {
        public override void Map(BsonClassMap<BusinessUnit> cm)
        {
            cm.AutoMap();
        }
    }
}
