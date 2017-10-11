using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.AuthorServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAuthorRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IAuthorFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new AuthorService(null, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAuthorFactoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new AuthorService(repositoryMock.Object, null,
                unitOfWorkMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new AuthorService(repositoryMock.Object, factoryMock.Object,
                null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object));
        }

        [Test]
        public void InitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            Assert.IsNotNull(service);
        }
    }
}
