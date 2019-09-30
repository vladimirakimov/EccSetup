using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateBusinessUnitRequest
    {
        private readonly CreateBusinessUnitFromQuery _query;
        private readonly CreateBusinessUnitFromBody _body;

        public CreateBusinessUnitRequest(CreateBusinessUnitFromQuery query, CreateBusinessUnitFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;

    }
}
