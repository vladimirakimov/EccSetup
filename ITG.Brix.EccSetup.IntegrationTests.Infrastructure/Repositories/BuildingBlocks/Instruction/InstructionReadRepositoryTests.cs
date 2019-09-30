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
    public class InstructionReadRepositoryTests
    {
        private IInstructionReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.InstructionCollectionName);
            _repository = new InstructionReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            string description = "description";
            string icon = "iconUrl";
            string content = "content";
            string image = "imageUrl";
            string video = "videoUrl";
            List<Tag> tags = new List<Tag>() { new Tag("tag1"), new Tag("tag2") };

            RepositoryHelper.ForInstruction.CreateInstruction(id, name, description, icon, content, image, video, tags);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Content.Should().Be(content);
            result.Tags.Count.Should().Be(2);
            result.Image.Should().Be(image);
            result.Video.Should().Be(video);
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
            List<Tag> tags = new List<Tag>() { new Tag("tag1"), new Tag("tag2") };
            RepositoryHelper.ForInstruction.CreateInstruction(Guid.NewGuid(), "name-1", "description", "icon", "content", "image", "video", tags);
            RepositoryHelper.ForInstruction.CreateInstruction(Guid.NewGuid(), "name-2", "description", "icon", "content", "image", "video", tags);
            RepositoryHelper.ForInstruction.CreateInstruction(Guid.NewGuid(), "name-3", "description", "icon", "content", "image", "video", tags);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }

    }
}
