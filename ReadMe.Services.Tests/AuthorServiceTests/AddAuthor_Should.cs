using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services.Tests.AuthorServiceTests
{
    [TestFixture]
    public class AddAuthor_Should
    {
        [TestCase("John", "Grisham", "American", 54, "biography", "website")]
        public void CallFactoryCreateAuthor_WhenInvoked(string firstName, string lastName, string nationality, 
            int age, string biography, string website)
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddAuthor(firstName, lastName, nationality, age, biography, website);

            factoryMock.Verify(f => f.CreateAuthor(firstName, lastName, nationality, age, biography, website), Times.Once);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website")]
        public void CallRepositoryAdd_WhenInvoked(string firstName, string lastName, string nationality,
            int age, string biography, string website)
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddAuthor(firstName, lastName, nationality, age, biography, website);

            repositoryMock.Verify(r => r.Add(It.IsAny<Author>()), Times.Once);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website")]
        public void CallUnitOfWorkCommit_WhenInvoked(string firstName, string lastName, string nationality,
            int age, string biography, string website)
        {
            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.AddAuthor(firstName, lastName, nationality, age, biography, website);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
