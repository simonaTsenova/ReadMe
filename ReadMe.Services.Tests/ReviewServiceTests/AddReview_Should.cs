using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.ReviewServiceTests
{
    [TestFixture]
    public class AddReview_Should
    {
        [Test]
        public void CallProviderGetCurrentTime_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            string content = "Lorem ipsum dolor sit";

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddReview(userId, bookId, content);

            providerMock.Verify(v => v.GetCurrentTime(), Times.Once);
        }

        [Test]
        public void CallFactoryCreateReview_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            string content = "Lorem ipsum dolor sit";
            DateTime date = DateTime.UtcNow;

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddReview(userId, bookId, content);

            factoryMock.Verify(f => f.CreateReview(userId, bookId, content, date), Times.Once);
        }

        [Test]
        public void CallRepositoryAdd_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            string content = "Lorem ipsum dolor sit";
            DateTime date = DateTime.UtcNow;

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddReview(userId, bookId, content);

            repositoryMock.Verify(r => r.Add(It.IsAny<Review>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            string content = "Lorem ipsum dolor sit";
            DateTime date = DateTime.UtcNow;

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddReview(userId, bookId, content);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public void ReturnCreatedReviewCorrectly_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            string content = "Lorem ipsum dolor sit";
            DateTime date = DateTime.UtcNow;

            var reviewMock = new Mock<Review>();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            factoryMock.Setup(f => f.CreateReview(userId, bookId, content, date)).Returns(reviewMock.Object);
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.AddReview(userId, bookId, content);

            Assert.AreSame(reviewMock.Object, result);
        }
    }
}
