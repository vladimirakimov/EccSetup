using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Domain.Model.OperatorActivities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.EccSetup.UnitTests.Domain.Model.OperatorActivities
{
    [TestClass]
    public class OperatorActivityTests
    {
        [TestMethod]
        public void ConstructorWithoutParametersShouldSucceed()
        {
            //Act
            var operatorActivity = new OperatorActivity();

            //Assert
            operatorActivity.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorWithOneParameterShouldSuceed()
        {
            //Act
            var operatorActivity = new OperatorActivity(Guid.NewGuid());

            //Assert
            operatorActivity.Should().NotBeNull();
        }

        [TestMethod]
        public void SetTypeShouldSucceed()
        {
            //Act
            var blockType = BlockType.Checklist;
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetType(blockType);

            //Assert
            operatorActivity.Type.Should().Be(blockType);
        }

        [TestMethod]
        public void SetTitleShouldSucceed()
        {
            //Act
            var title = "Validate licence plate";
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetTitle(title);

            //Assert
            operatorActivity.Title.Should().Be(title);
        }

        [TestMethod]
        public void SetNameShouldSucceed()
        {
            //Arrange
            var name = "Step name";
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetName(name);

            //Assert
            operatorActivity.Name.Should().Be(name);
        }

        [TestMethod]
        public void SetCreatedShouldSucceed()
        {
            //Arrange 
            var created = DateTime.Now;
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetCreated(created);

            //Assert
            operatorActivity.Created.Should().Be(created);
        }

        [TestMethod]
        public void SetOperatorNameShouldSucceed()
        {
            //Arrange
            var operatorLogin = "eOrder";
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetOperatorLogin(operatorLogin);

            //Assert
            operatorActivity.OperatorLogin.Should().Be(operatorLogin);
        }

        [TestMethod]
        public void SetOperatorIdShouldSucceed()
        {
            //Arrange
            var operatorId = Guid.NewGuid();
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetOperatorId(operatorId);

            //Assert
            operatorActivity.OperatorId.Should().Be(operatorId);
        }

        [TestMethod]
        public void SetWorkOrderIdShouldSucceed()
        {
            //Arrange
            var workOrderId = Guid.NewGuid();
            var operatorActivity = new OperatorActivity();

            //Act
            operatorActivity.SetWorkOrderId(workOrderId);

            //Assert
            operatorActivity.WorkOrderId.Should().Be(workOrderId);
        }
    }
}
