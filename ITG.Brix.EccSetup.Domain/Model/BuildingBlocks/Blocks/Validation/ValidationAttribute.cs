using ITG.Brix.EccSetup.Domain.Bases;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class ValidationAttribute : ValueObject
    {
        public string Name { get; private set; }
        public bool ScanningOnly { get; private set; }
        public int MaximumRetries { get; private set; }
        public int MinimumLength { get; private set; }
        public int MaximumLength { get; private set; }
        public string Prefix { get; private set; }
        public int CheckLastXCharacters { get; private set; }
        public IEnumerable<Behavior> Behaviors { get; private set; }

        public ValidationAttribute(string name,
                                            bool scanningOnly,
                                            int maximumRetries,
                                            int minimumLength,
                                            int maximumLength,
                                            string prefix,
                                            int checkLastXCharacters,
                                            IEnumerable<Behavior> behaviors)
        {
            Name = name;
            ScanningOnly = scanningOnly;
            MaximumRetries = maximumRetries;
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
            Prefix = prefix;
            CheckLastXCharacters = checkLastXCharacters;
            Behaviors = behaviors;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return ScanningOnly;
            yield return MaximumRetries;
            yield return MinimumLength;
            yield return MaximumLength;
            yield return Prefix;
            yield return CheckLastXCharacters;
            yield return Behaviors;
        }
    }
}
