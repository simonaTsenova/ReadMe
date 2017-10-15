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
    class SearchByYear_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var year = "2017";

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.SearchByYear(year, It.IsAny<string[]>());

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectBooks_WhenPassedGenresAreNull()
        {
            var year = "17";

            var books = new List<Book>
            {
                new Book() { Published = DateTime.Now},
                new Book() { Published = new DateTime(2016, 10, 10) }
            };
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.All).Returns(books.AsQueryable());
            var expected = new List<Book>
            {
                books[0]
            }.AsQueryable();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.SearchByYear(year, It.IsAny<string[]>());

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReturnCorrectBooks_WhenFilteredByGenres()
        {
            var year = "17";
            var genres = new string[] { "Mystery" };

            var books = new List<Book>
            {
                new Book()
                {
                    Published = DateTime.Now,
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Mystery" }
                    }
                },
                new Book()
                {
                    Published = DateTime.Now,
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Thriller" }
                    }
                },
                new Book()
                {
                    Published = new DateTime(2016, 10, 10),
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Mystery" }
                    }
                }
            };
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.All).Returns(books.AsQueryable());
            var expected = new List<Book>
            {
                books[0]
            }.AsQueryable();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.SearchByYear(year, genres);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
