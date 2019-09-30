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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.BuildingBlocks
{
    [TestClass]
    [TestCategory("Integration")]
    public class ValidationWriteRepositoryTests
    {
        private IValidationWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.ValidationCollectionName);
            _repository = new ValidationWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIcon(Guid.NewGuid());

            var validation = new Validation(id, name, nameOnApplication, description, instruction, icon);

            var validationAttributeName = "name";
            var validationAttributeScanningOnly = false;
            var validationAttributeMaximumRetries = 1;
            var validationAttributeMinimumLength = 1;
            var validationAttributeMaximumLength = 5;
            var validationAttributePrefix = "prefix";
            var validationAttributeCheckLastXCharacters = 2;
            var validationAttributeBehaviors = new List<Behavior>() { Behavior.BlockOrder, Behavior.InformSupervisor };
            var validationAttribute = new ValidationAttribute(validationAttributeName,
                                                              validationAttributeScanningOnly,
                                                              validationAttributeMaximumRetries,
                                                              validationAttributeMinimumLength,
                                                              validationAttributeMaximumLength,
                                                              validationAttributePrefix,
                                                              validationAttributeCheckLastXCharacters,
                                                              validationAttributeBehaviors);
            validation.AddValidationAttribute(validationAttribute);

            // Act
            await _repository.CreateAsync(validation);

            // Assert
            var data = RepositoryHelper.ForValidation.GetValidations();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Name.Should().Be(name);
            result.NameOnApplication.Should().Be(nameOnApplication);
            result.Description.Should().Be(description);
            result.Instruction.Should().Be(instruction);
            result.Icon.Should().Be(icon);
            result.ValidationAttributes.Should().HaveCount(1);
            (result.ValidationAttributes.First() != validationAttribute).Should().BeTrue();
            result.ValidationAttributes.First().Should().Be(validationAttribute);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIcon(Guid.NewGuid());

            RepositoryHelper.ForValidation.CreateValidation(id, name, nameOnApplication, description, instruction, icon);

            var otherId = Guid.NewGuid();
            var otherName = name;
            var otherNameOnApplication = "nameOnApplication";
            var otherDescription = "description";
            var otherInstruction = "instruction";
            var otherIcon = new BuildingBlockIcon(Guid.NewGuid());

            var remark = new Validation(otherId, otherName, otherNameOnApplication, otherDescription, otherInstruction, otherIcon);

            // Act
            Action act = () => { _repository.CreateAsync(remark).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIcon(Guid.NewGuid());

            var validation = RepositoryHelper.ForValidation.CreateValidation(id, name, nameOnApplication, description, instruction, icon);

            var image = new Image("image", "url");
            validation.AddImage(image);

            // Act
            await _repository.UpdateAsync(validation);

            // Assert
            var data = RepositoryHelper.ForValidation.GetValidations();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Images.Should().HaveCount(1);
            result.Images.First().Should().Be(image);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";
            var nameOnApplication = "nameOnApplication";
            var description = "description";
            var instruction = "instruction";
            var icon = new BuildingBlockIcon(Guid.NewGuid());

            var validation = RepositoryHelper.ForValidation.CreateValidation(id, name, nameOnApplication, description, instruction, icon);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";
            var otherNameOnApplication = "nameOnApplication";
            var otherDescription = "description";
            var otherInstruction = "instruction";
            var otherIcon = new BuildingBlockIcon(Guid.NewGuid());


            var other = RepositoryHelper.ForValidation.CreateValidation(otherId, otherName, otherNameOnApplication, otherDescription, otherInstruction, otherIcon);

            other.ChangeName(name);

            // Act
            Action act = () => { _repository.UpdateAsync(other).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "License plate check on truck";
            var nameOnApplication = "IdCheck";
            var description = "Description";
            var instruction = "Keep";
            var icon = new BuildingBlockIcon(Guid.NewGuid());
            RepositoryHelper.ForValidation.CreateValidation(id, name, nameOnApplication, description, instruction, icon);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForValidation.GetValidations();
            data.Should().HaveCount(0);
        }
    }
}
