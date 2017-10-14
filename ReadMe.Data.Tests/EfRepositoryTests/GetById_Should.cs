using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Data.Tests.Fake;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReadMe.Data.Tests.EfRepositoryTests
{
    [TestFixture]
    public class GetById_Should
    {
        private IQueryable<EfRepositoryTypeFake> GetData()
        {
            var data = new List<EfRepositoryTypeFake>()
            {
                new EfRepositoryTypeFake(),
                new EfRepositoryTypeFake()
            }.AsQueryable();

            return data;
        }

        [Test]
        public void CallDbContextDbSet_WhenInvoked()
        {
            var data = this.GetData();
            var id = Guid.NewGuid();

            var dbSetMock = new Mock<IDbSet<EfRepositoryTypeFake>>();
            dbSetMock.Setup(s => s.Provider).Returns(data.Provider);
            dbSetMock.Setup(s => s.Expression).Returns(data.Expression);
            dbSetMock.Setup(s => s.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());
            var dbContextMock = new Mock<IReadMeDbContext>();
            dbContextMock.Setup(c => c.DbSet<EfRepositoryTypeFake>()).Returns(dbSetMock.Object);
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            repository.GetById(id);

            dbContextMock.Verify(c => c.DbSet<EfRepositoryTypeFake>(), Times.Once);
        }

        [Test]
        public void CallDbSetFind_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var dbSetMock = new Mock<IDbSet<EfRepositoryTypeFake>>();
            var dbContextMock = new Mock<IReadMeDbContext>();
            dbContextMock.Setup(c => c.DbSet<EfRepositoryTypeFake>()).Returns(dbSetMock.Object);
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            repository.GetById(id);

            dbSetMock.Verify(s => s.Find(id), Times.Once);
        }

        [Test]
        public void ReturnCorrectEntity_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var entityMock = new Mock<EfRepositoryTypeFake>();
            var dbSetMock = new Mock<IDbSet<EfRepositoryTypeFake>>();
            dbSetMock.Setup(s => s.Find(id)).Returns(entityMock.Object);
            var dbContextMock = new Mock<IReadMeDbContext>();
            dbContextMock.Setup(c => c.DbSet<EfRepositoryTypeFake>()).Returns(dbSetMock.Object);
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            var result = repository.GetById(id);

            Assert.AreSame(entityMock.Object, result);
        }
    }
}
