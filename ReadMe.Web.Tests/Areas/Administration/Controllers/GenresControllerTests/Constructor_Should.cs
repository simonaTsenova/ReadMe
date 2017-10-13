using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using System;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.GenresControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new GenresController(null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var genreServiceMock = new Mock<IGenreService>();

            Assert.DoesNotThrow(() => new GenresController(genreServiceMock.Object));
        }

        [Test]
        public void InitializeGenresControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var genreServiceMock = new Mock<IGenreService>();

            var controller = new GenresController(genreServiceMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
