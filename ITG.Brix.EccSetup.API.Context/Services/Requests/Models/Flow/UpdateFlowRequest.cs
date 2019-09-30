using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class UpdateFlowRequest
    {
        private readonly UpdateFlowFromRoute _route;
        private readonly UpdateFlowFromQuery _query;
        private readonly UpdateFlowFromHeader _header;
        private readonly UpdateFlowFromBody _body;

        public UpdateFlowRequest(UpdateFlowFromRoute route, UpdateFlowFromQuery query, UpdateFlowFromHeader header, UpdateFlowFromBody body)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _header = header ?? throw new ArgumentNullException(nameof(header));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;

        public string HeaderIfMatch => _header.IfMatch;

        public string HeaderContentType => _header.ContentType;

        public string BodyPatch => _body.Patch;
    }
}
