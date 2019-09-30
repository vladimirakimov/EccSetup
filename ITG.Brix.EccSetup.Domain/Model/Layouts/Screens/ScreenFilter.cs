using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;

namespace ITG.Brix.EccSetup.Domain
{
    public class ScreenFilter : Entity
    {
        public string WorkOrderAttribute { get; private set; }
        public string WorkOrderValue { get; private set; }

        public ScreenFilter(Guid id, string workOrderAttribute, string workOrderValue)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be dafault guid.", nameof(id)));
            }

            if (string.IsNullOrEmpty(workOrderAttribute))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(workOrderAttribute)));
            }

            Id = id;
            WorkOrderAttribute = workOrderAttribute;
            WorkOrderValue = workOrderValue;
        }
    }
}
