using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.ClassMaps;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.Configurations
{
    [TestClass]
    [TestCategory("Integration")]
    public class SourceWriteRepositoryTests
    {
        private ISourceWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.SourceCollectionName);
            _repository = new SourceWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }


        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Kalo";
            var description = "SourceDescription";
            var businessUnit1Name = "BSG";
            var businessUnit2Name = "BSG1";
            var source = new Source(id, name, description);
            var sourceBusinessUnit1 = new SourceBusinessUnit(businessUnit1Name);
            var sourceBusinessUnit2 = new SourceBusinessUnit(businessUnit2Name);
            source.AddSourceBusinessUnit(sourceBusinessUnit1);
            source.AddSourceBusinessUnit(sourceBusinessUnit2);

            // Act
            await _repository.CreateAsync(source);

            // Assert
            var data = RepositoryHelper.ForSource.GetSources();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.SourceBusinessUnits.Should().NotBeNull();
            result.SourceBusinessUnits.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Kalo";
            var description = "SourceDescription";
            var businessUnit1Name = "BSG";
            var businessUnit2Name = "BSG1";
            var sourceBusinessUnit1 = new SourceBusinessUnit(businessUnit1Name);
            var sourceBusinessUnit2 = new SourceBusinessUnit(businessUnit2Name);
            var sourceBusinessUnits = new List<SourceBusinessUnit>() { sourceBusinessUnit1, sourceBusinessUnit2 };
            var source = RepositoryHelper.ForSource.CreateSource(id, name, description, sourceBusinessUnits);

            source.RemoveSourceBusinessUnit(sourceBusinessUnit2);

            // Act
            await _repository.UpdateAsync(source);

            // Assert
            var data = RepositoryHelper.ForSource.GetSources();
            data.Should().HaveCount(1);
            var result = data.First();
            result.SourceBusinessUnits.Should().NotBeNull();
            result.SourceBusinessUnits.Count().Should().Be(1);
            result.SourceBusinessUnits.First().Should().Be(sourceBusinessUnit1);
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Kalo";
            var description = "SourceDescription";
            var businessUnit1Name = "BSG";
            var businessUnit2Name = "BSG1";
            var source = new Source(id, name, description);
            var sourceBusinessUnit1 = new SourceBusinessUnit(businessUnit1Name);
            var sourceBusinessUnit2 = new SourceBusinessUnit(businessUnit2Name);
            var sourceBusinessUnits = new List<SourceBusinessUnit>() { sourceBusinessUnit1, sourceBusinessUnit2 };

            RepositoryHelper.ForSource.CreateSource(id, name, description, sourceBusinessUnits);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForSource.GetSources();
            data.Should().HaveCount(0);
        }

    }
}
