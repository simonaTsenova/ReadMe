using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Models.Books;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class LatestBooks_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookShortViewModel>();
            });
        }

        [Test]
        public void CallBookServiceGetLatestBooks_WhenInvoked()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var controller = new HomeController(bookServiceMock.Object, reviewServiceMock.Object);

            controller.LatestBooks();

            bookServiceMock.Verify(s => s.GetLatestBooks(), Times.Once);
        }

        [Test]
        public void ReturnViewCorrectly_WhenInvoked()
        {
            var books = new List<Book>()
            {
                new Book() { Title = "Sample" }
            };
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetLatestBooks()).Returns(books.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var controller = new HomeController(bookServiceMock.Object, reviewServiceMock.Object);
            var expectedModel = new List<BookShortViewModel>()
            {
                new BookShortViewModel() { Title = "Sample" }
            };

            controller.WithCallTo(c => c.LatestBooks())
                .ShouldRenderPartialView("_LatestBooksListPartial")
                .WithModel<IList<BookShortViewModel>>(
                    m => Assert.AreEqual(expectedModel.Count, m.Count));
        }
    }
}
