using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Search;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallGenreServiceGetAll_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);

            controller.Index();

            genreServiceMock.Verify(s => s.GetAll(), Times.Once);   
        }

        [Test]
        public void CallFactoryCreateSearchViewModel_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);

            controller.Index();

            factoryMock.Verify(f => f.CreateSearchViewModel(It.IsAny<ICollection<Genre>>()), Times.Once);
        }

        [Test]
        public void ReturnCorrectView_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new SearchController(bookServiceMock.Object, genreServiceMock.Object, factoryMock.Object);
            var modelMock = new Mock<SearchViewModel>();
            factoryMock.Setup(f => f.CreateSearchViewModel(It.IsAny<ICollection<Genre>>())).Returns(modelMock.Object);

            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel(modelMock.Object);
        }
    }
}
