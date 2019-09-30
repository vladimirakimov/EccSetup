using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class SiteTests
    {
        [TestMethod]
        public void CreateSiteShouldSucceed()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = "Belgium";
            string code = "code";
            // Act
            var site = new Site(id, code, name, source);

            // Assert
            site.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateSiteShouldSucceedWhenNameIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = string.Empty;
            string source = "Belgium";
            string code = "code";
            // Act
            var site = new Site(id, code, name, source);

            // Assert
            site.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateSiteShouldSucceedWhenNameIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = null;
            string source = "Belgium";
            string code = "code";

            // Act
            var site = new Site(id, code, name, source);

            // Assert
            site.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateSiteShouldSucceedWhenNameIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "   ";
            string source = "Belgium";
            string code = "code";

            // Act
            var site = new Site(id, code, name, source);

            // Assert
            site.Name.Should().Be(name);
        }


        [TestMethod]
        public void CreateSiteShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = "Belgium";
            string code = string.Empty;

            // Act
            Action ctor = () => { new Site(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateSiteShouldFailWhenCodeIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = "Belgium";
            string code = null;

            // Act
            Action ctor = () => { new Site(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateSiteShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = "Belgium";
            string code = "  ";

            // Act
            Action ctor = () => { new Site(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateSiteShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = string.Empty;
            string code = "code";

            // Act
            Action ctor = () => { new Site(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateSiteShouldFailWhenSourceIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = null;
            string code = "code";

            // Act
            Action ctor = () => { new Site(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateSiteShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "SiteTest";
            string source = "  ";
            string code = "code";

            // Act
            Action ctor = () => { new Site(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}

