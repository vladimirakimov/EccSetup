using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class BusinessUnitTests
    {
        [TestMethod]
        public void CreateBusinessUnitShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // Act 
            var result = new BusinessUnit(id, name);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBusinessUnitShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;
            var name = Guid.NewGuid().ToString();

            // Act
            new BusinessUnit(id, name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateBusinessUnitShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;

            // Act
            new BusinessUnit(id, name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateBusinessUnitShouldFailWhenNameIsEmptyAndNoId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;

            // Act
            new BusinessUnit(id, name);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateBusinessUnitShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;

            // Act
            new BusinessUnit(id, name);
        }
    }
}
