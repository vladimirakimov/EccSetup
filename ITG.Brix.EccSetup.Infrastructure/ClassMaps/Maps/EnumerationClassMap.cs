using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class EnumerationClassMap : DomainClassMap<Enumeration>
    {
        public override void Map(BsonClassMap<Enumeration> cm)
        {
            cm.MapMember(x => x.Id);
            cm.MapMember(x => x.Name);
            cm.AddKnownType(typeof(BlockType));
            cm.AddKnownType(typeof(Behavior));
        }
    }
}
