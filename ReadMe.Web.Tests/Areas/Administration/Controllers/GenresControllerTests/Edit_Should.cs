using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.GenresControllerTests
{
    [TestFixture]
    public class Edit_Should
    {
        [Test]
        public void CallGenreServiceUpdateGenre_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var name = "Fiction";

            var genreServiceMock = new Mock<IGenreService>();
            var controller = new GenresController(genreServiceMock.Object);
            var model = new GenreViewModel() { Id = id, Name = name };

            controller.Edit(model);

            genreServiceMock.Verify(s => s.UpdateGenre(id, name), Times.Once);
        }

        [Test]
        public void RedirectToControllerIndexAction_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var name = "Fiction";

            var genreServiceMock = new Mock<IGenreService>();
            var controller = new GenresController(genreServiceMock.Object);
            var model = new GenreViewModel() { Id = id, Name = name };

            controller.WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo(c => c.Index(1));
        }
    }
}
