using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using TestStack.FluentMVCTesting;
using System;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.UsersControllerTests
{
    [TestFixture]
    public class AddAdmin_Should
    {
        [Test]
        public void CallProviderAddToRole_WhenInvoked()
        {
            var id = Guid.NewGuid().ToString();
            var page = 1;

            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);

            controller.AddAdmin(id, page);

            providerMock.Verify(v => v.AddToRole(id, "Admin"), Times.Once);
        }

        [Test]
        public void RedirectToControllerIndexAction_WhenInvoked()
        {
            var id = Guid.NewGuid().ToString();
            var page = 1;

            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);

            controller.WithCallTo(c => c.AddAdmin(id, page))
                .ShouldRedirectTo(c => c.Index(page));
        }
    }
}
