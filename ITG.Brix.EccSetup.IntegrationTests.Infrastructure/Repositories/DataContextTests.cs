using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    public class DataContextTests
    {
        [TestMethod]
        public void CreateDataContextShouldSucceed()
        {
            // Arrange
            var dataContext = new DataContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString));

            // Act
            var database = dataContext.Database;

            // Assert
            database.Should().NotBeNull();
        }
    }
}
