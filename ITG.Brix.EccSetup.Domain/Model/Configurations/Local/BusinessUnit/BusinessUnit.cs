using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class BusinessUnit : Entity
    {
        public string Name { get; private set; }

        public BusinessUnit(Guid id, string name)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Id = id;
            Name = name;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}
