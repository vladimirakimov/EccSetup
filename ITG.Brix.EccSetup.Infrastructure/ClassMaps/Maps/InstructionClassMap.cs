using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class InstructionClassMap : DomainClassMap<Instruction>
    {
        public override void Map(BsonClassMap<Instruction> cm)
        {
            cm.AutoMap();
            cm.MapField("_tags").SetElementName("Tags");
        }
    }
}
