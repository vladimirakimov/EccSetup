using ITG.Brix.EccSetup.Infrastructure.Configurations;
using ITG.Brix.EccSetup.Infrastructure.Repositories;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases
{
    internal class PersistenceContext : DataContext
    {
        public PersistenceContext(IPersistenceConfiguration persistenceConfiguration) : base(persistenceConfiguration)
        {
        }
    }
}