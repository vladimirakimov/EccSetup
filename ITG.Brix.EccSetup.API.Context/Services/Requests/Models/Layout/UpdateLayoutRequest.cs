using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class UpdateLayoutRequest
    {
        private readonly UpdateLayoutFromRoute _route;
        private readonly UpdateLayoutFromQuery _query;
        private readonly UpdateLayoutFromHeader _header;
        private readonly UpdateLayoutFromBody _body;

        public UpdateLayoutRequest(UpdateLayoutFromRoute route, UpdateLayoutFromQuery query, UpdateLayoutFromHeader header, UpdateLayoutFromBody body)
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
