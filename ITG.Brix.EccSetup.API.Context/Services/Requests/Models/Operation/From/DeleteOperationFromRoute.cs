using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From
{
    public class DeleteOperationFromRoute
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
