using Microsoft.AspNetCore.Mvc;

namespace ITG.Brix.EccSetup.API.Context.Services.Arrangements.Bases
{
    public interface IValidatorActionResult
    {
        IActionResult Result { get; }
    }
}
