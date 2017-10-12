using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Profile;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Edit_Should
    {
        [Test]
        public void ReturnEditPartialViewAgain_WhenModelIsNotValid()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);
            controller.ModelState.AddModelError("field", "message");
            var modelMock = new Mock<UserDetailsViewModel>();

            controller.WithCallTo(c => c.Edit(modelMock.Object))
                .ShouldRenderPartialView("_EditUserPartial")
                .WithModel(modelMock.Object);
        }

        [Test]
        public void CallUserServiceEditUser_WhenModelIsValid()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);
            var model = new UserDetailsViewModel()
            {
                Id = "9b79028c-f20f-4971-82f1-ce15bec4e0d8",
                Email = "test@mail.com",
                FirstName = "Test",
                LastName = "Test",
                Nationality = "Bulgarian",
                Age = 25,
                FavouriteQuote = "quote"
            };


            controller.Edit(model);

            userServiceMock.Verify(s => s.EditUser(model.Id, model.Email, model.FirstName,
                model.LastName, model.Nationality, model.Age, model.FavouriteQuote), Times.Once);
        }

        [Test]
        public void CallMapperMap_WhenModelIsValid()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);
            var model = new UserDetailsViewModel()
            {
                Id = "9b79028c-f20f-4971-82f1-ce15bec4e0d8",
                Email = "test@mail.com",
                FirstName = "Test",
                LastName = "Test",
                Nationality = "Bulgarian",
                Age = 25,
                FavouriteQuote = "quote"
            };
            var userMock = new Mock<User>();
            userServiceMock.Setup(s => s.EditUser(model.Id, model.Email, model.FirstName,
                model.LastName, model.Nationality, model.Age, model.FavouriteQuote)).Returns(userMock.Object);

            controller.Edit(model);

            mapperMock.Verify(m => m.Map<UserDetailsViewModel>(userMock.Object), Times.Once);
        }

        [Test]
        public void ReturnUserInfoPartialView_WhenModelIsValid()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);
            var model = new UserDetailsViewModel()
            {
                Id = "9b79028c-f20f-4971-82f1-ce15bec4e0d8",
                Email = "test@mail.com",
                FirstName = "Test",
                LastName = "Test",
                Nationality = "Bulgarian",
                Age = 25,
                FavouriteQuote = "quote"
            };
            var userMock = new Mock<User>();
            userServiceMock.Setup(s => s.EditUser(model.Id, model.Email, model.FirstName,
                model.LastName, model.Nationality, model.Age, model.FavouriteQuote)).Returns(userMock.Object);
            mapperMock.Setup(m => m.Map<UserDetailsViewModel>(userMock.Object)).Returns(model);

            controller.WithCallTo(c => c.Edit(model))
                .ShouldRenderPartialView("_UserInfoPartial")
                .WithModel(model);
        }
    }
}
