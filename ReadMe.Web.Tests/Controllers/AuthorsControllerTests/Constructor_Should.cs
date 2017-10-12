using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using System;

namespace ReadMe.Web.Tests.Controllers.AuthorsControllerTests
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
        public void ThrowArgumentNullException_WhenBookServiceIsNull()
        {
            var authorServiceMock = new Mock<IAuthorService>();

            Assert.Throws<ArgumentNullException>(() => new AuthorsController(authorServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var authorServiceMock = new Mock<IAuthorService>();

            Assert.DoesNotThrow(() => 
                new AuthorsController(authorServiceMock.Object, bookServiceMock.Object));
        }

        [Test]
        public void SetAuthorsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var authorServiceMock = new Mock<IAuthorService>();

            var controller = new AuthorsController(authorServiceMock.Object, bookServiceMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
