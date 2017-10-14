using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Infrastructure.Factories;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void CallGenreServiceGetAll_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Add();

            genreServiceMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void CallFactoryCreateAddBookViewModel_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Add();

            factoryMock.Verify(f => f.CreateAddBookViewModel(It.IsAny<ICollection<Genre>>()), Times.Once);
        }

        [Test]
        public void ReturnAddBookPartialView_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateAddBookViewModel(It.IsAny<ICollection<Genre>>()))
                .Returns(model);
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Add())
                .ShouldRenderPartialView("_AddBookPartial")
                .WithModel<AddBookViewModel>(m => Assert.AreEqual(model, m));
        }
    }
}
