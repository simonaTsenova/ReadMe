using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.AcoountControllerTests
{
    [TestFixture]
    public class PostRegister_Should
    {
        [Test]
        public void CallProviderIsAuthenticated_WhenInvoked()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<RegisterViewModel>();

            controller.Register(modelMock.Object);

            providerMock.Verify(v => v.IsAuthenticated, Times.Once);
        }

        [Test]
        public void RedirectToHomeControllerIndexAction_WhenProviderIsAuthenticatedIsTrue()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(true);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var modelMock = new Mock<RegisterViewModel>();

            controller.WithCallTo(c => c.Register(modelMock.Object))
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }

        [Test]
        public void ReturnDefaultViewWithModel_WhenModelINotValid()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.IsAuthenticated).Returns(false);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            controller.ModelState.AddModelError("field", "message error");
            var modelMock = new Mock<RegisterViewModel>();

            controller.WithCallTo(c => c.Register(modelMock.Object))
                .ShouldRenderDefaultView()
                .WithModel(modelMock.Object);
        }

        [TestCase("username", "123456", "user@mail.com", "name", "lastname")]
        public void CallFactoryCreateUser_WhenModelIsValid(string username, string password, string email,
            string firstname, string lastname)
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(), 
                It.IsAny<bool>(), It.IsAny<bool>())).Returns(IdentityResult.Success);
            var factoryMock = new Mock<IUserFactory>();
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new RegisterViewModel()
            {
                UserName = username,
                Password = password,
                Email = email,
                FirstName = firstname,
                LastName = lastname
            };

            controller.Register(model);

            factoryMock.Verify(f => f.CreateUser(email, username, firstname, lastname), Times.Once);
        }

        [TestCase("username", "123456", "user@mail.com", "name", "lastname")]
        public void CallProviderRegisterAndLoginUser_WhenModelIsValid(string username, string password, string email,
            string firstname, string lastname)
        {
            var userMock = new Mock<User>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>())).Returns(IdentityResult.Success);
            var factoryMock = new Mock<IUserFactory>();
            factoryMock.Setup(f => f.CreateUser(email, username, firstname, lastname)).Returns(userMock.Object);
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new RegisterViewModel()
            {
                UserName = username,
                Password = password,
                Email = email,
                FirstName = firstname,
                LastName = lastname
            };

            controller.Register(model);

            providerMock.Verify(v => v.RegisterAndLoginUser(userMock.Object, password, false, false), Times.Once);
        }

        [TestCase("username", "123456", "user@mail.com", "name", "lastname")]
        public void RedirectToHomeControllerIndexAction_WhenResultSucceededIsTrue(string username, string password, string email,
            string firstname, string lastname)
        {
            var userMock = new Mock<User>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>())).Returns(IdentityResult.Success);
            var factoryMock = new Mock<IUserFactory>();
            factoryMock.Setup(f => f.CreateUser(email, username, firstname, lastname)).Returns(userMock.Object);
            var controller = new AccountController(providerMock.Object, factoryMock.Object);
            var model = new RegisterViewModel()
            {
                UserName = username,
                Password = password,
                Email = email,
                FirstName = firstname,
                LastName = lastname
            };

            controller.WithCallTo(c => c.Register(model))
                .ShouldRedirectTo<HomeController>(ctr => ctr.Index());
        }
    }
}
