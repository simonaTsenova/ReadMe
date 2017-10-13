using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.GenresControllerTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ReturnAddGenrePartialView_WhenInvoked()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var controller = new GenresController(genreServiceMock.Object);

            controller.WithCallTo(c => c.Add())
                .ShouldRenderPartialView("_AddGenrePartial");
        }
    }
}
