using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
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
    public class TypePlanningWriteRepositoryTests
    {
        private TypePlanningWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.TypePlanningCollectionName);
            _repository = new TypePlanningWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var code = "TestCode";
            var name = "TestName";
            var source = "TestSource";

            var TypePlanning = new TypePlanning(Guid.NewGuid(), code, name, source);

            //Act
            await _repository.CreateAsync(TypePlanning);
            //Asssert
            var data = RepositoryHelper.ForTypePlanning.GetTypePlannings();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Name.Should().Be(name);
            result.Code.Should().Be(code);
            result.Source.Should().Be(source);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenCodeandNameAndSourceSame()
        {
            //Arrange
            var code = "TestCode";
            var name = "TestName";
            var source = "TestSource";

            var typePlanning = new TypePlanning(Guid.NewGuid(), code, name, source);
            await _repository.CreateAsync(typePlanning);
            var typePlanning2 = new TypePlanning(Guid.NewGuid(), code, name, source);
            //Act
            Action act = () => { _repository.CreateAsync(typePlanning2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task CreateShouldSucceedWhenNameIsSame()
        {
            //Arrange
            var name = "TestName";

            var typePlanning = new TypePlanning(Guid.NewGuid(), "TestCode1", name, "TestSource1");
            await _repository.CreateAsync(typePlanning);
            var typePlanning2 = new TypePlanning(Guid.NewGuid(), "TestCode2", name, "TestSource2");
            //Act
            Action act = () => { _repository.CreateAsync(typePlanning2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().NotThrow();
        }
    }
}
