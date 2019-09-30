using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class DeleteLayoutRequest
    {
        private readonly DeleteLayoutFromRoute _route;
        private readonly DeleteLayoutFromQuery _query;
        private readonly DeleteLayoutFromHeader _header;

        public DeleteLayoutRequest(DeleteLayoutFromRoute route, DeleteLayoutFromQuery query, DeleteLayoutFromHeader header)
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
