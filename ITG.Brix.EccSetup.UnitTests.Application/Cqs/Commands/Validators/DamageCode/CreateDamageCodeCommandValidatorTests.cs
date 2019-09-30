using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators.DamageCode
{
    [TestClass]
    public class CreateDamageCodeCommandValidatorTests
    {
        private CreateDamageCodeCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateDamageCodeCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new CreateDamageCodeCommand("Code", "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeCodeMandatoryValidationFailureWhenCodeIsNull()
        {
            // Arrange
            string code = null;

            var command = new CreateDamageCodeCommand(code, "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeCodeMandatoryValidationFailureWhenCodeIsEmpty()
        {
            // Arrange
            string code = string.Empty;

            var command = new CreateDamageCodeCommand(code, "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeCodeMandatoryValidationFailureWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "     ";

            var command = new CreateDamageCodeCommand(code, "Name", "true", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeSourceMandatoryValidationFailureWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            var command = new CreateDamageCodeCommand("Code", "Name", "true", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeSourceMandatoryValidationFailureWhenSourceIsEmpty()
        {
            // Arrange
            string source = string.Empty;

            var command = new CreateDamageCodeCommand("Code", "Name", "true", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeSourceMandatoryValidationFailureWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "     ";

            var command = new CreateDamageCodeCommand("Code", "Name", "true", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeDamagedQuantityRequiredMandatoryValidationFailureWhenDamagedQuantityRequiredIsNull()
        {
            // Arrange
            string damagedQuantityRequired = null;

            var command = new CreateDamageCodeCommand("Code", "Name", damagedQuantityRequired, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("DamagedQuantityRequired") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeDamagedQuantityRequiredMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeDamagedQuantityRequiredMandatoryValidationFailureWhenDamagedQuantityRequiredIsEmpty()
        {
            // Arrange
            string damagedQuantityRequired = string.Empty;

            var command = new CreateDamageCodeCommand("Code", "Name", damagedQuantityRequired, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("DamagedQuantityRequired") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeDamagedQuantityRequiredMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeDamagedQuantityRequiredMandatoryValidationFailureWhenDamagedQuantityRequiredIsWhiteSpace()
        {
            // Arrange
            string damagedQuantityRequired = "  ";

            var command = new CreateDamageCodeCommand("Code", "Name", damagedQuantityRequired, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("DamagedQuantityRequired") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeDamagedQuantityRequiredMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveDamageCodeDamagedQuantityNotBooleanMandatoryValidationFailureWhenDamagedQuantityRequiredIsNotBoolean()
        {
            // Arrange
            string damagedQuantityRequired = "notBoolean";

            var command = new CreateDamageCodeCommand("Code", "Name", damagedQuantityRequired, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("DamagedQuantityNotBoolean") && a.ErrorMessage.Contains(ValidationFailures.DamageCodeIsNotBoolean));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
