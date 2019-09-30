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
    public class CreateInputCommandValidatorTests
    {
        private CreateInputCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateInputCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            string name = "name";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

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
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>();
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveInputNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);

            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InputNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InputNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputNameMandatoryValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var name = "   ";
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InputNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputTagMandatoryValidationFailureWhenTagsContainsNullValue()
        {
            // Arrange
            var name = string.Empty;
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", null, "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InputTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputTagMandatoryValidationFailureWhenTagsContainsEmptyValue()
        {
            // Arrange
            var name = string.Empty;
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", string.Empty, "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InputTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveInputTagMandatoryValidationFailureWhenTagsContainsWhitespaceValue()
        {
            // Arrange
            var name = string.Empty;
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "  ", "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.InputTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Input")]
        [DataRow("Input ")]
        [DataRow(" Input ")]
        [DataRow("  Input  ")]
        public void ShouldHaveInputNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            string name = symbols;
            string description = "description";
            Guid icon = Guid.NewGuid();
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };
            string instruction = "instruction";
            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateInputCommand(name, description, icon, tags, instruction, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.InputNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
