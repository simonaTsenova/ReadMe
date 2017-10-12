using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Profile;
using ReadMe.Web.Models.Reviews;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.ProfileControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            //var mapper = new AutoMapperConfig();
            //mapper.Execute(Assembly.GetExecutingAssembly());
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserDetailsViewModel>();
                cfg.CreateMap<Book, BookShortViewModel>();
                cfg.CreateMap<Review, ReviewViewModel>();
            });
        }

        [TestCase("admin")]
        public void CallUserServiceGetUserByUsername_WhenInvoked(string username)
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.Details(username);

            userServiceMock.Verify(s => s.GetUserByUsername(username), Times.Once);
        }

        [TestCase("admin")]
        public void RenderErrorView_WhenUserIsNotFound(string username)
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.WithCallTo(c => c.Details(username))
                .ShouldRenderView("Error");
        }

        [TestCase("admin")]
        public void CallUserServiceGetUserCurrentlyReadingBooks_WhenUserIsFound(string username)
        {
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUserByUsername(username))
                .Returns(new List<User> { user }.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.Details(username);

            userServiceMock.Verify(s => s.GetUserCurrentlyReadingBooks(It.IsAny<string>()), Times.Once);
        }

        [TestCase("admin")]
        public void CallUserServiceGetUserWantToReadBooks_WhenUserIsFound(string username)
        {
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUserByUsername(username))
                .Returns(new List<User> { user }.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.Details(username);

            userServiceMock.Verify(s => s.GetUserWantToReadBooks(It.IsAny<string>()), Times.Once);
        }

        [TestCase("admin")]
        public void CallUserServiceGetUserReadBooks_WhenUserIsFound(string username)
        {
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUserByUsername(username))
                .Returns(new List<User> { user }.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.Details(username);

            userServiceMock.Verify(s => s.GetUserReadBooks(It.IsAny<string>()), Times.Once);
        }

        [TestCase("admin")]
        public void CallReviewServiceGetByUserId_WhenUserIsFound(string username)
        {
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUserByUsername(username))
                .Returns(new List<User> { user }.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.Details(username);

            reviewServiceMock.Verify(s => s.GetByUserId(It.IsAny<string>()), Times.Once);
        }

        [TestCase("admin")]
        public void CallFactoryCreateProfileViewModel_WhenUserIsFound(string username)
        {
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUserByUsername(username))
                .Returns(new List<User> { user }.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.Details(username);

            factoryMock.Verify(f => f.CreateProfileViewModel(
                It.IsAny<UserDetailsViewModel>(),
                It.IsAny<ICollection<BookShortViewModel>>(),
                It.IsAny<ICollection<BookShortViewModel>>(),
                It.IsAny<ICollection<BookShortViewModel>>(),
                It.IsAny<ICollection<ReviewViewModel>>()
                ), Times.Once);
        }

        [TestCase("admin")]
        public void ReturnCorrectView_WhenUserIsFound(string username)
        {
            var user = new User();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(s => s.GetUserByUsername(username))
                .Returns(new List<User> { user }.AsQueryable());
            var reviewServiceMock = new Mock<IReviewService>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var model = new ProfileViewModel();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateProfileViewModel(
                It.IsAny<UserDetailsViewModel>(),
                It.IsAny<ICollection<BookShortViewModel>>(),
                It.IsAny<ICollection<BookShortViewModel>>(),
                It.IsAny<ICollection<BookShortViewModel>>(),
                It.IsAny<ICollection<ReviewViewModel>>())).Returns(model);
            var mapperMock = new Mock<IMapper>();
            var controller = new ProfileController(providerMock.Object, userServiceMock.Object,
                reviewServiceMock.Object, factoryMock.Object, mapperMock.Object);

            controller.WithCallTo(c => c.Details(username))
                .ShouldRenderDefaultView()
                .WithModel<ProfileViewModel>(model);
        }
    }
}
