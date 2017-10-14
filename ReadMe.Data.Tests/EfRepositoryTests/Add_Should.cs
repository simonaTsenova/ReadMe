using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Data.Tests.Fake;

namespace ReadMe.Data.Tests.EfRepositoryTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void CallDbContextAdd_WhenInvoked()
        {
            var entity = new Mock<EfRepositoryTypeFake>();
            var dbContextMock = new Mock<IReadMeDbContext>();
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            repository.Add(entity.Object);

            dbContextMock.Verify(c => c.Add(entity.Object), Times.Once);
        }
    }
}
