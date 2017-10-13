using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.GenresControllerTests
{
    [TestFixture]
    public class PostAdd_Should
    {
        [Test]
        public void CallGenreServiceAddGenre_WhenInvoked()
        {
            var name = "Fiction";

            var genreServiceMock = new Mock<IGenreService>();
            var controller = new GenresController(genreServiceMock.Object);
            var model = new GenreViewModel() { Name = name };

            controller.Add(model);

            genreServiceMock.Verify(s => s.AddGenre(name), Times.Once);
        }

        [Test]
        public void RedirectToControllerIndexAction_WhenInvoked()
        {
            var name = "Fiction";

            var genreServiceMock = new Mock<IGenreService>();
            var controller = new GenresController(genreServiceMock.Object);
            var model = new GenreViewModel() { Name = name };

            controller.WithCallTo(c => c.Add(model))
                .ShouldRedirectTo(c => c.Index(1));
        }
    }
}
