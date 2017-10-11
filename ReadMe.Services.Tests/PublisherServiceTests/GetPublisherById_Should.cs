using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.PublisherServiceTests
{
    [TestFixture]
    public class GetPublisherById_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetPublisherById(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnPublisherCorrectly_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var publisher = new Publisher();
            publisher.Id = id;
            var expected = new List<Publisher> { publisher }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            repositoryMock.Setup(r => r.All).Returns(expected);
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetPublisherById(id);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
