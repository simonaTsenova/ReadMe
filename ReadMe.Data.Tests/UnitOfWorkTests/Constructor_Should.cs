using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using System;

namespace ReadMe.Data.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDbContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UnitOfWork(null));
        }

        [Test]
        public void NotThrow_WhenDbContextIsNotNull()
        {
            var dbContextMock = new Mock<IReadMeDbContext>();

            Assert.DoesNotThrow(() => new UnitOfWork(dbContextMock.Object));
        }

        [Test]
        public void ConstructorShould_InitializeCorrectly_WhenPassedDbContextIsNotNull()
        {
            var dbContextMock = new Mock<IReadMeDbContext>();

            var unitOfWork = new UnitOfWork(dbContextMock.Object);

            Assert.IsNotNull(unitOfWork);
        }
    }
}
