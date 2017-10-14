using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using System;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBookServiceIsNull()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(null, genreServiceMock.Object, 
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenGenreServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object, 
                null, authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAuthorServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object, 
                genreServiceMock.Object, null, publisherServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPublisherServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object, 
                genreServiceMock.Object, authorServiceMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFactoryIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(
                bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.DoesNotThrow(() => new BooksController(
                bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void InitializeBooksControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}

