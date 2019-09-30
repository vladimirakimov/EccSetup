using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class BlockTypeClassMap : DomainClassMap<BlockType>
    {
        public override void Map(BsonClassMap<BlockType> cm)
        {
            cm.MapCreator(x => new BlockType(x.Id, x.Name));
        }
    }
}
