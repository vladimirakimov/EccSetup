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
    public class InstructionWriteRepositoryTests
    {
        private IInstructionWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.InstructionCollectionName);
            _repository = new InstructionWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "any description";
            var icon = "iconUrl";
            var content = "any content";
            var image = "any imageUrl";
            var video = "any videoUrl";
            var instruction = new Instruction(id, name, description, icon, content, image, video);
            var tag = new Tag("tag");
            instruction.AddTag(tag);

            // Act
            await _repository.CreateAsync(instruction);

            // Assert
            var data = RepositoryHelper.ForInstruction.GetInstructions();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Content.Should().Be(content);
            result.Image.Should().Be(image);
            result.Video.Should().Be(video);
            result.Tags.Should().HaveCount(1);
            result.Tags.First().Should().Be(tag);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon";
            var content = "content";
            var image = "image";
            var video = "video";
            var tags = new List<Tag>();

            RepositoryHelper.ForInstruction.CreateInstruction(id, name, description, icon, content, image, video, tags);


            var otherId = Guid.NewGuid();
            var otherName = name;
            var otherDescription = "description";
            var otherIcon = "icon";
            var otherContent = "content";
            var otherImage = "image";
            var otherVideo = "video";

            var input = new Instruction(otherId, otherName, otherDescription, otherIcon, otherContent, otherImage, otherVideo);

            // Act
            Action act = () => { _repository.CreateAsync(input).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon";
            var content = "content";
            var image = "image";
            var video = "video";
            var tags = new List<Tag>();

            var instruction = RepositoryHelper.ForInstruction.CreateInstruction(id, name, description, icon, content, image, video, tags);

            var tag = new Tag("tag");
            instruction.AddTag(tag);

            // Act
            await _repository.UpdateAsync(instruction);

            // Assert
            var data = RepositoryHelper.ForInstruction.GetInstructions();
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
            var description = "description";
            var icon = "icon";
            var content = "content";
            var image = "image";
            var video = "video";
            var tags = new List<Tag>();

            var instruction = RepositoryHelper.ForInstruction.CreateInstruction(id, name, description, icon, content, image, video, tags);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";
            var otherDescription = "description";
            var otherIcon = "icon";
            var otherContent = "content";
            var otherImage = "image";
            var otherVideo = "video";
            var otherTags = new List<Tag>();

            var other = RepositoryHelper.ForInstruction.CreateInstruction(otherId, otherName, otherDescription, otherIcon, otherContent, otherImage, otherVideo, otherTags);

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
            var description = "description";
            var icon = "icon";
            var content = "content";
            var image = "image";
            var video = "video";
            var tags = new List<Tag>();

            RepositoryHelper.ForInstruction.CreateInstruction(id, name, description, icon, content, image, video, tags);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForInstruction.GetInstructions();
            data.Should().HaveCount(0);
        }

    }
}
