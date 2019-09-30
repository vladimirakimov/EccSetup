using ITG.Brix.EccSetup.API.Context.Services.Arrangements.Bases;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Context.Services.Arrangements
{
    public interface IValidationArrangement
    {
        Task<IValidatorActionResult> Validate<T>(T request);
    }
}
