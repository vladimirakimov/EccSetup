using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.Application.Bases;
using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Responses
{
    public interface IApiResponse
    {
        IActionResult Fail(ValidationResult validationResult);
        IActionResult Fail(Result result);
        IActionResult Ok(object value);
        IActionResult Ok(object value, string eTagValue);
        IActionResult Created(string uri, string eTagValue);
        IActionResult Updated(string eTagValue);
        IActionResult Deleted();
        IActionResult Error();
    }
}
