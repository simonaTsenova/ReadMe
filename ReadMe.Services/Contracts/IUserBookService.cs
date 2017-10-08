using ReadMe.Models;
using ReadMe.Models.Enumerations;
using System;

namespace ReadMe.Services.Contracts
{
    public interface IUserBookService
    {
        UserBook GetByUserIdAndBookId(string userId, Guid bookId);

        void AddUserBook(string userId, Guid bookId, ReadStatus status);

        void UpdateStatus(string userId, Guid bookId, ReadStatus status);
    }
}
