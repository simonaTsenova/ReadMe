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
using AutoMapper;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Publisher, PublisherViewModel>();
            });
        }

        [Test]
        public void CallPublisherServiceGetAllAndDeleted_WhenInvoked()
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.Index(null);

            publisherServiceMock.Verify(s => s.GetAllAndDeleted(), Times.Once);
        }

        [TestCase(1, 15)]
        public void ReturnPublishersPartialView_WhenInvoked(int page, int count)
        {
            var publisherMock = new Mock<Publisher>();
            var publisherServiceMock = new Mock<IPublisherService>();
            publisherServiceMock.Setup(s => s.GetAllAndDeleted())
                .Returns(new List<Publisher> { publisherMock.Object }.AsQueryable());
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Index(page))
                .ShouldRenderPartialView("_PublishersPartial")
                .WithModel<IPagedList<PublisherViewModel>>();
        }
    }
}
