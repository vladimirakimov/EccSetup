using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Domain.BuildingBlocks
{
    [TestClass]
    public class InformationTests
    {
        [TestMethod]
        public void CreateShouldSucceed()
        {
            //Act
            var information = new Information(Guid.NewGuid(), Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
            information.AddTag(new Tag("tag"));

            //Assert
            information.Should().NotBeNull();
            information.BlockType.Should().Be(BlockType.InformationPopup);
            information.Tags.Count.Should().Be(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;
            // Act
            new Information(id, Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            // Act
            new Information(Guid.NewGuid(), name, "any", "any", Guid.NewGuid());
        }

        [TestMethod]
        public void AddTagShouldSucceed()
        {
            // Arrange
            var entity = new Information(Guid.NewGuid(), Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
            var tag = new Tag(Guid.NewGuid().ToString());

            // Act
            entity.AddTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Should().Be(tag);
        }

        [TestMethod]
        public void AddExistingTagShouldNotModifyCollection()
        {
            // Arrange
            var entity = new Information(Guid.NewGuid(), Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);

            // Act
            entity.AddTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Should().Be(tag);
        }

        [TestMethod]
        public void RemoveTagShouldSucceed()
        {
            // Arrange
            var entity = new Information(Guid.NewGuid(), Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);

            // Act
            entity.RemoveTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveUnexistingTagShouldPassSilentlyWithoutAnyImpactOnCollection()
        {
            // Arrange
            var entity = new Information(Guid.NewGuid(), Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);
            var tagToRemove = new Tag(Guid.NewGuid().ToString());

            // Act
            entity.RemoveTag(tagToRemove);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Should().Be(tag);
        }

        [TestMethod]
        public void ClearTagsShouldSucceed()
        {
            // Arrange
            var entity = new Information(Guid.NewGuid(), Guid.NewGuid().ToString(), "any", "any", Guid.NewGuid());
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);

            // Act
            entity.ClearTags();

            // Assert
            entity.Tags.Count.Should().Be(0);
        }
    }
}
