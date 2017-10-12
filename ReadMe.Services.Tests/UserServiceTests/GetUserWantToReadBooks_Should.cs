using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Models.Enumerations;
using ReadMe.Providers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class GetUserWantToReadBooks_Should
    {
        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallRepositoryAll_WhenInvoked(string userId)
        {
            var user = new User()
            {
                Id = userId,
                //UserBooks = new List<UserBook>
                //{
                //    new UserBook() { ReadStatus = ReadStatus.Read },
                //    new UserBook() { ReadStatus = ReadStatus.CurrentlyReading }
                //}
            };
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            userRepositoryMock.Setup(r => r.All).Returns(
                 new List<User> { user }.AsQueryable());
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.GetUserWantToReadBooks(userId);

            userRepositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void ReturnCorrectNumberOfBooks_WhenInvoked(string userId)
        {
            var user = new User()
            {
                Id = userId,
                UserBooks = new List<UserBook>
                {
                    new UserBook() { ReadStatus = ReadStatus.WantToRead },
                    new UserBook() { ReadStatus = ReadStatus.CurrentlyReading }
                }
            };
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            userRepositoryMock.Setup(r => r.All).Returns(
                new List<User> { user }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var result = service.GetUserWantToReadBooks(userId);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void ReturnCorrectCollectionOfBooks_WhenInvoked(string userId)
        {
            var user = new User()
            {
                Id = userId,
                UserBooks = new List<UserBook>
                {
                    new UserBook() { ReadStatus = ReadStatus.WantToRead, Book = new Book { Title = "Book1" } },
                    new UserBook() { ReadStatus = ReadStatus.CurrentlyReading, Book = new Book { Title = "Book2" } }
                }
            };
            var expected = user.UserBooks.ElementAt(0).Book;
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            userRepositoryMock.Setup(r => r.All).Returns(
                new List<User> { user }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var result = service.GetUserWantToReadBooks(userId);

            Assert.AreSame(expected, result.FirstOrDefault());
        }
    }
}

