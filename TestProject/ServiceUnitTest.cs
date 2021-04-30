using DataService;
using Models;
using Models.Base;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestProject
{
    public class ServiceUnitTest
    {
        [Fact]
        public void Create_PassNewEntity_ReturnsEntityWithId()
        {
            // arrange
            Entity entity = new Student();
            var service = new Service<Entity>();

            //act
            entity = service.Create(entity);

            //assert
            Assert.NotEqual(0, entity.Id);
        }

        [Fact]
        public void Read_PassEntityId_ReturnsSameEntity()
        {
            // arrange
            Entity entity = new Student();
            var service = new Service<Entity>();
            entity = service.Create(entity);

            //act
            var entity2 = service.Read(entity.Id);

            //assert
            Assert.Equal(entity2.Id, entity.Id);
        }

        [Fact]
        public void Read_PassNotExistingId_ReturnsNull()
        {
            // arrange
            var service = new Service<Entity>();

            //act
            var entity = service.Read(0);

            //assert
            Assert.Null(entity);
        }

        [Fact]
        public void Delete_PassNotExistingId_ThrowsKeyNotFoundException()
        {
            // arrange
            var service = new Service<Entity>();

            //act and assert
            var keyNotFoundException = Assert.Throws<KeyNotFoundException>(() => service.Delete(0));
            Assert.Equal("0", keyNotFoundException.Message);
        }

        [Fact]
        public void Delete_PassIdToDelete_ReadIdResutnNull()
        {
            // arrange
            Entity entity = new Student();
            var service = new Service<Entity>();
            entity = service.Create(entity);

            //act
            service.Delete(entity.Id);
            entity = service.Read(entity.Id);

            //assert
            Assert.Null(entity);
        }

        [Fact]
        public void BadTest()
        {
            Entity entity = new Student();
            var service = new Service<Entity>();

            entity = service.Create(entity);
            Assert.NotEqual(0, entity.Id);

            var entity2 = service.Read(entity.Id);
            Assert.Equal(entity2.Id, entity.Id);

            service.Delete(entity.Id);
            entity = service.Read(entity.Id);
            Assert.Null(entity);
        }
    }
}
