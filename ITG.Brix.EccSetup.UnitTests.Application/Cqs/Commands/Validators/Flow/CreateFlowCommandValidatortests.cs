using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateFlowCommandValidatorTests
    {
        private CreateFlowCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateFlowCommandValidator();
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
            var command = new CreateFlowCommand(name, description, image);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveFlowNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var description = "description";
            var image = "image";
            var command = new CreateFlowCommand(name, description, image);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveFlowNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var description = "description";
            var image = "image";
            var command = new CreateFlowCommand(name, description, image);


            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveFlowNameMandatoryValidationFailureWhenNameIsWhiteSpace()
        {
            // Arrange
            var name = "   ";
            var description = "description";
            var image = "image";
            var command = new CreateFlowCommand(name, description, image);


            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Flow")]
        [DataRow("Flow ")]
        [DataRow(" Flow ")]
        [DataRow("  Flow  ")]
        public void ShouldHaveFlowNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var invalidName = symbols;
            var command = new CreateFlowCommand(invalidName, "any", "any");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
