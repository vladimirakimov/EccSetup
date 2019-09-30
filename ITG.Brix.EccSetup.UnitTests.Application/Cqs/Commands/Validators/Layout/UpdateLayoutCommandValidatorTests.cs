using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.DataTypes;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class UpdateLayoutCommandValidatorTests
    {
        private UpdateLayoutCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateLayoutCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("Name");
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            var version = 1;

            var command = new UpdateLayoutCommand(id, name, description, image, diagram, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveLayoutNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            Optional<string> name = new Optional<string>("Name");
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            var version = 1;

            var command = new UpdateLayoutCommand(id, name, description, image, diagram, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.LayoutNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLayoutNameCannotBeEmptyValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string nameValue = null;
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            var version = 1;

            var command = new UpdateLayoutCommand(id, name, description, image, diagram, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLayoutNameCannotBeEmptyValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nameValue = string.Empty;
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            var version = 1;

            var command = new UpdateLayoutCommand(id, name, description, image, diagram, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLayoutNameCannotBeEmptyValidationFailureWhenNameIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nameValue = "   ";
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            var version = 1;

            var command = new UpdateLayoutCommand(id, name, description, image, diagram, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Layout")]
        [DataRow("Layout ")]
        [DataRow(" Layout ")]
        [DataRow("  Layout  ")]
        public void ShouldHaveLayoutNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var id = Guid.NewGuid();
            var nameValue = symbols;
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            var version = 1;

            var command = new UpdateLayoutCommand(id, name, description, image, diagram, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
