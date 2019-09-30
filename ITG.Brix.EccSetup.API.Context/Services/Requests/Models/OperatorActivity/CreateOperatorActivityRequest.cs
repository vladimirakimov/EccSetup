using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivity.From;
using Newtonsoft.Json.Linq;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.OperatorActivities
{
    public class CreateOperatorActivityRequest
    {
        private readonly CreateOperatorActivityFromQuery _query;
        private readonly JObject _body;

        public CreateOperatorActivityRequest(CreateOperatorActivityFromQuery query,
                                               JObject body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public JObject Body => _body;
    }
}
