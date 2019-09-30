using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class CustomersModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<CustomerModel> Value { get; set; }
    }
}
