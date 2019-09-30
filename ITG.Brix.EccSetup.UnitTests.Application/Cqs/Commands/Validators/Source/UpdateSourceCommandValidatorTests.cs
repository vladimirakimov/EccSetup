using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.DataTypes;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class UpdateSourceCommandValidatorTests
    {
        private UpdateSourceCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateSourceCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("Name");
            Optional<string> description = new Optional<string>("Description");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>() { "Chemicals", "Food & Feed" });
            var version = 0;
            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

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
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("Name");
            Optional<string> description = new Optional<string>("Description");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveSourceNameCannotBeEmptyValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>(null);
            Optional<string> description = new Optional<string>("Description");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceNameCannotBeEmptyValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>(string.Empty);
            Optional<string> description = new Optional<string>("Description");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceNameCannotBeEmptyValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("   ");
            Optional<string> description = new Optional<string>("Description");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceDescriptionCannotBeEmptyValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>(null);
            Optional<string> description = new Optional<string>(null);
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Description") && a.ErrorMessage.Contains(ValidationFailures.SourceDescriptionCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceDescriptionCannotBeEmptyValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("name");
            Optional<string> description = new Optional<string>(string.Empty);
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Description") && a.ErrorMessage.Contains(ValidationFailures.SourceDescriptionCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceDescriptionCannotBeEmptyValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("name");
            Optional<string> description = new Optional<string>("   ");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Description") && a.ErrorMessage.Contains(ValidationFailures.SourceDescriptionCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        //[TestMethod]
        //public void ShouldHaveSourceBusinessUnitMandatoryValidationFailureWhenBusinessUnitsContainsNullValue()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var name = "name";
        //    var description = "description";
        //    var businessUnits = new List<string>() { "Chemicals", null, "Food & Feed" };
        //    var version = 0;

        //    var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

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
        //    var id = Guid.NewGuid();
        //    var name = "name";
        //    var description = "description";
        //    var businessUnits = new List<string>() { "Chemicals", string.Empty, "Food & Feed" };
        //    var version = 0;

        //    var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

        //    // Act
        //    var validationResult = _validator.Validate(command);
        //    var exists =
        //        validationResult.Errors.Any(
        //            a => a.PropertyName.Contains("BusinessUnits[1]") && a.ErrorMessage.Contains(ValidationFailures.SourceBusinessUnitMandatory));

        //    // Assert
        //    exists.Should().BeTrue();
        //}

        //[TestMethod]
        //public void ShouldHaveSourceBusinessUnitMandatoryValidationFailureWhenBusinessUnitsContainsWhitespaceValue()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var name = "name";
        //    var description = "description";
        //    var businessUnits = new List<string>() { "Chemicals", "   ", "Food & Feed" };
        //    var version = 0;

        //    var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

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
        public void ShouldHaveFlowNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>(symbols);
            Optional<string> description = new Optional<string>("description");
            var businessUnits = new Optional<IEnumerable<string>>(new List<string>());
            var version = 0;

            var command = new UpdateSourceCommand(id, name, description, businessUnits, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.SourceNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
