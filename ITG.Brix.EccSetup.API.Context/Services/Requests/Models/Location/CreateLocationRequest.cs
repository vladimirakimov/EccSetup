using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateLocationRequest
    {
        private readonly CreateLocationFromQuery _query;
        private readonly CreateLocationFromBody _body;

        public CreateLocationRequest(CreateLocationFromQuery query, CreateLocationFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;
        public string BodySource => _body.Source;
        public string BodySite => _body.Site;
        public string BodyWarehouse => _body.Warehouse;
        public string BodyGate => _body.Gate;
        public string BodyRow => _body.Row;
        public string BodyPosition => _body.Position;
        public string BodyType => _body.Type;
        public string BodyIsRack => _body.IsRack;
    }
}
