using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallBookServiceGetBooksByAuthor_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.Delete(id, page);

            bookServiceMock.Verify(s => s.GetBooksByAuthor(id), Times.Once);
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
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetBooksByAuthor(id)).Returns(books);
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.Delete(id, page);

            bookServiceMock.Verify(s => s.DeleteBook(It.IsAny<Guid>()), Times.Exactly(2));
        }

        [Test]
        public void CallAuthorServiceDeleteAuthor_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.Delete(id, page);

            authorServiceMock.Verify(s => s.DeleteAuthor(id), Times.Once);
        }

        [Test]
        public void RedirectToControllerIndexActions_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var page = 1;

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Delete(id, page))
                .ShouldRedirectTo(c => c.Index(page));
        }
    }
}
