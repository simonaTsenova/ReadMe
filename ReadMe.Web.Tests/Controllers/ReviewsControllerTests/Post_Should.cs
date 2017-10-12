using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models.Reviews;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.ReviewsControllerTests
{
    [TestFixture]
    public class Post_Should
    {
        [Test]
        public void ReturnFormReviewPartialView_WhenModelIsNotValid()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);
            controller.ModelState.AddModelError("field", "message");
            var modelMock = new Mock<ReviewViewModel>();

            controller.WithCallTo(c => c.Post(modelMock.Object))
                .ShouldRenderPartialView("_FormReviewPartial")
                .WithModel(modelMock.Object);
        }

        [Test]
        public void CallReviewServiceAddReview_WhenModelIsValid()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);
            var model = new ReviewViewModel()
            {
                UserId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8",
                BookId = Guid.NewGuid(),
                Content = "content"
            };
            var review = new Review() { UserId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8" };
            reviewServiceMock.Setup(s => s.AddReview(model.UserId, model.BookId, model.Content)).Returns(review);

            controller.Post(model);

            reviewServiceMock.Verify(r => r.AddReview(model.UserId, model.BookId, model.Content), Times.Once);
        }

        [Test]
        public void CallCallUserServiceGetUserById_WhenModelIsValid()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);
            var model = new ReviewViewModel()
            {
                UserId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8",
                BookId = Guid.NewGuid(),
                Content = "content"
            };
            var review = new Review() { UserId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8" };
            reviewServiceMock.Setup(s => s.AddReview(model.UserId, model.BookId, model.Content)).Returns(review);

            controller.Post(model);

            userServiceMock.Verify(s => s.GetUserById(review.UserId), Times.Once);
        }

        [Test]
        public void ReturnReviewPartialView_WhenModelIsValid()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userServiceMock = new Mock<IUserService>();
            var controller = new ReviewsController(reviewServiceMock.Object,
                userServiceMock.Object);
            var model = new ReviewViewModel()
            {
                UserId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8",
                BookId = Guid.NewGuid(),
                Content = "content"
            };
            var review = new Review() { UserId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8" };
            reviewServiceMock.Setup(s => s.AddReview(model.UserId, model.BookId, model.Content)).Returns(review);

            controller.WithCallTo(c => c.Post(model))
                .ShouldRenderPartialView("_ReviewPartial")
                .WithModel(model);
        }
    }
}
