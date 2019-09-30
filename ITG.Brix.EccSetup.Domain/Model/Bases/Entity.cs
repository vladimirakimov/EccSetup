using System;

namespace ITG.Brix.EccSetup.Domain.Bases
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public int Version { get; set; }
    }
}
