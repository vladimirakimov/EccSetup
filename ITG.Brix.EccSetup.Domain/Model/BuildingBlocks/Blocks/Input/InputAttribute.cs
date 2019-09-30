using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class InputAttribute : ValueObject
    {
        public string Name { get; private set; }
        public bool NotMadatory { get; private set; }
        public bool ScanningOnly { get; private set; }
        public int MinimumLenght { get; private set; }
        public int MaximumLenght { get; private set; }
        public string Prefix { get; private set; }
        public int CheckLastXCharacters { get; private set; }

        public InputAttribute(string name, bool notMadatory, bool scanningOnly, int minimumLenght, int maximumLenght, string prefix, int checkLastXCharacters)
        {
            Name = name;
            NotMadatory = notMadatory;
            ScanningOnly = scanningOnly;
            MinimumLenght = minimumLenght;
            MaximumLenght = maximumLenght;
            Prefix = prefix;
            CheckLastXCharacters = checkLastXCharacters;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return NotMadatory;
            yield return ScanningOnly;
            yield return MinimumLenght;
            yield return MaximumLenght;
            yield return Prefix;
            yield return CheckLastXCharacters;
        }
    }
}
