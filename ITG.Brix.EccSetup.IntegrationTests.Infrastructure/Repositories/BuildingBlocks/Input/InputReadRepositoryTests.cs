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
    public class InputReadRepositoryTests
    {
        private IInputReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.InputCollectionName);
            _repository = new InputReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "name";
            string description = "description";
            Guid icon = Guid.NewGuid();
            string instruction = "instruction";

            RepositoryHelper.ForInput.CreateInput(id, name, description, icon, instruction);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Instruction.Should().Be(instruction);
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentInstructionId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentInstructionId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            var icon = Guid.NewGuid();
            RepositoryHelper.ForInput.CreateInput(Guid.NewGuid(), "name-1", "description", icon, "instruction");
            RepositoryHelper.ForInput.CreateInput(Guid.NewGuid(), "name-2", "description", icon, "instruction");
            RepositoryHelper.ForInput.CreateInput(Guid.NewGuid(), "name-3", "description", icon, "instruction");

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
