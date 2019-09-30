using FluentAssertions;
using ITG.Brix.EccSetup.Domain.Repositories.OperationalDepartments;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations.External.OperationalDepartment
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperationalDepartmentReadRepositoryTests
    {
        private IOperationalDepartmentReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.OperationalDepartmentCollectionName);
            _repository = new OperationalDepartmentReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            RepositoryHelper.ForOperationalDepartment.CreateOperationalDepartment(Guid.NewGuid(), "code", "name", "site", "source");
            RepositoryHelper.ForOperationalDepartment.CreateOperationalDepartment(Guid.NewGuid(), "code2", "name", "site", "source");
            RepositoryHelper.ForOperationalDepartment.CreateOperationalDepartment(Guid.NewGuid(), "code3", "name", "site", "source");

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }
    }
}
