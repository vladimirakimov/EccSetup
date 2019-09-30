using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.API.Context.Providers.Impl
{
    public class JsonProvider : IJsonProvider
    {
        public IDictionary<string, object> ToDictionary(string json)
        {
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            var result = new Dictionary<string, object>(deserialized, StringComparer.InvariantCultureIgnoreCase);
            return result;
        }
    }
}
