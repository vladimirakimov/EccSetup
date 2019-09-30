using System;
using System.Collections.Generic;
using System.Text;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class OperatorActivitiesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<OperatorActivityModel> Value { get; set; }
    }
}
