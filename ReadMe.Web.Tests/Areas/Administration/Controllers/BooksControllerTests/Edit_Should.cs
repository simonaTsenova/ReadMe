using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Edit_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookViewModel>();
            });
        }

        [Test]
        public void CallBookServiceGetBookById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Edit(id);

            bookServiceMock.Verify(s => s.GetBookById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorPartialViee_WhenBookIsNotFound()
        {
            var id = Guid.NewGuid();

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);
            controller.ModelState.AddModelError("field", "message");

            controller.WithCallTo(c => c.Edit(id))
                .ShouldRenderPartialView("Error");
        }

        //[Test]
        //public void CallGenreServiceGetAll_WhenInvoked()
        //{
        //    var id = Guid.NewGuid();

        //    var book = new Book() { Id = id };
        //    var bookServiceMock = new Mock<IBookService>();
        //    bookServiceMock.Setup(s => s.GetBookById(id))
        //        .Returns(new List<Book>() { book }.AsQueryable());
        //    var genreServiceMock = new Mock<IGenreService>();
        //    var authorServiceMock = new Mock<IAuthorService>();
        //    var publisherServiceMock = new Mock<IPublisherService>();
        //    var factoryMock = new Mock<IViewModelFactory>();
        //    var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
        //        authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

        //    controller.Edit(id);

        //    genreServiceMock.Verify(s => s.GetAll(), Times.Once);
        //}

        //[Test]
        //public void ReturnEditBookPartialView_WhenInvoked()
        //{
        //    var id = Guid.NewGuid();

        //    var book = new Book();
        //    var bookServiceMock = new Mock<IBookService>();
        //    bookServiceMock.Setup(s => s.GetBookById(id))
        //       .Returns(new List<Book>() { book }.AsQueryable());
        //    var genreServiceMock = new Mock<IGenreService>();
        //    var authorServiceMock = new Mock<IAuthorService>();
        //    var publisherServiceMock = new Mock<IPublisherService>();
        //    var model = new AddBookViewModel();
        //    var factoryMock = new Mock<IViewModelFactory>();
        //    var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
        //        authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

        //    controller.WithCallTo(c => c.Edit(id))
        //        .ShouldRenderPartialView("_EditBookPartial")
        //        .WithModel<BookViewModel>();
        //}
    }
}
