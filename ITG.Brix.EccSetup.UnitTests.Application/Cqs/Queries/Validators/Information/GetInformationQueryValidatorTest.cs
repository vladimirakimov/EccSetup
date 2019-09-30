using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Validators
{
    [TestClass]
    public class GetInformationQueryValidatorTest
    {
        private GetInformationQueryValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new GetInformationQueryValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var query = new GetInformationQuery(id: Guid.NewGuid());

            // Act
            var validationResult = _validator.Validate(query);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveInformationNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var query = new GetInformationQuery(id: Guid.Empty);

            // Act
            var validationResult = _validator.Validate(query);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.InformationNotFound));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
