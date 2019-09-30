using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateIconFromBody
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DataPath { get; set; }
    }
}
