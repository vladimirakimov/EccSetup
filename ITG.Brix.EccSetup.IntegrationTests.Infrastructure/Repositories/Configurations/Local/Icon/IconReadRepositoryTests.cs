using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations
{
    [TestClass]
    [TestCategory("Integration")]
    public class IconReadRepositoryTests
    {
        private IIconReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitalize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.IconCollectionName);
            _repository = new IconReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var dataPath = "dataPath";
            RepositoryHelper.ForIcon.CreateIcon(id, name, dataPath);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentIconId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentIconId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RepositoryHelper.ForIcon.CreateIcon(Guid.NewGuid(), "name-1", "dataPath");
            RepositoryHelper.ForIcon.CreateIcon(Guid.NewGuid(), "name-2", "dataPath");

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(2);
        }
    }
}
