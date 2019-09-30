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
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.BuildingBlocks
{
    [TestClass]
    [TestCategory("Integration")]
    public class RemarkWriteRepositoryTests
    {
        private IRemarkWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.RemarkCollectionName);
            _repository = new RemarkWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var icon = new RemarkIcon(Guid.NewGuid());
            var defaultRemark = new DefaultRemark("defaultRemark1");
            var defaultRemarks = new List<DefaultRemark>() { defaultRemark };
            var tag = new Tag("tag1");
            var tags = new List<Tag>() { tag };

            var remark = new Remark(id, name, nameOnApplication, description, icon);
            defaultRemarks.ForEach(x => remark.AddDefaultRemark(x));
            tags.ForEach(x => remark.AddTag(x));

            // Act
            await _repository.CreateAsync(remark);

            // Assert
            var data = RepositoryHelper.ForRemark.GetRemarks();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Name.Should().Be(name);
            result.NameOnApplication.Should().Be(nameOnApplication);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);

            result.DefaultRemarks.Should().HaveCount(1);
            result.DefaultRemarks.First().Should().Be(defaultRemark);

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
            var description = "description";
            var icon = new RemarkIcon(Guid.NewGuid());
            var defaultRemark = new DefaultRemark("defaultRemark1");
            var defaultRemarks = new List<DefaultRemark>() { defaultRemark };
            var tag = new Tag("tag1");
            var tags = new List<Tag>() { tag };

            RepositoryHelper.ForRemark.CreateRemark(id, name, nameOnApplication, description, icon, defaultRemarks, tags);


            var otherId = Guid.NewGuid();
            var otherName = name;
            var otherNameOnApplication = "nameOnApplication";
            var otherDescription = "description";
            var otherIcon = new RemarkIcon(Guid.NewGuid());

            var remark = new Remark(otherId, otherName, otherNameOnApplication, otherDescription, otherIcon);

            // Act
            Action act = () => { _repository.CreateAsync(remark).GetAwaiter().GetResult(); };

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
            var description = "description";
            var icon = new RemarkIcon(Guid.NewGuid());
            var defaultRemark = new DefaultRemark("defaultRemark1");
            var defaultRemarks = new List<DefaultRemark>() { defaultRemark };
            var tags = new List<Tag>();

            var remark = RepositoryHelper.ForRemark.CreateRemark(id, name, nameOnApplication, description, icon, defaultRemarks, tags);

            var tag = new Tag("tag1");
            remark.AddTag(tag);

            // Act
            await _repository.UpdateAsync(remark);

            // Assert
            var data = RepositoryHelper.ForRemark.GetRemarks();
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
            var name = "nameOne";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var icon = new RemarkIcon(Guid.NewGuid());
            var defaultRemark = new DefaultRemark("defaultRemark1");
            var defaultRemarks = new List<DefaultRemark>() { defaultRemark };
            var tags = new List<Tag>();

            var remark = RepositoryHelper.ForRemark.CreateRemark(id, name, nameOnApplication, description, icon, defaultRemarks, tags);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";
            var otherNameOnApplication = "nameOnApplication";
            var otherDescription = "description";
            var otherIcon = new RemarkIcon(Guid.NewGuid());
            var otherDefaultRemarks = new List<DefaultRemark>();
            var otherTags = new List<Tag>();

            var other = RepositoryHelper.ForRemark.CreateRemark(otherId, otherName, otherNameOnApplication, otherDescription, otherIcon, defaultRemarks, otherTags);

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
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var icon = new RemarkIcon(Guid.NewGuid());
            var defaultRemark = new DefaultRemark("defaultRemark1");
            var defaultRemarks = new List<DefaultRemark>() { defaultRemark };
            var tags = new List<Tag>();

            var remark = RepositoryHelper.ForRemark.CreateRemark(id, name, nameOnApplication, description, icon, defaultRemarks, tags);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForRemark.GetRemarks();
            data.Should().HaveCount(0);
        }

    }
}
