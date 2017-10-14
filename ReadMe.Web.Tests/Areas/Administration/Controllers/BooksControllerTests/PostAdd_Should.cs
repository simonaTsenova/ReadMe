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
    public class PostAdd_Should
    {
        [Test]
        public void ReturnAddBookPartialView_WhenModelIsNotValid()
        {
            var bookServiceMock = new Mock<IBookService>();
            var genreServiceMock = new Mock<IGenreService>();
            var authorServiceMock = new Mock<IAuthorService>();
            var publisherServiceMock = new Mock<IPublisherService>();
            var model = new AddBookViewModel();
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);
            controller.ModelState.AddModelError("field", "message");

            controller.WithCallTo(c => c.Add(model))
                .ShouldRenderPartialView("_AddBookPartial")
                .WithModel<AddBookViewModel>();
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books")]
        public void CallGenreServiceGetById_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher)
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
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Add(model);

            genreServiceMock.Verify(s => s.GetById(It.IsAny<Guid>()), Times.Exactly(2));
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books")]
        public void CallAuthorServiceGetAuthorByName_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher)
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
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Add(model);

            authorServiceMock.Verify(s => s.GetAuthorByName(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books")]
        public void CallPublisherServiceGetPublisherByName_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher)
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
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Add(model);

            publisherServiceMock.Verify(s => s.GetPublisherByName(publisher), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books")]
        public void CallBookServiceAddBook_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher)
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
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.Add(model);

            bookServiceMock.Verify(s => s.AddBook(title, date, isbn, It.IsAny<Author>(), summary,
                language, It.IsAny<Publisher>(), It.IsAny<ICollection<Genre>>()), Times.Once);
        }

        [TestCase("Chasing the Dime", "044661162X", "Michael Connelly", "summary",
            "English", "Warner Books")]
        public void RedrirectToControllerIndexAction_WhenModelIsValid(string title, string isbn, string author,
            string summary, string language, string publisher)
        {
            var date = DateTime.Now;
            var page = 1;

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
                GenresIds = new Guid[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                }
            };
            var factoryMock = new Mock<IViewModelFactory>();
            var controller = new BooksController(bookServiceMock.Object, genreServiceMock.Object,
                authorServiceMock.Object, publisherServiceMock.Object, factoryMock.Object);

            controller.WithCallTo(c => c.Add(model))
                .ShouldRedirectTo(c => c.Index(page));
        }
    }
}
