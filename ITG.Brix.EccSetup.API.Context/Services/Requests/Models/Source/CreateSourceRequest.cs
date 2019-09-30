using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateSourceRequest
    {
        private readonly CreateSourceFromQuery _query;
        private readonly CreateSourceFromBody _body;

        public CreateSourceRequest(CreateSourceFromQuery query, CreateSourceFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;

        public string BodyDescription => _body.Description;

        public IEnumerable<string> BodyBusinessUnits => _body.BusinessUnits;

    }
}
