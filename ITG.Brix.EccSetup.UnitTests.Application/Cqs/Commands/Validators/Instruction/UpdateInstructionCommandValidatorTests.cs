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
    public class UpdateInstructionCommandValidatorTests
    {
        private UpdateInstructionCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateInstructionCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

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
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>();
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveInstructionNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.InstructionNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveNameMandatoryValidationFailureWhenNameIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "   ";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveContentMandatoryValidationFailureWhenContentIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = null;
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Content") && a.ErrorMessage.Contains(ValidationFailures.InstructionContentMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveContentMandatoryValidationFailureWhenContentIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = string.Empty;
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Content") && a.ErrorMessage.Contains(ValidationFailures.InstructionContentMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveContentMandatoryValidationFailureWhenContentIsWhiteSpace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = "   ";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Content") && a.ErrorMessage.Contains(ValidationFailures.InstructionContentMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionTagMandatoryValidationFailureWhenTagsContainsNullValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", null, "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InstructionTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionTagMandatoryValidationFailureWhenTagsContainsEmptyValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", string.Empty, "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InstructionTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionTagMandatoryValidationFailureWhenTagsContainsWhiteSpaceValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "   ", "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InstructionTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Instruction")]
        [DataRow("Instruction ")]
        [DataRow(" Instruction ")]
        [DataRow("  Instruction  ")]
        public void ShouldHaveInstructionNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = symbols;
            var description = "description";
            var icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            var content = "content";
            var image = "image";
            var video = "video";
            var version = 0;

            var command = new UpdateInstructionCommand(id, name, description, icon, tags, content, image, video, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
