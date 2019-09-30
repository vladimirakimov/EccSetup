using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ProductionSiteTests
    {
        [TestMethod]
        public void CreateProductionSiteShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            var name = "Description";
            var source = "Belgium";

            // Act
            var result = new ProductionSite(id, code, name, source);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            var name = string.Empty;
            var source = "Belgium";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
        [TestMethod]
        public void CreateProductionSiteShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            string name = null;
            var source = "Belgium";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenNameIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            string name = "   ";
            var source = "Belgium";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = string.Empty;
            string name = "SiteName";
            var source = "Belgium";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenCodeIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string code = null;
            string name = "SiteName";
            var source = "Belgium";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            string code = "   ";
            string name = "SiteName";
            var source = "Belgium";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            var name = "Description";
            var source = string.Empty;

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenSourceIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            var name = "Description";
            string source = null;

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateProductionSiteShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var code = "Production site";
            var name = "Description";
            string source = "  ";

            // Act
            Action ctor = () => new ProductionSite(id, code, name, source);

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
