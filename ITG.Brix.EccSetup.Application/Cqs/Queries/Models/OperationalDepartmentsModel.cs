using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class OperationalDepartmentsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<OperationalDepartmentModel> Value { get; set; }
    }
}
