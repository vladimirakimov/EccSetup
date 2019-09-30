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
    public class InputWriteRepositoryTests
    {
        private IInputWriteRepository _repository;

        Guid inputId = Guid.NewGuid();
        string name = "any name";
        string description = "any description";
        List<Tag> tags = new List<Tag>();
        string instruction = "any instruction";
        List<Image> images = new List<Image>();
        List<Video> videos = new List<Video>();
        Guid icon = Guid.NewGuid();

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.InputCollectionName);
            _repository = new InputWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var instruction = "instruction";
            var input = new Input(id, name, description, icon, instruction);

            var tag = new Tag("tag");
            input.AddTag(tag);

            var image = new Image("image", "url");
            input.AddImage(image);

            var video = new Video("image", "url");
            input.AddVideo(video);

            var inputAttributeName = "any name";
            var inputAttributeNotMandatory = true;
            var inputAttributeScanningOnly = true;
            var inputAttributeMinLenght = new Random().Next();
            var inputAttributeMaxLenght = new Random().Next();
            var inputAttributePrefix = "any prefix";
            var inputAttributeCheckLastXCharacters = new Random().Next();
            var inputAttribute = new InputAttribute(inputAttributeName, inputAttributeNotMandatory, inputAttributeScanningOnly, inputAttributeMinLenght, inputAttributeMaxLenght, inputAttributePrefix, inputAttributeCheckLastXCharacters);
            input.AddInputAttribute(inputAttribute);

            // Act
            await _repository.CreateAsync(input);

            // Assert
            var data = RepositoryHelper.ForInput.GetInputs();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Instruction.Should().Be(instruction);
            result.Tags.Should().HaveCount(1);
            result.Tags.First().Should().Be(tag);
            result.Images.Should().HaveCount(1);
            result.Images.First().Should().Be(image);
            result.Videos.Should().HaveCount(1);
            result.Videos.First().Should().Be(video);
            result.InputAttributes.Should().HaveCount(1);
            result.InputAttributes.First().Should().Be(inputAttribute);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var instruction = "instruction";

            RepositoryHelper.ForInput.CreateInput(id, name, description, icon, instruction);


            var otherId = Guid.NewGuid();
            var otherName = name;
            var otherDescription = "description";
            var otherIcon = Guid.NewGuid();
            var otherInstruction = "instruction";

            var input = new Input(otherId, otherName, otherDescription, otherIcon, otherInstruction);

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
            var icon = Guid.NewGuid();
            var instruction = "instruction";

            var input = RepositoryHelper.ForInput.CreateInput(id, name, description, icon, instruction);

            var tag = new Tag("tag");
            input.AddTag(tag);

            // Act
            await _repository.UpdateAsync(input);

            // Assert
            var data = RepositoryHelper.ForInput.GetInputs();
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
            var icon = Guid.NewGuid();
            var instruction = "instruction";

            RepositoryHelper.ForInput.CreateInput(id, name, description, icon, instruction);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";
            var otherDescription = "description";
            var otherIcon = Guid.NewGuid();
            var otherInstruction = "instruction";

            var other = RepositoryHelper.ForInput.CreateInput(otherId, otherName, otherDescription, otherIcon, otherInstruction);

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
            var icon = Guid.NewGuid();
            var instruction = "instruction";

            RepositoryHelper.ForInput.CreateInput(id, name, description, icon, instruction);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForInput.GetInputs();
            data.Should().HaveCount(0);
        }

    }
}
