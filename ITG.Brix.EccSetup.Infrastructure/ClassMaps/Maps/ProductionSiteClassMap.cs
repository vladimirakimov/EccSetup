using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class ProductionSiteClassMap : DomainClassMap<ProductionSite>
    {
        public override void Map(BsonClassMap<ProductionSite> cm)
        {
            cm.AutoMap();
        }
    }
}
