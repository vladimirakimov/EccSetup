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

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.BuildingBlocks
{
    [TestClass]
    [TestCategory("Integration")]
    public class ChecklistReadRepositoryTests
    {
        private IChecklistReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.ChecklistCollectionName);
            _repository = new ChecklistReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var description = "any description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;

            RepositoryHelper.ForChecklist.CreateChecklist(id, name, description, icon, shuffleQuestions);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.ShuffleQuestions.Should().Be(shuffleQuestions);
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentChecklistId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentChecklistId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            var icon = Guid.NewGuid();
            RepositoryHelper.ForChecklist.CreateChecklist(Guid.NewGuid(), "name-1", "description", icon, false);
            RepositoryHelper.ForChecklist.CreateChecklist(Guid.NewGuid(), "name-2", "description", icon, false);
            RepositoryHelper.ForChecklist.CreateChecklist(Guid.NewGuid(), "name-3", "description", icon, false);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
