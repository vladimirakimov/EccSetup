using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateOperationCommandValidatorTests
    {
        private CreateOperationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateOperationCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var name = "Operation";
            var description = "Description";
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new List<string>() { "tag1", "tag2" };
            var command = new CreateOperationCommand(name, description, icon, tags);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveOperationNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var description = "Description";
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new List<string>() { "tag1", "tag2" };
            var command = new CreateOperationCommand(name, description, icon, tags);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperationNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            var description = "Description";
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new List<string>() { "tag1", "tag2" };
            var command = new CreateOperationCommand(name, description, icon, tags);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Operation")]
        [DataRow("Operation ")]
        [DataRow(" Operation ")]
        [DataRow("  Operation  ")]
        public void ShouldHaveOperationNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var name = symbols;
            var description = "Description";
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new List<string>() { "tag1", "tag2" };

            var command = new CreateOperationCommand(name, description, icon, tags);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
