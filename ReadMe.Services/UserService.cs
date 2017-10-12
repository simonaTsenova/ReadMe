using ReadMe.Data.Contracts;
using ReadMe.Models;
using ReadMe.Models.Enumerations;
using ReadMe.Providers.Contracts;
using ReadMe.Services.Contracts;
using System;
using System.Data.Entity;
using System.Linq;

namespace ReadMe.Services
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateProvider;

        public UserService(IEfRepository<User> userRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateProvider)
        {
            if(userRepository == null)
            {
                throw new ArgumentNullException("User repository cannot be null.");
            }

            if(unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (dateProvider == null)
            {
                throw new ArgumentNullException("Date provider cannot be null.");
            }

            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.dateProvider = dateProvider;
        }

        public IQueryable<User> GetUserByUsername(string username)
        {
            var user = this.userRepository.All
                .Where(u => u.UserName == username)
                .Include(u => u.UserBooks);

            return user;
        }

        public User EditUser(string id, string email, string firstName, string lastName,
            string nationality, int age, string favouriteQuote)
        {
            var user = this.userRepository.GetById(id);
            
            if(user != null)
            {
                user.Email = email;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Nationality = nationality;
                user.Age = age;
                user.FavouriteQuote = favouriteQuote;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }

            return user;
        }

        public User GetUserById(string id)
        {
            var user = this.userRepository.GetById(id);

            return user;
        }

        public IQueryable<User> GetAll()
        {
            return this.userRepository.All;
        }

        public void DeleteUser(string userId)
        {
            var user = this.userRepository.GetById(userId);
            var dateDeleted = dateProvider.GetCurrentTime();

            if(user != null)
            {
                user.IsDeleted = true;
                user.DeletedOn = dateDeleted;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }
        }

        public void RestoreUser(string userId)
        {
            var user = this.userRepository.GetById(userId);

            if (user != null)
            {
                user.IsDeleted = false;
                user.DeletedOn = null;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<User> GetAllAndDeleted()
        {
            return this.userRepository.AllAndDeleted;
        }

        public IQueryable<Book> GetUserReadBooks(string userId)
        {
            var books = this.userRepository
                .All
                .Where(u => u.Id == userId)
                .Include(u => u.UserBooks)
                .FirstOrDefault()
                .UserBooks
                .Where(b => b.ReadStatus == ReadStatus.Read)
                .Select(u => u.Book)
                .AsQueryable();

            return books;
        }

        public IQueryable<Book> GetUserWantToReadBooks(string userId)
        {
            var books = this.userRepository
                .All
                .Where(u => u.Id == userId)
                .Include(u => u.UserBooks)
                .FirstOrDefault()
                .UserBooks
                .Where(b => b.ReadStatus == ReadStatus.WantToRead)
                .Select(u => u.Book)
                .AsQueryable();

            return books;
        }

        public IQueryable<Book> GetUserCurrentlyReadingBooks(string userId)
        {
            var books = this.userRepository
                .All
                .Where(u => u.Id == userId)
                .Include(u => u.UserBooks)
                .FirstOrDefault()
                .UserBooks
                .Where(b => b.ReadStatus == ReadStatus.CurrentlyReading)
                .Select(u => u.Book)
                .AsQueryable();

            return books;
        }
    }
}
