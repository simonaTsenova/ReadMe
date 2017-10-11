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
    public class UpdatePublisher_Should
    {
        [TestCase("Penguin books", "Jane Doe", "0878123456", "Sofia", "150 Vitosha blvd",
            "Bulgaria", "test", "logo")]
        public void CallRepositoryGetById_WhenInvoked(string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdatePublisher(id, name, owner, phone, city, address, country, website, logoUrl);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("Penguin books", "Jane Doe", "0878123456", "Sofia", "150 Vitosha blvd",
            "Bulgaria", "test", "logo")]
        public void NotCallRepositoryUpdate_WhenPublisherIsNotFound(string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.UpdatePublisher(id, name, owner, phone, city, address, country, website, logoUrl);

            repositoryMock.Verify(r => r.Update(It.IsAny<Publisher>()), Times.Never);
        }

        [TestCase("Penguin books", "Jane Doe", "0878123456", "Sofia", "150 Vitosha blvd",
            "Bulgaria", "test", "logo")]
        public void NotCallUnitOfWorkCommit_WhenPublisherIsNotFound(string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var providerMock = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, providerMock.Object);

            service.UpdatePublisher(id, name, owner, phone, city, address, country, website, logoUrl);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("Penguin books", "Jane Doe", "0878123456", "Sofia", "150 Vitosha blvd",
            "Bulgaria", "test", "logo")]
        public void CallRepositoryUpdate_WhenPublisherIsFound(string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
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

            service.UpdatePublisher(id, name, owner, phone, city, address, country, website, logoUrl);

            repositoryMock.Verify(r => r.Update(It.IsAny<Publisher>()), Times.Once);
        }

        [TestCase("Penguin books", "Jane Doe", "0878123456", "Sofia", "150 Vitosha blvd",
            "Bulgaria", "test", "logo")]
        public void CallUnitOfWorkCommit_WhenPublisherIsFound(string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
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

            service.UpdatePublisher(id, name, owner, phone, city, address, country, website, logoUrl);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase("Penguin books", "Jane Doe", "0878123456", "Sofia", "150 Vitosha blvd",
            "Bulgaria", "test", "logo")]
        public void ShouldSetPublisherPropertiesCorrectly_WhenPublisherIsFound(string name, string owner, string phone, string city,
            string address, string country, string website, string logoUrl)
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

            service.UpdatePublisher(id, name, owner, phone, city, address, country, website, logoUrl);

            Assert.AreEqual(name, publisherMock.Object.Name);
            Assert.AreEqual(owner, publisherMock.Object.Owner);
            Assert.AreEqual(owner, publisherMock.Object.Owner);
            Assert.AreEqual(city, publisherMock.Object.City);
            Assert.AreEqual(address, publisherMock.Object.Address);
            Assert.AreEqual(country, publisherMock.Object.Country);
            Assert.AreEqual(website, publisherMock.Object.Website);
            Assert.AreEqual(logoUrl, publisherMock.Object.LogoUrl);
        }
    }
}
