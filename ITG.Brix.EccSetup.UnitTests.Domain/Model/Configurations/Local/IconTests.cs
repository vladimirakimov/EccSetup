using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class IconTests
    {
        [TestMethod]
        public void CreateIconShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Forklift";
            var dataPath = "45wggfsdfgt643";

            // Act
            var icon = new Icon(id, name, dataPath);

            // Assert
            icon.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateIconWithIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Forklift";
            var dataPath = "45wggfsdfgt643";

            // Act
            var icon = new Icon(id, name, dataPath);

            // Assert
            icon.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIconShouldFailWhenIdIsDefault()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Forklift";
            var dataPath = "45wggfsdfgt643";

            // Act
            new Icon(id, name, dataPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateIconShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var dataPath = "45wggfsdfgt643";

            // Act
            new Icon(id, name, dataPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateiconShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var dataPath = "45wggfsdfgt643";

            // Act
            new Icon(id, name, dataPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateIconShouldFailWhenDataPathIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Forklift";
            var dataPath = string.Empty;

            // Act
            new Icon(id, name, dataPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateiconShouldFailWhenDataPathIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Forklift";
            string dataPath = null;

            // Act
            new Icon(id, name, dataPath);
        }
    }
}
