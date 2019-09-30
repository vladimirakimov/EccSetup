using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class BuildingBlockIcon : ValueObject
    {
        public Guid Id { get; private set; }

        public BuildingBlockIcon(Guid id)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            Id = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }
    }
}
