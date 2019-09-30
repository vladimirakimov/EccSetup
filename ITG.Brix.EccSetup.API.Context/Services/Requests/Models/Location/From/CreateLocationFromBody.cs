using System.Runtime.Serialization;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class CreateLocationFromBody
    {
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Site { get; set; }
        [DataMember]
        public string Warehouse { get; set; }
        [DataMember]
        public string Gate { get; set; }
        [DataMember]
        public string Row { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string IsRack { get; set; }

    }
}
