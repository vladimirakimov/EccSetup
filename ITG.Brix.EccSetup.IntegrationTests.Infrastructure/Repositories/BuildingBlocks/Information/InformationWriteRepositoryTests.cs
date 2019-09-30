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
    public class InformationWriteRepositoryTests
    {
        private IInformationWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.InformationCollectionName);
            _repository = new InformationWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "any";
            var icon = Guid.NewGuid();
            var information = new Information(id, name, "any", description, icon);
            var tag = new Tag("tag");
            information.AddTag(tag);

            // Act
            await _repository.CreateAsync(information);

            // Assert
            var data = RepositoryHelper.ForInformation.GetInformations();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Description.Should().Be(description);
            result.Tags.Should().HaveCount(1);
            result.Tags.First().Should().Be(tag);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "any";
            var icon = Guid.NewGuid();

            RepositoryHelper.ForInformation.CreateInformation(id, name, nameOnApplication, description, icon);


            var otherId = Guid.NewGuid();
            var otherName = name;
            var otherNameOnApplication = "nameOnApplication";
            var otherDescription = "Description";
            var otherIcon = Guid.NewGuid();

            var information = new Information(otherId, otherName, otherNameOnApplication, otherDescription, otherIcon);

            // Act
            Action act = () => { _repository.CreateAsync(information).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "any";
            var icon = Guid.NewGuid();

            var information = RepositoryHelper.ForInformation.CreateInformation(id, name, nameOnApplication, description, icon);

            var tag = new Tag("tag");
            information.AddTag(tag);

            // Act
            await _repository.UpdateAsync(information);

            // Assert
            var data = RepositoryHelper.ForInformation.GetInformations();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Tags.Should().HaveCount(1);
            result.Tags.First().Should().Be(tag);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "any";
            var icon = Guid.NewGuid();

            var information = RepositoryHelper.ForInformation.CreateInformation(id, name, nameOnApplication, description, icon);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";
            var otherNameOnApplication = "nameOnApplication";
            var otherDescription = "Description";
            var otherIcon = Guid.NewGuid();

            var other = RepositoryHelper.ForInformation.CreateInformation(otherId, otherName, otherNameOnApplication, otherDescription, otherIcon);

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
            var nameOnApplication = "nameOnApplication";
            var description = "any";
            var icon = Guid.NewGuid();

            RepositoryHelper.ForInformation.CreateInformation(id, name, nameOnApplication, description, icon);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForInformation.GetInformations();
            data.Should().HaveCount(0);
        }
    }
}
