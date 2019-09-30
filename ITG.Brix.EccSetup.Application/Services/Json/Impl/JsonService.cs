using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ITG.Brix.EccSetup.Application.Services.Json.Impl
{
    public class JsonService : IJsonService<object>
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T ToObject<T>(JToken jToken)
        {
            return jToken.ToObject<T>();
        }
    }
}
