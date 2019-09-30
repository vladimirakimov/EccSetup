using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain.Model.Configurations.External
{
    [TestClass]
    public class StorageStatusTests
    {
        [TestMethod]
        public void CreateStorageStatusShouldSucceed()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "StorageStatusTest";
            string source = "Belgium";
            bool @default = true;
            string code = "code";
            // Act
            var storageStatus = new StorageStatus(id, code, name, @default, source);

            // Assert
            storageStatus.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "StorageStatusTest";
            string source = "Belgium";
            bool @default = true;
            string code = string.Empty;

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenCodeIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "StorageStatusTest";
            string source = "Belgium";
            bool @default = true;
            string code = null;

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "StorageStatusTest";
            string source = "Belgium";
            bool @default = true;
            string code = "   ";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenNameIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = string.Empty;
            string source = "Belgium";
            bool @default = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenNameIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = null;
            string source = "Belgium";
            bool @default = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenNameIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "   ";
            string source = "Belgium";
            bool @default = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "TName";
            string source = string.Empty;
            bool @default = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenSourceIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "TName";
            string source = null;
            bool @default = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateStorageStatusShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "TName";
            string source = "   ";
            bool @default = true;
            string code = "TCode";

            // Act
            Action ctor = () => { new StorageStatus(id, code, name, @default, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
