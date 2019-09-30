using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain.Model.Configurations.External
{
    [TestClass]
    public class LocationTests
    {
        Guid id = Guid.NewGuid();
        string source = "Source";
        string site = "Site";
        string warehouse = "Warehouse";
        string gate = "gate";
        string row = "row";
        string position = "position";
        string type = "type";
        bool isRack = true;

        [TestMethod]
        public void CreateLocationShouldSucceed()
        {
            // Arrange

            // Act 
            var result = new Location(id, source, site, warehouse, gate, row, position, type, isRack);

            // Assert
            result.Source.Should().Be(source);
            result.Site.Should().Be(site);
            result.Warehouse.Should().Be(warehouse);
            result.Gate.Should().Be(gate);
            result.Row.Should().Be(row);
            result.Position.Should().Be(position);
            result.Type.Should().Be(type);
            result.IsRack.Should().BeTrue();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenSourceIsEmpty()
        {
            // Arrange
            var _source = string.Empty;

            // Act
            Action ctor = () => { new Location(id, _source, site, warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenSourceIsNull()
        {
            // Arrange
            string _source = null;

            // Act
            Action ctor = () => { new Location(id, _source, site, warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenSourceIsWhiteSpace()
        {
            // Arrange
            string _source = "    ";

            // Act
            Action ctor = () => { new Location(id, _source, site, warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenSiteIsEmpty()
        {
            // Arrange
            string _site = string.Empty;

            // Act
            Action ctor = () => { new Location(id, source, _site, warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenSiteIsNull()
        {
            // Arrange
            string _site = null;

            // Act
            Action ctor = () => { new Location(id, source, _site, warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenSiteIsWhiteSpace()
        {
            // Arrange
            string _site = "    ";

            // Act
            Action ctor = () => { new Location(id, source, _site, warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenWarehouseIsEmpty()
        {
            // Arrange
            string _warehouse = string.Empty;

            // Act
            Action ctor = () => { new Location(id, source, site, _warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenWarehouseIsNull()
        {
            // Arrange
            string _warehouse = null;

            // Act
            Action ctor = () => { new Location(id, source, site, _warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenWarehouseIsWhiteSpace()
        {
            // Arrange
            string _warehouse = "    ";

            // Act
            Action ctor = () => { new Location(id, source, site, _warehouse, gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenGateIsEmpty()
        {
            // Arrange
            string _gate = string.Empty;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, _gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenGateIsNull()
        {
            // Arrange
            string _gate = null;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, _gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenGateIsWhiteSpace()
        {
            // Arrange
            string _gate = "    ";

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, _gate, row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenRowIsEmpty()
        {
            // Arrange
            string _row = string.Empty;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, _row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenRowIsNull()
        {
            // Arrange
            string _row = null;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, _row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenRowIsWhiteSpace()
        {
            // Arrange
            string _row = "    ";

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, _row, position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenPositionIsEmpty()
        {
            // Arrange
            string _position = string.Empty;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, row, _position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenPositionIsNull()
        {
            // Arrange
            string _position = null;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, row, _position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenPositionIsWhiteSpace()
        {
            // Arrange
            string _position = "    ";

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, row, _position, type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenTypeIsEmpty()
        {
            // Arrange
            string _type = string.Empty;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, row, position, _type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenTypeIsNull()
        {
            // Arrange
            string _type = null;

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, row, position, _type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void CreateLocationShouldFailWhenTypeIsWhiteSpace()
        {
            // Arrange
            string _type = "    ";

            // Act
            Action ctor = () => { new Location(id, source, site, warehouse, gate, row, position, _type, isRack); };

            // Assert
            ctor.Should().Throw<ArgumentException>();
        }
    }
}
