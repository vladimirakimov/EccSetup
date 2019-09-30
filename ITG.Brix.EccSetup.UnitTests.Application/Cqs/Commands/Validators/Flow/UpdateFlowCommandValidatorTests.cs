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
    public class UpdateFlowCommandValidatorTests
    {
        private UpdateFlowCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateFlowCommandValidator();
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
            Optional<string> filterContent = new Optional<string>("FilterContent");
            var version = 1;

            var command = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveFlowNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            Optional<string> name = new Optional<string>("Name");
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            Optional<string> filterContent = new Optional<string>("FilterContent");
            var version = 1;

            var command = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.FlowNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveFlowNameCannotBeEmptyValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string nameValue = null;
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            Optional<string> filterContent = new Optional<string>("FilterContent");
            var version = 1;

            var command = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveFlowNameCannotBeEmptyValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nameValue = string.Empty;
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            Optional<string> filterContent = new Optional<string>("FilterContent");
            var version = 1;

            var command = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveFlowNameCannotBeEmptyValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var nameValue = "   ";
            Optional<string> name = new Optional<string>(nameValue);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            Optional<string> filterContent = new Optional<string>("FilterContent");
            var version = 1;

            var command = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameCannotBeEmpty));

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
            var id = Guid.NewGuid();
            var invalidName = symbols;
            Optional<string> name = new Optional<string>(invalidName);
            Optional<string> description = new Optional<string>("Description");
            Optional<string> image = new Optional<string>("Image");
            Optional<string> diagram = new Optional<string>("Diagram");
            Optional<string> filterContent = new Optional<string>("FilterContent");
            var version = 1;

            var command = new UpdateFlowCommand(id, name, description, image, diagram, filterContent, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.FlowNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
