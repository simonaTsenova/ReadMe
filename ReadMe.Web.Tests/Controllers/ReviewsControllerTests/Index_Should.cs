using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallReviewServiceGetById_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var username = "test";

            var review = new Review()
            {
                User = new User() { UserName = username }
            };
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetById(id)).Returns(review);
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);

            controller.Index(id);

            reviewServiceMock.Verify(s => s.GetById(id), Times.Once);
        }

        [Test]
        public void CallReviewServiceDeleteReview_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var username = "test";

            var review = new Review()
            {
                User = new User() { UserName = username }
            };
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetById(id)).Returns(review);
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);

            controller.Index(id);

            reviewServiceMock.Verify(s => s.DeleteReview(id), Times.Once);
        }

        [Test]
        public void RedirectToProfileControllerDetailsAction_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var username = "test";

            var review = new Review()
            {
                User = new User() { UserName = username }
            };
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetById(id)).Returns(review);
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);

            controller.WithCallTo(c => c.Index(id))
                .ShouldRedirectTo<ProfileController>(ctr => ctr.Details(username));
        }
    }
}
