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
    public class SiteWriteRepositoryTests
    {
        private ISiteWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.SiteCollectionName);
            _repository = new SiteWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }
        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var code = "TestCode";
            var name = "TestName";
            var source = "TestSource";

            var site = new Site(Guid.NewGuid(), code, name, source);

            //Act
            await _repository.CreateAsync(site);
            //Asssert
            var data = RepositoryHelper.ForSite.GetSites();
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

            var site = new Site(Guid.NewGuid(), code, name, source);
            await _repository.CreateAsync(site);
            var site2 = new Site(Guid.NewGuid(), code, name, source);
            //Act
            Action act = () => { _repository.CreateAsync(site2).GetAwaiter().GetResult(); };
            //Assert
            act.Should().Throw<UniqueKeyException>();
        }
    }
}
