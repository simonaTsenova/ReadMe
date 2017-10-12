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
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Search_Should
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
        public void CallBookServiceSearch_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);
            var searchModelMock = new Mock<SearchViewModel>();
            var genres = new string[1];

            controller.Search(searchModelMock.Object, genres);

            bookServiceMock.Verify(s => s.Search(It.IsAny<string>(), It.IsAny<string>(),
                genres), Times.Once);
        }

        [Test]
        public void ReturnCorrectPartialViewWithSearchResults_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);
            var searchModelMock = new Mock<SearchViewModel>();
            var genres = new string[1];

            controller.WithCallTo(c => c.Search(searchModelMock.Object, genres))
                .ShouldRenderPartialView("_BooksListPartial")
                .WithModel<IEnumerable<BookShortViewModel>>();
        }
    }
}
