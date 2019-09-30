using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ChecklistTests
    {
        Guid id = Guid.NewGuid();
        string name = Guid.NewGuid().ToString();
        string descriprion = "anyDescription";
        Guid icon = new Guid();
        List<Tag> tags = new List<Tag>();

        [TestMethod]
        public void CreateChecklistShouldSuccess()
        {
            // Arrange

            // Act
            var result = new Checklist(id, name, descriprion, icon, true);
            // Assert
            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(descriprion);
            result.Icon.Should().Be(icon);
        }

        [TestMethod]
        public void CreateChecklistWithNoIdShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = new Checklist(id, name, descriprion, icon, true);
            // Assert
            result.Name.Should().Be(name);
            result.Description.Should().Be(descriprion);
            result.Icon.Should().Be(icon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateChecklistShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;
            // Act
            new Checklist(id, name, descriprion, icon, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateChecklistShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            // Act
            new Checklist(id, name, descriprion, icon, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateChecklistShouldFailWhenNameIsEmptyAndNoId()
        {
            // Arrange
            var name = string.Empty;
            // Act
            new Checklist(id, name, descriprion, icon, true);
        }
    }
}
