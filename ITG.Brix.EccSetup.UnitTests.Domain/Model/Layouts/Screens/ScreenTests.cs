using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ScreenTests
    {
        Guid id = Guid.NewGuid();
        string name = "any name";
        string description = "any description";
        string imageUrl = "any url";

        [TestMethod]
        public void CreateScreenShouldSucceed()
        {
            //Act
            var result = new Screen(id, name, description, imageUrl, ScreenType.Button);
            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateScreenNoIdShouldSucceed()
        {
            //Arrange 
            var id = Guid.NewGuid();

            //Act
            var result = new Screen(id, name, description, imageUrl, ScreenType.Button);
            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateScreenShouldFailWhenIdIsGuidEmpty()
        {
            //Arrange 
            var id = Guid.Empty;

            //Act
            new Screen(id, name, description, imageUrl, ScreenType.Button);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenShouldFailWhenNameEmpty()
        {
            //Arrange 
            var name = string.Empty;

            //Act
            new Screen(id, name, description, imageUrl, ScreenType.Button);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenShouldFailWhenNameNull()
        {
            //Arrange 
            string name = null;

            //Act
            new Screen(id, name, description, imageUrl, ScreenType.Button);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenShouldFailWhenNameIsEmptyAndNoId()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;

            //Act
            new Screen(id, name, description, imageUrl, ScreenType.Button);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenShouldFailWhenNameIsNull()
        {
            //Arrange
            string name = null;

            //Act
            new Screen(id, name, description, imageUrl, ScreenType.Button);
        }
    }
}
