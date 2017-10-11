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
    public class UpdateRating_Should
    {
        [TestCase(4.25)]
        public void CallRepositoryGetById_WhenInvoked(double rating)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateRating(id, rating);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase(4.25)]
        public void NotCallRepositoryUpdate_WhenBookIsNotFound(double rating)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateRating(id, rating);

            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
        }

        [TestCase(4.25)]
        public void NotCallUnitOfWorkCommit_WhenBookIsNotFound(double rating)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateRating(id, rating);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase(4.25)]
        public void CallRepositoryUpdate_WhenBookIsFound(double rating)
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateRating(id, rating);

            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
        }

        [TestCase(4.25)]
        public void CallUnitOfWorkCommit_WhenBookIsFound(double rating)
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateRating(id, rating);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase(4.25)]
        public void SetRatingCorrectly_WhenBookIsFound(double rating)
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateRating(id, rating);

            Assert.AreEqual(rating, bookMock.Object.Rating);
        }
    }
}
