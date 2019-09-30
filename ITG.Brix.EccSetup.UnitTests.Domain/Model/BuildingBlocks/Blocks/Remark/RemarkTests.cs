using FluentAssertions;
using ITG.Brix.EccSetup.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.EccSetup.UnitTests.Domain
{
    [TestClass]
    public class RemarksTests
    {
        Guid id = Guid.NewGuid();
        string name = Guid.NewGuid().ToString();
        string nameOnApplication = "any";
        string description = "any";
        RemarkIcon icon = new RemarkIcon(Guid.NewGuid());
        List<Tag> tags = new List<Tag>();
        List<DefaultRemark> defaultRemarks = new List<DefaultRemark>();

        [TestMethod]
        public void CreateShouldSuccess()
        {
            // Arrange

            // Act
            var result = new Remark(id, name, nameOnApplication, description, icon);
            tags.ForEach(x => result.AddTag(x));
            defaultRemarks.ForEach(x => result.AddDefaultRemark(x));

            // Assert
            result.Should().NotBeNull();
            result.Tags.Should().AllBeOfType(typeof(Tag));
            result.DefaultRemarks.Should().AllBeOfType(typeof(DefaultRemark));
            result.Name.Should().Be(name);
            result.Id.Should().Be(id);
            result.Description.Should().Be(description);
            result.Icon.Should().Be(icon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange 
            var id = Guid.Empty;
            // Act
            new Remark(id, name, nameOnApplication, description, icon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateShouldFailWhenNameIsEmpty()
        {
            // Arrange
            var name = string.Empty;
            // Act
            new Remark(id, name, nameOnApplication, description, icon);
        }

        #region Tags
        [TestMethod]
        public void AddTagShouldSucceed()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var tag = new Tag(Guid.NewGuid().ToString());

            // Act
            entity.AddTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Should().Be(tag);
        }

        [TestMethod]
        public void AddExistingTagShouldNotModifyCollection()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);

            // Act
            entity.AddTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Should().Be(tag);
        }

        [TestMethod]
        public void RemoveTagShouldSucceed()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);

            // Act
            entity.RemoveTag(tag);

            // Assert
            entity.Tags.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveUnexistingTagShouldPassSilentlyWithoutAnyImpactOnCollection()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);
            var tagToRemove = new Tag(Guid.NewGuid().ToString());

            // Act
            entity.RemoveTag(tagToRemove);

            // Assert
            entity.Tags.Count.Should().Be(1);
            entity.Tags.ElementAt(0).Should().Be(tag);
        }

        [TestMethod]
        public void ClearTagsShouldSucceed()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var tag = new Tag(Guid.NewGuid().ToString());
            entity.AddTag(tag);

            // Act
            entity.ClearTags();

            // Assert
            entity.Tags.Count.Should().Be(0);
        }
        #endregion

        #region DefaultRemark
        [TestMethod]
        public void AddDefaultRemarkShouldSucceed()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var defaultRemark = new DefaultRemark(Guid.NewGuid().ToString());

            // Act
            entity.AddDefaultRemark(defaultRemark);

            // Assert
            entity.DefaultRemarks.Count.Should().Be(1);
            entity.DefaultRemarks.ElementAt(0).Should().Be(defaultRemark);
        }

        [TestMethod]
        public void AddExistingDefaultRemarkShouldNotModifyCollection()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var defaultRemark = new DefaultRemark(Guid.NewGuid().ToString());
            entity.AddDefaultRemark(defaultRemark);

            // Act
            entity.AddDefaultRemark(defaultRemark);

            // Assert
            entity.DefaultRemarks.Count.Should().Be(1);
            entity.DefaultRemarks.ElementAt(0).Should().Be(defaultRemark);
        }

        [TestMethod]
        public void RemoveDefaultRemarkShouldSucceed()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var defaultRemark = new DefaultRemark(Guid.NewGuid().ToString());
            entity.AddDefaultRemark(defaultRemark);

            // Act
            entity.RemoveDefaultRemark(defaultRemark);

            // Assert
            entity.DefaultRemarks.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveUnexistingDefaultRemarkShouldPassSilentlyWithoutAnyImpactOnCollection()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var defaultRemark = new DefaultRemark(Guid.NewGuid().ToString());
            entity.AddDefaultRemark(defaultRemark);
            var defaultRemarkToRemove = new DefaultRemark(Guid.NewGuid().ToString());

            // Act
            entity.RemoveDefaultRemark(defaultRemarkToRemove);

            // Assert
            entity.DefaultRemarks.Count.Should().Be(1);
            entity.DefaultRemarks.ElementAt(0).Should().Be(defaultRemark);
        }

        [TestMethod]
        public void ClearDefaultRemarksShouldSucceed()
        {
            // Arrange
            var entity = new Remark(id, name, nameOnApplication, description, icon);
            var defaultRemark = new DefaultRemark(Guid.NewGuid().ToString());
            entity.AddDefaultRemark(defaultRemark);

            // Act
            entity.ClearDefaultRemarks();

            // Assert
            entity.DefaultRemarks.Count.Should().Be(0);
        }
        #endregion
    }
}
