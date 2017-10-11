using ReadMe.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using ReadMe.Models;
using ReadMe.Data.Contracts;
using System.Data.Entity;
using ReadMe.Factories;
using ReadMe.Providers.Contracts;

namespace ReadMe.Services
{
    public class BookService : IBookService
    {
        private readonly IEfRepository<Book> bookRepository;
        private readonly IBookFactory bookFactory;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider provider;

        public BookService(IEfRepository<Book> bookRepository, IBookFactory bookFactory,
            IUnitOfWork unitOfWork, IDateTimeProvider provider)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("Book repository cannot be null.");
            }

            if (bookFactory == null)
            {
                throw new ArgumentNullException("Book factory cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (provider == null)
            {
                throw new ArgumentNullException("DateTime provider cannot be null.");
            }

            this.bookRepository = bookRepository;
            this.bookFactory = bookFactory;
            this.unitOfWork = unitOfWork;
            this.provider = provider;
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

        public void UpdateRating(Guid id, double rating)
        {
            var book = this.bookRepository.GetById(id);

            if(book != null)
            {
                book.Rating = rating;

                this.bookRepository.Update(book);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<Book> GetAllAndDeleted()
        {
            return this.bookRepository.AllAndDeleted;
        }

        public void AddBook(string title, DateTime published, string isbn, 
            Author author, string summary, string language, Publisher publisher, 
            ICollection<Genre> genres)
        {
            var book = this.bookFactory.CreateBook(title, published, isbn, author, summary,
                language, publisher, genres);

            this.bookRepository.Add(book);
            this.unitOfWork.Commit();
        }

        public void UpdateBook(Guid id, string title, DateTime published, 
            string isbn, string summary, string language, ICollection<Genre> genres,
            Author author, Publisher publisher, string photoUrl)
        {
            var book = this.bookRepository.GetById(id);

            if (book != null)
            {
                book.Title = title;
                book.Published = published;
                book.ISBN = isbn;
                book.Summary = summary;
                book.Language = language;
                book.Genres = genres;
                book.Author = author;
                book.Publisher = publisher;
                book.PhotoUrl = photoUrl;

                this.bookRepository.Update(book);
                this.unitOfWork.Commit();
            }
        }

        public void DeleteBook(Guid bookId)
        {
            var book = this.bookRepository.GetById(bookId);
            var dateDeleted = this.provider.GetCurrentTime();

            if (book != null)
            {
                book.IsDeleted = true;
                book.DeletedOn = dateDeleted;

                this.bookRepository.Update(book);
                this.unitOfWork.Commit();
            }
        }

        public void RestoreBook(Guid bookId)
        {
            var book = this.bookRepository.GetById(bookId);

            if (book != null)
            {
                book.IsDeleted = false;
                book.DeletedOn = null;

                this.bookRepository.Update(book);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<Book> GetBooksByAuthor(Guid authorId)
        {
            var books = this.bookRepository.All
                .Where(b => b.Author.Id == authorId);

            return books;
        }

        public IQueryable<Book> GetAllBooksByAuthor(Guid authorId)
        {
            var books = this.bookRepository.AllAndDeleted
                .Where(b => b.Author.Id == authorId);

            return books;
        }

        public IQueryable<Book> GetBooksByPublisher(Guid publisherId)
        {
            var books = this.bookRepository.All
                .Where(b => b.Publisher.Id == publisherId);

            return books;
        }

        public IQueryable<Book> GetAllBooksByPublisher(Guid publisherId)
        {
            var books = this.bookRepository.AllAndDeleted
                .Where(b => b.Publisher.Id == publisherId);

            return books;
        }
    }
}
