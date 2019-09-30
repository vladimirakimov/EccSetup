using ITG.Brix.EccSetup.Application.DataTypes;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Extensions
{
    public static class IDictionaryExtensions
    {
        public static Optional<string> GetOptional(this IDictionary<string, object> dictionary, string key)
        {
            Optional<string> result = dictionary.ContainsKey(key) ? new Optional<string>(dictionary[key] as string) : new Optional<string>();

            return result;
        }

        public static Optional<IEnumerable<string>> GetOptionalEnumerable(this IDictionary<string, object> dictionary, string key)
        {
            Optional<IEnumerable<string>> result = dictionary.ContainsKey(key) ? new Optional<IEnumerable<string>>(dictionary[key].ToStringArray()) : new Optional<IEnumerable<string>>(new string[] { });

            return result;
        }
    }
}
