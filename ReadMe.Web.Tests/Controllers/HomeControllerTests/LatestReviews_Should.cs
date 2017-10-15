using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Reviews;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.HomeControllerTests
{

    public class LatestReviews_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Review, ReviewViewModel>();
            });
        }

        [Test]
        public void CallReviewServiceGetLatestReviews_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var controller = new HomeController(bookServiceMock.Object, reviewServiceMock.Object);

            controller.LatestReviews();

            reviewServiceMock.Verify(s => s.GetLatestReviews(), Times.Once);
        }

        [Test]
        public void ReturnViewCorrectly_WhenInvoked()
        {
            var reviews = new List<Review>()
            {
                new Review()
            };
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetLatestReviews()).Returns(reviews.AsQueryable());
            var controller = new HomeController(bookServiceMock.Object, reviewServiceMock.Object);
            var expectedModel = new List<ReviewViewModel>()
            {
                new ReviewViewModel()
            };

            controller.WithCallTo(c => c.LatestReviews())
                .ShouldRenderPartialView("_ReviewsPartial")
                .WithModel<IList<ReviewViewModel>>(
                    m => Assert.AreEqual(expectedModel.Count, m.Count));
        }
    }
}
