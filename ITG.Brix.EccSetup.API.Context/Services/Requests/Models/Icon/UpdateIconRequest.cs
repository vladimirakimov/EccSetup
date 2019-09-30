using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class UpdateIconRequest
    {
        private readonly UpdateIconFromRoute _route;
        private readonly UpdateIconFromQuery _query;
        private readonly UpdateIconFromHeader _header;
        private readonly UpdateIconFromBody _body;

        public UpdateIconRequest(UpdateIconFromRoute route, UpdateIconFromQuery query, UpdateIconFromHeader header, UpdateIconFromBody body)
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
