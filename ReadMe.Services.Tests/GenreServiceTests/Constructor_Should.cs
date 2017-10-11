using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using System;

namespace ReadMe.Services.Tests.GenreServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenGenreRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IGenreFactory>();

            Assert.Throws<ArgumentNullException>(() => new GenreService(null, factoryMock.Object, unitOfWorkMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();

            Assert.Throws<ArgumentNullException>(() => new GenreService(repositoryMock.Object, factoryMock.Object, null));
        }

        [Test]
        public void ThrowArgumentNullException_WhenGenreFactoryIsNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new GenreService(repositoryMock.Object, null, unitOfWorkMock.Object));
        }

        [Test]
        public void NotThrow_WhenDependanciesAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.DoesNotThrow(() => new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object));
        }

        [Test]
        public void InitializeDependenciesCorrectly_WhenTheyAreNotNull()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            Assert.IsNotNull(service);
        }
    }
}
