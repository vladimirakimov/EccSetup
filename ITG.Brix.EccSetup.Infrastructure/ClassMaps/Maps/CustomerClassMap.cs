using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class CustomerClassMap : DomainClassMap<Customer>
    {
        public override void Map(BsonClassMap<Customer> cm)
        {
            cm.AutoMap();
        }
    }
}
