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
    public class GetAuthorByName_Should
    {
        [TestCase("John", "Grisham")]
        public void CallRepositoryAll_WhenInvoked(string firstName, string lastName)
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.GetAuthorByName(firstName, lastName);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("John", "Grisham")]
        public void ReturnAuthorCorrectly_WhenInvoked(string firstName, string lastName)
        {
            var author = new Author();
            author.FirstName = firstName;
            author.LastName = lastName;
            var expected = new List<Author> { author }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.All).Returns(expected);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            var result = service.GetAuthorByName(firstName, lastName);

            Assert.AreSame(author, result);
        }
    }
}
