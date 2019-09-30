using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperationalDepartmentWriteRepositoryTests
    {
        private IOperationalDepartmentWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.OperationalDepartmentCollectionName);
            _repository = new OperationalDepartmentWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var id = Guid.NewGuid();
            var source = "TestSource";
            var site = "TestSite";
            var code = "TestCode";
            var name = "TestName";

            var operationalDepartment = new OperationalDepartment(id, code, name, site, source);

            //Act
            await _repository.CreateAsync(operationalDepartment);
            //Asssert
            var data = RepositoryHelper.ForOperationalDepartment.GetOperationalDepartments();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Code.Should().Be(code);
            result.Id.Should().Be(id);
            result.Source.Should().Be(source);
            result.Name.Should().Be(name);
            result.Site.Should().Be(site);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenCodeAndSourceSame()
        {
            //Arrange
            var id = Guid.NewGuid();
            var source = "TestSource";
            var site = "TestSite";
            var code = "TestCode";
            var name = "TestName";

            var operationalDepartment = new OperationalDepartment(id, code, name, site, source);
            await _repository.CreateAsync(operationalDepartment);
            var operationalDepartment2 = new OperationalDepartment(id, code, name, site, source);
            //Act
            Action act = () => { _repository.CreateAsync(operationalDepartment2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().Throw<UniqueKeyException>();
        }
    }
}
