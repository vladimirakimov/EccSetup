﻿using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateProductionSiteFromBody
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Source { get; set; }
    }
}
