using FluentAssertions;
using ITG.Brix.EccSetup.API.Context.Bases;
using ITG.Brix.EccSetup.API.Context.Constants;
using ITG.Brix.EccSetup.API.Context.Resources;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Validators.Components.Impl;
using ITG.Brix.EccSetup.Application.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.API.Context.Services.Requests.Validators.Components
{
    [TestClass]
    public class RequestComponentValidatorTests
    {
        [TestMethod]
        public void RouteIdShouldReturnNullWhenIdIsValid()
        {
            // Arrange
            var requestComponentValidator = new RequestComponentValidator();
            var id = Guid.NewGuid().ToString();

            // Act
            var result = requestComponentValidator.RouteId(id);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void RouteIdShouldReturnValidationResultWhenIdIsNotValid()
        {
            // Arrange
            var requestComponentValidator = new RequestComponentValidator();
            var id = "invalid-id";

            // Act
            var result = requestComponentValidator.RouteId(id);

            // Assert
            result.Should().BeOfType<ValidationResult>();
        }

        [TestMethod]
        public void FileMediaExtensionShouldSucceedOnLowercaseExtension()
        {
            //Arrange 
            var requestValidator = new RequestComponentValidator();
            var extension = ".jpeg";

            //Act
            var result = requestValidator.FileMediaExtension(extension);

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void FileMediaExtensionShouldSucceedOnUppercaseExtension()
        {
            //Arrange 
            var requestValidator = new RequestComponentValidator();
            var extension = ".JPG";

            //Act
            var result = requestValidator.FileMediaExtension(extension);

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void FileMediaExtensionShoudFailWhenExtensionIsUnsuported()
        {
            //Arrange 
            var requestValidator = new RequestComponentValidator();
            var extension = ".apk";
            var expectedFailure = new Failure
            {
                Code = Consts.Failure.Detail.Code.InvalidFileExtension,
                Message = RequestFailures.InvalidFileExtension,
                Target = Consts.Failure.Detail.Target.Extension
            };

            //Act
            var result = requestValidator.FileMediaExtension(extension);

            //Assert
            result.Should().BeOfType<ValidationResult>();
            result.Errors.Should().ContainEquivalentOf(expectedFailure);
        }
    }
}
