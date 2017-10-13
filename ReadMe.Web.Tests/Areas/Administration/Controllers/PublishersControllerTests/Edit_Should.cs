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

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class Edit_Should
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
        public void CallPublisherServiceGetPublisherById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.Edit(id);

            publisherServiceMock.Verify(s => s.GetPublisherById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorView_WhenPublisherIsNotFound()
        {
            var id = Guid.NewGuid();

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);

            controller.WithCallTo(c => c.Edit(id))
                .ShouldRenderPartialView("Error");
        }

        [Test]
        public void ReturnEditPublisherPartialView_WhenPublisherIsFound()
        {
            var id = Guid.NewGuid();

            var publisher = new Publisher() { Id = id };
            var queryables = new List<Publisher>() { publisher }.AsQueryable();
            var publisherServiceMock = new Mock<IPublisherService>();
            publisherServiceMock.Setup(s => s.GetPublisherById(id)).Returns(queryables);
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            var model = new PublisherViewModel() { Id = id };

            controller.WithCallTo(c => c.Edit(id))
                .ShouldRenderPartialView("_EditPublisherPartial")
                .WithModel<PublisherViewModel>();
        }
    }
}
