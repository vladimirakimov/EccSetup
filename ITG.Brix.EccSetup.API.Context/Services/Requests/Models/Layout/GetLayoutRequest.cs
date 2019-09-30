using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class GetLayoutRequest
    {
        private readonly GetLayoutFromRoute _route;
        private readonly GetLayoutFromQuery _query;

        public GetLayoutRequest(GetLayoutFromRoute route, GetLayoutFromQuery query)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;
    }
}
