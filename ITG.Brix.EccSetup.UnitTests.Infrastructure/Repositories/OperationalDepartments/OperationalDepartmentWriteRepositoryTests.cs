using FluentAssertions;
using ITG.Brix.EccSetup.Infrastructure.Configurations.Impl;
using ITG.Brix.EccSetup.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Infrastructure.Repositories.OperationalDepartments
{
    [TestClass]
    public class OperationalDepartmentWriteRepositoryTests
    {
        [TestMethod]
        public void CtorShouldSucceed()
        {
            //Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            var dataContext = new Mock<DataContext>(persistenceConfiguration).Object;

            // Act
            Action ctor = () => { new OperationalDepartmentWriteRepository(dataContext); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void CtorShouldFailWhenDataContextIsNull()
        {
            // Arrange
            DataContext dataContext = null;

            // Act
            Action ctor = () => { new OperationalDepartmentWriteRepository(dataContext); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
