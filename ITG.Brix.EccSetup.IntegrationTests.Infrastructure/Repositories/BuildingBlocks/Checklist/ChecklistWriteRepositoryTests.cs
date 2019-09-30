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

namespace ITG.Brix.EccSetup.IntegrationTests.Infrastructure.Repositories.BuildingBlocks
{
    [TestClass]
    [TestCategory("Integration")]
    public class ChecklistWriteRepositoryTests
    {
        private IChecklistWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init(Consts.Collections.ChecklistCollectionName);
            _repository = new ChecklistWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;
            var tag = new Tag("tag");

            var questionId = Guid.NewGuid();
            var quesitonChecklistId = id;
            var questionContent = "content";
            var questionIntroduction = "introduction";
            var questionImage = "image";
            var questionVideo = "video";
            var questionSequence = true;
            var questionShuffleAnswer = false;
            var questionRequired = false;
            var questionLinksTo = Guid.NewGuid();
            var question = new Question(id, quesitonChecklistId, questionContent, questionIntroduction, questionImage, questionVideo, QuestionType.SingleChoice, questionSequence, questionShuffleAnswer, questionRequired, questionLinksTo);

            var checklist = new Checklist(id, name, description, icon, shuffleQuestions);
            checklist.AddTag(tag);
            checklist.AddQuestion(question);

            // Act
            await _repository.CreateAsync(checklist);

            // Assert
            var data = RepositoryHelper.ForChecklist.GetChecklists();
            data.Should().HaveCount(1);
            var result = data.First();

            result.Name.Should().Be(name);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
            result.ShuffleQuestions.Should().Be(shuffleQuestions);
            result.Tags.Should().NotBeNull();
            result.Tags.Count().Should().Be(1);
            result.Tags.First().Should().Be(tag);

            result.Questions.Should().NotBeNull();
            result.Questions.Count().Should().Be(1);
            result.Questions.First().Should().Be(question);
        }

        [TestMethod]
        public void CreateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;

            RepositoryHelper.ForChecklist.CreateChecklist(id, name, description, icon, shuffleQuestions);


            var otherId = Guid.NewGuid();
            var otherName = name;
            var otherDescription = "Description";
            var otherIcon = Guid.NewGuid();
            var otherShuffleQuestions = false;

            var checklist = new Checklist(otherId, otherName, otherDescription, otherIcon, otherShuffleQuestions);

            // Act
            Action act = () => { _repository.CreateAsync(checklist).GetAwaiter().GetResult(); };

            // Assert
            act.Should().Throw<UniqueKeyException>();
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;

            var checklist = RepositoryHelper.ForChecklist.CreateChecklist(id, name, description, icon, shuffleQuestions);

            var tag = new Tag("tag");
            checklist.AddTag(tag);

            // Act
            await _repository.UpdateAsync(checklist);

            // Assert
            var data = RepositoryHelper.ForChecklist.GetChecklists();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Tags.Should().HaveCount(1);
            result.Tags.First().Should().Be(tag);
        }

        [TestMethod]
        public void UpdateWithAlreadyExistingNameShouldFail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "nameOne";
            var description = "description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;

            var checklist = RepositoryHelper.ForChecklist.CreateChecklist(id, name, description, icon, shuffleQuestions);


            var otherId = Guid.NewGuid();
            var otherName = "nameTwo";
            var otherDescription = "Description";
            var otherIcon = Guid.NewGuid();
            var otherShuffleQuestions = false;

            var other = RepositoryHelper.ForChecklist.CreateChecklist(otherId, otherName, otherDescription, otherIcon, otherShuffleQuestions);

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
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;

            RepositoryHelper.ForChecklist.CreateChecklist(id, name, description, icon, shuffleQuestions);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForChecklist.GetChecklists();
            data.Should().HaveCount(0);
        }
    }
}
