using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations.External
{
    [TestClass]
    [TestCategory("Integration")]
    public class StorageStatusReadRepositoryTests
    {
        private StorageStatusReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.StorageStatusCollectionName);
            _repository = new StorageStatusReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            //Arrange
            RepositoryHelper.ForStorageStatus.CreateStorageStatus(Guid.NewGuid(), "code-1", "name", true, "source");
            RepositoryHelper.ForStorageStatus.CreateStorageStatus(Guid.NewGuid(), "code-2", "name", true, "source");
            RepositoryHelper.ForStorageStatus.CreateStorageStatus(Guid.NewGuid(), "code-3", "name", true, "source");
            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
