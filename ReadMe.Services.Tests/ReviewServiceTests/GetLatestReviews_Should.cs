using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.ReviewServiceTests
{
    [TestFixture]
    public class GetLatestReviews_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetLatestReviews();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectCountOfReviews_WhenInvoked()
        {
            var reviews = new List<Review>
            {
                new Review { PostedOn = new DateTime(2017, 1, 18) },
                new Review { PostedOn = new DateTime(2017, 4, 18) },
                new Review { PostedOn = new DateTime(2017, 2, 18) },
                new Review { PostedOn = new DateTime(2017, 12, 18) },
                new Review { PostedOn = new DateTime(2017, 9, 18) },
                new Review { PostedOn = new DateTime(2017, 1, 18) },
            }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(reviews);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetLatestReviews();

            Assert.AreEqual(4, result.ToList().Count());
        }

        [Test]
        public void ReturnReviewsCorrectly_WhenInvoked()
        {
            var reviews = new List<Review>
            {
                new Review { PostedOn = new DateTime(2017, 1, 18) },
                new Review { PostedOn = new DateTime(2017, 4, 18) },
                new Review { PostedOn = new DateTime(2017, 2, 18) },
                new Review { PostedOn = new DateTime(2017, 12, 18) },
                new Review { PostedOn = new DateTime(2017, 9, 18) },
                new Review { PostedOn = new DateTime(2017, 1, 18) },
            };
            var reviewsQueryable = reviews.AsQueryable();
            var expected = new List<Review>
            {
                reviews[3], reviews[4], reviews[1], reviews[2]
            }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(reviewsQueryable);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetLatestReviews();

            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
