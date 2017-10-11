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
    public class GetAuthorById_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetAuthorById(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnAuthorCorrectly_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var author = new Author();
            author.Id = id;
            var expected = new List<Author> { author }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.All).Returns(expected);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetAuthorById(id);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
