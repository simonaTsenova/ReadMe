using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallBookServiceGetBooksByPublisher_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.Delete(id, page);

            bookServiceMock.Verify(s => s.GetBooksByPublisher(id), Times.Once);
        }

        [Test]
        public void CallBookServiceDeleteBook_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var books = new List<Book>()
            {
                new Book(),
                new Book()
            }.AsQueryable();
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetBooksByPublisher(id)).Returns(books);
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.Delete(id, page);

            bookServiceMock.Verify(s => s.DeleteBook(It.IsAny<Guid>()), Times.Exactly(2));
        }

        [Test]
        public void CallPublisherServiceDeletePublisher_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.Delete(id, page);

            publisherServiceMock.Verify(s => s.DeletePublisher(id), Times.Once);
        }

        [Test]
        public void RedirectToControllerIndexAction_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Delete(id, page))
                .ShouldRedirectTo(c => c.Index(page));
        }
    }
}
