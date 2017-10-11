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
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IRatingFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new RatingService(null, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFactoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new RatingService(repositoryMock.Object, null,
                factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void InitializeRatingServiceCorrectly_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Rating>>();
            var factoryMock = new Mock<IRatingFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            var service = new RatingService(repositoryMock.Object, unitOfWorkMock.Object,
                factoryMock.Object, providerMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
