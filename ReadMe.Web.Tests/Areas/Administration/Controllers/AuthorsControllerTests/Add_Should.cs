using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ReturnAddAuthorPartialView_WhenInvoked()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Add())
                .ShouldRenderPartialView("_AddAuthorPartial");
        }
    }
}
