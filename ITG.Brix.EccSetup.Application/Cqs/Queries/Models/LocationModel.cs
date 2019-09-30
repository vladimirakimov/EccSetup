using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class LocationModel
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Site { get; set; }
        public string Warehouse { get; set; }
        public string Gate { get; set; }
        public string Row { get; set; }
        public string Position { get; set; }
        public string Type { get; set; }
        public bool IsRack { get; set; }
    }
}
