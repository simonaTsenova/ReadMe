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
    public class SearchByAuthor_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var author = "author";

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.SearchByAuthor(author, It.IsAny<string[]>());

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectBooks_WhenPassedGenresAreNull()
        {
            var author = "author";

            var books = new List<Book>
            {
                new Book() { Author = new Author() { FirstName = "author", LastName = "author"} },
                new Book() { Author = new Author() { FirstName = "other", LastName =  "other" }}
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

            var result = service.SearchByAuthor(author, It.IsAny<string[]>());

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReturnCorrectBooks_WhenFilteredByGenres()
        {
            var author = "author";
            var genres = new string[] { "Mystery" };

            var books = new List<Book>
            {
                new Book()
                {
                    Author = new Author() { FirstName = "author", LastName = "author"},
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Mystery" }
                    }
                },
                new Book()
                {
                    Author = new Author() { FirstName = "author", LastName = "author"},
                    Genres = new List<Genre>()
                    {
                        new Genre() { Name = "Thriller" }
                    }
                },
                new Book()
                {
                    Author = new Author() { FirstName = "other", LastName =  "other" },
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

            var result = service.SearchByAuthor(author, genres);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
