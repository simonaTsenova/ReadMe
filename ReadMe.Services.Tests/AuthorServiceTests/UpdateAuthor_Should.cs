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
    public class UpdateAuthor_Should
    {
        [TestCase("John", "Grisham", "American", 54, "biography", "website", "photoUrl")]
        public void CallRepositoryGetById_WhenInvoked(string firstName, string lastName, string nationality, 
            int age, string biography, string website, string photoUrl)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateAuthor(id, firstName, lastName, nationality, age, biography, website, photoUrl);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website", "photoUrl")]
        public void NotCallRepositoryUpdate_WhenAuthorIsNotFound(string firstName, string lastName, string nationality,
            int age, string biography, string website, string photoUrl)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateAuthor(id, firstName, lastName, nationality, age, biography, website, photoUrl);

            repositoryMock.Verify(r => r.Update(It.IsAny<Author>()), Times.Never);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website", "photoUrl")]
        public void NotCallUnitOfWorkCommit_WhenAuthorIsNotFound(string firstName, string lastName, string nationality,
            int age, string biography, string website, string photoUrl)
        {
            var id = Guid.NewGuid();

            var repositoryMock = new Mock<IEfRepository<Author>>();
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateAuthor(id, firstName, lastName, nationality, age, biography, website, photoUrl);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website", "photoUrl")]
        public void CallRepositoryUpdate_WhenAuthorIsFound(string firstName, string lastName, string nationality,
            int age, string biography, string website, string photoUrl)
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateAuthor(id, firstName, lastName, nationality, age, biography, website, photoUrl);

            repositoryMock.Verify(r => r.Update(It.IsAny<Author>()), Times.Once);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website", "photoUrl")]
        public void CallUnitOfWorkCommit_WhenAuthorIsFound(string firstName, string lastName, string nationality,
            int age, string biography, string website, string photoUrl)
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateAuthor(id, firstName, lastName, nationality, age, biography, website, photoUrl);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [TestCase("John", "Grisham", "American", 54, "biography", "website", "photoUrl")]
        public void ShouldSetAuthorCorrectly_WhenAuthorIsFound(string firstName, string lastName, string nationality,
            int age, string biography, string website, string photoUrl)
        {
            var id = Guid.NewGuid();

            var authorMock = new Mock<Author>();
            var repositoryMock = new Mock<IEfRepository<Author>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(authorMock.Object);
            var factoryMock = new Mock<IAuthorFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();
            var service = new AuthorService(repositoryMock.Object, factoryMock.Object,
                unitOfWorkMock.Object, provider.Object);

            service.UpdateAuthor(id, firstName, lastName, nationality, age, biography, website, photoUrl);

            Assert.AreEqual(firstName, authorMock.Object.FirstName);
            Assert.AreEqual(firstName, authorMock.Object.FirstName);
            Assert.AreEqual(nationality, authorMock.Object.Nationality);
            Assert.AreEqual(age, authorMock.Object.Age);
            Assert.AreEqual(biography, authorMock.Object.Biography);
            Assert.AreEqual(website, authorMock.Object.Website);
            Assert.AreEqual(photoUrl, authorMock.Object.PhotoUrl);
        }
    }
}
