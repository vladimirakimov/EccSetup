using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateStorageStatusCommandValidatorTests
    {
        private CreateStorageStatusCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateStorageStatusCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new CreateStorageStatusCommand("Code", "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusCodeMandatoryValidationFailureWhenCodeIsNull()
        {
            // Arrange
            string code = null;

            var command = new CreateStorageStatusCommand(code, "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusCodeMandatoryValidationFailureWhenCodeIsEmpty()
        {
            // Arrange
            string code = string.Empty;

            var command = new CreateStorageStatusCommand(code, "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusCodeMandatoryValidationFailureWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "     ";

            var command = new CreateStorageStatusCommand(code, "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusSourceMandatoryValidationFailureWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            var command = new CreateStorageStatusCommand("Code", "Name", "true", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusSourceMandatoryValidationFailureWhenSourceIsEmpty()
        {
            // Arrange
            string source = string.Empty;

            var command = new CreateStorageStatusCommand("Code", "Name", "true", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusSourceMandatoryValidationFailureWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "     ";

            var command = new CreateStorageStatusCommand("Code", "Name", "true", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusDefaultMandatoryValidationFailureWhenDefaultIsNull()
        {
            // Arrange
            string @default = null;

            var command = new CreateStorageStatusCommand("Code", "Name", @default, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Default") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusDefaultMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusDefaultMandatoryValidationFailureWhenDefaultIsEmpty()
        {
            // Arrange
            string @default = string.Empty;

            var command = new CreateStorageStatusCommand("Code", "Name", @default, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Default") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusDefaultMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusDefaultMandatoryValidationFailureWhenDefaultIsWhiteSpace()
        {
            // Arrange
            string @default = "  ";

            var command = new CreateStorageStatusCommand("Code", "Name", @default, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Default") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusDefaultMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveStorageStatusDefaultNotBooleanMandatoryValidationFailureWhenDefaultIsNotBoolean()
        {
            // Arrange
            string @default = "notBoolean";

            var command = new CreateStorageStatusCommand("Code", "Name", @default, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Default") && a.ErrorMessage.Contains(ValidationFailures.StorageStatusDefaultNotBoolean));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
