using AutoMapper;
using Moq;
using NUnit.Framework;
using PagedList;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Infrastructure.Factories;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookViewModel>();
            });
        }

        [Test]
        public void CallBookServiceGetAllAndDeleted_WhenInvoked()
        {
            var page = 1;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Index(page);

            bookServiceMock.Verify(s => s.GetAllAndDeleted(), Times.Once);
        }

        [Test]
        public void ReturnBooksPartialView_WhenInvoked()
        {
            var page = 1;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Index(page))
                .ShouldRenderPartialView("_BooksPartial")
                .WithModel<IPagedList<BookViewModel>>();
        }
    }
}
