using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using System;

namespace ReadMe.Services.Tests.UserBookServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();

            Assert.Throws<ArgumentNullException>(() => new UserBookService(null, unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var factoryMock = new Mock<IUserBookFactory>();

            Assert.Throws<ArgumentNullException>(() => new UserBookService(repositoryMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFactoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();

            Assert.DoesNotThrow(() => new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void InitializeUserBookServiceCorrectly_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();

            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
