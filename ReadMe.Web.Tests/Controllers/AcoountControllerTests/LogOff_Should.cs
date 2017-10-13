using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Factories;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.AcoountControllerTests
{
    [TestFixture]
    public class LogOff_Should
    {
        [Test]
        public void CallProviderIsAuthenticated_WhenInvoked()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<LoginViewModel>();

            controller.LogOff();

            providerMock.Verify(v => v.IsAuthenticated, Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndexAction_WhenProviderIsAuthenticatedIsFalse()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(false);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<LoginViewModel>();

            controller.WithCallTo(c => c.LogOff())
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }

        [Test]
        public void CallProviderSignOut_WhenIsAuthenticatedIsTrue()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<LoginViewModel>();

            controller.LogOff();

            providerMock.Verify(v => v.SignOut(), Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndexAction_WhenProviderIsAuthenticatedIsTrue()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<LoginViewModel>();

            controller.WithCallTo(c => c.LogOff())
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }
    }
}
