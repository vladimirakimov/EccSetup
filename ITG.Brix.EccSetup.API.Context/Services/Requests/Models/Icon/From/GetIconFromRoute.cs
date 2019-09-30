using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class GetIconFromRoute
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
