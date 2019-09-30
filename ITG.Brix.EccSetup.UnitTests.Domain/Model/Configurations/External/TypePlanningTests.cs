using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class TypePlanningTests
    {
        [TestMethod]
        public void CreateTypePlanningShouldSucceed()
        {
            // Arrange
            var code = "TCode";
            var name = "TName";
            var source = "TSource";
            var id = Guid.NewGuid();

            // Act
            var result = new TypePlanning(id, code, name, source);

            // Assert
            result.Name.Should().Be(name);
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var code = "TCode";
            var name = string.Empty;
            var source = "TSource";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenNameIsNull()
        {
            // Arrange
            var code = "TCode";
            string name = null;
            var source = "TSource";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenNameIsWhiteSpace()
        {
            // Arrange
            var code = "TCode";
            string name = "   ";
            var source = "TSource";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            var code = "TCode";
            string name = "TName";
            var source = string.Empty;
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenSourceIsNull()
        {
            // Arrange
            var code = "TCode";
            string name = "TName";
            string source = null;
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            var code = "TCode";
            string name = "TName";
            string source = "  ";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenCodeIsEmpty()
        {
            // Arrange
            string code = string.Empty;
            string name = "TName";
            string source = "TSource";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenCodeIsNull()
        {
            // Arrange
            string code = null;
            string name = "TName";
            string source = "TSource";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CreateTypePlanningShouldFailWhenCodeIsWhiteSpace()
        {
            // Arrange
            var code = "  ";
            string name = "TName";
            string source = "TSource";
            var id = Guid.NewGuid();

            // Act
            Action ctor = () => { new TypePlanning(id, code, name, source); };

            //Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
