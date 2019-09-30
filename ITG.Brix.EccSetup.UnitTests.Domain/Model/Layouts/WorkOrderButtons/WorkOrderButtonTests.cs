using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.ValueObjects.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class WorkOrderButtonTests
    {
        Guid id = Guid.NewGuid();
        List<string> workOrderAttribute = new List<string>();
        bool highlight = true;
        bool showCaption = true;
        bool hideOnButton = true;

        [TestMethod]
        public void CreateWorkOrderButtonShouldSucceed()
        {
            //Act
            var result = new WorkOrderButton(id, workOrderAttribute, highlight, showCaption, 1, SortOrderEnum.Ascending, hideOnButton);
            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateWorkOrderButtonNoIdShouldSucceed()
        {
            //Arrange 
            var id = Guid.NewGuid();

            //Act
            var result = new WorkOrderButton(id, workOrderAttribute, highlight, showCaption, 1, SortOrderEnum.Ascending, hideOnButton);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorkOrderButtonShouldFailWhenWorkOrderAttributeIsNull()
        {
            // Arrange 
            List<string> workOrderAttribute = null;
            //Act
            new WorkOrderButton(id, workOrderAttribute, highlight, showCaption, 1, SortOrderEnum.Ascending, hideOnButton);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateWorkOrderButtonShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;

            //Act
            new WorkOrderButton(id, workOrderAttribute, highlight, showCaption, 1, SortOrderEnum.Ascending, hideOnButton);
        }
    }
}
