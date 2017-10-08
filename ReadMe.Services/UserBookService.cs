using System;
using ReadMe.Models;
using ReadMe.Services.Contracts;
using ReadMe.Data.Contracts;
using System.Linq;
using ReadMe.Models.Enumerations;
using ReadMe.Factories;

namespace ReadMe.Services
{
    public class UserBookService : IUserBookService
    {
        private readonly IEfRepository<UserBook> userBookRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserBookFactory userBookFactory;

        public UserBookService(IEfRepository<UserBook> userBookRepository, IUnitOfWork unitOfWork, IUserBookFactory userBookFactory)
        {
            if (userBookRepository == null)
            {
                throw new ArgumentNullException("UserBook repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (userBookFactory == null)
            {
                throw new ArgumentNullException("UserBook factory cannot be null.");
            }

            this.userBookRepository = userBookRepository;
            this.unitOfWork = unitOfWork;
            this.userBookFactory = userBookFactory;
        }

        public void AddUserBook(string userId, Guid bookId, ReadStatus status)
        {
            var userBook = this.userBookFactory.CreateUserBook(userId, bookId, status);
            this.userBookRepository.Add(userBook);
            this.unitOfWork.Commit();
        }

        public UserBook GetByUserIdAndBookId(string userId, Guid bookId)
        {
            var result = this.userBookRepository.All
                .Where(ub => ub.UserId == userId && ub.BookId == bookId)
                .FirstOrDefault();

            return result;
        }

        public void UpdateStatus(string userId, Guid bookId, ReadStatus status)
        {
            var userBook = this.GetByUserIdAndBookId(userId, bookId);

            if(userBook == null)
            {
                this.AddUserBook(userId, bookId, status);
            }
            else
            {
                userBook.ReadStatus = status;

                this.userBookRepository.Update(userBook);
                this.unitOfWork.Commit();
            }
        }
    }
}
