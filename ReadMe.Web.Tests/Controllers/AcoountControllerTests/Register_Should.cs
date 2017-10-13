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
    public class Register_Should
    {
        [Test]
        public void CallProviderIsAuthenticated_WhenInvoked()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            controller.Register();

            providerMock.Verify(v => v.IsAuthenticated, Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndexAction_WhenProviderIsAuthenticatedIsTrue()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Register())
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }

        [Test]
        public void ReturnDefaultView_WhenProviderIsAuthenticatedIsFalse()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(false);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Register())
                .ShouldRenderDefaultView();
        }
    }
}
