using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Factories;
using ReadMe.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Tests.GenreServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void CallRepositoryAll_WhenInvoked()
        {
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectGenres_WhenInvoked()
        {
            var genre = new Mock<Genre>();
            var genres = new List<Genre> { genre.Object }.AsQueryable();
            var repositoryMock = new Mock<IEfRepository<Genre>>();
            repositoryMock.Setup(r => r.All).Returns(genres);
            var factoryMock = new Mock<IGenreFactory>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new GenreService(repositoryMock.Object, factoryMock.Object, unitOfWorkMock.Object);

            var result = service.GetAll();

            CollectionAssert.AreEqual(genres, result);            
        }
    }
}
