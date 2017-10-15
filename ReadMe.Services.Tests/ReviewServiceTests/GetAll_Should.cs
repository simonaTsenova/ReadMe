using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.ReviewServiceTests
{
    [TestFixture]
    public class GetAll_Should
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

            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectReviews_WhenInvoked()
        {
            var reviewMock = new Mock<Review>();
            var reviews = new List<Review> { reviewMock.Object }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(reviews);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetAll();

            CollectionAssert.AreEqual(reviews, result);
        }
    }
}
