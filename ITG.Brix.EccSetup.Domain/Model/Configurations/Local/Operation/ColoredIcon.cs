using ITG.Brix.EccSetup.Domain.Bases;
using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class ColoredIcon : ValueObject
    {
        public Guid IconId { get; private set; }
        public string FillColor { get; private set; }

        public ColoredIcon(Guid iconId, string fillColor)
        {
            if (iconId == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default Guid.", nameof(iconId)));
            }

            if (string.IsNullOrEmpty(fillColor))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(fillColor)));
            }

            IconId = iconId;
            FillColor = fillColor;
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return IconId;
            yield return FillColor;
        }
    }
}
