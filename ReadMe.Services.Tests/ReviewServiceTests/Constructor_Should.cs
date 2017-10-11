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
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new ReviewService(null, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new ReviewService(repositoryMock.Object, null,
                factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFactorykIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var factoryMock = new Mock<IReviewFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void InitializeReviewServiceCorrectly_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            var service = new ReviewService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
