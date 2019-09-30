using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class GetBusinessUnitRequest
    {
        private readonly GetBusinessUnitFromRoute _route;
        private readonly GetBusinessUnitFromQuery _query;

        public GetBusinessUnitRequest(GetBusinessUnitFromRoute route, GetBusinessUnitFromQuery query)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;
    }
}
