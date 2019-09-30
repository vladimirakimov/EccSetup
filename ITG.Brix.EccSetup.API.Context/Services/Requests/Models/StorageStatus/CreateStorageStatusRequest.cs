using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateStorageStatusRequest
    {
        private readonly CreateStorageStatusFromQuery _query;
        private readonly CreateStorageStatusFromBody _body;

        public CreateStorageStatusRequest(CreateStorageStatusFromQuery query, CreateStorageStatusFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;
        public string BodyCode => _body.Code;
        public string BodyDefault => _body.Default;
        public string BodySource => _body.Source;
    }
}
