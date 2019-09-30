using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateProductionSiteCommandValidatorTests
    {
        private CreateProductionSiteCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateProductionSiteCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new CreateProductionSiteCommand("Code", "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteCodeMandatoryValidationFailureWhenCodeIsNull()
        {
            // Arrange
            string code = null;

            var command = new CreateProductionSiteCommand(code, "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveProductionSiteCodeMandatoryValidationFailureWhenCodeIsEmpty()
        {
            // Arrange
            string code = string.Empty;

            var command = new CreateProductionSiteCommand(code, "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteSourceMandatoryValidationFailureWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            var command = new CreateProductionSiteCommand("Code", "Name", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteSourceMandatoryValidationFailureWhenSourceIsEmpty()
        {
            // Arrange
            string source = string.Empty;

            var command = new CreateProductionSiteCommand("Code", "Name", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteSourceMandatoryValidationFailureWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "     ";

            var command = new CreateProductionSiteCommand("Code", "Name", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteCodeMandatoryValidationFailureWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "     ";

            var command = new CreateProductionSiteCommand(code, "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteSourceMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;

            var command = new CreateProductionSiteCommand("Code", name, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteSourceMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            string name = string.Empty;

            var command = new CreateProductionSiteCommand("Code", name, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveProductionSiteSourceMandatoryValidationFailureWhenNameIsWhiteSpace()
        {
            // Arrange
            string name = "    ";

            var command = new CreateProductionSiteCommand("Code", name, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.ProductionSiteNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
