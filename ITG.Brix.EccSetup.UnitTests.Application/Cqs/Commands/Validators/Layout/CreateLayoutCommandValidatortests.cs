using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateLayoutCommandValidatorTests
    {
        private CreateLayoutCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateLayoutCommandValidator();
        }

        [DataTestMethod]
        [DataRow("description", "image")]
        [DataRow("description", null)]
        [DataRow("description", "")]
        [DataRow("description", "   ")]
        [DataRow(null, "image")]
        [DataRow("", "image")]
        [DataRow("   ", "image")]
        public void ShouldContainNoErrors(string description, string image)
        {
            // Arrange
            var name = "name";
            var command = new CreateLayoutCommand(name, description, image);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveLayoutNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var description = "description";
            var image = "image";
            var command = new CreateLayoutCommand(name, description, image);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLayoutNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var description = "description";
            var image = "image";
            var command = new CreateLayoutCommand(name, description, image);


            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLayoutNameMandatoryValidationFailureWhenNameIsWhiteSpace()
        {
            // Arrange
            var name = "   ";
            var description = "description";
            var image = "image";
            var command = new CreateLayoutCommand(name, description, image);


            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameMandatory));

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
            var name = symbols;
            var description = "description";
            var image = "image";
            var command = new CreateLayoutCommand(name, description, image);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.LayoutNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
