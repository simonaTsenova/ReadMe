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
    public class SearchByTitle_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var title = "title";

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.SearchByTitle(title, It.IsAny<string[]>());

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectBooks_WhenPassedGenresAreNull()
        {
            var title = "title";

            var books = new List<Book>
            {
                new Book() { Title = "title" },
                new Book() { Title = "other" }
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

            var result = service.SearchByTitle(title, It.IsAny<string[]>());

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReturnCorrectBooks_WhenFilteredByGenres()
        {
            var title = "title";
            var genres = new string[] {"Mystery" };

            var books = new List<Book>
            {
                new Book()
                {
                    Title = "title",
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Mystery" }
                    }
                },
                new Book()
                {
                    Title = "title",
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Thriller" }
                    }
                },
                new Book()
                {
                    Title = "other",
                    Genres =  new List<Genre>()
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

            var result = service.SearchByTitle(title, genres);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
