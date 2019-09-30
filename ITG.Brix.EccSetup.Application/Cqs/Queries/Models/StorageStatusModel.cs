using System;

namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models
{
    public class StorageStatusModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Default { get; set; }
        public string Source { get; set; }
    }
}
