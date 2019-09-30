using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateLayoutRequest
    {
        private readonly CreateLayoutFromQuery _query;
        private readonly CreateLayoutFromBody _body;

        public CreateLayoutRequest(CreateLayoutFromQuery query, CreateLayoutFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;

        public string BodyDescription => _body.Description;

        public string BodyImage => _body.Image;

    }
}
