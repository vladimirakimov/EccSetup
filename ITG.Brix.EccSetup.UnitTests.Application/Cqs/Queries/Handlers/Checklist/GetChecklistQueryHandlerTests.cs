using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class GetChecklistQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var checklistReadRepository = new Mock<IChecklistReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetChecklistQueryHandler(mapper, checklistReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var checklistReadRepository = new Mock<IChecklistReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new GetChecklistQueryHandler(mapper, checklistReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenChecklistReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IChecklistReadRepository checklistReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new GetChecklistQueryHandler(mapper, checklistReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "name";
            var description = "description";
            var icon = Guid.NewGuid();
            var shuffleQuestions = false;

            var checklistReadRepositoryMock = new Mock<IChecklistReadRepository>();
            checklistReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new Checklist(id, name, description, icon, shuffleQuestions)));
            var checklistReadRepository = checklistReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ChecklistModel>(It.IsAny<object>())).Returns(new ChecklistModel());
            var mapper = mapperMock.Object;

            var query = new GetChecklistQuery(id);

            var handler = new GetChecklistQueryHandler(mapper, checklistReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<ChecklistModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var checklistReadRepositoryMock = new Mock<IChecklistReadRepository>();
            checklistReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
            var checklistReadRepository = checklistReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ChecklistModel>(It.IsAny<object>())).Returns(new ChecklistModel());
            var mapper = mapperMock.Object;

            var query = new GetChecklistQuery(id);

            var handler = new GetChecklistQueryHandler(mapper, checklistReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name);
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();

            var checklistReadRepositoryMock = new Mock<IChecklistReadRepository>();
            checklistReadRepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
            var checklistReadRepository = checklistReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ChecklistModel>(It.IsAny<object>())).Returns(new ChecklistModel());
            var mapper = mapperMock.Object;

            var query = new GetChecklistQuery(id);

            var handler = new GetChecklistQueryHandler(mapper, checklistReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetChecklistFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
