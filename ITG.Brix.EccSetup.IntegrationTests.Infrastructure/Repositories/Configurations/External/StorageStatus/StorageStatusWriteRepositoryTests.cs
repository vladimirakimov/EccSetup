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

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations.External
{
    [TestClass]
    [TestCategory("Integration")]
    public class StorageStatusWriteRepositoryTests
    {
        private StorageStatusWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.StorageStatusCollectionName);
            _repository = new StorageStatusWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var code = "TestCode";
            var name = "TestName";
            var source = "TestSource";
            var @default = true;

            var storageStatus = new StorageStatus(Guid.NewGuid(), code, name, @default, source);

            //Act
            await _repository.CreateAsync(storageStatus);
            //Asssert
            var data = RepositoryHelper.ForStorageStatus.GetStorageStatus();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Name.Should().Be(name);
            result.Code.Should().Be(code);
            result.Source.Should().Be(source);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenCodeAndSourceSame()
        {
            //Arrange
            var code = "TestCode";
            var name = "TestName";
            var source = "TestSource";
            var @default = true;

            var StorageStatus = new StorageStatus(Guid.NewGuid(), code, name, @default, source);
            await _repository.CreateAsync(StorageStatus);
            var StorageStatus2 = new StorageStatus(Guid.NewGuid(), code, name, @default, source);
            //Act
            Action act = () => { _repository.CreateAsync(StorageStatus2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task CreateShouldSucceedWhenNameIsSame()
        {
            //Arrange
            var name = "TestName";

            var StorageStatus = new StorageStatus(Guid.NewGuid(), "TestCode1", name, true, "TestSource1");
            await _repository.CreateAsync(StorageStatus);
            var StorageStatus2 = new StorageStatus(Guid.NewGuid(), "TestCode2", name, true, "TestSource2");
            //Act
            Action act = () => { _repository.CreateAsync(StorageStatus2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().NotThrow();
        }
    }
}
