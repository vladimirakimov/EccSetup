using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class GetOperationRequest
    {
        private readonly GetOperationFromRoute _route;
        private readonly GetOperationFromQuery _query;

        public GetOperationRequest(GetOperationFromRoute route, GetOperationFromQuery query)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;
    }
}
