using ReadMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadMe.Services.Contracts
{
    public interface IBookService
    {
        void AddBook(string title, DateTime published, string isbn,
                    Author author, string summary, string language,
                    Publisher publisher, ICollection<Genre> genres);

        IQueryable<Book> GetAllAndDeleted();

        IQueryable<Book> GetBookById(Guid id);

        IQueryable<Book> GetBooksByAuthor(Guid authorId);

        IQueryable<Book> Search(string pattern, string searchType, string[] genres);

        void UpdateRating(Guid id, double rating);

        void UpdateBook(Guid id, string title, DateTime published, string isbn,
                string summary, string language, ICollection<Genre> genres, Author author, Publisher publisher, string photoUrl);

        void DeleteBook(Guid bookId);

        void RestoreBook(Guid bookId);
    }
}
