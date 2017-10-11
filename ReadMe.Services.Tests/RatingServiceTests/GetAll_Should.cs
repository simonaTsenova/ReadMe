using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.RatingServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectRatings_WhenInvoked()
        {
            var ratingMock = new Mock<Rating>();
            var ratings = new List<Rating> { ratingMock.Object }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            repositoryMock.Setup(r => r.All).Returns(ratings);
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetAll();

            CollectionAssert.AreEqual(ratings, result);
        }
    }
}
