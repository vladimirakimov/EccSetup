using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class LayoutTests
    {
        [TestMethod]
        public void CreateLayoutShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";

            // Act
            var result = new Layout(id, name);

            // Assert
            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(null);
            result.Image.Should().Be(null);
            result.Diagram.Should().Be(null);
        }

        [TestMethod]
        public void CreateLayoutShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;
            var name = "name";

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(id)}*");
        }

        [TestMethod]
        public void CreateLayoutShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateLayoutShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateLayoutShouldFailWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "   ";

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }
    }
}
