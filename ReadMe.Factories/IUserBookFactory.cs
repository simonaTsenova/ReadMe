using ReadMe.Models;
using ReadMe.Models.Enumerations;
using System;

namespace ReadMe.Factories
{
    public interface IUserBookFactory
    {
        UserBook CreateUserBook(string userId, Guid bookId, ReadStatus status);
    }
}
