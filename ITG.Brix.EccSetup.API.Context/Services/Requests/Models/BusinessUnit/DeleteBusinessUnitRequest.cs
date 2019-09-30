using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class DeleteBusinessUnitRequest
    {
        private readonly DeleteBusinessUnitFromRoute _route;
        private readonly DeleteBusinessUnitFromQuery _query;
        private readonly DeleteBusinessUnitFromHeader _header;

        public DeleteBusinessUnitRequest(DeleteBusinessUnitFromRoute route, DeleteBusinessUnitFromQuery query, DeleteBusinessUnitFromHeader header)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _header = header ?? throw new ArgumentNullException(nameof(header));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;

        public string HeaderIfMatch => _header.IfMatch;
    }
}
