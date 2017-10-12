using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using System;

namespace ReadMe.Web.Tests.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenReviewServiceIsNull()
        {
            var userServiceMock = new Mock<IUserService>();

            Assert.Throws<ArgumentNullException>(() => new ReviewsController(null, userServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            var reviewServiceMock = new Mock<IReviewService>();

            Assert.Throws<ArgumentNullException>(() => new ReviewsController(reviewServiceMock.Object, null));
        }

        [Test]
        public void NotThrowArgumentNullException_WhenDependenciesAreNotNull()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userServiceMock = new Mock<IUserService>();

            Assert.DoesNotThrow(() => new ReviewsController(reviewServiceMock.Object, 
                userServiceMock.Object));
        }

        [Test]
        public void InitializeReviewsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
