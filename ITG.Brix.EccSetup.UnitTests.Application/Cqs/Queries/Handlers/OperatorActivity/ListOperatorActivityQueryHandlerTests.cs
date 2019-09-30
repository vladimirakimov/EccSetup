using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions.OperatorActivity;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using ITG.Brix.EccSetup.Domain.Repositories.OperatorActivities;
using ITG.Brix.EccSetup.Infrastructure.DataAccess.ClassModels;
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
    public class ListOperatorActivityQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            //Arrange
            var mapper = new Mock<IMapper>().Object;
            var operatorActivityReadRepository = new Mock<IOperatorActivityReadRepository>().Object;
            var odataProvider = new Mock<IOperatorActivityOdataProvider>().Object;

            //Act
            new ListOperatorActivityQueryHandler(operatorActivityReadRepository, odataProvider, mapper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShuldFailWhenMapperIsNull()
        {
            //Arrange
            IMapper mapper = null;
            var operatorActivityReadRepository = new Mock<IOperatorActivityReadRepository>().Object;
            var odataProvider = new Mock<IOperatorActivityOdataProvider>().Object;

            //Act
            new ListOperatorActivityQueryHandler(operatorActivityReadRepository, odataProvider, mapper);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CosntructorShouldFailWhenReadRepositoryIsNull()
        {
            //Arrange
            var mapper = new Mock<IMapper>().Object;
            IOperatorActivityReadRepository operatorActivityReadRepository = null;
            var odataProvider = new Mock<IOperatorActivityOdataProvider>().Object;

            //Act
            new ListOperatorActivityQueryHandler(operatorActivityReadRepository, odataProvider, mapper);
        }

        [TestMethod]
        public async Task HandlerShouldReturnOk()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorActivityModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperatorActivityModel>);
            var mapper = mapperMock.Object;

            var operatorActivityReadRepositoryMock = new Mock<IOperatorActivityReadRepository>();
            operatorActivityReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<OperatorActivity>()));
            var operatorActivityReadRepository = operatorActivityReadRepositoryMock.Object;

            var operatorActivityOdataProviderMock = new Mock<IOperatorActivityOdataProvider>();
            operatorActivityOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<OperatorActivityClass, bool>>)null);
            var operatorActivityOdataProvider = operatorActivityOdataProviderMock.Object;

            var query = new ListOperatorActivityQuery(null, null, null);
            var handler = new ListOperatorActivityQueryHandler(operatorActivityReadRepository, operatorActivityOdataProvider, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<OperatorActivitiesModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenFilterOdataErrorOccurs()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorActivityModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperatorActivityModel>);
            var mapper = mapperMock.Object;

            var operatorActivityReadRepositoryMock = new Mock<IOperatorActivityReadRepository>();
            operatorActivityReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<FilterODataException>();
            var operatorActivityReadRepository = operatorActivityReadRepositoryMock.Object;

            var operatorActivityOdataProviderMock = new Mock<IOperatorActivityOdataProvider>();
            operatorActivityOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<OperatorActivityClass, bool>>)null);
            var operatorActivityOdataProvider = operatorActivityOdataProviderMock.Object;

            var query = new ListOperatorActivityQuery(null, null, null);
            var handler = new ListOperatorActivityQueryHandler(operatorActivityReadRepository, operatorActivityOdataProvider, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.InvalidQueryFilter.Name &&
                                                    x.Message == HandlerFailures.InvalidQueryFilter &&
                                                    x.Target == "$filter");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenExceptionThrown()
        {
            //Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<OperatorActivityModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<OperatorActivityModel>);
            var mapper = mapperMock.Object;

            var operatorActivityReadRepositoryMock = new Mock<IOperatorActivityReadRepository>();
            operatorActivityReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<ArgumentNullException>();
            var operatorActivityReadRepository = operatorActivityReadRepositoryMock.Object;

            var operatorActivityOdataProviderMock = new Mock<IOperatorActivityOdataProvider>();
            operatorActivityOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<OperatorActivityClass, bool>>)null);
            var operatorActivityOdataProvider = operatorActivityOdataProviderMock.Object;

            var query = new ListOperatorActivityQuery(null, null, null);
            var handler = new ListOperatorActivityQueryHandler(operatorActivityReadRepository, operatorActivityOdataProvider, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListOperatorActivitiesFailure);
        }
    }
}
