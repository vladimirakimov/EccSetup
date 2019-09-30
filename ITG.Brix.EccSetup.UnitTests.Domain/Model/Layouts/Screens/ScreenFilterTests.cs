using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ScreenFilterTests
    {
        Guid id = Guid.NewGuid();
        string workOrderAttribute = "any";
        string workOrderValue = "any";

        [TestMethod]
        public void CreateScreenFilterShouldSucceed()
        {
            // Act
            var id = Guid.NewGuid();
            var result = new ScreenFilter(id, workOrderAttribute, workOrderValue);
            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateScreenFilterWithIdShouldSucceed()
        {
            // Act
            var result = new ScreenFilter(id, workOrderAttribute, workOrderValue);
            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateScreenFilterShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;

            // Act
            var result = new ScreenFilter(id, workOrderAttribute, workOrderValue);
            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenFilterShouldFailWhenWorkOrderAttributeIsNull()
        {
            // Arrange 
            string workOrderAttribute = null;
            // Act
            new ScreenFilter(id, workOrderAttribute, workOrderValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenFilterShouldFailWhenWorkOrderAttributeIsNullAndNoId()
        {
            // Arrange 
            var id = Guid.NewGuid();
            string workOrderAttribute = null;

            // Act
            new ScreenFilter(id, workOrderAttribute, workOrderValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateScreenFilterShouldFailWhenWorkOrderAttributeIsEmpty()
        {
            // Arrange 
            string workOrderAttribute = string.Empty;
            // Act
            new ScreenFilter(id, workOrderAttribute, workOrderValue);
        }
    }
}
