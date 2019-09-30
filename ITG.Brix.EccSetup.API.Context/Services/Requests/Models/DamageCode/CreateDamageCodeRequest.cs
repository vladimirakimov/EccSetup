using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateDamageCodeRequest
    {
        private readonly CreateDamageCodeFromQuery _query;
        private readonly CreateDamageCodeFromBody _body;

        public CreateDamageCodeRequest(CreateDamageCodeFromQuery query, CreateDamageCodeFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;
        public string BodyCode => _body.Code;
        public string BodyDamagedQuantityRequired => _body.DamagedQuantityRequired;
        public string BodySource => _body.Source;
    }
}
