using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var controller = new HomeController(bookServiceMock.Object, reviewServiceMock.Object);

            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
