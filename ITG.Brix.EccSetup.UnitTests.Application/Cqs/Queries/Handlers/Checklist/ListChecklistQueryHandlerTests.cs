using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class ListChecklistQueryHandlerTests
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
                new ListChecklistQueryHandler(mapper, checklistReadRepository);
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
                new ListChecklistQueryHandler(mapper, checklistReadRepository);
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
                new ListChecklistQueryHandler(mapper, checklistReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var checklistReadRepositoryMock = new Mock<IChecklistReadRepository>();
            checklistReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Checklist, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Checklist>()));
            var checklistReadRepository = checklistReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<ChecklistModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<ChecklistModel>());
            var mapper = mapperMock.Object;

            var query = new ListChecklistQuery(null, null, null);

            var handler = new ListChecklistQueryHandler(mapper, checklistReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<ChecklistsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var checklistReadRepositoryMock = new Mock<IChecklistReadRepository>();
            checklistReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Checklist, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var checklistReadRepository = checklistReadRepositoryMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<ChecklistModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<ChecklistModel>());
            var mapper = mapperMock.Object;

            var query = new ListChecklistQuery(null, null, null);

            var handler = new ListChecklistQueryHandler(mapper, checklistReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListChecklistFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
