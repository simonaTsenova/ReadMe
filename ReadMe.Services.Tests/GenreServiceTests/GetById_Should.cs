using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using System;

namespace ReadMe.Services.Tests.GenreServiceTests
{
    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallRepositoryGetById_WhenInvoked()
        {
            Guid id = Guid.NewGuid();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.GetById(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void ReturnCorrectGenre_WhenFound()
        {
            Guid id = Guid.NewGuid();
            var genreMock = new Mock<Genre>();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            repositoryMock.Setup(r => r.GetById(id)).Returns(genreMock.Object);
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            var result = service.GetById(id);

            Assert.AreSame(genreMock.Object, result);
        }
    }
}
