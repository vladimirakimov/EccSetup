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

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class LayoutWriteRepositoryTests
    {
        private ILayoutWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitalize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.LayoutCollectionName);
            _repository = new LayoutWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var layout = new Layout(id, name);

            // Act
            await _repository.CreateAsync(layout);

            // Assert
            var data = RepositoryHelper.ForLayout.GetLayouts();
            data.Should().HaveCount(1);
            var result = data.First();


            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.Description.Should().Be(null);
            result.Image.Should().Be(null);
            result.Diagram.Should().Be(null);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            RepositoryHelper.ForLayout.CreateLayout(id, name);

            var otherId = Guid.NewGuid();
            var otherName = name;

            var layout = new Layout(otherId, otherName);

            // Act
            Action act = () => { _repository.CreateAsync(layout).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            var layout = RepositoryHelper.ForLayout.CreateLayout(id, name);

            var otherName = "otherName";
            layout.ChangeName(otherName);
            layout.SetDiagram("diagram");

            // Act
            await _repository.UpdateAsync(layout);

            // Assert
            var data = RepositoryHelper.ForLayout.GetLayouts();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Name.Should().Be(otherName);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            RepositoryHelper.ForLayout.CreateLayout(id, name);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";

            var other = RepositoryHelper.ForLayout.CreateLayout(otherId, otherName);

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
            var name = "name";

            RepositoryHelper.ForLayout.CreateLayout(id, name);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForLayout.GetLayouts();
            data.Should().HaveCount(0);
        }
    }
}
