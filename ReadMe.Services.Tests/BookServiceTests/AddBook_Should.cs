using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;
using System.Collections.Generic;

namespace ReadMe.Services.Tests.BookServiceTests
{
    [TestFixture]
    public class AddBook_Should
    {
        [TestCase("Chasing the Dime", "044661162X", "details", "English")]
        public void CallFactoryCreateBook_WhenInvoked(string title, string isbn, string summary, string language)
        {
            var authorMock = new Mock<Author>();
            var publisherMock = new Mock<Publisher>();
            var date = DateTime.Now;
            var genresMock = new Mock<List<Genre>>();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddBook(title, date, isbn, authorMock.Object, summary,
                language, publisherMock.Object, genresMock.Object);

            factoryMock.Verify(f => f.CreateBook(title, date, isbn, authorMock.Object, summary,
                language, publisherMock.Object, genresMock.Object), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English")]
        public void CallRepositoryAdd_WhenBookIsCreated(string title, string isbn, string summary, string language)
        {
            var authorMock = new Mock<Author>();
            var publisherMock = new Mock<Publisher>();
            var date = DateTime.Now;
            var genresMock = new Mock<List<Genre>>();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddBook(title, date, isbn, authorMock.Object, summary,
                language, publisherMock.Object, genresMock.Object);

            repositoryMock.Verify(r => r.Add(It.IsAny<Book>()), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English")]
        public void CallUnitOfWorkCommit_WhenInvoked(string title, string isbn, string summary, string language)
        {
            var authorMock = new Mock<Author>();
            var publisherMock = new Mock<Publisher>();
            var date = DateTime.Now;
            var genresMock = new Mock<List<Genre>>();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddBook(title, date, isbn, authorMock.Object, summary,
                language, publisherMock.Object, genresMock.Object);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
