using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators.OperationalDepartment;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators.OperationalDepartment
{
    [TestClass]
    public class CreateOperationalDepartmentCommandValidatorTests
    {
        private CreateOperationalDepartmentCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateOperationalDepartmentCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new CreateOperationalDepartmentCommand("Code", "Name", "Site", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveCodeMandatoryValidationFailureWhenCodeIsNull()
        {
            // Arrange
            string code = null;

            var command = new CreateOperationalDepartmentCommand(code, "Name", "Site", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveCodeMandatoryValidationFailureWhenCodeIsEmpty()
        {
            // Arrange
            string code = string.Empty;

            var command = new CreateOperationalDepartmentCommand(code, "Name", "Site", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceMandatoryValidationFailureWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            var command = new CreateOperationalDepartmentCommand("Code", "Name", "Site", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceMandatoryValidationFailureWhenSourceIsEmpty()
        {
            // Arrange
            string source = string.Empty;

            var command = new CreateOperationalDepartmentCommand("Code", "Name", "Site", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceMandatoryValidationFailureWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "     ";

            var command = new CreateOperationalDepartmentCommand("Code", "Name", "Site", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveCodeMandatoryValidationFailureWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "     ";

            var command = new CreateOperationalDepartmentCommand(code, "Name", "Site", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceMandatoryValidationFailureWhenSiteIsNull()
        {
            // Arrange
            string site = null;

            var command = new CreateOperationalDepartmentCommand("Code", "Name", site, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentSiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceMandatoryValidationFailureWhenSiteIsEmpty()
        {
            // Arrange
            string site = string.Empty;

            var command = new CreateOperationalDepartmentCommand("Code", "Name", site, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentSiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSourceMandatoryValidationFailureWhenSiteIsWhiteSpace()
        {
            // Arrange
            string site = "     ";

            var command = new CreateOperationalDepartmentCommand("Code", "Name", site, "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.OperationalDepartmentSiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
