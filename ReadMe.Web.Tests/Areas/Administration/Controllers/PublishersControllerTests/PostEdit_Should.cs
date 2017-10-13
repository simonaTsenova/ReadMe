using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class PostEdit_Should
    {
        [Test]
        public void ReturnEditPublisherPartialView_WhenModelIsNotValid()
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            controller.ModelState.AddModelError("field", "message");
            var modelMock = new Mock<PublisherViewModel>();

            controller.WithCallTo(c => c.Edit(modelMock.Object))
                .ShouldRenderPartialView("_EditPublisherPartial");
        }

        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd", 
            "Bulgaria", "www.peng.com", "logoUrl")]
        public void CallPublisherServiceUpdatePublisher_WhenModelIsValid(string name, string owner, string phone, string city,
            string address, string country, string website, string logo)
        {
            var id = Guid.NewGuid();

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            var model = new PublisherViewModel()
            {
                Id = id,
                Name = name,
                Owner = owner,
                PhoneNumber = phone,
                City = city,
                Address = address,
                Country = country,
                Website = website,
                LogoUrl = logo
            };

            controller.Edit(model);

            publisherServiceMock.Verify(s => s.UpdatePublisher(
                id, name, owner, phone, city, address, country, website, logo), Times.Once);
        }

        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd",
            "Bulgaria", "www.peng.com", "logoUrl")]
        public void RedirectToControllerIndexAction_WhenModelIsValid(string name, string owner, string phone, string city,
            string address, string country, string website, string logo)
        {
            var id = Guid.NewGuid();

            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            var model = new PublisherViewModel()
            {
                Id = id,
                Name = name,
                Owner = owner,
                PhoneNumber = phone,
                City = city,
                Address = address,
                Country = country,
                Website = website,
                LogoUrl = logo
            };

            controller.WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo(c => c.Index(1));
        }
    }
}
