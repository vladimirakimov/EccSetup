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
    public class UpdateInformationCommandValidatorTests
    {
        private UpdateInformationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateInformationCommandValidator();
        }


        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "name";
            string nameOnAplication = "nameOnAplication";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldContainNoErrorsWhenNoTags()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = "name";
            string nameOnAplication = "nameOnAplication";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>();
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveInformationNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            Guid id = Guid.Empty;
            string name = "name";
            string nameOnAplication = "nameOnAplication";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);
            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.InformationNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInformationNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = null;
            string nameOnAplication = "nameOnAplication";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InformationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInformationNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnAplication = "nameOnAplication";
            var description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InformationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInformationTagMandatoryValidationFailureWhenTagsContainsNullValue()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnAplication = "nameOnAplication";
            var description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", null, "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InformationTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInformationTagMandatoryValidationFailureWhenTagsContainsEmptyValue()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnAplication = "nameOnAplication";
            var description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", string.Empty, "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InformationTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInformationTagMandatoryValidationFailureWhenTagsContainsWhitespaceValue()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnAplication = "nameOnAplication";
            var description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "   ", "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InformationTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Information")]
        [DataRow("Information ")]
        [DataRow(" Information ")]
        [DataRow("  Information  ")]
        public void ShouldHaveInformationNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string name = symbols;
            string nameOnAplication = "nameOnAplication";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var version = 0;

            var command = new UpdateInformationCommand(id, name, nameOnAplication, description, icon, tags, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InformationNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
