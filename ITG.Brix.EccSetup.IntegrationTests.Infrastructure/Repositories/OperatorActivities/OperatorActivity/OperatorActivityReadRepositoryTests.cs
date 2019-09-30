using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Providers;
using ITG.Brix.EccSetup.Infrastructure.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.OperatorActivities
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperatorActivityReadRepositoryTests
    {
        private IOperatorActivityReadRepository _readRepository;
        private IMapper mapper;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.OperatorActivityCollectionName);
            var odataProviderMock = new Mock<IOperatorActivityOdataProvider>();
            odataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((object o) => null);
            var odataProvider = odataProviderMock.Object;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
                cfg.AddProfile<ClassToDomainProfile>();
            });
            mapper = new Mapper(config);
            _readRepository = new OperatorActivityReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, mapper);
        }

        [TestMethod]
        public async Task ListShouldReturnAll()
        {
            //Arrange  
            RepositoryHelper.ForOperatorActivity.CreateOperatorActivity(Guid.NewGuid());
            RepositoryHelper.ForOperatorActivity.CreateOperatorActivity(Guid.NewGuid());
            RepositoryHelper.ForOperatorActivity.CreateOperatorActivity(Guid.NewGuid());

            //Act
            var result = await _readRepository.ListAsync(null, null, null);

            //Assert
            result.Should().HaveCount(3);
            result.Should().NotBeNull();

        }
    }
}
