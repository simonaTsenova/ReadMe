using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;

namespace ReadMe.Services.Tests.GenreServiceTests
{
    [TestFixture]
    public class AddGenre_Should
    {
        [TestCase("mystery")]
        public void CallFactoryCreateGenre_WhenInvoked(string name)
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.AddGenre(name);

            factoryMock.Verify(f => f.CreateGenre(name), Times.Once);
        }

        [TestCase("mystery")]
        public void CallRepositoryAdd_WhenInvoked(string name)
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.AddGenre(name);

            repositoryMock.Verify(r => r.Add(It.IsAny<Genre>()), Times.Once);
        }

        [TestCase("mystery")]
        public void CallUnitOfWorkCommit_WhenInvoked(string name)
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.AddGenre(name);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
