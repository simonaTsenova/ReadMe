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
    public class GetTopRatedBooks_Should
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

            service.GetTopRatedBooks();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectCountOfBooks_WhenInvoked()
        {
            var books = new List<Book>
            {
                new Book { Rating = 5 },
                new Book { Rating = 1 },
                new Book { Rating = 5 },
                new Book { Rating = 2 },
                new Book { Rating = 4 },
                new Book { Rating = 3 },
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
                new Book { Rating = 5 },
                new Book { Rating = 1 },
                new Book { Rating = 5 },
                new Book { Rating = 2 },
                new Book { Rating = 4 },
                new Book { Rating = 3 },
            };
            var booksQueryable = books.AsQueryable();
            var expected = new List<Book>
            {
                books[0], books[2], books[4], books[5], books[3]
            }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.All).Returns(booksQueryable);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetTopRatedBooks();

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
