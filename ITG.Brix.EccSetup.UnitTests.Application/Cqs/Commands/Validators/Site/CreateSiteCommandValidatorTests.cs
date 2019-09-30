using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators.Site
{
    [TestClass]
    public class CreateSiteCommandValidatorTests
    {
        private CreateSiteCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateSiteCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new CreateSiteCommand("Code", "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveSiteCodeMandatoryValidationFailureWhenCodeIsNull()
        {
            // Arrange
            string code = null;

            var command = new CreateSiteCommand(code, "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.SiteCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSiteCodeMandatoryValidationFailureWhenCodeIsEmpty()
        {
            // Arrange
            string code = string.Empty;

            var command = new CreateSiteCommand(code, "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.SiteCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSiteCodeMandatoryValidationFailureWhenCodeIsWhiteSpace()
        {
            // Arrange
            string code = "     ";

            var command = new CreateSiteCommand(code, "Name", "Source");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Code") && a.ErrorMessage.Contains(ValidationFailures.SiteCodeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSiteSourceMandatoryValidationFailureWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            var command = new CreateSiteCommand("Code", "Name", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.SiteSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSiteSourceMandatoryValidationFailureWhenSourceIsEmpty()
        {
            // Arrange
            string source = string.Empty;

            var command = new CreateSiteCommand("Code", "Name", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.SiteSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveSiteSourceMandatoryValidationFailureWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "     ";

            var command = new CreateSiteCommand("Code", "Name", source);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.SiteSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
