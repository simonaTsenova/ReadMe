using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using System;

namespace ReadMe.Services.Tests.GenreServiceTests
{
    [TestFixture]
    public class UpdateGenre_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            Guid id = Guid.NewGuid();
            string name = "thriller";

            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.UpdateGenre(id, name);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void NotCallRepositoryUpdate_WhenGenreIsNotFound()
        {
            Guid id = Guid.NewGuid();
            string name = "thriller";

            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.UpdateGenre(id, name);

            repositoryMock.Verify(r => r.Update(It.IsAny<Genre>()), Times.Never);
        }

        [Test]
        public void NotCallUnitOfWorkCommit_WhenGenreIsNotFound()
        {
            Guid id = Guid.NewGuid();
            string name = "thriller";

            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.UpdateGenre(id, name);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [Test]
        public void SetGenreNameCorrectly_WhenGenreIsFound()
        {
            Guid id = Guid.NewGuid();
            string name = "thriller";

            var genreMock = new Mock<Genre>();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(genreMock.Object);
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.UpdateGenre(id, name);

            Assert.AreEqual(name, genreMock.Object.Name);
        }

        [Test]
        public void CallRepositoryUpdate_WhenGenreIsFound()
        {
            Guid id = Guid.NewGuid();
            string name = "thriller";

            var genreMock = new Mock<Genre>();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(genreMock.Object);
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.UpdateGenre(id, name);

            repositoryMock.Verify(r => r.Update(It.IsAny<Genre>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_WhenGenreIsFound()
        {
            Guid id = Guid.NewGuid();
            string name = "thriller";

            var genreMock = new Mock<Genre>();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(genreMock.Object);
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.UpdateGenre(id, name);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
