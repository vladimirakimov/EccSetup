namespace ITG.Brix.EccSetup.Infrastructure.Providers
{
    public interface IFileNameProvider
    {
        string Generate(string extension);
    }
}
