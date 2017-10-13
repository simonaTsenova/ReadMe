using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Factories;
using ReadMe.Web.Controllers;
using System;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.AcoountControllerTests
{
    [TestFixture]
    public class Login_Should
    {
        [Test]
        public void CallProviderIsAuthenticated_WhenInvoked()
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            controller.Login(returnUrl);

            providerMock.Verify(v => v.IsAuthenticated, Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndexAction_WhenProviderIsAuthenticatedIsTrue()
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Login(returnUrl))
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }

        [Test]
        public void ReturnDefaultView_WhenProviderIsAuthenticatedIsFalse()
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(false);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void SetViewBagReturnUrlCorrectly_WhenProviderIsAuthenticatedIsFalse()
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(false);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            var result = controller.Login(returnUrl) as ViewResult;

            Assert.AreSame(returnUrl, result.ViewBag.ReturnUrl);
        }
    }
}
