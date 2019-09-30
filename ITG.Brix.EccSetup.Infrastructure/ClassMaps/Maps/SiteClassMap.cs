using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class SiteClassMap : DomainClassMap<Site>
    {
        public override void Map(BsonClassMap<Site> cm)
        {
            cm.AutoMap();
        }
    }
}
