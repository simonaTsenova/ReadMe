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
    public class PostLogin_Should
    {
        [Test]
        public void CallProviderIsAuthenticated_WhenInvoked()
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<LoginViewModel>();

            controller.Login(modelMock.Object, returnUrl);

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
            var modelMock = new Mock<LoginViewModel>();

            controller.WithCallTo(c => c.Login(modelMock.Object, returnUrl))
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }

        [Test]
        public void ReturnDefaultViewWithModel_WhenModelINotValid()
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(false);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            controller.ModelState.AddModelError("field", "message error");
            var modelMock = new Mock<LoginViewModel>();

            controller.WithCallTo(c => c.Login(modelMock.Object, returnUrl))
                .ShouldRenderDefaultView()
                .WithModel(modelMock.Object);
        }

        [TestCase("username", "123456", true)]
        public void CallProviderSignInWithPassword_WhenModelIsValid(string username, string password, bool remember)
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new LoginViewModel()
            {
                UserName = username,
                Password = password,
                RememberMe = remember
            };

            controller.Login(model, returnUrl);

            providerMock.Verify(v => v.SignInWithPassword(username, password, remember, It.IsAny<bool>()), Times.Once);
        }

        [TestCase("username", "123456", true)]
        public void RedirectToCorrectUrl_WhenProviderReturnsSuccess(string username, string password, bool remember)
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.SignInWithPassword(username, password, remember, It.IsAny<bool>()))
                .Returns(SignInStatus.Success);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new LoginViewModel()
            {
                UserName = username,
                Password = password,
                RememberMe = remember
            };

            controller.WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [TestCase("username", "123456", true)]
        public void ReturnLockoutView_WhenProviderReturnsLockedOut(string username, string password, bool remember)
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.SignInWithPassword(username, password, remember, It.IsAny<bool>()))
                .Returns(SignInStatus.LockedOut);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new LoginViewModel()
            {
                UserName = username,
                Password = password,
                RememberMe = remember
            };

            controller.WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRenderView("Lockout");
        }

        [TestCase("username", "123456", true)]
        public void AddErrorsToModelState_WhenProviderReturnsFailure(string username, string password, bool remember)
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.SignInWithPassword(username, password, remember, It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new LoginViewModel()
            {
                UserName = username,
                Password = password,
                RememberMe = remember
            };

            controller.Login(model, returnUrl);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestCase("username", "123456", true)]
        public void ReturnDefaultView_WhenProviderReturnsFailure(string username, string password, bool remember)
        {
            var returnUrl = "url";

            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.SignInWithPassword(username, password, remember, It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new LoginViewModel()
            {
                UserName = username,
                Password = password,
                RememberMe = remember
            };

            controller.WithCallTo(c => c.Login(model, returnUrl))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
