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
    public class CreateValidationCommandValidatorTests
    {
        private CreateValidationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateValidationCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>();

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveValidationNameMandatoryValidationFailureWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
            var name = string.Empty;
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
            string name = "   ";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", null, "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", string.Empty, "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
            string name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIconDto() { Id = Guid.NewGuid() };
            IEnumerable<string> tags = new List<string>() { "tag1", "   ", "tag2" };

            List<ImageDto> images = new List<ImageDto>();
            List<VideoDto> videos = new List<VideoDto>();

            var command = new CreateValidationCommand(name, nameOnApplication, description, instruction, icon, tags, images, videos);

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
