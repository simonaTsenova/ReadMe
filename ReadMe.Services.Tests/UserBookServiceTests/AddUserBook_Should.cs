using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Models.Enumerations;
using System;

namespace ReadMe.Services.Tests.UserBookServiceTests
{
    [TestFixture]
    public class AddUserBook_Should
    {
        [Test]
        public void CallFactoryCreateUserBook_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.AddUserBook(userId, bookId, status);

            factoryMock.Verify(f => f.CreateUserBook(userId, bookId, status), Times.Once);
        }

        [Test]
        public void CallUserBookRepositoryAdd_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.AddUserBook(userId, bookId, status);

            repositoryMock.Verify(r => r.Add(It.IsAny<UserBook>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.AddUserBook(userId, bookId, status);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
