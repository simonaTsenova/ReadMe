using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.BookServiceTests
{
    [TestFixture]
    public class GetAllAndDeleted_Should
    {
        [Test]
        public void CallRepositoryAllAndDeleted_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetAllAndDeleted();

            repositoryMock.Verify(r => r.AllAndDeleted, Times.Once);
        }

        [Test]
        public void ReturnBooksCorrectly_WhenInvoked()
        {
            var bookMock = new Mock<Book>();
            var expected = new List<Book> { bookMock.Object }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.AllAndDeleted).Returns(expected);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetAllAndDeleted();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
