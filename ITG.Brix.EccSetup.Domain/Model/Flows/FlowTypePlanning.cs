using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class FlowTypePlanning : ValueObject
    {
        public string Name { get; private set; }

        public FlowTypePlanning(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
