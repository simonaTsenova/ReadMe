using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Factories;
using ReadMe.Web.Controllers;
using System;

namespace ReadMe.Web.Tests.Controllers.AcoountControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenProviderIsNull()
        {
            var factoryMock = new Mock<IUserFactory>();

            Assert.Throws<ArgumentNullException>(() => new AccountController(null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserFactoryIsNull()
        {
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new AccountController(providerMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IUserFactory>();

            Assert.DoesNotThrow(() => new AccountController(providerMock.Object, factoryMock.Object));
        }

        [Test]
        public void InitializeAccountControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IUserFactory>();

            var controller = new AccountController(providerMock.Object, factoryMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
