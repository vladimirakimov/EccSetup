using System;

namespace ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels
{
    public class LocationClass
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Sc { get; set; }
        public string St { get; set; }
        public string W { get; set; }
        public string G { get; set; }
        public string R { get; set; }
        public string P { get; set; }
        public string T { get; set; }
        public bool Ra { get; set; }
    }
}
