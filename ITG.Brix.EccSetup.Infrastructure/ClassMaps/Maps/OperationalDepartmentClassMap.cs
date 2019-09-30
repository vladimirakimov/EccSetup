using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps.Bases;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.EccSetup.Infrastructure.ClassMaps.Maps
{
    public class OperationalDepartmentClassMap : DomainClassMap<OperationalDepartment>
    {
        public override void Map(BsonClassMap<OperationalDepartment> cm)
        {
            cm.AutoMap();
        }
    }
}
