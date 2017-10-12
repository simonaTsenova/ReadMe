using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using System;

namespace ReadMe.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBookServiceIsNull()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new SearchController(null, genreServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenGenreServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new SearchController(bookServiceMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenViewModelFactoryIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();

            Assert.Throws<ArgumentNullException>(() => new SearchController(bookServiceMock.Object, genreServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.DoesNotThrow(() => new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void SetSearchControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
