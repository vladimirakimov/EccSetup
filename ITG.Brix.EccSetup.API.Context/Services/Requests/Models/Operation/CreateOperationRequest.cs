using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class CreateOperationRequest
    {
        private readonly CreateOperationFromQuery _query;
        private readonly CreateOperationFromBody _body;

        public CreateOperationRequest(CreateOperationFromQuery query, CreateOperationFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyName => _body.Name;

        public string BodyDescription => _body.Description;

        public IEnumerable<string> BodyTags => _body.Tags;

    }
}
