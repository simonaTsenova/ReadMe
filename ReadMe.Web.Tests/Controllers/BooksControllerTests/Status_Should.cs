using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Models.Enumerations;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Books;
using System;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Status_Should
    {
        [Test]
        public void CallProviderCurrentUserId_WhenModelIsValid()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var modelMock = new Mock<BookInfoViewModel>();
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Status(modelMock.Object);

            providerMock.Verify(v => v.CurrentUserId, Times.Once);
        }

        [Test]
        public void CallUserBookServiceUpdateStatus_WhenModelIsValid()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var status = ReadStatus.CurrentlyReading;

            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var model = new BookInfoViewModel()
            {
                Id = id
            };
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Status(model);

            userBookServiceMock.Verify(s => s.UpdateStatus(userId, id, status), Times.Once);
        }

        [Test]
        public void ReturnContentResultCorrectly_WhenModelIsValid()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var message = "Status updated";

            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var modelMock = new Mock<BookInfoViewModel>();
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.WithCallTo(c => c.Status(modelMock.Object))
                .ShouldReturnContent(message);
        }

        [Test]
        public void ReturnContentResultCorrectly_WhenModelIsNotValid()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";
            var message = "Oops! Status could not be updated";

            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var modelMock = new Mock<BookInfoViewModel>();
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);
            controller.ModelState.AddModelError("field", "message");

            controller.WithCallTo(c => c.Status(modelMock.Object))
                .ShouldReturnContent(message);
        }
    }
}
