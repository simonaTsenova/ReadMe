using Moq;
using NUnit.Framework;
using PagedList;
using ReadMe.Authentication.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.UsersControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallUserServiceGetAllAndDeleted_WhenInvoked()
        {
            var page = 1;

            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);

            controller.Index(page);

            userServiceMock.Verify(s => s.GetAllAndDeleted(), Times.Once);
        }

        [Test]
        public void CallAuthenticationProviderIsInRole_WhenInvoked()
        {
            var id = Guid.NewGuid().ToString();
            var page = 1;

            var users = new List<User>()
            {
                new User() { Id = id },
                new User() { Id = id }
            };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetAllAndDeleted()).Returns(users.AsQueryable());
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);

            controller.Index(page);

            providerMock.Verify(v => v.IsInRole(id, It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void CallFactoryCreateUserViewModel_WhenInvoked()
        {
            var id = Guid.NewGuid().ToString();
            var page = 1;

            var users = new List<User>()
            {
                new User() { Id = id },
                new User() { Id = id }
            };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetAllAndDeleted()).Returns(users.AsQueryable());
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);

            controller.Index(page);

            factoryMock.Verify(f => f.CreateUserViewModel(It.IsAny<User>(), It.IsAny<bool>()));
        }

        [Test]
        public void ReturnUsersPartialView_WhenInvoked()
        {
            var id = Guid.NewGuid().ToString();
            var page = 1;
            var count = 15;

            var users = new List<User>()
            {
                new User() { Id = id }
            };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetAllAndDeleted()).Returns(users.AsQueryable());
            var model = new Mock<UserViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateUserViewModel(It.IsAny<User>(), It.IsAny<bool>()))
                .Returns(model.Object);
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);
            var expected = new List<UserViewModel>() { model.Object }
                .ToPagedList(page, count);

            controller.WithCallTo(c => c.Index(page))
                .ShouldRenderPartialView("_UsersPartial")
                .WithModel<IPagedList<UserViewModel>>(
                    m => CollectionAssert.AreEqual(expected, m));
        }
    }
}
