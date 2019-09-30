using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class CustomerReadRepositoryTests
    {
        private ICustomerReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.CustomerCollectionName);
            _repository = new CustomerReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RepositoryHelper.ForCustomer.CreateCustomer(Guid.NewGuid(), "login-1", "description", "source");
            RepositoryHelper.ForCustomer.CreateCustomer(Guid.NewGuid(), "login-2", "description", "source");
            RepositoryHelper.ForCustomer.CreateCustomer(Guid.NewGuid(), "login-3", "description", "source");

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
