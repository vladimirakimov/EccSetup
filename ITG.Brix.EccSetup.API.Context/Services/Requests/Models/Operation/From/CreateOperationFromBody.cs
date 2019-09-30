using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateOperationFromBody
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public IEnumerable<string> Tags { get; set; }
    }
}
