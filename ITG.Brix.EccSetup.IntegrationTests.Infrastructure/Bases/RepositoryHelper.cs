using AutoMapper;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using ITG.Brix.EccSetup.Infrastructure.Providers.Impl;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using ITG.Brix.EccSetup.Infrastructure.Repositories.OperatorActivities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases
{
    public static class RepositoryHelper
    {
        public static class ForBusinessUnit
        {
            public static BusinessUnit CreateBusinessUnit(Guid id, string name)
            {
                // prepare
                var writeRepository = new BusinessUnitWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new BusinessUnitReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var businessUnit = new BusinessUnit(id, name);

                writeRepository.CreateAsync(businessUnit).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<BusinessUnit> GetBusinessUnits()
            {
                var repository = new BusinessUnitReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForOperation
        {
            public static Operation CreateOperation(Guid id, string name)
            {
                // prepare
                var writeRepository = new OperationWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new OperationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var operation = new Operation(id, name);

                writeRepository.CreateAsync(operation).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Operation> GetOperations()
            {
                var repository = new OperationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForSource
        {
            public static Source CreateSource(Guid id, string name, string description, IEnumerable<SourceBusinessUnit> sourceBusinessUnits)
            {
                // prepare
                var writeRepository = new SourceWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new SourceReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var source = new Source(id, name, description);
                foreach (var sourceBusinessUnit in sourceBusinessUnits)
                {
                    source.AddSourceBusinessUnit(sourceBusinessUnit);
                }

                writeRepository.CreateAsync(source).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Source> GetSources()
            {
                var repository = new SourceReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }


        public static class ForIcon
        {
            public static Icon CreateIcon(Guid id, string name, string dataPath)
            {
                // prepare
                var writeRepository = new IconWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new IconReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var icon = new Icon(id, name, dataPath);

                writeRepository.CreateAsync(icon).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Icon> GetIcons()
            {
                var repository = new IconReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForValidation
        {
            public static Validation CreateValidation(Guid id, string name, string nameOnApplication, string description, string instruction, BuildingBlockIcon icon)
            {
                // prepare
                var writeRepository = new ValidationWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new ValidationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var validation = new Validation(id, name, nameOnApplication, description, instruction, icon);

                writeRepository.CreateAsync(validation).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Validation> GetValidations()
            {
                var repository = new ValidationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForRemark
        {
            public static Remark CreateRemark(Guid id, string name, string nameOnApplication, string description, RemarkIcon icon, IEnumerable<DefaultRemark> defaultRemarks, IEnumerable<Tag> tags)
            {
                // prepare
                var writeRepository = new RemarkWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new RemarkReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var remark = new Remark(id, name, nameOnApplication, description, icon);
                foreach (var defaultRemark in defaultRemarks)
                {
                    remark.AddDefaultRemark(defaultRemark);
                }
                foreach (var tag in tags)
                {
                    remark.AddTag(tag);
                }

                writeRepository.CreateAsync(remark).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Remark> GetRemarks()
            {
                var repository = new RemarkReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForInstruction
        {
            public static Instruction CreateInstruction(Guid id, string name, string description, string icon, string content, string image, string video, IEnumerable<Tag> tags)
            {
                // prepare
                var writeRepository = new InstructionWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new InstructionReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var instruction = new Instruction(id, name, description, icon, content, image, video);
                foreach (var tag in tags)
                {
                    instruction.AddTag(tag);
                }

                writeRepository.CreateAsync(instruction).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Instruction> GetInstructions()
            {
                var repository = new InstructionReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForInput
        {
            public static Input CreateInput(Guid id, string name, string description, Guid icon, string instruction)
            {
                // prepare
                var writeRepository = new InputWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new InputReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var input = new Input(id, name, description, icon, instruction);

                writeRepository.CreateAsync(input).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Input> GetInputs()
            {
                var repository = new InputReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForChecklist
        {
            public static Checklist CreateChecklist(Guid id, string name, string description, Guid icon, bool shuffleQuestions)
            {
                // prepare
                var writeRepository = new ChecklistWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new ChecklistReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var checklist = new Checklist(id, name, description, icon, shuffleQuestions);

                writeRepository.CreateAsync(checklist).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Checklist> GetChecklists()
            {
                var repository = new ChecklistReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForCustomer
        {
            public static Customer CreateCustomer(Guid id, string name, string description, string source)
            {
                // prepare
                var writeRepository = new CustomerWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new CustomerReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var customer = new Customer(id, name, description, source);
                IEnumerable<Customer> customers = new List<Customer>() { customer };
                foreach (var item in customers)
                {
                    writeRepository.CreateAsync(item).GetAwaiter().GetResult();
                }
             
                // result
                var result = readRepository.GetAsync(customer.Id).Result;

                return result;
            }

            public static IEnumerable<Customer> GetCustomers()
            {
                var repository = new CustomerReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForStorageStatus
        {
            public static StorageStatus CreateStorageStatus(Guid id, string code, string name, bool @default, string source)
            {
                // prepare
                var writeRepository = new StorageStatusWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new StorageStatusReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var storageStatus = new StorageStatus(id, code, name, @default, source);
                IEnumerable<StorageStatus> damageCodes = new List<StorageStatus>() { storageStatus };
                foreach (var item in damageCodes)
                {
                    writeRepository.CreateAsync(item).GetAwaiter().GetResult();
                }

                // result
                var result = readRepository.GetAsync(storageStatus.Id).Result;

                return result;
            }

            public static IEnumerable<StorageStatus> GetStorageStatus()
            {
                var repository = new StorageStatusReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForDamageCode
        {
            public static DamageCode CreateDamageCode(Guid id, string code, string name, bool DamagedQuantityRequired, string source)
            {
                // prepare
                var writeRepository = new DamageCodeWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new DamageCodeReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var damageCode = new DamageCode(id, code, name, DamagedQuantityRequired, source);
                IEnumerable<DamageCode> damageCodes = new List<DamageCode>() { damageCode };
                foreach (var item in damageCodes)
                {
                    writeRepository.CreateAsync(item).GetAwaiter().GetResult();
                }

                // result
                var result = readRepository.GetAsync(damageCode.Id).Result;

                return result;
            }

            public static IEnumerable<DamageCode> GetDamageCodes()
            {
                var repository = new DamageCodeReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForLocation
        {
            public static Location CreateLocation(Guid id, string source, string site, string warehouse, string gate, string row, string position, string type, bool isRack)
            {
                // prepare
                var odataProvider = new Mock<ILocationOdataProvider>().Object;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DomainToClassProfile>();
                    cfg.AddProfile<ClassToDomainProfile>();
                });
                var mapper = new Mapper(config);
                var writeRepository = new LocationWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), mapper);
                var readRepository = new LocationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, mapper);

                // create
                var location = new Location(id, source, site, warehouse, gate, row, position, type, isRack);
                IEnumerable<Location> locations = new List<Location>() { location };
                foreach (var item in locations)
                {
                    writeRepository.CreateAsync(item).GetAwaiter().GetResult();
                }

                // result

                return location;
            }

            public static IEnumerable<Location> GetLocations()
            {
                var odataProvider = new Mock<ILocationOdataProvider>().Object;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DomainToClassProfile>();
                    cfg.AddProfile<ClassToDomainProfile>();
                });
                var mapper = new Mapper(config);
                var repository = new LocationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, mapper);
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForOperationalDepartment
        {
            public static OperationalDepartment CreateOperationalDepartment(Guid id, string code, string name, string site, string source)
            {
                // prepare
                var writeRepository = new OperationalDepartmentWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new OperationalDepartmentReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var operationalDepartment = new OperationalDepartment(id, code, name, site, source);
                IEnumerable<OperationalDepartment> operationalDepartments = new List<OperationalDepartment>() { operationalDepartment };
                foreach (var item in operationalDepartments)
                {
                    writeRepository.CreateAsync(item).GetAwaiter().GetResult();
                }

                // result
                var result = readRepository.GetAsync(operationalDepartment.Id).Result;

                return result;
            }

            public static IEnumerable<OperationalDepartment> GetOperationalDepartments()
            {
                var repository = new OperationalDepartmentReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForProductionSite
        {
            public static ProductionSite CreateProductionSite(string code, string name, string source)
            {
                // prepare
                var writeRepository = new ProductionSiteWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new ProductionSiteReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var productionSite = new ProductionSite(Guid.NewGuid(), code, name, source);
                IEnumerable<ProductionSite> productionSites = new List<ProductionSite>() { productionSite };

                productionSites.ToList().ForEach(x => writeRepository.CreateAsync(x).GetAwaiter().GetResult());

                // result
                var result = readRepository.GetAsync(productionSite.Id).Result;

                return result;
            }

            public static IEnumerable<ProductionSite> GetProductionSites()
            {
                var repository = new ProductionSiteReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForSite
        {
            public static Site CreateSite(string name, string description, string source)
            {
                // prepare
                var writeRepository = new SiteWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new SiteReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var site = new Site(Guid.NewGuid(), name, description, source);
                IEnumerable<Site> sites = new List<Site>() { site };

                sites.ToList().ForEach(x => writeRepository.CreateAsync(x).GetAwaiter().GetResult());

                // result
                var result = readRepository.GetAsync(site.Id).Result;

                return result;
            }

            public static IEnumerable<Site> GetSites()
            {
                var repository = new SiteReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForTransportType
        {
            public static TransportType CreateTransportType(string code, string name, string source)
            {
                // prepare
                var writeRepository = new TransportTypeWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new TransportTypeReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var transportType = new TransportType(Guid.NewGuid(), code, name, source);
                IEnumerable<TransportType> transportTypes = new List<TransportType>() { transportType };

                transportTypes.ToList().ForEach(x => writeRepository.CreateAsync(x).GetAwaiter().GetResult());

                // result
                var result = readRepository.GetAsync(transportType.Id).Result;

                return result;
            }

            public static IEnumerable<TransportType> GetTransportTypes()
            {
                var repository = new TransportTypeReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForTypePlanning
        {
            public static TypePlanning CreateTypePlanning(string name, string description, string source)
            {
                // prepare
                var writeRepository = new TypePlanningWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new TypePlanningReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var typePlanning = new TypePlanning(Guid.NewGuid(), name, description, source);
                IEnumerable<TypePlanning> typePlannings = new List<TypePlanning>() { typePlanning };

                typePlannings.ToList().ForEach(x => writeRepository.CreateAsync(x).GetAwaiter().GetResult());

                // result
                var result = readRepository.GetAsync(typePlanning.Id).Result;

                return result;
            }

            public static IEnumerable<TypePlanning> GetTypePlannings()
            {
                var repository = new TypePlanningReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForFlow
        {
            public static Flow CreateFlow(Guid id, string name)
            {
                // prepare
                var flowOdataProvider = new Mock<IFlowOdataProvider>().Object;
                var writeRepository = new FlowWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new FlowReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), flowOdataProvider);

                // create
                var flow = new Flow(id, name);

                writeRepository.CreateAsync(flow).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Flow> GetFlows()
            {
                var flowOdataProvider = new Mock<IFlowOdataProvider>().Object;
                var repository = new FlowReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), flowOdataProvider);
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForOperatorActivity
        {
            public static OperatorActivity CreateOperatorActivity(Guid id)
            {              
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DomainToClassProfile>();
                    cfg.AddProfile<ClassToDomainProfile>();
                });
                var mapper = new Mapper(config);
                var odataProvider = new Mock<IOperatorActivityOdataProvider>().Object;
                var writeRepository = new OperatorActivityWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), mapper);
                var readRepository = new OperatorActivityReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, mapper);
                var operatorActivity = new OperatorActivity(id);
                operatorActivity.SetType(BlockType.Instruction);

                writeRepository.CreateAsync(operatorActivity).GetAwaiter().GetResult();

                return operatorActivity;
            }

            public static IEnumerable<OperatorActivity> GetOperatorActivities()
            {
                var odataProvider = new Mock<IOperatorActivityOdataProvider>().Object;
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new ClassToDomainProfile()));
                var mapper = new Mapper(configuration);
                var repository = new OperatorActivityReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, mapper);

                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForInformation
        {
            public static Information CreateInformation(Guid id, string name, string nameOnApplication, string description, Guid icon)
            {
                // prepare
                var writeRepository = new InformationWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new InformationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var information = new Information(id, name, nameOnApplication, description, icon);

                writeRepository.CreateAsync(information).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Information> GetInformations()
            {
                var repository = new InformationReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }

        public static class ForLayout
        {
            public static Layout CreateLayout(Guid id, string name)
            {
                // prepare
                var writeRepository = new LayoutWriteRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var readRepository = new LayoutReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));

                // create
                var layout = new Layout(id, name);

                writeRepository.CreateAsync(layout).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<Layout> GetLayouts()
            {
                var repository = new LayoutReadRepository(new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }
    }
}
