using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators.Icon
{
    [TestClass]
    public class CreateIconCommandValidatorTests
    {
        private CreateIconCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateIconCommandValidator();
        }

        [DataTestMethod]
        [DataRow("Name", "DataPath")]
        [DataRow("Name1 SecondPartName", "DataPath")]
        [DataRow("Name1 SecondPartName", "DataPath1 SecondPartDataPath")]
        [DataRow("Name", "DataPath1 DecondpartDataPath")]

        public void ShouldContainNoErrors(string name, string datapath)
        {
            // Arrange
            var command = new CreateIconCommand(name, datapath);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow(null, "DataPath")]
        [DataRow("", "DataPath")]
        [DataRow("   ", "DataPath")]
        public void ShouldHaveIconNameMandatoryValidationFailureWhenNameIsNullOrIsEmptyOrIsWhiteSpace(string name, string datapath)
        {
            // Arrange

            var command = new CreateIconCommand(name, datapath);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.IconNameMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(" Icon")]
        [DataRow("Icon ")]
        [DataRow(" Icon ")]
        [DataRow("  Icon  ")]
        public void ShouldHaveIconNameCannotStartOrEndWithWhiteSpaceValidationErrorWhenNameStartsOrEndsWithWhiteSpace(string symbols)
        {
            // Arrange
            var name = symbols;
            var command = new CreateIconCommand(name, "any");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Any(a => a.PropertyName.Equals("Name") && a.ErrorMessage.Contains(ValidationFailures.IconNameCannotStartOrEndWithWhiteSpace));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
