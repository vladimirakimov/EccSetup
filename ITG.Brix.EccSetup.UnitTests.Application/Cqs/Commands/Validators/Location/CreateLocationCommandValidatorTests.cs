using FluentAssertions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Validators.Location;
using ITG.Brix.EccSetup.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Commands.Validators.Location
{
    [TestClass]
    public class CreateLocationCommandValidatorTests
    {
        private CreateLocationCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateLocationCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", "type", "true");

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveLocationSourceMandatoryValidationFailureWhenSourceIsNull()
        {
            // Arrange
            string source = null;

            var command = new CreateLocationCommand(source, "site", "warehouse", "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.LocationSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationSourceMandatoryValidationFailureWhenSourceIsEmpty()
        {
            // Arrange
            string source = string.Empty;

            var command = new CreateLocationCommand(source, "site", "warehouse", "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.LocationSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationSourceMandatoryValidationFailureWhenSourceIsWhiteSpace()
        {
            // Arrange
            string source = "    ";

            var command = new CreateLocationCommand(source, "site", "warehouse", "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Source") && a.ErrorMessage.Contains(ValidationFailures.LocationSourceMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationSiteMandatoryValidationFailureWhenSiteIsNull()
        {
            // Arrange
            string site = null;

            var command = new CreateLocationCommand("source", site, "warehouse", "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.LocationSiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocatinSiteMandatoryValidationFailureWhenSiteIsEmpty()
        {
            // Arrange
            string site = string.Empty;

            var command = new CreateLocationCommand("source", site, "warehouse", "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.LocationSiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationSiteMandatoryValidationFailureWhenSiteIsWhiteSpace()
        {
            // Arrange
            string site = "     ";

            var command = new CreateLocationCommand("source", site, "warehouse", "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.LocationSiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationWarehouseMandatoryValidationFailureWhenWarehouseIsNull()
        {
            // Arrange
            string warehouse = null;

            var command = new CreateLocationCommand("source", "site", warehouse, "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Warehouse") && a.ErrorMessage.Contains(ValidationFailures.LocationWarehouseMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationWarehouseMandatoryValidationFailureWhenWarehouseIsEmpty()
        {
            // Arrange
            string warehouse = string.Empty;

            var command = new CreateLocationCommand("source", "site", warehouse, "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Warehouse") && a.ErrorMessage.Contains(ValidationFailures.LocationWarehouseMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationWarehouseMandatoryValidationFailureWhenWarehouseIsWhiteSpace()
        {
            // Arrange
            string warehouse = "    ";

            var command = new CreateLocationCommand("source", "site", warehouse, "gate", "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Warehouse") && a.ErrorMessage.Contains(ValidationFailures.LocationWarehouseMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationGateMandatoryValidationFailureWhenGateIsNull()
        {
            // Arrange
            string gate = null;

            var command = new CreateLocationCommand("source", "site", "warehouse", gate, "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Gate") && a.ErrorMessage.Contains(ValidationFailures.LocationGateMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationGateMandatoryValidationFailureWhenGateIsEmpty()
        {
            // Arrange
            string gate = string.Empty;

            var command = new CreateLocationCommand("source", "site", "warehouse", gate, "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Gate") && a.ErrorMessage.Contains(ValidationFailures.LocationGateMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationGateMandatoryValidationFailureWhenGateIsWhiteSpace()
        {
            // Arrange
            string gate = "    ";

            var command = new CreateLocationCommand("source", "site", "warehouse", gate, "row", "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Gate") && a.ErrorMessage.Contains(ValidationFailures.LocationGateMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationRowMandatoryValidationFailureWhenRowIsNull()
        {
            // Arrange
            string row = null;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", row, "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Row") && a.ErrorMessage.Contains(ValidationFailures.LocationRowMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationRowMandatoryValidationFailureWhenRowIsEmpty()
        {
            // Arrange
            string row = string.Empty;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", row, "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Row") && a.ErrorMessage.Contains(ValidationFailures.LocationRowMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationRowMandatoryValidationFailureWhenRowIsWhiteSpace()
        {
            // Arrange
            string row = "    ";

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", row, "position", "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Row") && a.ErrorMessage.Contains(ValidationFailures.LocationRowMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationPositionMandatoryValidationFailureWhenPositionIsNull()
        {
            // Arrange
            string position = null;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", position, "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Position") && a.ErrorMessage.Contains(ValidationFailures.LocationPositionMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationPositionMandatoryValidationFailureWhenPositionIsEmpty()
        {
            // Arrange
            string position = string.Empty;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", position, "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Position") && a.ErrorMessage.Contains(ValidationFailures.LocationPositionMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationPositionMandatoryValidationFailureWhenPositionIsWhiteSpace()
        {
            // Arrange
            string position = "    ";

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", position, "type", "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Position") && a.ErrorMessage.Contains(ValidationFailures.LocationPositionMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationTypeMandatoryValidationFailureWhenTypeIsNull()
        {
            // Arrange
            string type = null;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", type, "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Type") && a.ErrorMessage.Contains(ValidationFailures.LocationTypeMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationTypeMandatoryValidationFailureWhenTypeIsEmpty()
        {
            // Arrange
            string type = string.Empty;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", type, "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Type") && a.ErrorMessage.Contains(ValidationFailures.LocationTypeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationTypeMandatoryValidationFailureWhenTypeIsWhiteSpace()
        {
            // Arrange
            string type = "    ";

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", type, "isRack");

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Type") && a.ErrorMessage.Contains(ValidationFailures.LocationTypeMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationIsRackMandatoryValidationFailureWhenIsRackIsNull()
        {
            // Arrange
            string isRack = null;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", "type", isRack);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("IsRack") && a.ErrorMessage.Contains(ValidationFailures.LocationIsRackMandatory));

            // Assert
            exists.Should().BeTrue();
        }
        [TestMethod]
        public void ShouldHaveLocationIsRackMandatoryValidationFailureWhenIsRackIsEmpty()
        {
            // Arrange
            string isRack = string.Empty;

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", "type", isRack);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("IsRack") && a.ErrorMessage.Contains(ValidationFailures.LocationIsRackMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationIsRackMandatoryValidationFailureWhenIsRackIsWhiteSpace()
        {
            // Arrange
            string isRack = "    ";

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", "type", isRack);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("IsRack") && a.ErrorMessage.Contains(ValidationFailures.LocationIsRackMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveLocationIsRackNotBooleanValidationFailureWhenIsRackIsNotBoolean()
        {
            // Arrange
            string isRack = "notBoolean";

            var command = new CreateLocationCommand("source", "site", "warehouse", "gate", "row", "position", "type", isRack);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("IsRack") && a.ErrorMessage.Contains(ValidationFailures.LocationIsRackNotBoolean));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
