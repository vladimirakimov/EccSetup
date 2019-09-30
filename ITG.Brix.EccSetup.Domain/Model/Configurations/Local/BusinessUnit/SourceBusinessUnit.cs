using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class SourceBusinessUnit : ValueObject
    {
        public string Name { get; set; }

        public SourceBusinessUnit(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
