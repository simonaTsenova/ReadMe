using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.RatingServiceTests
{
    [TestFixture]
    public class AddRating_Should
    {
        [Test]
        public void CallProviderGetCurrentTime_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            int stars = 5;

            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddRating(bookId, userId, stars);

            providerMock.Verify(v => v.GetCurrentTime(), Times.Once);
        }

        [Test]
        public void CallFactoryCreateRating_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            int stars = 5;
            DateTime date = DateTime.UtcNow;

            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddRating(bookId, userId, stars);

            factoryMock.Verify(f => f.CreateRating(bookId, userId, stars, date), Times.Once);
        }

        [Test]
        public void CallRepositoryAdd_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            int stars = 5;
            DateTime date = DateTime.UtcNow;

            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddRating(bookId, userId, stars);

            repositoryMock.Verify(r => r.Add(It.IsAny<Rating>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            int stars = 5;
            DateTime date = DateTime.UtcNow;

            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.AddRating(bookId, userId, stars);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
