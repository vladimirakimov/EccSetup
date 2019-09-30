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
    public class BusinessUnitWriteRepositoryTests
    {
        private IBusinessUnitWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.BusinessUnitCollectionName);
            _repository = new BusinessUnitWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            var businessUnit = new BusinessUnit(id, name);

            // Act
            await _repository.CreateAsync(businessUnit);

            // Assert
            var data = RepositoryHelper.ForBusinessUnit.GetBusinessUnits();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            RepositoryHelper.ForBusinessUnit.CreateBusinessUnit(id, name);

            var otherId = Guid.NewGuid();
            var otherName = name;

            var businessUnit = new BusinessUnit(id, name);

            // Act
            Action act = () => { _repository.CreateAsync(businessUnit).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            var businessUnit = RepositoryHelper.ForBusinessUnit.CreateBusinessUnit(id, name);

            var otherName = "nameUpdated";
            businessUnit.ChangeName(otherName);

            // Act
            await _repository.UpdateAsync(businessUnit);

            // Assert
            var data = RepositoryHelper.ForBusinessUnit.GetBusinessUnits();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be(otherName);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";

            var businessUnit = RepositoryHelper.ForBusinessUnit.CreateBusinessUnit(id, name);

            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";

            var other = RepositoryHelper.ForBusinessUnit.CreateBusinessUnit(otherId, otherName);

            other.ChangeName(name);

            // Act
            Action act = () => { _repository.UpdateAsync(other).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task DeleteIconShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            var businessUnit = RepositoryHelper.ForBusinessUnit.CreateBusinessUnit(id, name);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForBusinessUnit.GetBusinessUnits();
            data.Should().HaveCount(0);
        }

    }
}
