using System.Collections.Generic;

namespace ITG.Brix.EccSetup.API.Context.Providers
{
    public interface IJsonProvider
    {
        IDictionary<string, object> ToDictionary(string json);
    }
}
