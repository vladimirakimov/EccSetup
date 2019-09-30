using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateBusinessUnitFromBody
    {
        [DataMember]
        public string Name { get; set; }
    }
}
