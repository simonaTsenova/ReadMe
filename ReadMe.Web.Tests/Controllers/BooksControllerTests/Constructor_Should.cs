using Moq;
using NUnit.Framework;
using ReadMe.Authentication.Contracts;
using ReadMe.Services.Contracts;
using ReadMe.Web.Controllers;
using ReadMe.Web.Infrastructure.Factories;
using System;

namespace ReadMe.Web.Tests.Controllers.BooksControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenBookServiceIsNull()
        {
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(null, reviewServiceMock.Object,
                userBookServiceMock.Object, factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenReviewServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object, null,
                userBookServiceMock.Object, factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserBookServiceIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, null, factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenFactoryIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, null, providerMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenAuthenticationProviderIsNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            Assert.DoesNotThrow(() => new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object));
        }

        [Test]
        public void InitializeBooksControllerCorrectly_WhenDependenciesAreNotNull()
        {
            var bookServiceMock = new Mock<IBookService>();
            var reviewServiceMock = new Mock<IReviewService>();
            var userBookServiceMock = new Mock<IUserBookService>();
            var factoryMock = new Mock<IViewModelFactory>();
            var providerMock = new Mock<IAuthenticationProvider>();

            var controller = new BooksController(bookServiceMock.Object,
                reviewServiceMock.Object, userBookServiceMock.Object, factoryMock.Object, providerMock.Object);

            Assert.IsNotNull(controller);
        }
    }
}
