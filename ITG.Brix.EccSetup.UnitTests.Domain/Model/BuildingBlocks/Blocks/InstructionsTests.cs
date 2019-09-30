using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class InstructionsTests
    {
        Guid id = Guid.NewGuid();
        string name = Guid.NewGuid().ToString();
        string description = "any description";
        string icon = "anyUrl";
        List<Tag> tags = new List<Tag>();
        string content = "content";
        string image = "imageUrl";
        string video = "videoUrl";

        [TestMethod]
        public void CreateInstructionShouldSuccess()
        {
            // Arrange

            // Act
            var result = new Instruction(id, name, description, icon, content, image, video);
            // Assert
            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(description);
            result.Image.Should().Be(image);
            result.Content.Should().Be(content);
            result.Video.Should().Be(video);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInstructionShouldFailWhenIdIsGuidEmpty()
        {
            // Act
            var result = new Instruction(Guid.Empty, name, description, icon, content, image, video);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateLayoutShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            // Act
            var result = new Instruction(id, name, description, icon, content, image, video);
        }
    }
}
