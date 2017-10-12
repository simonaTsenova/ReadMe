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
    public class GetById_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            Guid id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            service.GetById(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void ReturnCorrectReview_WhenInvoked()
        {
            Guid id = Guid.NewGuid();

            var reviewMock = new Mock<Review>();
            var repositoryMock = new Mock<IEfRepository<Review>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(reviewMock.Object);
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            var result = service.GetById(id);

            Assert.AreSame(reviewMock.Object, result);
        }
    }
}
