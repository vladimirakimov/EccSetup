using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class OperationTests
    {
        [TestMethod]
        public void CreateOperationShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Operation";
            var description = "Description";
            var icon = new ColoredIcon(new Guid("16c924d0-73da-417a-a110-cd328595243a"), "#0000");

            // Act
            var result = new Operation(id, name);
            result.SetDescription(description);
            result.SetIcon(icon);

            // Assert
            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
        }

        [TestMethod]
        public void CreateOperationShouldFailWhenIdIsDefaultGuid()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Operation";

            // Act
            Action ctor = () => { new Operation(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(id)}*");
        }

        [TestMethod]
        public void CreateOperationShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;

            // Act
            Action ctor = () => { new Operation(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateOperationShouldFailWhenNameEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;

            // Act
            Action ctor = () => { new Operation(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(name)}*");
        }

        #region Tags

        [TestMethod]
        public void AddTagShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Operation";
            var entity = new Operation(id, name);
            var tagName = "custom-tag";
            var tag = new Tag(tagName);

            // Act
            entity.AddTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Name.Should().Be(tagName);
        }

        [TestMethod]
        public void AddExistingTagShouldNotModifyCollection()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Operation";
            var entity = new Operation(id, name);
            var tagName = "custom-tag";
            var tag = new Tag(tagName);
            entity.AddTag(tag);

            // Act
            entity.AddTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Name.Should().Be(tagName);
        }

        [TestMethod]
        public void RemoveTagShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Operation";
            var entity = new Operation(id, name);
            var tagName = "custom-tag";
            var tag = new Tag(tagName);
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
            var id = Guid.NewGuid();
            var name = "Operation";
            var entity = new Operation(id, name);
            var tagName = "custom-tag";
            var tag = new Tag(tagName);
            entity.AddTag(tag);
            var tagToRemove = new Tag("TagToRemove");

            // Act
            entity.RemoveTag(tagToRemove);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Name.Should().Be(tagName);
        }

        [TestMethod]
        public void RemoveTagsShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Operation";
            var entity = new Operation(id, name);
            var tagName = "custom-tag";
            var tag = new Tag(tagName);
            entity.AddTag(tag);

            // Act
            entity.ClearTags();

            // Assert
            entity.Tags.Count.Should().Be(0);
        }

        #endregion
    }
}
