using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Data.Tests.Fake;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReadMe.Data.Tests.EfRepositoryTests
{
    [TestFixture]
    public class All_Should
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

            var dbSetMock = new Mock<IDbSet<EfRepositoryTypeFake>>();
            dbSetMock.Setup(s => s.Provider).Returns(data.Provider);
            dbSetMock.Setup(s => s.Expression).Returns(data.Expression);
            dbSetMock.Setup(s => s.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());
            var dbContextMock = new Mock<IReadMeDbContext>();
            dbContextMock.Setup(c => c.DbSet<EfRepositoryTypeFake>()).Returns(dbSetMock.Object);
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            var result = repository.All;

            dbContextMock.Verify(c => c.DbSet<EfRepositoryTypeFake>(), Times.Once);
        }
    }
}
