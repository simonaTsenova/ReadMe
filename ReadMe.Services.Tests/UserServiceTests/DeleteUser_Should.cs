using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class DeleteUser_Should
    {
        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallUserRepositoryGetById_WhenInvoked(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.DeleteUser(id);

            userRepositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallDateTimeProviderGetTime_WhenInvoked(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, providerMock.Object);

            service.DeleteUser(id);

            providerMock.Verify(x => x.GetCurrentTime(), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void NotCallRepositoryUpdate_WhenUserIsNotFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.DeleteUser(id);

            userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void NotCallUnitOfWorkCommit_WhenUserIsNotFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.DeleteUser(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void SetIsDeletedToTrue_IfUserIsFound(string id)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.DeleteUser(id);

            Assert.IsTrue(user.Object.IsDeleted);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void SetDeletedOnCorrectly_IfUserIsFound(string id)
        {
            var date = DateTime.Now;
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            provider.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.DeleteUser(id);

            Assert.AreEqual(date, user.Object.DeletedOn);
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

            service.DeleteUser(id);

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

            service.DeleteUser(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
