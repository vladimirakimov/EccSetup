using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class FlowTests
    {


        List<Guid> operations = new List<Guid>();
        List<Guid> sources = new List<Guid>();
        List<Guid> sites = new List<Guid>();
        List<Guid> operationalDepartments = new List<Guid>();
        List<Guid> typePlanning = new List<Guid>();
        List<Guid> customers = new List<Guid>();
        List<Guid> productionSites = new List<Guid>();
        List<Guid> transportTypes = new List<Guid>();
        List<BuildingBlock> buildingBlocks = new List<BuildingBlock>();
        List<BuildingBlock> freeActions = new List<BuildingBlock>();

        [TestMethod]
        public void CreateShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var image = "image";
            var diagram = "diagram";

            // Act
            var result = new Flow(id, name);
            result.SetDescription(description);
            result.SetImage(image);
            result.SetDiagram(diagram);

            // Assert
            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(description);
            result.Image.Should().Be(image);
            result.Diagram.Should().Be(diagram);
        }

        [TestMethod]
        public void CreateShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;
            var name = "name";

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(id)}*");
        }

        [TestMethod]
        public void CreateShouldFailWhenNameIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            string name = null;

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        [TestMethod]
        public void CreateShouldFailWhenNameIsWhitespace()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "   ";

            // Act
            Action ctor = () => { new Flow(id, name); };

            // Assert
            ctor.Should().Throw<ArgumentException>().WithMessage($"*{nameof(name)}*");
        }

        //#region Operations
        //[TestMethod]
        //public void AddOperationShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operation = new FlowOperation(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddOperation(operation);

        //    // Assert
        //    entity.Operations.Count.Should().Be(1);
        //    entity.Operations.ElementAt(0).Should().Be(operation);
        //}

        //[TestMethod]
        //public void AddExistingOperationShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operation = new FlowOperation(Guid.NewGuid().ToString());
        //    entity.AddOperation(operation);

        //    // Act
        //    entity.AddOperation(operation);

        //    // Assert
        //    entity.Operations.Count.Should().Be(1);
        //    entity.Operations.ElementAt(0).Should().Be(operation);
        //}

        //[TestMethod]
        //public void RemoveOperationShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operation = new FlowOperation(Guid.NewGuid().ToString());
        //    entity.AddOperation(operation);

        //    // Act
        //    entity.RemoveOperation(operation);

        //    // Assert
        //    entity.Operations.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingOperationShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operation = new FlowOperation(Guid.NewGuid().ToString());
        //    entity.AddOperation(operation);
        //    var operationToRemove = new FlowOperation(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveOperation(operationToRemove);

        //    // Assert
        //    entity.Operations.Count.Should().Be(1);
        //    entity.Operations.ElementAt(0).Should().Be(operation);
        //}

        //[TestMethod]
        //public void ClearOperationsShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operation = new FlowOperation(Guid.NewGuid().ToString());
        //    entity.AddOperation(operation);

        //    // Act
        //    entity.ClearOperations();

        //    // Assert
        //    entity.Operations.Count.Should().Be(0);
        //}
        //#endregion

        //#region Sources
        //[TestMethod]
        //public void AddSourceShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var source = new FlowSource(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddSource(source);

        //    // Assert
        //    entity.Sources.Count.Should().Be(1);
        //    entity.Sources.ElementAt(0).Should().Be(source);
        //}

        //[TestMethod]
        //public void AddExistingSourceShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var source = new FlowSource(Guid.NewGuid().ToString());
        //    entity.AddSource(source);

        //    // Act
        //    entity.AddSource(source);

        //    // Assert
        //    entity.Sources.Count.Should().Be(1);
        //    entity.Sources.ElementAt(0).Should().Be(source);
        //}

        //[TestMethod]
        //public void RemoveSourceShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var source = new FlowSource(Guid.NewGuid().ToString());
        //    entity.AddSource(source);

        //    // Act
        //    entity.RemoveSource(source);

        //    // Assert
        //    entity.Sources.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingSourceShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var source = new FlowSource(Guid.NewGuid().ToString());
        //    entity.AddSource(source);
        //    var sourceToRemove = new FlowSource(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveSource(sourceToRemove);

        //    // Assert
        //    entity.Sources.Count.Should().Be(1);
        //    entity.Sources.ElementAt(0).Should().Be(source);
        //}

        //[TestMethod]
        //public void ClearSourcesShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var source = new FlowSource(Guid.NewGuid().ToString());
        //    entity.AddSource(source);

        //    // Act
        //    entity.ClearSources();

        //    // Assert
        //    entity.Sources.Count.Should().Be(0);
        //}
        //#endregion

        //#region Sites
        //[TestMethod]
        //public void AddSiteShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var site = new FlowSite(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddSite(site);

        //    // Assert
        //    entity.Sites.Count.Should().Be(1);
        //    entity.Sites.ElementAt(0).Should().Be(site);
        //}

        //[TestMethod]
        //public void AddExistingSiteShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var site = new FlowSite(Guid.NewGuid().ToString());
        //    entity.AddSite(site);

        //    // Act
        //    entity.AddSite(site);

        //    // Assert
        //    entity.Sites.Count.Should().Be(1);
        //    entity.Sites.ElementAt(0).Should().Be(site);
        //}

        //[TestMethod]
        //public void RemoveSiteShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var site = new FlowSite(Guid.NewGuid().ToString());
        //    entity.AddSite(site);

        //    // Act
        //    entity.RemoveSite(site);

        //    // Assert
        //    entity.Sites.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingSiteShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var site = new FlowSite(Guid.NewGuid().ToString());
        //    entity.AddSite(site);
        //    var siteToRemove = new FlowSite(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveSite(siteToRemove);

        //    // Assert
        //    entity.Sites.Count.Should().Be(1);
        //    entity.Sites.ElementAt(0).Should().Be(site);
        //}

        //[TestMethod]
        //public void ClearSitesShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var site = new FlowSite(Guid.NewGuid().ToString());
        //    entity.AddSite(site);

        //    // Act
        //    entity.ClearSites();

        //    // Assert
        //    entity.Sites.Count.Should().Be(0);
        //}
        //#endregion

        //#region OperationalDepartments
        //[TestMethod]
        //public void AddOperationalDepartmentShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operationalDepartment = new FlowOperationalDepartment(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddOperationalDepartment(operationalDepartment);

        //    // Assert
        //    entity.OperationalDepartments.Count.Should().Be(1);
        //    entity.OperationalDepartments.ElementAt(0).Should().Be(operationalDepartment);
        //}

        //[TestMethod]
        //public void AddExistingOperationalDepartmentShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operationalDepartment = new FlowOperationalDepartment(Guid.NewGuid().ToString());
        //    entity.AddOperationalDepartment(operationalDepartment);

        //    // Act
        //    entity.AddOperationalDepartment(operationalDepartment);

        //    // Assert
        //    entity.OperationalDepartments.Count.Should().Be(1);
        //    entity.OperationalDepartments.ElementAt(0).Should().Be(operationalDepartment);
        //}

        //[TestMethod]
        //public void RemoveOperationalDepartmentShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operationalDepartment = new FlowOperationalDepartment(Guid.NewGuid().ToString());
        //    entity.AddOperationalDepartment(operationalDepartment);

        //    // Act
        //    entity.RemoveOperationalDepartment(operationalDepartment);

        //    // Assert
        //    entity.OperationalDepartments.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingOperationalDepartmentShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operationalDepartment = new FlowOperationalDepartment(Guid.NewGuid().ToString());
        //    entity.AddOperationalDepartment(operationalDepartment);
        //    var operationalDepartmentToRemove = new FlowOperationalDepartment(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveOperationalDepartment(operationalDepartmentToRemove);

        //    // Assert
        //    entity.OperationalDepartments.Count.Should().Be(1);
        //    entity.OperationalDepartments.ElementAt(0).Should().Be(operationalDepartment);
        //}

        //[TestMethod]
        //public void ClearOperationalDepartmentsShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var operationalDepartment = new FlowOperationalDepartment(Guid.NewGuid().ToString());
        //    entity.AddOperationalDepartment(operationalDepartment);

        //    // Act
        //    entity.ClearOperationalDepartments();

        //    // Assert
        //    entity.OperationalDepartments.Count.Should().Be(0);
        //}
        //#endregion

        //#region TypePlannings
        //[TestMethod]
        //public void AddTypePlanningsShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var typePlanning = new FlowTypePlanning(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddTypePlanning(typePlanning);

        //    // Assert
        //    entity.TypePlannings.Count.Should().Be(1);
        //    entity.TypePlannings.ElementAt(0).Should().Be(typePlanning);
        //}

        //[TestMethod]
        //public void AddExistingTypePlanningShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var typePlanning = new FlowTypePlanning(Guid.NewGuid().ToString());
        //    entity.AddTypePlanning(typePlanning);

        //    // Act
        //    entity.AddTypePlanning(typePlanning);

        //    // Assert
        //    entity.TypePlannings.Count.Should().Be(1);
        //    entity.TypePlannings.ElementAt(0).Should().Be(typePlanning);
        //}

        //[TestMethod]
        //public void RemoveTypePlanningsShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var typePlanning = new FlowTypePlanning(Guid.NewGuid().ToString());
        //    entity.AddTypePlanning(typePlanning);

        //    // Act
        //    entity.RemoveTypePlannings(typePlanning);

        //    // Assert
        //    entity.TypePlannings.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingTypePlanningShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var typePlanning = new FlowTypePlanning(Guid.NewGuid().ToString());
        //    entity.AddTypePlanning(typePlanning);
        //    var typePlanningToRemove = new FlowTypePlanning(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveTypePlannings(typePlanningToRemove);

        //    // Assert
        //    entity.TypePlannings.Count.Should().Be(1);
        //    entity.TypePlannings.ElementAt(0).Should().Be(typePlanning);
        //}

        //[TestMethod]
        //public void CleartypePlanningsShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var typePlanning = new FlowTypePlanning(Guid.NewGuid().ToString());
        //    entity.AddTypePlanning(typePlanning);

        //    // Act
        //    entity.ClearTypePlannings();

        //    // Assert
        //    entity.TypePlannings.Count.Should().Be(0);
        //}
        //#endregion

        //#region Customers
        //[TestMethod]
        //public void AddCustomerShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var customer = new FlowCustomer(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddCustomer(customer);

        //    // Assert
        //    entity.Customers.Count.Should().Be(1);
        //    entity.Customers.ElementAt(0).Should().Be(customer);
        //}

        //[TestMethod]
        //public void AddExistingCustomerShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var customer = new FlowCustomer(Guid.NewGuid().ToString());
        //    entity.AddCustomer(customer);

        //    // Act
        //    entity.AddCustomer(customer);

        //    // Assert
        //    entity.Customers.Count.Should().Be(1);
        //    entity.Customers.ElementAt(0).Should().Be(customer);
        //}

        //[TestMethod]
        //public void RemoveCustomerShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var customer = new FlowCustomer(Guid.NewGuid().ToString());
        //    entity.AddCustomer(customer);

        //    // Act
        //    entity.RemoveCustomer(customer);

        //    // Assert
        //    entity.Customers.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingCustomerShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var customer = new FlowCustomer(Guid.NewGuid().ToString());
        //    entity.AddCustomer(customer);
        //    var customerToRemove = new FlowCustomer(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveCustomer(customerToRemove);

        //    // Assert
        //    entity.Customers.Count.Should().Be(1);
        //    entity.Customers.ElementAt(0).Should().Be(customer);
        //}

        //[TestMethod]
        //public void ClearCustomersShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var customer = new FlowCustomer(Guid.NewGuid().ToString());
        //    entity.AddCustomer(customer);

        //    // Act
        //    entity.ClearCustomers();

        //    // Assert
        //    entity.Customers.Count.Should().Be(0);
        //}
        //#endregion

        //#region ProductionSites
        //[TestMethod]
        //public void AddProductionSiteShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var productionSite = new FlowProductionSite(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddProductionSite(productionSite);

        //    // Assert
        //    entity.ProductionSites.Count.Should().Be(1);
        //    entity.ProductionSites.ElementAt(0).Should().Be(productionSite);
        //}

        //[TestMethod]
        //public void AddExistingProductionSiteShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var productionSite = new FlowProductionSite(Guid.NewGuid().ToString());
        //    entity.AddProductionSite(productionSite);

        //    // Act
        //    entity.AddProductionSite(productionSite);

        //    // Assert
        //    entity.ProductionSites.Count.Should().Be(1);
        //    entity.ProductionSites.ElementAt(0).Should().Be(productionSite);
        //}

        //[TestMethod]
        //public void RemoveProductionSiteShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var productionSite = new FlowProductionSite(Guid.NewGuid().ToString());
        //    entity.AddProductionSite(productionSite);

        //    // Act
        //    entity.RemoveProductionSite(productionSite);

        //    // Assert
        //    entity.ProductionSites.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingProductionSiteShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var productionSite = new FlowProductionSite(Guid.NewGuid().ToString());
        //    entity.AddProductionSite(productionSite);
        //    var productionSiteToRemove = new FlowProductionSite(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveProductionSite(productionSiteToRemove);

        //    // Assert
        //    entity.ProductionSites.Count.Should().Be(1);
        //    entity.ProductionSites.ElementAt(0).Should().Be(productionSite);
        //}

        //[TestMethod]
        //public void ClearProductionSitesShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var productionSite = new FlowProductionSite(Guid.NewGuid().ToString());
        //    entity.AddProductionSite(productionSite);

        //    // Act
        //    entity.ClearProductionSites();

        //    // Assert
        //    entity.ProductionSites.Count.Should().Be(0);
        //}
        //#endregion

        //#region TransportTypes
        //[TestMethod]
        //public void AddTransportTypeShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var transportType = new FlowTransportType(Guid.NewGuid().ToString());

        //    // Act
        //    entity.AddTransportType(transportType);

        //    // Assert
        //    entity.TransportTypes.Count.Should().Be(1);
        //    entity.TransportTypes.ElementAt(0).Should().Be(transportType);
        //}

        //[TestMethod]
        //public void AddExistingTransportTypeShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var transportType = new FlowTransportType(Guid.NewGuid().ToString());
        //    entity.AddTransportType(transportType);

        //    // Act
        //    entity.AddTransportType(transportType);

        //    // Assert
        //    entity.TransportTypes.Count.Should().Be(1);
        //    entity.TransportTypes.ElementAt(0).Should().Be(transportType);
        //}

        //[TestMethod]
        //public void RemoveTransportTypeShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var transportType = new FlowTransportType(Guid.NewGuid().ToString());
        //    entity.AddTransportType(transportType);

        //    // Act
        //    entity.RemoveTransportType(transportType);

        //    // Assert
        //    entity.TransportTypes.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingTransportTypeShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var transportType = new FlowTransportType(Guid.NewGuid().ToString());
        //    entity.AddTransportType(transportType);
        //    var transportTypeToRemove = new FlowTransportType(Guid.NewGuid().ToString());

        //    // Act
        //    entity.RemoveTransportType(transportTypeToRemove);

        //    // Assert
        //    entity.TransportTypes.Count.Should().Be(1);
        //    entity.TransportTypes.ElementAt(0).Should().Be(transportType);
        //}

        //[TestMethod]
        //public void ClearTransportTypeShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var transportType = new FlowTransportType(Guid.NewGuid().ToString());
        //    entity.AddTransportType(transportType);

        //    // Act
        //    entity.ClearTransportTypes();

        //    // Assert
        //    entity.TransportTypes.Count.Should().Be(0);
        //}
        //#endregion

        //#region BuildingBlocks
        //[TestMethod]
        //public void AddBuildingBlockShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));

        //    // Act
        //    entity.AddBuildingBlock(validation);

        //    // Assert
        //    entity.BuildingBlocks.Count.Should().Be(1);
        //    entity.BuildingBlocks.ElementAt(0).Should().Be(validation);
        //}

        //[TestMethod]
        //public void AddExistingBuildingBlockShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddBuildingBlock(validation);

        //    // Act
        //    entity.AddBuildingBlock(validation);

        //    // Assert
        //    entity.BuildingBlocks.Count.Should().Be(1);
        //    entity.BuildingBlocks.ElementAt(0).Should().Be(validation);
        //}

        //[TestMethod]
        //public void RemoveBuildingBlockShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddBuildingBlock(validation);

        //    // Act
        //    entity.RemoveBuildingBlock(validation);

        //    // Assert
        //    entity.BuildingBlocks.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingBuildingBlockShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddBuildingBlock(validation);
        //    var buildingBlockTypeToRemove = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));

        //    // Act
        //    entity.RemoveBuildingBlock(buildingBlockTypeToRemove);

        //    // Assert
        //    entity.BuildingBlocks.Count.Should().Be(1);
        //    entity.BuildingBlocks.ElementAt(0).Should().Be(validation);
        //}

        //[TestMethod]
        //public void ClearBuildingBlockShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddBuildingBlock(validation);

        //    // Act
        //    entity.ClearBuildingBlocks();

        //    // Assert
        //    entity.BuildingBlocks.Count.Should().Be(0);
        //}
        //#endregion

        //#region FreeActions
        //[TestMethod]
        //public void AddFreeActionShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));

        //    // Act
        //    entity.AddFreeAction(validation);

        //    // Assert
        //    entity.FreeActions.Count.Should().Be(1);
        //    entity.FreeActions.ElementAt(0).Should().Be(validation);
        //}

        //[TestMethod]
        //public void AddExistingFreeActionShouldNotModifyCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddFreeAction(validation);

        //    // Act
        //    entity.AddFreeAction(validation);

        //    // Assert
        //    entity.FreeActions.Count.Should().Be(1);
        //    entity.FreeActions.ElementAt(0).Should().Be(validation);
        //}

        //[TestMethod]
        //public void RemoveFreeActionShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddFreeAction(validation);

        //    // Act
        //    entity.RemoveFreeAction(validation);

        //    // Assert
        //    entity.FreeActions.Count.Should().Be(0);
        //}

        //[TestMethod]
        //public void RemoveUnexistingFreeActionShouldPassSilentlyWithoutAnyImpactOnCollection()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddFreeAction(validation);
        //    var freeActionToRemove = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));

        //    // Act
        //    entity.RemoveFreeAction(freeActionToRemove);

        //    // Assert
        //    entity.FreeActions.Count.Should().Be(1);
        //    entity.FreeActions.ElementAt(0).Should().Be(validation);
        //}

        //[TestMethod]
        //public void ClearFreeActionsShouldSucceed()
        //{
        //    // Arrange
        //    var entity = new Flow(id, name, description, imageUrl);
        //    var validation = new Validation(Guid.NewGuid(), "any", "any", "any", "any", new BuildingBlockIcon(Guid.NewGuid()));
        //    entity.AddFreeAction(validation);

        //    // Act
        //    entity.ClearFreeActions();

        //    // Assert
        //    entity.FreeActions.Count.Should().Be(0);
        //}
        //#endregion
    }
}
