using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class SourceTests
    {
        [TestMethod]
        public void CreateSourceShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Belgium";
            var description = "Source description";

            // Act
            var result = new Source(id, name, description);

            // Assert
            result.Id.Should().Be(id);
            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
        }

        [TestMethod]
        public void CreateSourceWithIdShouldFailWhenIdIsDefaultGuid()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Source";
            var description = "Source description";

            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(id)}*");
        }

        [TestMethod]
        public void CreateSourceShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var description = "Source description";

            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateSourceShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var description = "Source description";

            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateSourceShouldFailWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "   ";
            var description = "Source description";

            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateSourceShouldFailedWhenDescriptionIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "Source";
            string description = null;


            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(description)}*");
        }


        [TestMethod]
        public void CreateSourceShouldFailWhenDescriptionIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "Name";
            var description = string.Empty;

            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(description)}*");
        }

        [TestMethod]
        public void CreateSourceShouldFailWhenDescriptionIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "Source";
            string description = "   ";

            // Act
            Action ctor = () => { new Source(id, name, description); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(description)}*");
        }
    }
}
