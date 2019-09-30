using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class LayoutClassMap : DomainClassMap<Layout>
    {
        public override void Map(BsonClassMap<Layout> cm)
        {
            cm.AutoMap();
        }
    }
}
