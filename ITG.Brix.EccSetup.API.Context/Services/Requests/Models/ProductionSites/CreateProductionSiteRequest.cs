using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateProductionSiteRequest
    {
        private readonly CreateProductionSiteFromQuery _query;
        private readonly CreateProductionSiteFromBody _body;

        public CreateProductionSiteRequest(CreateProductionSiteFromQuery query, CreateProductionSiteFromBody body)
        {
            _query = query;
            _body = body;
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;
        public string BodyCode => _body.Code;
        public string BodySource => _body.Source;
    }
}
