using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.RatingServiceTests
{
    [TestFixture]
    public class GetByBookIdAndUserId_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetByBookIdAndUserId(bookId, userId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnRatingCorrectly_WhenFound()
        {
            Guid bookId = Guid.NewGuid();
            string userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var rating = new Rating();
            rating.BookId = bookId;
            rating.UserId = userId;
            var ratings = new List<Rating> { rating }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            repositoryMock.Setup(r => r.All).Returns(ratings);
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetByBookIdAndUserId(bookId, userId);

            Assert.AreSame(rating, result);
        }
    }
}
