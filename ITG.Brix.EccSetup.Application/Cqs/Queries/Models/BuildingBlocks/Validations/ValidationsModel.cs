using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.BuildingBlocks.Validations
{
    public class ValidationsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<ValidationModel> Value { get; set; }
    }
}
