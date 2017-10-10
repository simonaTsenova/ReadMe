using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
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

            Assert.Throws<ArgumentNullException>(() => new UserService(null, unitOfWorkMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();

            Assert.Throws<ArgumentNullException>(() => new UserService(userRepositoryMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.DoesNotThrow(() => new UserService(userRepositoryMock.Object, unitOfWorkMock.Object));
        }

        [Test]
        public void ShouldInitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
