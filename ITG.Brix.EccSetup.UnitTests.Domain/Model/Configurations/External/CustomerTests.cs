using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class CustomerTests
    {
        Guid id = Guid.NewGuid();
        string code = "Exonn";
        string name = "Test customer";
        string source = "Belgium";

        [TestMethod]
        public void CreateCustomerShouldSucceed()
        {
            // Arrange

            // Act 
            var result = new Customer(id, code, name, source);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateCustomerShouldSucceedWhenNameIsEmpty()
        {
            // Arrange
            var _name = string.Empty;

            // Act
            var result = new Customer(id, code, _name, source);

            // Assert
            result.Name.Should().Be(_name);
        }

        [TestMethod]
        public void CreateCustomerShouldSucceedWhenNameIsNull()
        {
            // Arrange
            string _name = null;

            // Act 
            var result = new Customer(id, code, _name, source);

            // Assert
            result.Name.Should().Be(_name);
        }

        [TestMethod]
        public void CreateCustomerShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            var _code = string.Empty;

            // Act
            Action ctor = () => { new Customer(id, _code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateCustomerShouldFailWhenCodeIsNull()
        {
            // Arrange
            string _code = null;

            // Act
            Action ctor = () => { new Customer(id, _code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateCustomerShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            string _source = string.Empty;

            // Act
            Action ctor = () => { new Customer(id, code, name, _source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateCustomerShouldFailWhenSourceIsNull()
        {
            // Arrange
            string _source = null;

            // Act
            Action ctor = () => { new Customer(id, code, name, _source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateCustomerShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            string _source = "    ";

            // Act
            Action ctor = () => { new Customer(id, code, name, _source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateCustomerShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            string _code = "    ";

            // Act
            Action ctor = () => { new Customer(id, _code, name, source); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateCustomerShouldSuccessWhenNameIsWhiteSpace()
        {
            // Arrange
            string _name = "    ";

            // Act
            var result = new Customer(id, code, _name, source);

            // Assert
            result.Name.Should().Be(_name);
        }
    }
}
