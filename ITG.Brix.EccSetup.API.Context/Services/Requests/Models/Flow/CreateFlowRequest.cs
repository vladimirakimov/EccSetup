using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateFlowRequest
    {
        private readonly CreateFlowFromQuery _query;
        private readonly CreateFlowFromBody _body;

        public CreateFlowRequest(CreateFlowFromQuery query, CreateFlowFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;

        public string BodyDescription => _body.Description;

        public string BodyImage => _body.Image;

    }
}
