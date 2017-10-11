using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class GetUserById_Should
    {
        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallUserRepositoryGetById_WhenInvoked(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.GetUserById(id);

            userRepositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void ReturnCorrectUser_WhenFound(string id)
        {
            var userMock = new Mock<User>();
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            userRepositoryMock.Setup(r => r.GetById(id)).Returns(userMock.Object);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var result = service.GetUserById(id);

            Assert.AreSame(userMock.Object, result);
        }
    }
}
