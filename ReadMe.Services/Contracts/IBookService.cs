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

        IQueryable<Book> GetAll();

        IQueryable<Book> GetAllAndDeleted();

        IQueryable<Book> GetBookById(Guid id);

        IQueryable<Book> GetBooksByAuthor(Guid authorId);

        IQueryable<Book> GetAllBooksByAuthor(Guid authorId);

        IQueryable<Book> GetBooksByPublisher(Guid publisherId);

        IQueryable<Book> GetAllBooksByPublisher(Guid publisherId);

        IQueryable<Book> GetTopRatedBooks();

        IQueryable<Book> GetLatestBooks();

        IQueryable<Book> Search(string pattern, string searchType, string[] genres);

        IQueryable<Book> SearchByTitle(string searchPattern, string[] genres);

        IQueryable<Book> SearchByAuthor(string searchPattern, string[] genres);

        IQueryable<Book> SearchByYear(string searchPattern, string[] genres);

        void UpdateRating(Guid id, double rating);

        void UpdateBook(Guid id, string title, DateTime published, string isbn,
                string summary, string language, Author author, Publisher publisher, string photoUrl);

        void DeleteBook(Guid bookId);

        void RestoreBook(Guid bookId);
    }
}
