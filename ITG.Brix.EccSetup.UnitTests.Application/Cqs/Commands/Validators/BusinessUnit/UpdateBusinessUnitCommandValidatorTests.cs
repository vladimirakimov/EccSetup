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
    public class UpdateBusinessUnitCommandValidatorTests
    {
        private UpdateBusinessUnitCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateBusinessUnitCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = new Optional<string>("name");
            var version = 0;

            var command = new UpdateBusinessUnitCommand(id, name, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveBusinessUnitNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var name = new Optional<string>("name");
            var version = 0;

            var command = new UpdateBusinessUnitCommand(id, name, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.BusinessUnitNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveBusinessUnitNameCannotBeEmptyValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.Empty;
            var name = new Optional<string>(null);
            var version = 0;

            var command = new UpdateBusinessUnitCommand(id, name, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.BusinessUnitNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveBusinessUnitNameCannotBeEmptyValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var name = new Optional<string>(string.Empty);
            var version = 0;

            var command = new UpdateBusinessUnitCommand(id, name, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.BusinessUnitNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" BusinessUnitName")]
        [DataRow("BusinessUnitName ")]
        [DataRow(" BusinessUnitName ")]
        [DataRow("  BusinessUnitName  ")]
        public void ShouldHaveBusinessUnitNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var id = Guid.Empty;
            var name = new Optional<string>(symbols);
            var version = 0;

            var command = new UpdateBusinessUnitCommand(id, name, version);

            // Act
            var validationResult = _validator.Validate(command);

            var exists = validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.BusinessUnitNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
