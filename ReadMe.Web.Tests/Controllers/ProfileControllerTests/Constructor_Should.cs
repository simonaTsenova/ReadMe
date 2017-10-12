using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using System;

namespace ReadMe.Web.Tests.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAuthProviderIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();

            Assert.Throws<ArgumentNullException>(() => new ProfileController(null, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            var providerMock = new Mock<IAuthenticationProvider>();
            var reviewServiceMock = new Mock<IReviewService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();

            Assert.Throws<ArgumentNullException>(() => new ProfileController(providerMock.Object, null,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenReviewServiceIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();

            Assert.Throws<ArgumentNullException>(() => new ProfileController(providerMock.Object, userServiceMock.Object,
                null, factoryMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenViewModelFactoryIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var reviewServiceMock = new Mock<IReviewService>();
            var mapperMock = new Mock<IMapper>();

            Assert.Throws<ArgumentNullException>(() => new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMapperIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenPassedDependenciesAreNotNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();

            Assert.DoesNotThrow(() => new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object));
        }

        [Test]
        public void InitializeControllerCorrectly_WhenPassedDependenciesAreNotNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();

            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
