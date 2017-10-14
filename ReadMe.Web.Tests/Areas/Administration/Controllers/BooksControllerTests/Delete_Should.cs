using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallBookServiceDeleteBook_WhenInvoked()
        {
            var page = 1;
            var id = Guid.NewGuid();

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Delete(id, page);

            bookServiceMock.Verify(s => s.DeleteBook(id), Times.Once);
        }

        [Test]
        public void RedirectToControllerIndexAction_WhenInvoked()
        {
            var page = 1;
            var id = Guid.NewGuid();

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Delete(id, page))
                .ShouldRedirectTo(c => c.Index(page));
        }
    }
}
