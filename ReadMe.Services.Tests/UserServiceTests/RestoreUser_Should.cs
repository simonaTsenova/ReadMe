using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services.Tests.UserServiceTests
{
    public class RestoreUser_Should
    {
        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallUserRepositoryGetById_WhenInvoked(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            userRepositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void NotCallRepositoryUpdate_WhenUserIsNotFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void NotCallUnitOfWorkCommit_WhenUserIsNotFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void SetIsDeletedToFalse_IfUserIsFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            Assert.IsFalse(user.Object.IsDeleted);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void SetDeletedOnToNull_IfUserIsFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            Assert.AreEqual(null, user.Object.DeletedOn);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallRepisitoryUpdate_WhenUserIsFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallUnitOfWorkCommit_WhenUserIsFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.RestoreUser(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
