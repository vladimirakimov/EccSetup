using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
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
    public class UpdateOperationCommandValidatorTests
    {
        private UpdateOperationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateOperationCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = new Optional<string>("Operation");
            var description = new Optional<string>("Description");
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new Optional<IEnumerable<string>>(new List<string>() { "tag1", "tag2" });

            var command = new UpdateOperationCommand(id, name, description, icon, tags, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveOperationNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var name = new Optional<string>("Operation");
            var description = new Optional<string>("Description");
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new Optional<IEnumerable<string>>(new List<string>() { "tag1", "tag2" });

            var command = new UpdateOperationCommand(id, name, description, icon, tags, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.OperationNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperationNameCannotBeEmptyValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>(null);
            Optional<string> description = new Optional<string>("Description");
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new Optional<IEnumerable<string>>(new List<string>() { "tag1", "tag2" });
            var version = 0;

            var command = new UpdateOperationCommand(id, name, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperationNameCannotBeEmptyValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>(string.Empty);
            Optional<string> description = new Optional<string>("Description");
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new Optional<IEnumerable<string>>(new List<string>() { "tag1", "tag2" });
            var version = 0;

            var command = new UpdateOperationCommand(id, name, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameCannotBeEmpty));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperationNameCannotBeEmptyValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            Optional<string> name = new Optional<string>("   ");
            Optional<string> description = new Optional<string>("Description");
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new Optional<IEnumerable<string>>(new List<string>() { "tag1", "tag2" });
            var version = 0;

            var command = new UpdateOperationCommand(id, name, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameCannotBeEmpty));

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
            var id = Guid.NewGuid();
            var invalidName = symbols;
            Optional<string> name = new Optional<string>(invalidName);
            Optional<string> description = new Optional<string>("Description");
            var icon = new ColoredIconDto { IconId = Guid.NewGuid(), FillColor = "#FFFFFF" };
            var tags = new Optional<IEnumerable<string>>(new List<string>() { "tag1", "tag2" });
            var version = 0;

            var command = new UpdateOperationCommand(id, name, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.OperationNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
