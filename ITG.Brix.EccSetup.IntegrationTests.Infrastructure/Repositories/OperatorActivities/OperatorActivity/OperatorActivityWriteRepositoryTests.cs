using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Constants;
using ITG.Brix.EccSetup.Infrastructure.MappingProfiles;
using ITG.Brix.EccSetup.Infrastructure.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.OperatorActivities
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperatorActivityWriteRepositoryTests
    {
        private IOperatorActivityWriteRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToClassProfile>();
                cfg.AddProfile<ClassToDomainProfile>();
            });
            var mapper = new Mapper(config);
            RepositoryTestsHelper.Init(Consts.Collections.OperatorActivityCollectionName);
            _repository = new OperatorActivityWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), mapper);
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            //Arrange
            var id = Guid.NewGuid();
            var operatorActivity = new OperatorActivity(id);
            operatorActivity.SetType(BlockType.Instruction);

            //Act
            await _repository.CreateAsync(operatorActivity);

            //Assert
            var activities = RepositoryHelper.ForOperatorActivity.GetOperatorActivities();
            activities.Should().HaveCount(1);
        }
    }
}
