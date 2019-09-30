using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateTypePlanningRequest
    {
        private readonly CreateTypePlanningFromQuery _query;
        private readonly CreateTypePlanningFromBody _body;

        public CreateTypePlanningRequest(CreateTypePlanningFromQuery query, CreateTypePlanningFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;
        public string BodyCode => _body.Code;
        public string BodySource => _body.Source;
    }
}
