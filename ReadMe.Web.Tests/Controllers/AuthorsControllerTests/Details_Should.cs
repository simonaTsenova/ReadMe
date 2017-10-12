using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class Details_Should
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
            var controller = new AuthorsController(authorServiceMock.Object, bookServiceMock.Object);

            controller.Details(id);

            authorServiceMock.Verify(s => s.GetAuthorById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorView_WhenAuthorIsNotFound()
        {
            var id = Guid.NewGuid();

            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(authorServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Details(id))
                .ShouldRenderView("Error");
        }

        [Test]
        public void ReturnDefaultIndexView_WhenAuthorIsFound()
        {
            var id = Guid.NewGuid();

            var author = new Mock<Author>();
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(s => s.GetAuthorById(id))
                .Returns(new List<Author> { author.Object }.AsQueryable());
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(authorServiceMock.Object, bookServiceMock.Object);
            var modelMock = new Mock<AuthorViewModel>();

            controller.WithCallTo(c => c.Details(id))
                .ShouldRenderDefaultView()
                .WithModel<AuthorViewModel>();
        }
    }
}
