using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class SiteReadRepositoryTests
    {
        private ISiteReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.SiteCollectionName);
            _repository = new SiteReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
