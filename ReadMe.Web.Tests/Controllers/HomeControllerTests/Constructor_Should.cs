using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using System;

namespace ReadMe.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBookServiceIsNull()
        {
            var reviewServiceMock = new Mock<IReviewService>();

            Assert.Throws<ArgumentNullException>(() => new HomeController(null, reviewServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenReviewServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();

            Assert.Throws<ArgumentNullException>(() => new HomeController(bookServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();

            Assert.DoesNotThrow(() => new HomeController(bookServiceMock.Object, reviewServiceMock.Object));
        }

        [Test]
        public void InitializeHomeControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();

            var controller = new HomeController(bookServiceMock.Object, reviewServiceMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
