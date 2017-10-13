using NUnit.Framework;
using ReadMe.Web.Areas.Administration.Controllers;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.MainControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WhenInvoked()
        {
            var controller = new MainController();

            controller.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
