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
    public class GetByUserIdAndBookId_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var bookId = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetByUserIdAndBookId(userId, bookId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnFoundReviewCorrectly_WhenInvoked()
        {
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var bookId = Guid.NewGuid();

            var review = new Review();
            review.UserId = userId;
            review.BookId = bookId;
            var reviews = new List<Review> { review }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(reviews);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetByUserIdAndBookId(userId, bookId);

            Assert.AreSame(review, result);
        }
    }
}
