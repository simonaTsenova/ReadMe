using System;
using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPublisherServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();

            Assert.Throws<ArgumentNullException>(() => new PublishersController(null, bookServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenBookServiceServiceIsNull()
        {
            var publisherServiceMock = new Mock<IPublisherService>();

            Assert.Throws<ArgumentNullException>(() => new PublishersController(publisherServiceMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();

            Assert.DoesNotThrow(() => new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object));
        }

        [Test]
        public void InitializeAuthorsControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();

            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
