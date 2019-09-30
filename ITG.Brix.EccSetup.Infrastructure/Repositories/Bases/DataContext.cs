using ITG.Brix.EccSetup.Infrastructure.Configurations;
using MongoDB.Driver;
using System;
using System.Security.Authentication;

namespace ITG.Brix.EccSetup.Infrastructure.Repositories
{
    public class DataContext
    {
        public IMongoDatabase Database { get; }

        public DataContext(IPersistenceConfiguration persistenceConfiguration)
        {
            if (persistenceConfiguration == null)
            {
                throw new ArgumentNullException(nameof(persistenceConfiguration));
            }

            MongoClientSettings settings = MongoClientSettings.FromUrl(
                  new MongoUrl(persistenceConfiguration.ConnectionString)
                );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            MongoClient client = new MongoClient(settings);

            var databaseName = "Brix-EccSetup";
            Database = client.GetDatabase(databaseName);
        }
    }
}
