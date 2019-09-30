using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class InstructionsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<InstructionModel> Value { get; set; }
    }
}
