using AutoMapper;
using Moq;
using NUnit.Framework;
using PagedList;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.GenresControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Genre, GenreViewModel>();
            });
        }

        [Test]
        public void CallGenreServiceGetAllAndDeleted_WhenInvoked()
        {
            var page = 1;

            var genreServiceMock = new Mock<IGenreService>();
            var controller = new GenresController(genreServiceMock.Object);

            controller.Index(page);

            genreServiceMock.Verify(s => s.GetAllAndDeleted(), Times.Once);
        }

        [Test]
        public void ReturnGenresPartialView_WhenInvoked()
        {
            var page = 1;

            var genres = new List<Genre>()
            {
                new Genre()
            }.AsQueryable();
            var genreServiceMock = new Mock<IGenreService>();
            genreServiceMock.Setup(s => s.GetAllAndDeleted()).Returns(genres);
            var controller = new GenresController(genreServiceMock.Object);

            controller.WithCallTo(c => c.Index(page))
                .ShouldRenderPartialView("_GenresPartial")
                .WithModel<IPagedList<GenreViewModel>>();
        }
    }
}
