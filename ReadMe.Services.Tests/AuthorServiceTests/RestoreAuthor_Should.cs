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
    public class RestoreAuthor_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.RestoreAuthor(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void NotCallRepositoryUpdate_WhenAuthorIsNotFound()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreAuthor(id);

            repositoryMock.Verify(r => r.Update(It.IsAny<Author>()), Times.Never);
        }

        [Test]
        public void NotCallUnitOfWorkCommit_WhenAuthorIsNotFound()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreAuthor(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [Test]
        public void CallRepositoryUpdate_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreAuthor(id);

            repositoryMock.Verify(r => r.Update(It.IsAny<Author>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreAuthor(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public void SetIsDeletedToFalse_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreAuthor(id);

            Assert.IsFalse(authorMock.Object.IsDeleted);
        }

        [Test]
        public void SetDeletedOnCorrectly_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestoreAuthor(id);

            Assert.IsNull(authorMock.Object.DeletedOn);
        }
    }
}
