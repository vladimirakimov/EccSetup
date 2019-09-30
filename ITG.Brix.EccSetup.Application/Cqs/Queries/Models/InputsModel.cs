using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class InputsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<InputModel> Value { get; set; }
    }
}
