using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class UpdateRemarkCommandValidatorTests
    {
        private UpdateRemarkCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateRemarkCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var nameOnAplication = "any";
            var description = "any";
            var icon = Guid.NewGuid();
            var tags = new List<string>();
            var defaultRemarks = new List<string>();

            var command = new UpdateRemarkCommand(id, name, nameOnAplication, description, icon, tags, defaultRemarks, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveRemarkNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var nameOnAplication = "any";
            var description = "any";
            var icon = Guid.NewGuid();
            var tags = new List<string>();
            var defaultRemarks = new List<string>();

            var command = new UpdateRemarkCommand(Guid.Empty, name, nameOnAplication, description, icon, tags, defaultRemarks, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.RemarkNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var nameOnAplication = "any";
            var description = "any";
            var icon = Guid.NewGuid();
            var tags = new List<string>();
            var defaultRemarks = new List<string>();

            var command = new UpdateRemarkCommand(id, name, nameOnAplication, description, icon, tags, defaultRemarks, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.RemarkNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveNameMandatoryValidationErrorWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnAplication = "any";
            var description = "any";
            var icon = Guid.NewGuid();
            var tags = new List<string>();
            var defaultRemarks = new List<string>();

            var command = new UpdateRemarkCommand(id, name, nameOnAplication, description, icon, tags, defaultRemarks, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.RemarkNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Remark")]
        [DataRow("Remark ")]
        [DataRow(" Remark ")]
        [DataRow("  Remark  ")]
        public void ShouldHaveRemarkNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = symbols;
            var nameOnAplication = "any";
            var description = "any";
            var icon = Guid.NewGuid();
            var tags = new List<string>();
            var defaultRemarks = new List<string>();

            var command = new UpdateRemarkCommand(id, name, nameOnAplication, description, icon, tags, defaultRemarks, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.RemarkNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
