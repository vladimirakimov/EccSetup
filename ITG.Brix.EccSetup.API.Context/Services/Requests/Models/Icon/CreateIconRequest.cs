using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateIconRequest
    {
        private readonly CreateIconFromQuery _query;
        private readonly CreateIconFromBody _body;

        public CreateIconRequest(CreateIconFromQuery query, CreateIconFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;

        public string BodyDataPath => _body.DataPath;

    }
}
