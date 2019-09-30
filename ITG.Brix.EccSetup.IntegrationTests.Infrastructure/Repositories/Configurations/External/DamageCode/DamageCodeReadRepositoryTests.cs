using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class DamageCodeReadRepositoryTests
    {
        private DamageCodeReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.DamageCodeCollectionName);
            _repository = new DamageCodeReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            //Arrange
            RepositoryHelper.ForDamageCode.CreateDamageCode(Guid.NewGuid(), "code-1", "name", true, "source");
            RepositoryHelper.ForDamageCode.CreateDamageCode(Guid.NewGuid(), "code-2", "name", true, "source");
            RepositoryHelper.ForDamageCode.CreateDamageCode(Guid.NewGuid(), "code-3", "name", true, "source");
            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
