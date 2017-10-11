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
    public class GetBooksByPublisher_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetBooksByPublisher(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnBookCorrectly_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var publisher = new Publisher() { Id = id };
            var book = new Book() { Publisher = publisher };
            var expected = new List<Book> { book }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.All).Returns(expected);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetBooksByPublisher(id);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
