using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using System;

namespace ReadMe.Web.Tests.Controllers.RatingsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRatingServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new RatingsController(null,
                bookServiceMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenBookServiceIsNull()
        {
            var ratingServiceMock = new Mock<IRatingService>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new RatingsController(ratingServiceMock.Object,
                null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAuthenticationProviderIsNull()
        {
            var ratingServiceMock = new Mock<IRatingService>();
            var bookServiceMock = new Mock<IBookService>();

            Assert.Throws<ArgumentNullException>(() => new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var ratingServiceMock = new Mock<IRatingService>();
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.DoesNotThrow(() => new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object));
        }

        [Test]
        public void InitializeRatingsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var ratingServiceMock = new Mock<IRatingService>();
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();

            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
