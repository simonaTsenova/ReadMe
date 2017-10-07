using ReadMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadMe.Models;
using ReadMe.Data.Contracts;
using System.Data.Entity;

namespace ReadMe.Services
{
    public class BookService : IBookService
    {
        private readonly IEfRepository<Book> bookRepository;
        private readonly IUnitOfWork unitOfWork;

        public BookService(IEfRepository<Book> bookRepository, IUnitOfWork unitOfWork)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("Book repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Book> GetBookById(Guid id)
        {
            var book = this.bookRepository.All
                .Where(bk => bk.Id == id)
                .Include(x => x.Genres)
                .Include(x => x.UserBooks);

            return book;
        }

        public IQueryable<Book> Search(string pattern, string searchType, string[] genres)
        {
            switch (searchType)
            {
                case "title":
                    {
                        return this.SearchByTitle(pattern, genres);
                    }
                case "author":
                    {
                        return this.SearchByAuthor(pattern, genres);
                    }
                case "year":
                    {
                        return this.SearchByYear(pattern, genres);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        private IQueryable<Book> SearchByTitle(string searchPattern, string[] genres)
        {
            var books = this.bookRepository.All
                .Where(book => book.Title.ToLower().Contains(searchPattern.ToLower()))
                .Include(b => b.Genres);

            if (genres != null)
            {
                var results = this.FilterByGenres(books, genres);

                return results;
            }

            return books;
        }

        private IQueryable<Book> SearchByAuthor(string searchPattern, string[] genres)
        {
            var books = this.bookRepository.All
                .Where(book =>
                    book.Author.FirstName.ToLower().Contains(searchPattern.ToLower()) ||
                    book.Author.LastName.ToLower().Contains(searchPattern.ToLower())
                )
                .Include(book => book.Genres);

            if (genres != null)
            {
                var results = this.FilterByGenres(books, genres);

                return results;
            }

            return books;
        }

        private IQueryable<Book> SearchByYear(string searchPattern, string[] genres)
        {
            var books = this.bookRepository.All
                .Where(book => book.Published.ToString().ToLower().Contains(searchPattern.ToLower()))
                .Include(book => book.Genres);

            if (genres != null)
            {
                var results = this.FilterByGenres(books, genres);

                return results;
            }

            return books;
        }

        private IQueryable<Book> FilterByGenres(IQueryable<Book> books, string[] genres)
        {
            var genresList = genres.ToList();
            var filteredByGenres = new HashSet<Book>();

            foreach (var book in books)
            {
                if (genresList.All(x => book.Genres.Select(g => g.Name).Contains(x)))
                {
                    filteredByGenres.Add(book);
                }
            }

            return filteredByGenres.AsQueryable<Book>();
        }
    }
}
