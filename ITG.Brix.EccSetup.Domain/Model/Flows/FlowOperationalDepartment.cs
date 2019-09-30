using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class FlowOperationalDepartment : ValueObject
    {
        public string Name { get; private set; }

        public FlowOperationalDepartment(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
