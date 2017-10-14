using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Data.Tests.Fake;

namespace ReadMe.Data.Tests.EfRepositoryTests
{
    [TestFixture]
    public class Update_Should
    {
        [Test]
        public void CallDbContextAdd_WhenInvoked()
        {
            var entity = new Mock<EfRepositoryTypeFake>();
            var dbContextMock = new Mock<IReadMeDbContext>();
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            repository.Update(entity.Object);

            dbContextMock.Verify(c => c.Update(entity.Object), Times.Once);
        }
    }
}
