using Moq;
using NUnit.Framework;
using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class Edit_Should
    {
        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void CallUserRepositoryGetById_WhenInvoked(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            userRepositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void NotCallRepositoryUpdate_WhenUserIsNotFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void NotCallUnitOfWorkCommit_WhenUserIsNotFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var provider = new Mock<IDateTimeProvider>();

            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Never);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void SetUserFirstnameCorrectly_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var edited = service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            Assert.AreEqual(firstName, edited.FirstName);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void SetUserLastnameCorrectly_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var edited = service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            Assert.AreEqual(lastName, edited.LastName);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void SetUserNationalityCorrectly_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var edited = service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            Assert.AreEqual(nationality, edited.Nationality);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void SetUserAgeCorrectly_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var edited = service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            Assert.AreEqual(age, edited.Age);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void SetUserQuoteCorrectly_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            var edited = service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            Assert.AreEqual(favouriteQuote, edited.FavouriteQuote);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void CallRepisitoryUpdate_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [TestCase("9b79028c-f20f-4971-82f1-ce15bec4e0d8", "test@mail.com", "test", "test", "Bulgarian", 21, "quote")]
        public void CallUnitOfWorkCommit_WhenUserIsFound(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var userRepositoryMock = new Mock<IEfRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var user = new Mock<User>();
            userRepositoryMock.Setup(r => r.GetById(It.IsAny<string>())).Returns(user.Object);
            var provider = new Mock<IDateTimeProvider>();
            var service = new UserService(userRepositoryMock.Object, unitOfWorkMock.Object, provider.Object);

            service.EditUser(id, email, firstName, lastName, nationality, age, favouriteQuote);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
