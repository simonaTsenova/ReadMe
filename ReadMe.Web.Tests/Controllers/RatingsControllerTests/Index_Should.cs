using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models.Books;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.RatingsControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void CallAuthenticationProviderCurrentUserId_WhenInvoked()
        {
            var ratingServiceMock = new Mock<IRatingService>();
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var modelMock = new Mock<BookInfoViewModel>();

            controller.Index(modelMock.Object);

            providerMock.Verify(v => v.CurrentUserId, Times.Once);
        }

        [Test]
        public void CallRatingServiceGetByBookIdAndUserId_WhenInvoked()
        {
            var bookId = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var ratingServiceMock = new Mock<IRatingService>();
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var model = new BookInfoViewModel()
            {
                Id = bookId
            };

            controller.Index(model);

            ratingServiceMock.Verify(s => s.GetByBookIdAndUserId(bookId, userId), Times.Once);
        }

        [Test]
        public void CallRatingServiceAddRating_WhenRatingIsNotFound()
        {
            var bookId = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var stars = 4;

            var ratingServiceMock = new Mock<IRatingService>();
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var model = new BookInfoViewModel()
            {
                Id = bookId,
                Rating = stars
            };

            controller.Index(model);

            ratingServiceMock.Verify(s => s.AddRating(bookId,userId, stars), Times.Once);
        }

        [Test]
        public void CallRatingServiceUpdateRating_WhenRatingIsFound()
        {
            var bookId = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var stars = 4;

            var rating = new Mock<Rating>();
            var ratingServiceMock = new Mock<IRatingService>();
            ratingServiceMock.Setup(s => s.GetByBookIdAndUserId(bookId, userId)).Returns(rating.Object);
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var model = new BookInfoViewModel()
            {
                Id = bookId,
                Rating = stars
            };

            controller.Index(model);

            ratingServiceMock.Verify(s => s.UpdateRating(bookId, userId, stars), Times.Once);
        }

        [Test]
        public void CallRatingServiceGetAll_WhenInvoked()
        {
            var bookId = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var stars = 4;

            var rating = new Mock<Rating>();
            var ratingServiceMock = new Mock<IRatingService>();
            ratingServiceMock.Setup(s => s.GetByBookIdAndUserId(bookId, userId)).Returns(rating.Object);
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var model = new BookInfoViewModel()
            {
                Id = bookId,
                Rating = stars
            };

            controller.Index(model);

            ratingServiceMock.Verify(s => s.GetAll(), Times.Once);
        }

        [Test]
        public void CallBookServiceUpdateRating_WhenInvoked()
        {
            var bookId = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var stars = 4;

            var rating = new Mock<Rating>();
            var ratingServiceMock = new Mock<IRatingService>();
            ratingServiceMock.Setup(s => s.GetByBookIdAndUserId(bookId, userId)).Returns(rating.Object);
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var model = new BookInfoViewModel()
            {
                Id = bookId,
                Rating = stars
            };

            controller.Index(model);

            bookServiceMock.Verify(s => s.UpdateRating(bookId, It.IsAny<double>()), Times.Once);
        }

        [Test]
        public void RedirectToCorrectAction_WhenInvoked()
        {
            var bookId = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var stars = 4;

            var rating = new Mock<Rating>();
            var ratingServiceMock = new Mock<IRatingService>();
            ratingServiceMock.Setup(s => s.GetByBookIdAndUserId(bookId, userId)).Returns(rating.Object);
            var bookServiceMock = new Mock<IBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new RatingsController(ratingServiceMock.Object,
                bookServiceMock.Object, providerMock.Object);
            var model = new BookInfoViewModel()
            {
                Id = bookId,
                Rating = stars
            };

            controller.WithCallTo(c => c.Index(model))
                .ShouldRedirectTo<BooksController>(ctr => ctr.Details(bookId));
        }
    }
}
