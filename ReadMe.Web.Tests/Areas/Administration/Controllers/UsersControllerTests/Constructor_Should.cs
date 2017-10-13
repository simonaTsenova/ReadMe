using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using System;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.UsersControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() =>
                new UsersController(null, factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenViewModelFactoryIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() =>
                new UsersController(userServiceMock.Object, null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAuthenticationProviderIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() =>
                new UsersController(userServiceMock.Object, factoryMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.DoesNotThrow(() =>
                new UsersController(userServiceMock.Object,
                    factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void InitializeUsersControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            var controller = new UsersController(userServiceMock.Object,
                factoryMock.Object, providerMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
