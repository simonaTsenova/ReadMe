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
    public class GetByBookId_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetByBookId(bookId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnFoundReviewCorrectly_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();

            var review = new Review();
            review.BookId = bookId;
            var reviewQueryable = new List<Review> { review }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(reviewQueryable);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetByBookId(bookId);

            CollectionAssert.AreEqual(reviewQueryable, result);
        }
    }
}
