using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateSourceCommandValidatorTests
    {
        private CreateSourceCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateSourceCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var name = "name";
            var description = "description";
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };
            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldContainNoErrorsWhenNoBusinessUnits()
        {
            // Arrange
            var name = "name";
            var description = "description";
            var businessUnits = new List<string>();
            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveSourceNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var description = "description";
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var description = "description";
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceNameMandatoryValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var name = "  ";
            var description = "description";
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceDescriptionMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            var name = "name";
            string description = null;
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Description") && a.ErrorMessage.Contains(ValidationFailures.SourceDescriptionMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceDescriptionMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = "name";
            var description = string.Empty;
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Description") && a.ErrorMessage.Contains(ValidationFailures.SourceDescriptionMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceDescriptionMandatoryValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var name = "name";
            var description = "   ";
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Description") && a.ErrorMessage.Contains(ValidationFailures.SourceDescriptionMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        //[TestMethod]
        //public void ShouldHaveSourceBusinessUnitMandatoryValidationFailureWhenBusinessUnitsContainsNullValue()
        //{
        //    // Arrange
        //    var name = string.Empty;
        //    var description = "description";
        //    var businessUnits = new List<string>() { "Chemicals", null, "Food & Feed" };

        //    var command = new CreateSourceCommand(name, description, businessUnits);

        //    // Act
        //    var validationResult = _validator.Validate(command);
        //    var exists =
        //        validationResult.Errors.Any(
        //            a => a.PropertyName.Contains("BusinessUnits[1]") && a.ErrorMessage.Contains(ValidationFailures.SourceBusinessUnitMandatory));

        //    // Assert
        //    exists.Should().BeTrue();
        //}

        //[TestMethod]
        //public void ShouldHaveSourceBusinessUnitMandatoryValidationFailureWhenBusinessUnitsContainsEmptyValue()
        //{
        //    // Arrange
        //    var name = string.Empty;
        //    var description = "description";
        //    var businessUnits = new List<string>() { "Chemicals", string.Empty, "Food & Feed" };

        //    var command = new CreateSourceCommand(name, description, businessUnits);

        //    // Act
        //    var validationResult = _validator.Validate(command);
        //    var exists =
        //        validationResult.Errors.Any(
        //            a => a.PropertyName.Contains("BusinessUnits[1]") && a.ErrorMessage.Contains(ValidationFailures.SourceBusinessUnitMandatory));

        //    // Assert
        //    exists.Should().BeTrue();
        //}

        [DataTestMethod]
        [DataRow(" Source")]
        [DataRow("Source ")]
        [DataRow(" Source ")]
        [DataRow("  Source  ")]
        public void ShouldHaveSourceNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var name = symbols;
            var description = "description";
            var businessUnits = new List<string>() { "Chemicals", "Food & Feed" };

            var command = new CreateSourceCommand(name, description, businessUnits);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }


    }
}
