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
    public class RestoreBook_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.RestoreBook(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void NotCallRepositoryUpdate_WhenBookIsNotFound()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreBook(id);

            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
        }

        [Test]
        public void NotCallUnitOfWorkCommit_WhenBookIsNotFound()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Book>>();
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreBook(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [Test]
        public void CallRepositoryUpdate_WhenBookIsFound()
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreBook(id);

            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenBookIsFound()
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreBook(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public void SetIsDeletedToFalse_WhenBookIsFound()
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreBook(id);

            Assert.IsFalse(bookMock.Object.IsDeleted);
        }

        [Test]
        public void SetDeletedOnCorrectly_WhenBookIsFound()
        {
            var id = Guid.NewGuid();

            var bookMock = new Mock<Book>();
            var repositoryMock = new Mock<IEfRepository<Book>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(bookMock.Object);
            var factoryMock = new Mock<IBookFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new BookService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreBook(id);

            Assert.IsNull(bookMock.Object.DeletedOn);
        }
    }
}
