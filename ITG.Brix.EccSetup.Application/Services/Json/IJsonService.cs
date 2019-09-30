using Newtonsoft.Json.Linq;

namespace ITG.Brix.EccSetup.Application.Services.Json
{
    public interface IJsonService<T> where T : class
    {
        T Deserialize<T>(string json);
        T ToObject<T>(JToken jToken);
    }
}
