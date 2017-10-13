using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class PostEdit_Should
    {
        [Test]
        public void ReturnEditAuthorPartialView_WhenModelIsNotValid()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            controller.ModelState.AddModelError("field", "message");
            var modelMock = new Mock<AuthorViewModel>();

            controller.WithCallTo(c => c.Edit(modelMock.Object))
                .ShouldRenderPartialView("_EditAuthorPartial");
        }

        [TestCase("Danelle", "Steel", "American", 62, "biography", "site", "photo")]
        public void CallAuthorServiceUpdateAuthor_WhenModelIsValid(string firstname,
            string lastname, string nationality, int age,
            string biography, string website, string photo)
        {
            var id = Guid.NewGuid();

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            var model = new AuthorViewModel()
            {
                Id = id,
                FullName = firstname + " " + lastname,
                Nationality = nationality,
                Age = age,
                Biography = biography,
                Website = website,
                PhotoUrl = photo
            };

            controller.Edit(model);

            authorServiceMock.Verify(s => s.UpdateAuthor(id, firstname, 
                lastname, nationality, age, biography, website, photo), Times.Once);
        }

        [TestCase("Danelle", "Steel", "American", 62, "biography", "site", "photo")]
        public void RedirectToControllerIndexAction_WhenModelIsValid(string firstname,
            string lastname, string nationality, int age, 
            string biography, string website, string photo)
        {
            var id = Guid.NewGuid();

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            var model = new AuthorViewModel()
            {
                Id = id,
                FullName = firstname + " " + lastname,
                Nationality = nationality,
                Age = age,
                Biography = biography,
                Website = website,
                PhotoUrl = photo
            };

            controller.WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo(c => c.Index(1));
        }
    }
}
