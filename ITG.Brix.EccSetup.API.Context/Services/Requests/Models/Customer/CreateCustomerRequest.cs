using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.Customer.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateCustomerRequest
    {
        private readonly CreateCustomerFromQuery _query;
        private readonly CreateCustomerFromBody _body;

        public CreateCustomerRequest(CreateCustomerFromQuery query, CreateCustomerFromBody body)
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
