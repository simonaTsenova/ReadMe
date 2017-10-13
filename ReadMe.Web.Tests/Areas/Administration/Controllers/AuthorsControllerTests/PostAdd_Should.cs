using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class PostAdd_Should
    {
        [Test]
        public void ReturnAddAuthorPartialView_WhenModelIsNotValid()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            controller.ModelState.AddModelError("field", "message");
            var modelMock = new Mock<AddAuthorViewModel>();

            controller.WithCallTo(c => c.Add(modelMock.Object))
                .ShouldRenderPartialView("_AddAuthorPartial");
        }

        [TestCase("Danelle", "Steel", "American", 62, "biography", "site")]
        public void CallAuthorServiceAddAuthor_WhenModelIsValid(string firstname,
            string lastname, string nationality, int age, string biography, string website)
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            var model = new AddAuthorViewModel()
            {
                FullName = firstname + " " + lastname,
                Nationality = nationality,
                Age = age,
                Biography = biography,
                Website = website
            };

            controller.Add(model);

            authorServiceMock.Verify(s => s.AddAuthor(
                firstname, lastname, nationality, age, biography, website), Times.Once);
        }

        [TestCase("Danielle", "Steel", "American", 62, "biography", "site")]
        public void RedirectToControllerIndexAction_WhenModelIsValid(string firstname,
            string lastname, string nationality, int age, string biography, string website)
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            var model = new AddAuthorViewModel()
            {
                FullName = firstname + " " + lastname,
                Nationality = nationality,
                Age = age,
                Biography = biography,
                Website = website
            };

            controller.WithCallTo(c => c.Add(model))
                .ShouldRedirectTo(c => c.Index(1));
        }
    }
}
