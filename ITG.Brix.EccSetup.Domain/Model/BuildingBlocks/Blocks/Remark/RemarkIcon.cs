using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class RemarkIcon : ValueObject
    {
        public Guid IconId { get; private set; }

        public RemarkIcon(Guid iconId)
        {
            if (iconId == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default Guid.", nameof(iconId)));
            }

            IconId = iconId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return IconId;
        }
    }
}
