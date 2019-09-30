using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateInstructionCommandValidatorTests
    {
        private CreateInstructionCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateInstructionCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

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
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>();
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveInstructionContentMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionContentMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            string name = string.Empty;
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionContentMandatoryValidationFailureWhenNameIsWhiteSpace()
        {
            // Arrange
            string name = "   ";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionContentMandatoryValidationFailureWhenContentIsNull()
        {
            // Arrange
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = null;
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Content") && a.ErrorMessage.Contains(ValidationFailures.InstructionContentMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionContentMandatoryValidationFailureWhenContentIsEmpty()
        {
            // Arrange
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = string.Empty;
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Content") && a.ErrorMessage.Contains(ValidationFailures.InstructionContentMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInstructionContentMandatoryValidationFailureWhenContentIsWhiteSpace()
        {
            // Arrange
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "   ";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

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
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", null, "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

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
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", string.Empty, "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

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
            string name = "name";
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "   ", "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

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
            string name = symbols;
            string description = "description";
            string icon = "icon Url";
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string content = "content";
            string image = "image";
            string video = "video";

            var command = new CreateInstructionCommand(name, description, icon, tags, content, image, video);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InstructionNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }

    }
}
