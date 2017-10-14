using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using ReadMe.Web.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.BooksControllerTests
{
    [TestFixture]
    public class PostEdit_Should
    {
        [Test]
        public void ReturnEditBookPartialView_WhenModelIsNotValid()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new BookViewModel();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);
            controller.ModelState.AddModelError("field", "message");

            controller.WithCallTo(c => c.Edit(model))
                .ShouldRenderPartialView("_EditBookPartial")
                .WithModel<BookViewModel>();
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books", "photo")]
        public void CallGenreServiceGetById_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher, string photoUrl)
        {
            var date = DateTime.Now;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel()
            {
                Author = author,
                ISBN = isbn,
                Title = title,
                Summary = summary,
                Language = language,
                Publisher = publisher,
                Published = date,
                PhotoUrl = photoUrl,
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Edit(model);

            genreServiceMock.Verify(s => s.GetById(It.IsAny<Guid>()), Times.Exactly(2));
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books", "photo")]
        public void CallAuthorServiceGetAuthorByName_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher, string photoUrl)
        {
            var date = DateTime.Now;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel()
            {
                Author = author,
                ISBN = isbn,
                Title = title,
                Summary = summary,
                Language = language,
                Publisher = publisher,
                Published = date,
                PhotoUrl = photoUrl,
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Edit(model);

            authorServiceMock.Verify(s => s.GetAuthorByName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books", "photo")]
        public void CallPublisherServiceGetPublisherByName_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher, string photoUrl)
        {
            var date = DateTime.Now;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel()
            {
                Author = author,
                ISBN = isbn,
                Title = title,
                Summary = summary,
                Language = language,
                Publisher = publisher,
                Published = date,
                PhotoUrl = photoUrl,
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Edit(model);

            publisherServiceMock.Verify(s => s.GetPublisherByName(It.IsAny<string>()), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books", "photo")]
        public void CallBookServiceUpdateBook_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher, string photoUrl)
        {
            var id = Guid.NewGuid();
            var date = DateTime.Now;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel()
            {
                Id = id,
                Author = author,
                ISBN = isbn,
                Title = title,
                Summary = summary,
                Language = language,
                Publisher = publisher,
                Published = date,
                PhotoUrl = photoUrl,
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Edit(model);

            bookServiceMock.Verify(s => s.UpdateBook(id, title, date, isbn, summary,
                language, It.IsAny<ICollection<Genre>>(), It.IsAny<Author>(),
                It.IsAny<Publisher>(), photoUrl), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books", "photo")]
        public void RedirectToControllerIndexAction_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher, string photoUrl)
        {
            var page = 1;
            var id = Guid.NewGuid();
            var date = DateTime.Now;

            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel()
            {
                Id = id,
                Author = author,
                ISBN = isbn,
                Title = title,
                Summary = summary,
                Language = language,
                Publisher = publisher,
                Published = date,
                PhotoUrl = photoUrl,
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo(c => c.Index(page));
        }
    }
}
