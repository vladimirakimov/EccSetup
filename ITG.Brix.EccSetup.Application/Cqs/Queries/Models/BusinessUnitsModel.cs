﻿using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class BusinessUnitsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<BusinessUnitModel> Value { get; set; }
    }
}
