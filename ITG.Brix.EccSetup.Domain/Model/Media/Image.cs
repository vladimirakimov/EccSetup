using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Image : ValueObject
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public Image(string name, string url)
        {
            Name = name;
            Url = url;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Url;
        }
    }
}
