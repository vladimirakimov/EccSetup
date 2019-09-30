using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class TypePlanningReadRepositoryTests
    {
        private TypePlanningReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.TypePlanningCollectionName);
            _repository = new TypePlanningReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            //Arrange
            RepositoryHelper.ForTypePlanning.CreateTypePlanning("code-1", "name", "source");
            RepositoryHelper.ForTypePlanning.CreateTypePlanning("code-2", "name", "source");
            RepositoryHelper.ForTypePlanning.CreateTypePlanning("code-3", "name", "source");
            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
