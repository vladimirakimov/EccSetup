using ITG.Brix.EccSetup.API.Context.Bases;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests
{
    /// <summary>
    /// Request validation strategy.
    /// </summary>
    public interface IApiRequest
    {
        ValidationResult Validate<T>(T request);
    }
}
