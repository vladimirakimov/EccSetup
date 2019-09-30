namespace ITG.Brix.EccSetup.Infrastructure.Configurations
{
    public interface IPersistenceConfiguration
    {
        string ConnectionString { get; }
        string Database { get; }
    }
}
