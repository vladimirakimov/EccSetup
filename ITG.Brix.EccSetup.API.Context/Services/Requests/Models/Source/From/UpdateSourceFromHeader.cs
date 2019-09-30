using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class UpdateSourceFromHeader
    {
        [FromHeader(Name = "If-Match")]
        public string IfMatch { get; set; }

        [FromHeader(Name = "Content-Type")]
        public string ContentType { get; set; }
    }
}
