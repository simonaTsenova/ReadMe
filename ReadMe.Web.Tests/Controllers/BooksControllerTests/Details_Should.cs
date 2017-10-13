using AutoMapper;
using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using ReadMe.Web.Models.Books;
using ReadMe.Web.Models.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.FluentMVCTesting;

namespace ReadMe.Web.Tests.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Details_Should
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Book, BookInfoViewModel>();
                cfg.CreateMap<Review, ReviewViewModel>();
            });
        }

        [Test]
        public void CallBookServiceGetBookById_WhenInvoked()
        {
            var id = Guid.NewGuid();

            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Details(id);

            bookServiceMock.Verify(s => s.GetBookById(id), Times.Once);
        }

        [Test]
        public void ReturnErrorView_WhenBookIsNotFound()
        {
            var id = Guid.NewGuid();

            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.WithCallTo(c => c.Details(id))
                .ShouldRenderView("Error");
        }

        [Test]
        public void CallReviewServiceGetByBookId_WhenBookIsFound()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var book = new Book()
            {
                Id = id
            };
            var books = new List<Book> { book }.AsQueryable();
            var review = new Mock<Review>();
            var reviews = new List<Review> { review.Object }.AsQueryable();
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetBookById(id)).Returns(books);
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetByBookId(id)).Returns(reviews);
            reviewServiceMock.Setup(s => s.GetByUserIdAndBookId(userId, id)).Returns(review.Object);
            var userBookServiceMock = new Mock<IUserBookService>();
            var formModelMock = new Mock<ReviewViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateReviewViewModel()).Returns(formModelMock.Object);
            var providerMock = new Mock<IAuthenticationProvider>();
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Details(id);

            reviewServiceMock.Verify(s => s.GetByBookId(id), Times.Once);
        }

        [Test]
        public void CallAuthenticationProviderCurrentUserId_WhenBookIsFound()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var book = new Mock<Book>();
            var books = new List<Book> { book.Object }.AsQueryable();
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetBookById(id)).Returns(books);
            var reviewServiceMock = new Mock<IReviewService>();
            Review review = null;
            reviewServiceMock.Setup(s => s.GetByUserIdAndBookId(userId, id)).Returns(review);
            var userBookServiceMock = new Mock<IUserBookService>();
            var formModelMock = new Mock<ReviewViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateReviewViewModel()).Returns(formModelMock.Object);
            var providerMock = new Mock<IAuthenticationProvider>();

            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Details(id);

            providerMock.Verify(v => v.CurrentUserId, Times.Once);
        }

        [Test]
        public void CallReviewServiceGetByUserIdAndBookId_WhenBookIsFound()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var book = new Book()
            {
                Id = id
            };
            var books = new List<Book> { book }.AsQueryable();
            var review = new Mock<Review>();
            var reviews = new List<Review> { review.Object }.AsQueryable();
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetBookById(id)).Returns(books);
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetByBookId(id)).Returns(reviews);
            reviewServiceMock.Setup(s => s.GetByUserIdAndBookId(userId, id)).Returns(review.Object);
            var userBookServiceMock = new Mock<IUserBookService>();
            var formModelMock = new Mock<ReviewViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateReviewViewModel()).Returns(formModelMock.Object);
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);

            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Details(id);

            reviewServiceMock.Verify(s => s.GetByUserIdAndBookId(userId, id), Times.Once);
        }

        [Test]
        public void CallFactoryCreateReviewModel_WhenUserReviewIsNotFound()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var book = new Book()
            {
                Id = id
            };
            var books = new List<Book> { book }.AsQueryable();
            var review = new Mock<Review>();
            var reviews = new List<Review> { review.Object }.AsQueryable();
            var bookServiceMock = new Mock<IBookService>();
            bookServiceMock.Setup(s => s.GetBookById(id)).Returns(books);
            var reviewServiceMock = new Mock<IReviewService>();
            reviewServiceMock.Setup(s => s.GetByBookId(id)).Returns(reviews);
            var userBookServiceMock = new Mock<IUserBookService>();
            var formModelMock = new Mock<ReviewViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateReviewViewModel()).Returns(formModelMock.Object);
            var providerMock = new Mock<IAuthenticationProvider>();
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            controller.Details(id);

            factoryMock.Verify(f => f.CreateReviewViewModel(), Times.Once);
        }

        [Test]
        public void CallFactoryCreateBookDetailsViewModel_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var book = new Mock<Book>();
            var books = new List<Book> { book.Object }.AsQueryable();
            var review = new Mock<Review>();
            var reviews = new List<Review> { review.Object }.AsQueryable();
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var formModelMock = new Mock<ReviewViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateReviewViewModel()).Returns(formModelMock.Object);
            var providerMock = new Mock<IAuthenticationProvider>();

            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);
            bookServiceMock.Setup(s => s.GetBookById(id)).Returns(books);
            reviewServiceMock.Setup(s => s.GetByBookId(id)).Returns(reviews);
            reviewServiceMock.Setup(s => s.GetByUserIdAndBookId(userId, id)).Returns(review.Object);
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);

            controller.Details(id);

            factoryMock.Verify(f => f.CreateBookDetailsViewModel(It.IsAny<BookInfoViewModel>(),
                It.IsAny<ICollection<ReviewViewModel>>(), It.IsAny<ReviewViewModel>()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView_WhenInvoked()
        {
            var id = Guid.NewGuid();
            var userId = "9b79028c-f20f-4971-82f1-ce15bec4e0d8";

            var book = new Mock<Book>();
            var books = new List<Book> { book.Object }.AsQueryable();
            var review = new Mock<Review>();
            var reviews = new List<Review> { review.Object }.AsQueryable();
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var formModelMock = new Mock<ReviewViewModel>();
            var factoryMock = new Mock<IViewModelFactory>();
            factoryMock.Setup(f => f.CreateReviewViewModel()).Returns(formModelMock.Object);
            var providerMock = new Mock<IAuthenticationProvider>();

            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);
            bookServiceMock.Setup(s => s.GetBookById(id)).Returns(books);
            reviewServiceMock.Setup(s => s.GetByBookId(id)).Returns(reviews);
            reviewServiceMock.Setup(s => s.GetByUserIdAndBookId(userId, id)).Returns(review.Object);
            providerMock.Setup(v => v.CurrentUserId).Returns(userId);
            var detailsModelMock = new Mock<BookDetailsViewModel>();

            factoryMock.Setup(f => f.CreateBookDetailsViewModel(It.IsAny<BookInfoViewModel>(),
                It.IsAny<ICollection<ReviewViewModel>>(), It.IsAny<ReviewViewModel>())).Returns(detailsModelMock.Object);

            controller.WithCallTo(c => c.Details(id))
                .ShouldRenderDefaultView()
                .WithModel(detailsModelMock.Object);
        }
    }
}
