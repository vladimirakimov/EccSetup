using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperationWriteRepositoryTests
    {
        private IOperationWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.OperationCollectionName);
            _repository = new OperationWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var tag1Name = "BSG";
            var tag1 = new Tag(tag1Name);
            var tag2Name = "Chemicals";
            var tag2 = new Tag(tag2Name);

            var coloredIcon = new ColoredIcon(Guid.NewGuid(), "#45545645");
            var operation = new Operation(id, name);

            operation.SetDescription(description);
            operation.SetIcon(coloredIcon);

            operation.AddTag(tag1);
            operation.AddTag(tag2);

            // Act
            await _repository.CreateAsync(operation);

            // Assert
            var data = RepositoryHelper.ForOperation.GetOperations();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(coloredIcon);
            result.Tags.Count.Should().Be(2);
            result.Tags.ElementAt(0).Should().Be(tag1);
            result.Tags.ElementAt(1).Should().Be(tag2);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Load";

            RepositoryHelper.ForOperation.CreateOperation(id, name);

            var otherId = Guid.NewGuid();
            var otherName = name;

            var otherTagName = "BSG";
            var otherTag = new Tag(otherTagName);


            var operation = new Operation(otherId, otherName);


            operation.AddTag(otherTag);

            // Act
            Action act = () => { _repository.CreateAsync(operation).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Load";
            var description = "Description";
            var tag1Name = "BSG";
            var tag1 = new Tag(tag1Name);

            var coloredIcon = new ColoredIcon(Guid.NewGuid(), "#45545645");

            var operation = RepositoryHelper.ForOperation.CreateOperation(id, name);

            operation.SetIcon(coloredIcon);
            operation.SetDescription(description);
            operation.AddTag(tag1);

            // Act
            await _repository.UpdateAsync(operation);

            // Assert
            var data = RepositoryHelper.ForOperation.GetOperations();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Description.Should().Be(description);
            result.Icon.Should().Be(coloredIcon);
            result.Tags.Should().HaveCount(1);
            result.Tags.First().Should().Be(tag1);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";

            RepositoryHelper.ForOperation.CreateOperation(id, name);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";

            var other = RepositoryHelper.ForOperation.CreateOperation(otherId, otherName);

            other.ChangeName(name);

            // Act
            Action act = () => { _repository.UpdateAsync(other).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";

            RepositoryHelper.ForOperation.CreateOperation(id, name);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForOperation.GetOperations();
            data.Should().HaveCount(0);
        }
    }
}
