using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations
{
    [TestClass]
    [TestCategory("Integration")]
    public class SourceReadRepositoryTests
    {
        private ISourceReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitalize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.SourceCollectionName);
            _repository = new SourceReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var sourceBusinessUnitName = "BSG";
            var sourceBusinessUnit = new SourceBusinessUnit(sourceBusinessUnitName);
            var sourceBusinessUnits = new List<SourceBusinessUnit>() { sourceBusinessUnit };

            RepositoryHelper.ForSource.CreateSource(id, name, description, sourceBusinessUnits);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            var sourceBusinessUnitName = "BSG";
            var sourceBusinessUnit = new SourceBusinessUnit(sourceBusinessUnitName);
            var sourceBusinessUnits = new List<SourceBusinessUnit>() { sourceBusinessUnit };

            RepositoryHelper.ForSource.CreateSource(Guid.NewGuid(), "name-1", "description", sourceBusinessUnits);
            RepositoryHelper.ForSource.CreateSource(Guid.NewGuid(), "name-2", "description", sourceBusinessUnits);
            RepositoryHelper.ForSource.CreateSource(Guid.NewGuid(), "name-3", "description", sourceBusinessUnits);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
