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
    public class GetByUserId_Should
    {
        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void CallRepositoryAll_WhenInvoked(string userId)
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetByUserId(userId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8")]
        public void ReturnFoundReviewCorrectly_WhenInvoked(string userId)
        {
            var review = new Review();
            review.UserId = userId;
            var reviewQueryable = new List<Review> { review }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(reviewQueryable);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetByUserId(userId);

            CollectionAssert.AreEqual(reviewQueryable, result);
        }
    }
}
