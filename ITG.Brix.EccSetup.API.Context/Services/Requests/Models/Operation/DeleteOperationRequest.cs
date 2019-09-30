using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class DeleteOperationRequest
    {
        private readonly DeleteOperationFromRoute _route;
        private readonly DeleteOperationFromQuery _query;
        private readonly DeleteOperationFromHeader _header;

        public DeleteOperationRequest(DeleteOperationFromRoute route, DeleteOperationFromQuery query, DeleteOperationFromHeader header)
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
