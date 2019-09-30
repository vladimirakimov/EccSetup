using System;

namespace ITG.Brix.EccSetup.Infrastructure.Configurations.Impl
{
    public class PersistenceConfiguration : IPersistenceConfiguration
    {
        private readonly string _connectionString;

        public PersistenceConfiguration(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString => _connectionString;

        public string Database => "Brix-EccSetup";
    }
}
