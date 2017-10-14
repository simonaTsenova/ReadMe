using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;

namespace ReadMe.Data.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class Commit_Should
    {
        [Test]
        public void CallDbContextSaveChanges_WhenInvoked()
        {
            var dbContextMock = new Mock<IReadMeDbContext>();
            var unitOfWork = new UnitOfWork(dbContextMock.Object);

            unitOfWork.Commit();

            dbContextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
