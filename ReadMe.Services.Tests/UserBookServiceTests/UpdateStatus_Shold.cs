using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.UserBookServiceTests
{
    [TestFixture]
    public class UpdateStatus_Shold
    {
        [Test]
        public void CallUserBookRepositoryAll_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.UpdateStatus(userId, bookId, status);

            repositoryMock.Verify(r => r.All, Times.Once);
        }


        [Test]
        public void CallUserBookRepositoryAdd_WhenUserBookIsNotFound()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.UpdateStatus(userId, bookId, status);

            repositoryMock.Verify(r => r.Add(It.IsAny<UserBook>()), Times.Once);
        }

        [Test]
        public void CallUserBookRepositoryUpdate_WhenUserBookIsFound()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var userBook = new UserBook();
            userBook.UserId = userId;
            userBook.BookId = bookId;
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            repositoryMock.Setup(r => r.All).Returns(new List<UserBook> { userBook }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.UpdateStatus(userId, bookId, status);

            repositoryMock.Verify(r => r.Update(It.IsAny<UserBook>()), Times.Once);
        }

        [Test]
        public void SetReadStatusCorrectly_WhenUserBookIsFound()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var userBook = new UserBook();
            userBook.UserId = userId;
            userBook.BookId = bookId;
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            repositoryMock.Setup(r => r.All).Returns(new List<UserBook> { userBook }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.UpdateStatus(userId, bookId, status);

            Assert.AreEqual(status, userBook.ReadStatus);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();
            var status = ReadStatus.CurrentlyReading;

            var userBook = new UserBook();
            userBook.UserId = userId;
            userBook.BookId = bookId;
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            repositoryMock.Setup(r => r.All).Returns(new List<UserBook> { userBook }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.UpdateStatus(userId, bookId, status);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
