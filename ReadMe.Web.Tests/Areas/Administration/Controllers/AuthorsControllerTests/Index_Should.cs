using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.AuthorsControllerTests
{
    [TestFixture]
    public class Index_Should
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
        public void CallAuthorServiceGetAllAndDeleted_WhenInvoked()
        {
            var authorServiceMock = new Mock<IAuthorService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.Index(null);

            authorServiceMock.Verify(s => s.GetAllAndDeleted(), Times.Once);
        }

        [TestCase(1, 15)]
        public void ReturnAuthorsPartialView_WhenInvoked(int page, int count)
        {
            var authorMock = new Mock<Author>();
            var authorServiceMock = new Mock<IAuthorService>();
            authorServiceMock.Setup(s => s.GetAllAndDeleted())
                .Returns(new List<Author> { authorMock.Object }.AsQueryable());
            var bookServiceMock = new Mock<IBookService>();
            var controller = new AuthorsController(
                authorServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Index(page))
                .ShouldRenderPartialView("_AuthorsPartial")
                .WithModel<IPagedList<AuthorViewModel>>();
        }
    }
}
