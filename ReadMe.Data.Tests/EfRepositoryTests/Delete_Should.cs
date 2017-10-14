using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Data.Tests.Fake;

namespace ReadMe.Data.Tests.EfRepositoryTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallDbContextDelete_WhenInvoked()
        {
            var entity = new Mock<EfRepositoryTypeFake>();
            var dbContextMock = new Mock<IReadMeDbContext>();
            var repository = new EfRepository<EfRepositoryTypeFake>(dbContextMock.Object);

            repository.Delete(entity.Object);

            dbContextMock.Verify(c => c.Delete(entity.Object), Times.Once);
        }
    }
}
