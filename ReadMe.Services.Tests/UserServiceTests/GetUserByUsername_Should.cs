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
    public class GetUserByUsername_Should
    {
        [Test]
        public void CallUserRepositoryAll_WhenPassedValidUsername()
        {
            var username = "simona";
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            userService.GetUserByUsername(username);

            userRepositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectUser_WhenPassedValidUsername()
        {
            var username = "simona";
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userMock = new User(username);
            var usersList = new List<User>{ userMock };
            var expected = usersList.AsQueryable();
            userRepositoryMock.SetupGet(x => x.All).Returns(expected);
            var provider = new Mock<IDateTimeProvider>();
            var userService = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var result = userService.GetUserByUsername(username);

            Assert.AreEqual(expected, result);
        }
    }
}
