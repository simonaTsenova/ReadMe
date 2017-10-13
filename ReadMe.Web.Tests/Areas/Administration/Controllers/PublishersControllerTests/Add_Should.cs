using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void ReturnAddPublisherPartialView_WhenInvoked()
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Add())
                .ShouldRenderPartialView("_AddPublisherPartial");
        }
    }
}
