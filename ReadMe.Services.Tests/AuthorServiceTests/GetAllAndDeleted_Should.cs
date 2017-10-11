using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.AuthorServiceTests
{
    [TestFixture]
    public class GetAllAndDeleted_Should
    {
        [Test]
        public void CallRepositoryAllAndDeleted_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetAllAndDeleted();

            repositoryMock.Verify(r => r.AllAndDeleted, Times.Once);
        }

        [Test]
        public void ReturnAuthorsCorrectly_WhenInvoked()
        {
            var authorMock = new Mock<Author>();
            var expected = new List<Author> { authorMock.Object }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.AllAndDeleted).Returns(expected);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetAllAndDeleted();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
