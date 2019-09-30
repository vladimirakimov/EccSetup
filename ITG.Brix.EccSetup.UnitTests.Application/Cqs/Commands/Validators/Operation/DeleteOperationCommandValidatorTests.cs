using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class DeleteOperationCommandValidatorTests
    {
        private DeleteOperationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new DeleteOperationCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new DeleteOperationCommand(id: Guid.NewGuid(), version: 0);

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
            var command = new DeleteOperationCommand(id: Guid.Empty, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.OperationNotFound));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
