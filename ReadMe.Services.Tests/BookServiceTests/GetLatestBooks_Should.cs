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
    public class GetLatestBooks_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetLatestBooks();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectCountOfBooks_WhenInvoked()
        {
            var books = new List<Book>
            {
                new Book { Published = new DateTime(2017, 1, 18) },
                new Book { Published = new DateTime(2017, 4, 18) },
                new Book { Published = new DateTime(2017, 2, 18) },
                new Book { Published = new DateTime(2017, 12, 18) },
                new Book { Published = new DateTime(2017, 9, 18) },
                new Book { Published = new DateTime(2017, 1, 18) },
            }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.All).Returns(books);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetTopRatedBooks();

            Assert.AreEqual(5, result.ToList().Count());
        }

        [Test]
        public void ReturnBooksCorrectly_WhenInvoked()
        {
            var books = new List<Book>
            {
                new Book { Published = new DateTime(2017, 1, 18) },
                new Book { Published = new DateTime(2017, 4, 18) },
                new Book { Published = new DateTime(2017, 3, 18) },
                new Book { Published = new DateTime(2017, 10, 11) },
                new Book { Published = new DateTime(2017, 9, 18) },
                new Book { Published = new DateTime(2017, 2, 18) },
            };
            var booksQueryable = books.AsQueryable();
            var expected = new List<Book>
            {
                books[3], books[4], books[1], books[2], books[5]
            }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.All).Returns(booksQueryable);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetLatestBooks();

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
