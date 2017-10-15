using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Search;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class GetAllBooks_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookShortViewModel>();
            });
        }

        [Test]
        public void CallBookServiceGetAll_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);

            controller.GetAllBooks();

            bookServiceMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void ReturnCorrectPartialView_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.GetAllBooks())
                .ShouldRenderPartialView("_BooksListPartial")
                .WithModel<IEnumerable<BookShortViewModel>>();
        }
    }
}
