using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new UserService(null, unitOfWorkMock.Object, provider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var provider = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new UserService(userRepositoryMock.Object, null, provider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object));
        }

        [Test]
        public void ShouldInitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            Assert.IsNotNull(service);
        }
    }
}
