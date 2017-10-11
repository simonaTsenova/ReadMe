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
    public class GetPublisherByName_Should
    {
        [TestCase("Penguin books")]
        public void CallRepositoryAll_WhenInvoked(string name)
        {
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetPublisherByName(name);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Penguin books")]
        public void ReturnPublisherCorrectly_WhenInvoked(string name)
        {
            var publisher = new Publisher();
            publisher.Name = name;
            var expected = new List<Publisher> { publisher }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Publisher>>();
            repositoryMock.Setup(r => r.All).Returns(expected);
            var factoryMock = new Mock<IPublisherFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new PublisherService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetPublisherByName(name);

            Assert.AreSame(publisher, result);
        }
    }
}
