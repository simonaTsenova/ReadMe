using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IBookService
    {
        IQueryable<Book> GetBookById(Guid title);
    }
}
