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
    public class Edit_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorViewModel>();
            });
        }

        [Test]
        public void CallAuthorServiceGetAuthorById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.Edit(id);

            authorServiceMock.Verify(s => s.GetAuthorById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorView_WhenAuthorIsNotFound()
        {
            var id = Guid.NewGuid();

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Edit(id))
                .ShouldRenderPartialView("Error");
        }

        [Test]
        public void ReturnEditAuthorPartialView_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();

            var author = new Author() { Id = id };
            var queryables = new List<Author>() { author }.AsQueryable();
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(s => s.GetAuthorById(id)).Returns(queryables);
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);
            var model = new AuthorViewModel() { Id = id};

            controller.WithCallTo(c => c.Edit(id))
                .ShouldRenderPartialView("_EditAuthorPartial")
                .WithModel<AuthorViewModel>();
        }
    }
}
