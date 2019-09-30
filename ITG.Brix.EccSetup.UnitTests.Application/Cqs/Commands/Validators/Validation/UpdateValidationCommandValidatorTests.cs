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
    public class UpdateValidationCommandValidatorTests
    {
        private UpdateValidationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new UpdateValidationCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

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
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>();

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveValidationNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.ValidationNotFound));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveValidationNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);

            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.ValidationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveValidationNameMandatoryValidationFailureWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);

            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.ValidationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveValidationNameMandatoryValidationFailureWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = "   ";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);

            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.ValidationNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveValidationTagMandatoryValidationFailureWhenTagsContainsNullValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", null, "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.ValidationTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveValidationTagMandatoryValidationFailureWhenTagsContainsEmptyValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", string.Empty, "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.ValidationTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveValidationTagMandatoryValidationFailureWhenTagsContainsWhitespaceValue()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "   ", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();
            var version = 0;

            var command = new UpdateValidationCommand(id, name, nameOnApplication, description, instruction, icon, tags, images, videos, version);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Contains("Tags[1]") && a.ErrorMessage.Contains(ValidationFailures.ValidationTagMandatory));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
