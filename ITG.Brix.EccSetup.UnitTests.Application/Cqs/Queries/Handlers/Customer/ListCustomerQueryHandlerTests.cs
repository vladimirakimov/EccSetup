using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Definitions;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Handlers;
using ITG.Brix.EccSetup.Application.Cqs.Queries.Models;
using ITG.Brix.EccSetup.Application.Resources;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Repositories;
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
    public class ListCustomerQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var customerReadRepository = new Mock<ICustomerReadRepository>().Object;
            var customerOdataProvider = new Mock<ICustomerOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListCustomerQueryHandler(mapper, customerReadRepository, customerOdataProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            IMapper mapper = null;
            var customerReadRepository = new Mock<ICustomerReadRepository>().Object;
            var customerOdataProvider = new Mock<ICustomerOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListCustomerQueryHandler(mapper, customerReadRepository, customerOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhencustomerReadRepositoryIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            ICustomerReadRepository customerReadRepository = null;
            var customerOdataProvider = new Mock<ICustomerOdataProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new ListCustomerQueryHandler(mapper, customerReadRepository, customerOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhencustomerOdataProviderIsNull()
        {
            // Arrange
            var mapper = new Mock<IMapper>().Object;
            var customerReadRepository = new Mock<ICustomerReadRepository>().Object;
            ICustomerOdataProvider customerOdataProvider = null;

            // Act
            Action ctor = () =>
            {
                new ListCustomerQueryHandler(mapper, customerReadRepository, customerOdataProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<CustomerModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<CustomerModel>());
            var mapper = mapperMock.Object;

            var customerReadRepositoryMock = new Mock<ICustomerReadRepository>();
            customerReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Customer, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<Customer>()));
            var customerReadRepository = customerReadRepositoryMock.Object;

            var customerOdataProviderMock = new Mock<ICustomerOdataProvider>();
            customerOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Customer, bool>>)null);
            var customerOdataProvider = customerOdataProviderMock.Object;

            var query = new ListCustomerQuery(null, null, null);

            var handler = new ListCustomerQueryHandler(mapper, customerReadRepository, customerOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<CustomersModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<CustomerModel>>(It.IsAny<object>())).Returns(Enumerable.Empty<CustomerModel>());
            var mapper = mapperMock.Object;

            var customerReadRepositoryMock = new Mock<ICustomerReadRepository>();
            customerReadRepositoryMock.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Customer, bool>>>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var customerReadRepository = customerReadRepositoryMock.Object;

            var customerOdataProviderMock = new Mock<ICustomerOdataProvider>();
            customerOdataProviderMock.Setup(x => x.GetFilterPredicate(It.IsAny<string>())).Returns((Expression<Func<Customer, bool>>)null);
            var customerOdataProvider = customerOdataProviderMock.Object;

            var query = new ListCustomerQuery(null, null, null);

            var handler = new ListCustomerQueryHandler(mapper, customerReadRepository, customerOdataProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListCustomerFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
