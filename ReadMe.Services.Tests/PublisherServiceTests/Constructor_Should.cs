using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.PublisherServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPublisherRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPublisherFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new PublisherService(null, factoryMock.Object, 
                unitOfWorkMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPublisherFactoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new PublisherService(repositoryMock.Object, null,
                unitOfWorkMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new PublisherService(repositoryMock.Object, factoryMock.Object,
                null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object));
        }

        [Test]
        public void InitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            Assert.IsNotNull(service);
        }
    }
}
