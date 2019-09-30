using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class OperationalDepartmentTests
    {
        Guid id = Guid.NewGuid();
        string code = "Code";
        string name = "Test";
        string site = "Site";
        string source = "source";
        
        [TestMethod]
        public void CreateOperationalDepartmentShouldSucceed()
        {
            // Arrange
           
            // Act
            var result = new OperationalDepartment(id, code, name, site, source);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            var code = string.Empty;

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenCodeIsNull()
        {
            // Arrange
            string code = null;

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "     ";

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            var source = string.Empty;

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "    ";

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenSiteIsEmpty()
        {
            // Arrange
            var site = string.Empty;

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenSiteIsNull()
        {
            // Arrange
            string site = null;

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOperationalDepartmentShouldFailWhenSiteIsWhiteSpace()
        {
            // Arrange
            string site = "    ";

            // Act
            new OperationalDepartment(id, code, name, site, source);
        }
    }
}
