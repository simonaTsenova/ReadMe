using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services.Tests.PublisherServiceTests
{
    [TestFixture]
    public class AddPublisher_Should
    {
        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd", "Bulgaria", "www.peng.com")]
        public void CallFactoryCreatePublisher_WhenInvoked(string name, string owner, string phone, string city, 
            string address, string country, string website)
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddPublisher(name, owner, phone, city, address, country, website);

            factoryMock.Verify(f => f.CreatePublisher(name, owner, phone, city, address, country, website), Times.Once);
        }

        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd", "Bulgaria", "www.peng.com")]
        public void CallRepositoryAdd_WhenInvoked(string name, string owner, string phone, string city,
            string address, string country, string website)
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddPublisher(name, owner, phone, city, address, country, website);

            repositoryMock.Verify(r => r.Add(It.IsAny<Publisher>()), Times.Once);
        }

        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd", "Bulgaria", "www.peng.com")]
        public void CallUnitOfWorkCommit_WhenInvoked(string name, string owner, string phone, string city,
            string address, string country, string website)
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddPublisher(name, owner, phone, city, address, country, website);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
