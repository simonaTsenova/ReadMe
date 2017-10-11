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
    public class RestorePublisher_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.RestorePublisher(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void NotCallRepositoryUpdate_WhenPublisherIsNotFound()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestorePublisher(id);

            repositoryMock.Verify(r => r.Update(It.IsAny<Publisher>()), Times.Never);
        }

        [Test]
        public void NotCallUnitOfWorkCommit_WhenPublisherIsNotFound()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestorePublisher(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [Test]
        public void CallRepositoryUpdate_WhenPublisherIsFound()
        {
            var id = Guid.NewGuid();

            var publisherMock = new Mock<Publisher>();
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(publisherMock.Object);
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestorePublisher(id);

            repositoryMock.Verify(r => r.Update(It.IsAny<Publisher>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenPublisherIsFound()
        {
            var id = Guid.NewGuid();

            var publisherMock = new Mock<Publisher>();
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(publisherMock.Object);
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestorePublisher(id);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public void SetIsDeletedToFalse_WhenPublisherIsFound()
        {
            var id = Guid.NewGuid();

            var publisherMock = new Mock<Publisher>();
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(publisherMock.Object);
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestorePublisher(id);

            Assert.IsFalse(publisherMock.Object.IsDeleted);
        }

        [Test]
        public void SetDeletedOnCorrectly_WhenPublisherIsFound()
        {
            var id = Guid.NewGuid();

            var publisherMock = new Mock<Publisher>();
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(publisherMock.Object);
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.RestorePublisher(id);

            Assert.IsNull(publisherMock.Object.DeletedOn);
        }
    }
}
