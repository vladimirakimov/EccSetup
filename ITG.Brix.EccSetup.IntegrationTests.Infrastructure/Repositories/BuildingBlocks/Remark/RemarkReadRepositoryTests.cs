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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.BuildingBlocks
{
    [TestClass]
    [TestCategory("Integration")]
    public class RemarkReadRepositoryTests
    {
        private IRemarkReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.RemarkCollectionName);
            _repository = new RemarkReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            string description = "description";
            RemarkIcon icon = new RemarkIcon(Guid.NewGuid());
            List<DefaultRemark> defaultRemarks = new List<DefaultRemark>() { new DefaultRemark("defaultRemark1"), new DefaultRemark("defaultRemark2") };
            List<Tag> tags = new List<Tag>() { new Tag("tag1"), new Tag("tag2") };

            RepositoryHelper.ForRemark.CreateRemark(id, name, nameOnApplication, description, icon, defaultRemarks, tags);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Tags.Should().AllBeOfType<Tag>();
            result.DefaultRemarks.Should().AllBeOfType<DefaultRemark>();
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentRemarkId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentRemarkId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RemarkIcon icon = new RemarkIcon(Guid.NewGuid());
            List<DefaultRemark> defaultRemarks = new List<DefaultRemark>() { new DefaultRemark("defaultRemark1"), new DefaultRemark("defaultRemark2") };
            List<Tag> tags = new List<Tag>() { new Tag("tag1"), new Tag("tag2") };

            RepositoryHelper.ForRemark.CreateRemark(Guid.NewGuid(), "name-1", "nameOnApplication", "description", icon, defaultRemarks, tags);
            RepositoryHelper.ForRemark.CreateRemark(Guid.NewGuid(), "name-2", "nameOnApplication", "description", icon, defaultRemarks, tags);
            RepositoryHelper.ForRemark.CreateRemark(Guid.NewGuid(), "name-3", "nameOnApplication", "description", icon, defaultRemarks, tags);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
