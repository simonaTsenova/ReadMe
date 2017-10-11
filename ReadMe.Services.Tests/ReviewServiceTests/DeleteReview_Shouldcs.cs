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
    public class DeleteReview_Shouldcs
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            Guid reviewId = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.DeleteReview(reviewId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void NotCallRepositoryDelete_WhenReviewIsNotFound()
        {
            Guid reviewId = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.DeleteReview(reviewId);

            repositoryMock.Verify(r => r.Update(It.IsAny<Review>()), Times.Never);
        }

        [Test]
        public void NotCallUnitOfWorkCommit_WhenReviewIsNotFound()
        {
            Guid reviewId = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.DeleteReview(reviewId);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [Test]
        public void CallRepositoryDelete_WhenReviewIsFound()
        {
            Guid reviewId = Guid.NewGuid();

            var review = new Review();
            review.Id = reviewId;
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(new List<Review> { review }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.DeleteReview(reviewId);

            repositoryMock.Verify(r => r.Delete(It.IsAny<Review>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenReviewIsFound()
        {
            Guid reviewId = Guid.NewGuid();

            var review = new Review();
            review.Id = reviewId;
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.All).Returns(new List<Review> { review }.AsQueryable());
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.DeleteReview(reviewId);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
