using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class ChecklistAnswerTests
    {
        Guid id = Guid.NewGuid();
        string answer = "Test answer";
        string imageUrl = "any url";
        string videoUrl = "any url";
        string action = "any action";
        Guid linksTo = Guid.NewGuid();

        [TestMethod]
        public void CreateChecklistAnswerShouldSucceed()
        {
            // Arrange

            // Act
            var result = new ChecklistAnswer(id, answer, imageUrl, videoUrl, action, linksTo);

            // Assert
            result.Action.Should().Be(action);
            result.Answer.Should().Be(answer);
            result.Image.Should().Be(imageUrl);
            result.LinksTo.Should().Be(linksTo);
            result.QuestionId.Should().Be(id);
            result.Video.Should().Be(videoUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateChecklistAnswerShouldFailWhenNameIsEmpty()
        {
            // Arrange
            string _answer = string.Empty;
            // Act
            var result = new ChecklistAnswer(id, _answer, imageUrl, videoUrl, action, linksTo);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateChecklistAnswerShouldFailWhenNameIsNull()
        {
            // Arrange
            string _answer = null;
            // Act
            var result = new ChecklistAnswer(id, _answer, imageUrl, videoUrl, action, linksTo);
        }
    }
}
