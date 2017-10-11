using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class GetAllAndDeleted_Should
    {
        [Test]
        public void CallUserRepositoryAllAndDeleted_WhenInvoked()
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object,provider.Object);

            service.GetAllAndDeleted();

            userRepositoryMock.Verify(r => r.AllAndDeleted, Times.Once);
        }

        [Test]
        public void ReturnCorrectUsers_WhenInvoked()
        {
            var userMock = new Mock<User>();
            var users = new List<User> { userMock.Object }.AsQueryable();
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            userRepositoryMock.Setup(r => r.AllAndDeleted).Returns(users);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var result = service.GetAllAndDeleted();

            CollectionAssert.AreEqual(users, result);
        }
    }
}
