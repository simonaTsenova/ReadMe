using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.UserBookServiceTests
{
    [TestFixture]
    public class GetByUserIdAndBookId_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.GetByUserIdAndBookId(userId, bookId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnUserBookCorrectly_WhenInvoked()
        {
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            Guid bookId = Guid.NewGuid();

            var userBook = new UserBook();
            userBook.BookId = bookId;
            userBook.UserId = userId;
            var repositoryMock = new Mock<IEfRepository<UserBook>>();
            repositoryMock.Setup(r => r.All).Returns(new List<UserBook> { userBook }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserBookFactory>();
            var service = new UserBookService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetByUserIdAndBookId(userId, bookId);

            Assert.AreSame(userBook, result);
        }
    }
}
