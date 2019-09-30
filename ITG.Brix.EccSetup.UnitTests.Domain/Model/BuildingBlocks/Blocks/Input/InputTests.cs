using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class InputTests
    {
        Guid inputId = Guid.NewGuid();
        string name = "any name";
        string description = "any description";
        List<Tag> tags = new List<Tag>();
        string instruction = "any instruction";
        List<Image> images = new List<Image>();
        List<Video> videos = new List<Video>();
        Guid icon = Guid.NewGuid();

        [TestMethod]
        public void CreateShouldSuccess()
        {
            // Arrange

            // Act
            var result = new Input(inputId, name, description, icon, instruction);

            // Assert
            result.Name.Should().Be(name);
            result.Id.Should().Be(inputId);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.Instruction.Should().Be(instruction);
            result.Videos.Should().AllBeOfType(typeof(Video));
            result.Images.Should().AllBeOfType(typeof(Image));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var inputId = Guid.Empty;

            // Act
            new Input(inputId, name, description, icon, instruction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new Input(inputId, name, description, icon, instruction);
        }
    }
}
