using System;

namespace ITG.Brix.EccSetup.Application.Bases
{
    public sealed class CustomFaultCode
    {
        public static readonly CustomFaultCode NotFound = new CustomFaultCode("not-found");
        public static readonly CustomFaultCode InvalidQueryTop = new CustomFaultCode("invalid-query-top");
        public static readonly CustomFaultCode InvalidQuerySkip = new CustomFaultCode("invalid-query-skip");

        public string Name { get; private set; }

        private CustomFaultCode() { }

        public CustomFaultCode(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            Name = name;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as CustomFaultCode;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Name.Equals(otherValue.Name);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
