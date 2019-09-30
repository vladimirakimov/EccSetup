using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Repositories.Configurations.External;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class DamageCodeWriteRepositoryTests
    {
        private DamageCodeWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.DamageCodeCollectionName);
            _repository = new DamageCodeWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var code = "TestCode";
            var name = "TestName";
            var source = "TestSource";
            var damagedQuantityRequired = true;

            var damageCode = new DamageCode(Guid.NewGuid(), code, name, damagedQuantityRequired, source);

            //Act
            await _repository.CreateAsync(damageCode);
            //Asssert
            var data = RepositoryHelper.ForDamageCode.GetDamageCodes();
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
            var damagedQuantityRequired = true;

            var damageCode = new DamageCode(Guid.NewGuid(), code, name, damagedQuantityRequired, source);
            await _repository.CreateAsync(damageCode);
            var damageCode2 = new DamageCode(Guid.NewGuid(), code, name, damagedQuantityRequired, source);
            //Act
            Action act = () => { _repository.CreateAsync(damageCode2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task CreateShouldSucceedWhenNameIsSame()
        {
            //Arrange
            var name = "TestName";

            var DamageCode = new DamageCode(Guid.NewGuid(), "TestCode1", name, true, "TestSource1");
            await _repository.CreateAsync(DamageCode);
            var DamageCode2 = new DamageCode(Guid.NewGuid(), "TestCode2", name, true, "TestSource2");
            //Act
            Action act = () => { _repository.CreateAsync(DamageCode2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().NotThrow();
        }
    }
}
