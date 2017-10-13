using Moq;
using NUnit.Framework;
using ReadMe.Services.Contracts;
using ReadMe.Web.Areas.Administration.Controllers;
using ReadMe.Web.Areas.Administration.Models;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Areas.Administration.Controllers.PublishersControllerTests
{
    [TestFixture]
    public class PostAdd_Should
    {
        [Test]
        public void ReturnAddPublisherPartialView_WhenModelIsNotValid()
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            controller.ModelState.AddModelError("field", "message");
            var modelMock = new Mock<AddPublisherViewModel>();

            controller.WithCallTo(c => c.Add(modelMock.Object))
                .ShouldRenderPartialView("_AddPublisherPartial");
        }

        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd", "Bulgaria", "www.peng.com")]
        public void CallPublisherServiceAddPublisher_WhenModelIsValid(string name, string owner, string phone, string city,
            string address, string country, string website)
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            var model = new AddPublisherViewModel()
            {
                Name = name,
                Owner = owner,
                PhoneNumber = phone,
                City = city,
                Address = address,
                Country = country,
                Website = website
            };

            controller.Add(model);

            publisherServiceMock.Verify(s => s.AddPublisher(
                name, owner, phone, city, address, country, website), Times.Once);
        }

        [TestCase("Penguin books", "John Doe", "0887569423", "Sofia", "24 Bulgaria blvd", "Bulgaria", "www.peng.com")]
        public void RedirectToControllerIndexAction_WhenModelIsValid(string name, string owner, string phone, string city,
            string address, string country, string website)
        {
            var publisherServiceMock = new Mock<IPublisherService>();
            var bookServiceMock = new Mock<IBookService>();
            var controller = new PublishersController(
                publisherServiceMock.Object, bookServiceMock.Object);
            var model = new AddPublisherViewModel()
            {
                Name = name,
                Owner = owner,
                PhoneNumber = phone,
                City = city,
                Address = address,
                Country = country,
                Website = website
            };

            controller.WithCallTo(c => c.Add(model))
                .ShouldRedirectTo(c => c.Index(1));
        }
    }
}
