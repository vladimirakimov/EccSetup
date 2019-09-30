using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void CreateQuestionShouldSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var linksTo = Guid.NewGuid();
            var checkListId = Guid.NewGuid();
            // Act
            var result = new Question.QuestionBuilder()
                .WithQuestionId(id)
                .WithChecklistId(checkListId)
                .WithImage("imageUrl")
                .WithIntroduction("introduction")
                .WithLinksTo(linksTo)
                .WithContent("question")
                .WithQuestionType(QuestionType.MultipleChoice)
                .WithRequired(true)
                .WithSequence(true)
                .WithShuffleAnswers(true)
                .WithVideo("videoUrl").Build();
            // Assert
            result.Id.Should().Be(id);
            result.ChecklistId.Should().Be(checkListId);
            result.Image.Should().Be("imageUrl");
            result.Introduction.Should().Be("introduction");
            result.LinksTo.Should().Be(linksTo);
            result.Content.Should().Be("question");
            result.Type.Should().Be(QuestionType.MultipleChoice);
            result.Required.Should().BeTrue();
            result.Sequence.Should().BeTrue();
            result.ShuffleAnswers.Should().BeTrue();
            result.Video.Should().Be("videoUrl");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateQuestionShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange
            var id = Guid.Empty;
            // Act
            var result = new Question.QuestionBuilder().WithQuestionId(id).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateQuestionShouldFailWhenQuestionIsEmpty()
        {
            // Arrange
            var question = string.Empty;
            // Act
            var result = new Question.QuestionBuilder().WithContent(question).Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateQuestionWithIdShouldFailWhenQuestionIsEmpty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var question = string.Empty;
            // Act
            var result = new Question.QuestionBuilder()
                .WithQuestionId(id)
                .WithContent(question).Build();
        }
    }
}
