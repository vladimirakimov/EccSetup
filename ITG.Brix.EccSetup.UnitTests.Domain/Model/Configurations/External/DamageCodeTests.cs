using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain.Model.Configurations.External
{
    [TestClass]
    public class DamageCodeTests
    {
        [TestMethod]
        public void CreateDamageCodeShouldSucceed()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "DamageCodeTest";
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = "code";
            // Act
            var damageCode = new DamageCode(id, code, name, damagedQuantityRequired, source);

            // Assert
            damageCode.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "DamageCodeTest";
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = string.Empty;

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenCodeIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "DamageCodeTest";
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = null;

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "DamageCodeTest";
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = "   ";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenNameIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = string.Empty;
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenNameIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = null;
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenNameIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "   ";
            string source = "Belgium";
            bool damagedQuantityRequired = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "TName";
            string source = string.Empty;
            bool damagedQuantityRequired = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenSourceIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "TName";
            string source = null;
            bool damagedQuantityRequired = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateDamageCodeShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "TName";
            string source = "   ";
            bool damagedQuantityRequired = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new DamageCode(id, code, name, damagedQuantityRequired, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
