using ReadMe.Models;
using System;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IBookService
    {
        IQueryable<Book> GetBookById(Guid id);

        IQueryable<Book> Search(string pattern, string searchType, string[] genres);

        void UpdateRating(Guid id, double rating);
    }
}
