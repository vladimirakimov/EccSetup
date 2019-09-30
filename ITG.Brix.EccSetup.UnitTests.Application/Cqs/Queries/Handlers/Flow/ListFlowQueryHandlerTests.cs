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
using ITG.Brix.EccSetup.Infrastructure.Providers;
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
    public class ListFlowQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListFlowQueryHandler(mapper, flowReadRepository);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var flowReadRepository = new Mock<IFlowReadRepository>().Object;

            // Act
            Action ctor = () =>
            {
                new ListFlowQueryHandler(mapper, flowReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFlowReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            IFlowReadRepository flowReadRepository = null;

            // Act
            Action ctor = () =>
            {
                new ListFlowQueryHandler(mapper, flowReadRepository);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<FlowModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<FlowModel>());
            var mapper = mapperMock.Object;

            var flowReadRepositoryMock = new Mock<IFlowReadRepository>();
            flowReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Flow, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Flow>()));
            var flowReadRepository = flowReadRepositoryMock.Object;

            var flowOdataProviderMock = new Mock<IFlowOdataProvider>();
            flowOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Flow, bool>>)null);
            var flowOdataProvider = flowOdataProviderMock.Object;

            var query = new ListFlowQuery(null, null, null);

            var handler = new ListFlowQueryHandler(mapper, flowReadRepository);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<FlowsModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<FlowModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<FlowModel>());
            var mapper = mapperMock.Object;

            var flowReadRepositoryMock = new Mock<IFlowReadRepository>();
            flowReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<FilterODataException>();
            var flowReadRepository = flowReadRepositoryMock.Object;           

            var query = new ListFlowQuery(null, null, null);

            var handler = new ListFlowQueryHandler(mapper, flowReadRepository);

            // Act

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.InvalidQueryFilter.Name &&
                                                      x.Message == HandlerFailures.InvalidQueryFilter &&
                                                      x.Target == "$filter");
        }
    }
}
