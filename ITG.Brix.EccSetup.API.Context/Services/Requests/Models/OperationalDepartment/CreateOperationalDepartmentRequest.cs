using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateOperationalDepartmentRequest
    {
        private readonly CreateOperationalDepartmentFromQuery _query;
        private readonly CreateOperationalDepartmentFromBody _body;

        public CreateOperationalDepartmentRequest(CreateOperationalDepartmentFromQuery query, CreateOperationalDepartmentFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }
        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;
        public string BodyCode => _body.Code;
        public string BodySource => _body.Source;
        public string BodySite => _body.Site;
    }
}
