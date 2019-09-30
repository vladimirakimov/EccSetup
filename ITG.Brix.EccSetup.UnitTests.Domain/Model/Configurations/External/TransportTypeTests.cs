using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class TransportTypeTests
    {
        [TestMethod]
        public void CreateTransportTypeShouldSucceed()
        {
            // Arrange
            var code = "Truck";
            var name = "Truck transport";
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            var result = new TransportType(id, code, name, source);

            // Assert
            result.Name.Should().Be(name);
        }



        [TestMethod]
        public void CreateTransportTypeShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var code = "Truck";
            var name = string.Empty;
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenNameIsNull()
        {
            // Arrange
            var code = "Truck";
            string name = null;
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenNameIsWhiteSpace()
        {
            // Arrange
            var code = "Truck";
            string name = "  ";
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            var code = "Truck";
            string name = "Truck Name";
            var source = string.Empty;
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenSourceIsNull()
        {
            // Arrange
            var code = "Truck";
            string name = "Truck Name";
            string source = null;
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            var code = "Truck";
            string name = "Truck Name";
            string source = "  ";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            var code = string.Empty;
            string name = "Truck Name";
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenCodeIsNull()
        {
            // Arrange
            string code = null;
            string name = "Truck Name";
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateTransportTypeShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "   ";
            string name = "Truck Name";
            var source = "Belgium";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TransportType(id, code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
