using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class DeleteSourceFromHeader
    {
        [FromHeader(Name = "If-Match")]
        public string IfMatch { get; set; }
    }
}
