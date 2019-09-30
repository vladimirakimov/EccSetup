using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ScreenElementTests
    {
        Guid id = Guid.NewGuid();
        string name = Guid.NewGuid().ToString();
        string description = "any description";
        Icon icon = new Icon(Guid.NewGuid(), "any name", "any dataPath");
        string image = "any image";

        [TestMethod]
        public void CreateScreenElementShouldSucceed()
        {
            // Act
            var result = new ScreenElement(Guid.NewGuid(), name, description, icon, true, ScreenElementType.Button, image);
            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateScreenElementWithIdEmptyShouldSucceed()
        {
            // Act
            var result = new ScreenElement(id, name, description, icon, true, ScreenElementType.Button, image);
            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateScreenElementShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;

            // Act
            new ScreenElement(id, name, description, icon, true, ScreenElementType.Button, image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenElementShouldFailWhenNameEmpty()
        {
            // Arrange 
            var name = string.Empty;

            // Act
            new ScreenElement(id, name, description, icon, true, ScreenElementType.Button, image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenElementShouldFailWhenNameNull()
        {
            // Arrange 
            string name = null;

            // Act
            new ScreenElement(id, name, description, icon, true, ScreenElementType.Button, image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenElementShouldFailWhenNameIsEmptyAndNoId()
        {
            // Arrange
            var name = string.Empty;

            // Act
            new ScreenElement(id, name, description, icon, true, ScreenElementType.Button, image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenElementShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;

            // Act
            new ScreenElement(id, name, description, icon, true, ScreenElementType.Button, image);
        }
    }
}
