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
    public class DeleteAuthor_Should
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

            service.DeleteAuthor(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void CallProviderGetCurrentTime_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();

            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.DeleteAuthor(id);

            providerMock.Verify(v => v.GetCurrentTime(), Times.Once);
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

            service.DeleteAuthor(id);

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

            service.DeleteAuthor(id);

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

            service.DeleteAuthor(id);

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

            service.DeleteAuthor(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public void SetIsDeletedToTrue_WhenAuthorIsFound()
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

            service.DeleteAuthor(id);

            Assert.IsTrue(authorMock.Object.IsDeleted);
        }

        [Test]
        public void SetDeletedOnCorrectly_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();
            var date = DateTime.Now;

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            providerMock.Setup(v => v.GetCurrentTime()).Returns(date);
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.DeleteAuthor(id);

            Assert.AreEqual(date, authorMock.Object.DeletedOn);
        }
    }
}
