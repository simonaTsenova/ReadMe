using System;
using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenAuthorServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();

            Assert.Throws<ArgumentNullException>(() => new AuthorsController(null, bookServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenBookServiceServiceIsNull()
        {
            var authorServiceMock = new Mock<IAuthorService>();

            Assert.Throws<ArgumentNullException>(() => new AuthorsController(authorServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();

            Assert.DoesNotThrow(() => new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object));
        }

        [Test]
        public void InitializeAuthorsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();

            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
