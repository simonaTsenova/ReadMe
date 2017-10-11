using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;

namespace ReadMe.Services.Tests.BookServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBookRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new BookService(null, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenBookFactoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new BookService(repositoryMock.Object, null,
                unitOfWorkMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var providerMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => new BookService(repositoryMock.Object, factoryMock.Object,
                null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            Assert.DoesNotThrow(() => new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object));
        }

        [Test]
        public void InitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            Assert.IsNotNull(service);
        }
    }
}
