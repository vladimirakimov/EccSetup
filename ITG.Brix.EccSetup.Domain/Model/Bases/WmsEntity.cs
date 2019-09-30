using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain.Bases
{
    public abstract class WmsEntity : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Source { get; protected set; }

        protected WmsEntity(string name, string description, string source)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            if (string.IsNullOrEmpty(source))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(source)));
            }

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Source = source;
        }
    }
}
