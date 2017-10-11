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
    public class UpdateBook_Should
    {
        [TestCase("Chasing the Dime", "044661162X", "details", "English", "photoUrl")]
        public void CallRepositoryGetById_WhenInvoked(string title, string isbn, string summary,
            string language, string photoUrl)
        {
            var id = Guid.NewGuid();
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

            service.UpdateBook(id, title, date, isbn, summary, language,
                genresMock.Object, authorMock.Object, publisherMock.Object, photoUrl);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English", "photoUrl")]
        public void NotCallRepositoryUpdate_WhenBookIsNotFound(string title, string isbn, string summary,
            string language, string photoUrl)
        {
            var id = Guid.NewGuid();
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

            service.UpdateBook(id, title, date, isbn, summary, language,
                genresMock.Object, authorMock.Object, publisherMock.Object, photoUrl);

            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English", "photoUrl")]
        public void NotCallUnitOfWorkCommit_WhenBookIsNotFound(string title, string isbn, string summary,
            string language, string photoUrl)
        {
            var id = Guid.NewGuid();
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

            service.UpdateBook(id, title, date, isbn, summary, language,
                genresMock.Object, authorMock.Object, publisherMock.Object, photoUrl);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English", "photoUrl")]
        public void CallRepositoryUpdate_WhenBookIsFound(string title, string isbn, string summary,
            string language, string photoUrl)
        {
            var id = Guid.NewGuid();
            var authorMock = new Mock<Author>();
            var publisherMock = new Mock<Publisher>();
            var date = DateTime.Now;
            var genresMock = new Mock<List<Genre>>();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateBook(id, title, date, isbn, summary, language,
                genresMock.Object, authorMock.Object, publisherMock.Object, photoUrl);

            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English", "photoUrl")]
        public void CallUnitOfWorkCommit_WhenBookIsFound(string title, string isbn, string summary,
            string language, string photoUrl)
        {
            var id = Guid.NewGuid();
            var authorMock = new Mock<Author>();
            var publisherMock = new Mock<Publisher>();
            var date = DateTime.Now;
            var genresMock = new Mock<List<Genre>>();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateBook(id, title, date, isbn, summary, language,
                genresMock.Object, authorMock.Object, publisherMock.Object, photoUrl);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "details", "English", "photoUrl")]
        public void SetBookCorrectly_WhenBookIsFound(string title, string isbn, string summary,
            string language, string photoUrl)
        {
            var id = Guid.NewGuid();
            var authorMock = new Mock<Author>();
            var publisherMock = new Mock<Publisher>();
            var date = DateTime.Now;
            var genresMock = new Mock<List<Genre>>();

            var bookMock = new Mock<Book>();
            bookMock.Setup(b => b.Author).Returns(authorMock.Object);
            bookMock.Setup(b => b.Publisher).Returns(publisherMock.Object);
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateBook(id, title, date, isbn, summary, language,
                genresMock.Object, authorMock.Object, publisherMock.Object, photoUrl);

            Assert.AreEqual(title, bookMock.Object.Title);
            Assert.AreEqual(date, bookMock.Object.Published);
            Assert.AreEqual(isbn, bookMock.Object.ISBN);
            Assert.AreEqual(summary, bookMock.Object.Summary);
            Assert.AreEqual(language, bookMock.Object.Language);
            CollectionAssert.AreEqual(genresMock.Object, bookMock.Object.Genres);
            Assert.AreSame(authorMock.Object, bookMock.Object.Author);
            Assert.AreSame(publisherMock.Object, bookMock.Object.Publisher);
            Assert.AreEqual(photoUrl, bookMock.Object.PhotoUrl);
        }
    }
}
